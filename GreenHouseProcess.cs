using System;
using System.Reactive.Linq;
using NetDaemon.Common.Reactive;
using System.Threading.Tasks;
using NetDaemon.Common;
using System.Linq;
using System.Collections.Generic;
using NetDaemon.Generated.Reactive;
using NetDaemon.Common.Reactive.Services;

// Use unique namespaces for your apps if you going to share with others to avoid
// conflicting names
namespace Greenhouse
{

    public class GhProcedures
    {
        private NetDaemonRxApp _app;
        private GhConfig _ghConfig;
        //private GhZone _currentZone;
        private GHMain _ghMain;
        public GhProcedures(NetDaemonRxApp app)
        {
            _app = app;
            _ghConfig = new GhConfig(_app);
            _ghMain = _ghConfig.GhMain();
        }

        public bool MakeSureEverythingisOff()
        {
            _app.LogDebug("Shutting Down all the automation related pumps");
            if (_ghMain.FreshWaterPump != null)
            {
                _ghMain.FreshWaterPump.TurnOff();
            }
            if (_ghMain.WaterTankRefill != null)
            {
                _ghMain.WaterTankRefill.TurnOff();
            }
            return true;
        }

        public class EnsurePumpTurnsOff : IDisposable
        {
            private SwitchEntity SwitchEntity { get; set; }
            public EnsurePumpTurnsOff(SwitchEntity switchEntity)
            {
                SwitchEntity = switchEntity;
            }
            public void TurnOn()
            {
                SwitchEntity.TurnOn();
            }
            public void TurnOff()
            {
                SwitchEntity.TurnOff();
            }
            public bool IsOn()
            {
                return SwitchEntity.IsOn();
            }
            public bool IsOFf()
            {
                return SwitchEntity.IsOff();
            }
            public bool IsUnknown()
            {
                return SwitchEntity.IsUnknown();
            }
            public void Dispose()
            {
                SwitchEntity.TurnOff();
            }
        }

        public async Task<bool> RunOneTankEmptyRunForCurrentZone()
        {
            GhZone zone = _ghConfig.CurrentZone();
            using EnsurePumpTurnsOff dumpPump = new EnsurePumpTurnsOff(_ghMain.DumpToWastePump);

            if (_ghMain.FreshWaterPump == null || zone.MediumWater == null || zone.HighWater == null || zone.LowWater == null || _ghMain.TestingZoneLow == null || _ghMain.TestingZoneMedium == null || _ghMain.TestingZoneHigh == null || zone.TestingPump == null || zone.WateringPump == null)
            {
                SendAlert("Cannot run Tank Empty rutine because one of the needed items is null", "Should probably have a better message to figure out which one.");
            }
            else if (zone.MediumWater.IsOn() || zone.HighWater.IsOn())
            {
                using EnsurePumpTurnsOff testingPump = new EnsurePumpTurnsOff(zone.TestingPump);
                testingPump.TurnOn();
                var (Old, New) = await zone.LowWater.StateChanges.Merge(_ghMain.TestingZoneMedium.StateChanges).FirstOrTimeout(TimeSpan.FromSeconds(50)).FirstAsync();
                if (New.State == "TimeOut")
                {
                    SendAlert("Timeout refilling trying to run an empty tank cycle", "Not sure what happened.");
                }
                else
                {
                    if (_ghMain.TestingZoneMedium.IsOn())
                    {
                        dumpPump.TurnOn();
                        var refill = await zone.LowWater.StateChanges.Merge(_ghMain.TestingZoneHigh.StateChanges).FirstOrTimeout(TimeSpan.FromSeconds(80)).FirstAsync();
                        dumpPump.TurnOff();
                        if (refill.New.State == "TimeOut")
                        {
                            SendAlert("Empty Cycle Error - Did not get to low during ", "There was not enough water to get over medium on the testing tank so we can not run an empty cycle.");
                        }
                        else
                        {
                            if (zone.LowWater.IsOn() && _ghMain.TestingZoneHigh.IsOn())
                            {
                                testingPump.TurnOff();
                                await _app.DelayFor(TimeSpan.FromSeconds(5));
                                var refillAgain = await zone.LowWater.StateChanges.Merge(_ghMain.TestingZoneHigh.StateChanges).FirstOrTimeout(TimeSpan.FromSeconds(30)).FirstAsync();
                            }

                            var drain = await _ghMain.TestingZoneLow.StateChanges.FirstOrTimeout(TimeSpan.FromSeconds(75)).FirstAsync();
                            if (zone.LowWater.IsOff())
                            {
                                //I want to make sure some water flows into the tanks so that it has enough to start a siphon; some of this goes down the wrong siphon unfortunately but it seems the safest.
                                _ghMain.FreshWaterPump.TurnOn();
                                await _app.DelayFor(TimeSpan.FromSeconds(10));
                                _ghMain.FreshWaterPump.TurnOff();
                            }

                            if (zone.LowWater.IsOff())
                            {
                                SendAlert("Empty Cycle Error - Low Water is still off after filling with water", "There is a risk that we don't have enough water to start a siphon");
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        SendAlert("Empty Cycle Error - Got to low to fast", "There was not enough water to get over medium on the testing tank so we can not run an empty cycle.");
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Non of these items should be null. It is just that it won't crash if they are. But it wil send alerts.
        /// </summary>
        /// <param name="tankFullSensor">Sensor that shows if the tank that is trying to be filled is full</param>
        /// <param name="pumpOrValve">Pump or valve that fills up the tank</param>
        /// <param name="timeSpan">Max time to try and fill the tank. If this timespan is reached then the automation will end and send an alert./param>
        /// <returns></returns>
        private async Task<bool> FillUpTank(BinarySensorEntity? tankFullSensor, SwitchEntity? pumpOrValve, TimeSpan timeSpan)
        {
            if (pumpOrValve != null)
            {
                if (tankFullSensor == null)
                {
                    SendAlert($"Tank full sensor is null", "Tank could not be refilled");
                }
                else if (tankFullSensor.IsOff())
                {
                    using EnsurePumpTurnsOff pump = new EnsurePumpTurnsOff(pumpOrValve);
                    pump.TurnOn();
                    var (Old, New) = await tankFullSensor.StateChanges.Where(t => t.New.State == "on").FirstOrTimeout(timeSpan).FirstAsync();
                    pump.TurnOff();
                    if (New.State == "TimeOut")
                    {
                        SendAlert("Timeout refilling the Main Water tank", "Tank is probably not full but it took over 4 minutes to fill it up.");
                    }
                }
            }
            else
            {
                SendAlert($"pumpOrValve is null", "Tank could not be refilled");
            }
            return true;
        }

        public async Task<bool> RefillMainWaterTank()
        {
            return await FillUpTank(_ghMain.HighFreshWaterTank, _ghMain.WaterTankRefill, TimeSpan.FromMinutes(4));

        }

        public async Task<bool> RefillSwampCooler()
        {
            if (_ghMain.MediumFreshWaterTank != null && _ghMain.MediumFreshWaterTank.IsOn())
            {
                return await FillUpTank(_ghMain.HighSwampCoolerTank, _ghMain.SwampCoolerRefillPump, TimeSpan.FromMinutes(4));
            }
            else
            {
                SendAlert("Cannot fill up the swp cooler", "Fresh water tank needs to be atleast at Medium to initiat a refill so the swp cooler.");
            }
            return false;
        }


        internal async Task<bool> HoldCurrentZone()
        {
            var currentZone = _ghConfig.CurrentZone();
            InputBooleanEntities ibe = new InputBooleanEntities(_app);
            bool holdIsOn = ibe.TestingTankHold.State == "on";
            string currentStep = "Starting";

            if (!holdIsOn)
            {
                SendAlert($"Error holding the current zone. Last step to start is {currentStep}", "Testing Tank Hold is Off");
                return false;
            }
            //set testing tank hold to on;


            if (currentZone.TestingPump != null && _ghMain.TestingZoneMedium != null && _ghMain.TestingZoneHigh != null && _ghMain.TestingZoneLow != null)
            {
                if (_ghMain.TestingZoneHigh.IsOn() || _ghMain.TestingZoneMedium.IsOn() || _ghMain.TestingZoneLow.IsOn())
                {
                    SendAlert($"Error holding the current zone. Last step to start is {currentStep}", "The testing zone is not empty.");
                }
                try
                {
                    currentStep = "Initial Fill";
                    using EnsurePumpTurnsOff testingPump = new EnsurePumpTurnsOff(currentZone.TestingPump);
                    testingPump.TurnOn();
                    var initialTankFilledUp = _ghMain.TestingZoneMedium.StateChanges.Merge(_ghMain.TestingZoneHigh.StateChanges).NDFirstOrTimeout(TimeSpan.FromSeconds(60));
                    if (initialTankFilledUp == null)
                    {
                        SendAlert($"Error holding the current zone. Last step to start is {currentStep}", "Filling up the testing tank to medium timed-out.");
                        return false;
                    }
                    else if (_ghMain.TestingZoneHigh.IsOn() && _ghMain.TestingZoneMedium.IsOff())
                    {
                        SendAlert($"Error holding the current zone. Last step to start is {currentStep}", "High is on but medium is off. The Testing tank is an invalid state.");
                        return false;

                    }
                    else
                    {
                        testingPump.TurnOff();
                        while (holdIsOn)
                        {
                            var refillTime = TimeSpan.FromSeconds(10);
                            if (_ghMain.TestingZoneMedium.IsOn())
                            {
                                var waitFormediumToReset = _ghMain.TestingZoneMedium.StateChanges.Where(t => t.New.State == "off").NDFirstOrTimeout(TimeSpan.FromSeconds(60));
                                if (waitFormediumToReset == null)
                                {
                                    SendAlert($"Error holding the current zone. Last step to start is {currentStep}", "TextingZoneMedium did not reset");
                                    return false;
                                }
                            }
                            else
                            {
                                refillTime = TimeSpan.FromSeconds(15);
                            }
                            testingPump.TurnOn();
                            _ghMain.TestingZoneHigh.StateChanges.Where(t => t.New.State == "on").NDFirstOrTimeout(refillTime);
                            testingPump.TurnOff();
                            holdIsOn = ibe.TestingTankHold.State == "on";
                        }
                    }
                }
                catch (Exception ex)
                {
                    SendAlert($"Error holding the current zone. Last step to start is {currentStep}", ex);
                }

            }
            else
            {
                SendAlert($"Error holding the current zone.", "Some info on the current zone is null. So nothing could start.");
            }
            return true;
        }

        public async Task<bool> AddNutrientsToCurrentZone()
        {
            GhZone zone = _ghConfig.CurrentZone();
            var nuteFormula = zone.NutrientFormula();
            InputNumberEntities inputNumberEntities = new InputNumberEntities(_app);
            int numberOfUnits = (int)(float)inputNumberEntities.Nutrientunitstoadd.State;
            _app.LogInformation($"Starting adding a dose to Zone {zone.SelectorName}");
            _app.CallService("esphome", _ghMain.NutrientPump1Name, new { target = nuteFormula.pump1 * numberOfUnits });
            await _app.DelayFor(ConvertStepsToTime(nuteFormula.pump1 * numberOfUnits));
            _app.CallService("esphome", _ghMain.NutrientPump2Name, new { target = nuteFormula.pump2 * numberOfUnits });
            await _app.DelayFor(ConvertStepsToTime(nuteFormula.pump2 * numberOfUnits));
            _app.CallService("esphome", _ghMain.NutrientPump3Name, new { target = nuteFormula.pump3 * numberOfUnits });
            await _app.DelayFor(ConvertStepsToTime(nuteFormula.pump3 * numberOfUnits));

            return true;
        }

        public async Task<bool> AddNutrients(int pumpNumber, int doses)
        {
            _app.LogInformation($"Starting adding a {doses} dose from pump Number {pumpNumber}");
            int target = doses * 250;
            string pumpName = pumpNumber == 1 ? _ghMain.NutrientPump1Name : pumpNumber == 2 ? _ghMain.NutrientPump2Name : _ghMain.NutrientPump3Name;
            _app.CallService("esphome", pumpName, new { target = target });
            return true;
        }

        public bool SendAlert(string title, string description)
        {
            _app.CallService("persistent_notification", "create", new
            {
                title = title,
                message = description
            });
            _app.CallService("notify", "mobile_app_moto_g_stylus", new
            {
                title = title,
                message = description
            });
            return true;
        }
        public bool SendAlert(string title, Exception ex)
        {
            string description = ex?.Message ?? "Unknown Error";
            _app.CallService("persistent_notification", "create", new
            {
                title = title,
                message = description
            });
            _app.CallService("notify", "mobile_app_moto_g_stylus", new
            {
                title = title,
                message = description
            });
            return true;
        }
        private TimeSpan ConvertStepsToTime(int steps, int safetyBuffer = 3)
        {
            //Stepper is set at 250 steps per second
            var seconds = ((double)steps) / 250.00;
            return TimeSpan.FromSeconds(seconds + safetyBuffer);
        }

        public async Task<bool> TimeMainWaterPump()
        {
            var tcs = new TaskCompletionSource<bool>();
            //validation of Entitites
            if (
                        _ghMain.FreshWaterPump != null &&
                        _ghMain.TestingZoneLow != null &&
                        _ghMain.TestingZoneMedium != null
                        )
            {
                if (_ghMain.TestingZoneLow.IsOff())
                {
                    _ghMain.FreshWaterPump.TurnOn();
                    _ghMain.TestingZoneLow.StateChanges.Where(t => t.New.State == "on").FirstOrTimeout(TimeSpan.FromSeconds(20)).Subscribe(t =>
                    {
                        if (t.New.State == "TimeOut")
                        {
                            _app.LogDebug("Test Zone Sensor is not working or we could not get water into the testing zone");
                        }
                        else
                        {
                            DateTime startOfRun = DateTime.Now;
                            _ghMain.TestingZoneMedium.StateChanges.Where(t => t.New.State == "on").FirstOrTimeout(TimeSpan.FromSeconds(30)).Subscribe(t =>
                            {
                                _ghMain.FreshWaterPump.TurnOff();
                                double totalSeconds = (DateTime.Now - startOfRun).TotalSeconds;
                                _app.LogInformation($"It took {totalSeconds} seconds to get from low to medium for fresh water");
                                _app.LogInformation($"Don't forget that the water is left in the testing zone and needs to be manually drained into a reserviour with space");
                            });
                        }

                    });
                }
                else
                {
                    _app.LogDebug("There is water in the testing zone already.");
                }
            }
            await tcs.Task;
            return true;
        }

        public async Task<double> CreateAverage(GhZone zone, bool fromLowToMediumOnTestingZone)
        {
            //validation of Entitites
            if (_ghMain.TestingZoneLow != null)
            {
                //await gh.TimeFromHighToMedium(5);
                List<double> timings = new List<double>();
                int runCount = 3;
                for (int i = 0; i < runCount; i++)
                {
                    double result = 0;
                    if (fromLowToMediumOnTestingZone)
                    {
                        result = await TimeReservior(zone);
                    }
                    else
                    {
                        result = await TimeFromHighToMedium(zone);
                    }
                    if (result == 0)
                    {
                        _app.LogDebug("Something went wrong in trying to time the reservour");
                        break;
                    }
                    timings.Add(result);
                    var tcs = new TaskCompletionSource<bool>();
                    var currentRun = _ghMain.TestingZoneLow.StateChanges.Where(t => t.New.State == "off").FirstOrTimeout(TimeSpan.FromSeconds(90))
                        .Subscribe(t =>
                        {
                            if (t.Item1.State == "TimeOut")
                            {
                                _app.LogDebug("Water did not drain out fast enough");
                                tcs.SetResult(true);
                            }
                            else
                            {
                                _app.RunIn(TimeSpan.FromSeconds(20), () =>
                                        {
                                            tcs.SetResult(true);
                                        });
                            }
                        });

                    await tcs.Task;
                }
                double average = timings.Sum() / timings.Count;
                return average;
            }
            else
            {
                _app.LogDebug("Entity Validation Failed");
                return 0;
            }

        }

        public async Task<double> TimeFromHighToMedium(GhZone zone)
        {
            var tcs = new TaskCompletionSource<bool>();
            double result = 0;
            //validation of Entitites
            if (zone.HighWater != null &&
                        zone.MediumWater != null &&
                        zone.LowWater != null &&
                        zone.TestingPump != null &&
                        _ghMain.TestingZoneLow != null &&
                        _ghMain.TestingZoneMedium != null &&
                        _ghMain.TestingZoneHigh != null
                        )
            {
                if (_ghMain.TestingZoneLow.IsOn())
                {
                    _app.LogDebug("Testing zone must be empty");
                    tcs.SetResult(true);
                }
                if (zone.HighWater.IsOn())
                {
                    zone.TestingPump.TurnOn();
                    DateTime startOfRun = DateTime.Now;
                    zone.HighWater.StateChanges.Where(t => t.New.State == "off").FirstOrTimeout(TimeSpan.FromSeconds(30)).Subscribe(t =>
                    {
                        if (t.New.State == "TimeOut")
                        {
                            _app.LogDebug("The pump or the HighWater sensor is not working");
                            zone.TestingPump.TurnOff();
                        }
                        else
                        {
                            zone.MediumWater.StateChanges.Where(t => t.New.State == "off").Merge(_ghMain.TestingZoneHigh.StateChanges.Where(t => t.New.State == "on")).FirstOrTimeout(TimeSpan.FromSeconds(60)).Subscribe(t =>
                                    {
                                        zone.TestingPump.TurnOff();
                                        if (_ghMain.TestingZoneHigh.IsOn())
                                        {
                                            _app.LogDebug("Test Zone is full. It filled up before the bucket made it to medium.");
                                        }
                                        else if (t.New.State == "TimeOut")
                                        {
                                            _app.LogDebug("Test Zone Sensor is not working or we could not get water into the testing zone");
                                        }
                                        else
                                        {
                                            result = (DateTime.Now - startOfRun).TotalSeconds;
                                            _app.LogInformation($"It took {result} seconds to get from High to medium");

                                        }
                                        tcs.SetResult(true);
                                    });
                        }

                    });

                }
                else
                {
                    _app.LogDebug("Not enough water to run test. It needs to be at least at Full water.");
                }
            }
            await tcs.Task;
            return result;
        }


        public async Task<double> TimeReservior(GhZone zone)
        {
            double timing = 0;
            var tcs = new TaskCompletionSource<bool>();
            //validation of Entitites
            if (zone.HighWater != null &&
                        zone.MediumWater != null &&
                        zone.LowWater != null &&
                        zone.TestingPump != null &&
                        _ghMain.TestingZoneLow != null &&
                        _ghMain.TestingZoneMedium != null
                        )
            {
                if (zone.MediumWater.IsOn() && _ghMain.TestingZoneLow.IsOff())
                {
                    zone.TestingPump.TurnOn();
                    _ghMain.TestingZoneLow.StateChanges.Where(t => t.New.State == "on").FirstOrTimeout(TimeSpan.FromSeconds(40)).Subscribe(t =>
                    {
                        if (t.New.State == "TimeOut")
                        {
                            zone.TestingPump.TurnOff();
                            _app.LogDebug("Test Zone Sensor is not working or we could not get water into the testing zone");
                            tcs.SetResult(true);
                        }
                        else
                        {
                            DateTime startOfRun = DateTime.Now;
                            _ghMain.TestingZoneMedium.StateChanges.Where(t => t.New.State == "on").FirstOrTimeout(TimeSpan.FromSeconds(30)).Subscribe(t =>
                            {
                                zone.TestingPump.TurnOff();
                                double totalSeconds = (DateTime.Now - startOfRun).TotalSeconds;
                                _app.LogInformation($"It took {totalSeconds} seconds to get from low to medium");
                                timing = totalSeconds;
                                tcs.SetResult(true);
                            });
                        }

                    });
                }
                else
                {
                    if (_ghMain.TestingZoneLow.IsOff())
                    {
                        _app.LogDebug("Testing Zone already has water in it");
                    }
                    else
                    {
                        _app.LogDebug("Not enough water to run test. It needs to be at least at Medium water.");
                    }

                    tcs.SetResult(true);
                }
            }
            await tcs.Task;
            //Just in case
            return timing;
        }
        public async Task<bool> RefillAllReserviors(List<string> selectorNames)
        {
            InputSelectEntities ise = new InputSelectEntities(_app);
            if (ise.ReservoirRes.State == "None")
            {
                foreach (var sn in selectorNames)
                {
                    _app.CallService("input_select", "select_option", new { entity_id = "input_select.ReservoirRes", option = sn });
                    await Task.Delay(3000);
                    await RefillCurrentReservior();
                    ise.ReservoirRes.SelectOption("None");
                }
            }
            else
            {
                SendAlert("Cannot start Reservoir refill.", $"ReservoirRes state is currently {ise.ReservoirRes.State}");
            }

            return true;

        }

        public async Task<bool> RefillCurrentReservior()
        {
            GhZone currentZone = _ghConfig.CurrentZone();
            if (currentZone.LowWater == null)
            {
                SendAlert($"Error Refilling Reservior {currentZone.SelectorName}", "Low water is null");
                return false;
            }
            return await RefillReservior(currentZone);

        }

        private async Task<bool> RefillReservior(GhZone zone, int rerunCount = 0)
        {
            var tcs = new TaskCompletionSource<bool>();
            bool refillSucessful = false;
            try
            {
                //validation of Entitites

                if (zone.HighWater != null &&
                zone.MediumWater != null &&
                zone.LowWater != null &&
                zone.TestingPump != null &&
                _ghMain.FreshWaterPump != null &&
                _ghMain.TestingZoneLow != null &&
                _ghMain.TestingZoneMedium != null &&
                _ghMain.TestingZoneHigh != null &&
                zone.SecondsFromLowToMediumOnTestingZone > 0
                )
                {
                    if (zone.HighWater.IsOn() && zone.MediumWater.IsOff())
                    {
                        SendAlert("Reservoir is in an invalid state for refill", $"Reservoir {zone.SelectorName} has the highwater on but the medium water is off.");
                        throw new Exception("Reservoir in an invalid state");
                    }
                    if (zone.LowWater.IsOff() && (zone.MediumWater.IsOn() || zone.HighWater.IsOn()))
                    {
                        SendAlert("Reservoir is in an invalid state for refill", $"Reservoir {zone.SelectorName} has the lowWater off but the medium water isOn is {zone.HighWater.IsOn()} and high water isOn is  {zone.HighWater.IsOn()}.");
                        throw new Exception("Reservoir in an invalid state");
                    }

                    _app.LogInformation($"Starting refill of zone {zone.SelectorName}");
                    if (zone.HighWater.IsOff())
                    {
                        if (zone.MediumWater.IsOff())
                        {
                            zone.TestingPump.TurnOn();
                            var currentRun = _ghMain.TestingZoneLow.StateChanges.Where(t => t.New.State == "on").FirstOrTimeout(TimeSpan.FromSeconds(30))
                           .Subscribe(async t =>
                           {

                               zone.TestingPump.TurnOff();
                               if (t.New.State == "TimeOut")
                               {
                                   _app.LogDebug("Test Zone Sensor is not working or we could not get water into the testing zone");
                               }
                               else
                               {
                                   bool waterAdded = await SafelyAddFreshWater(30);

                                   if (rerunCount < 3)
                                   {
                                       rerunCount++;
                                       //need to let the water run down. 
                                       _app.RunIn(TimeSpan.FromSeconds(30), async () =>
                                                {
                                                    await RefillReservior(zone, rerunCount);
                                                    tcs.SetResult(true);
                                                });
                                   }
                               }
                           });
                        }
                        else
                        {
                            //handling the timing part of filling
                            DateTime startOfRun = DateTime.Now;
                            zone.TestingPump.TurnOn();
                            var timingRun = zone.MediumWater.StateChanges.Where(t => t.New.State == "off").Merge(_ghMain.TestingZoneHigh.StateChanges.Where(t => t.New.State == "on")).FirstOrTimeout(TimeSpan.FromSeconds(60))
                                    .Subscribe(async t =>
                                    {
                                        if (_ghMain.TestingZoneLow.IsOff())
                                        {
                                            //This is to make sure that there is enough time to get the siphon started.
                                            _app.Delay(TimeSpan.FromSeconds(5));
                                        }
                                        zone.TestingPump.TurnOff();
                                        int seconds = (int)(DateTime.Now - startOfRun).TotalSeconds;

                                        decimal secondsToRun = zone.SecondsFromHighToMediumOnBucket - seconds;
                                        secondsToRun = secondsToRun * (_ghMain.SecondsFromLowToMediumOnTestingZone / zone.SecondsFromLowToMediumOnTestingZone);
                                        if (_ghMain.TestingZoneHigh.IsOn())
                                        {
                                            _app.LogInformation("The testing Reservoir filled up before we reached the medium water mark on the current zone. It must be pretty full");
                                            refillSucessful = true;
                                            tcs.SetResult(true);
                                        }
                                        else if (secondsToRun < 3)
                                        {
                                            _app.LogInformation($"secondsToRun was {secondsToRun} so if looks like no filling is needed");
                                            tcs.SetResult(true);
                                        }
                                        else if (t.New.State == "TimeOut")
                                        {
                                            _app.LogInformation("It took too long to get the timing");
                                            tcs.SetResult(true);
                                        }
                                        else
                                        {
                                            bool waterAdded = await SafelyAddFreshWater((int)secondsToRun);
                                            refillSucessful = true;
                                            tcs.SetResult(true);

                                        }
                                    });
                        }
                    }
                }
                else
                {
                    return refillSucessful;
                }

            }
            catch (Exception ex)
            {

                if (zone.TestingPump != null)
                {
                    zone.TestingPump.TurnOff();
                }

                _app.LogError($"Refill had an exception of {ex.Message}");
                SendAlert($"Error refilling Reserviour {zone.SelectorName}", $"Refill had an exception of { ex.Message}");
                tcs.SetResult(true);
            }

            await tcs.Task;
            return refillSucessful;
        }

        private async Task<bool> SafelyAddFreshWater(int secondsToRun)
        {
            DateTime startTime = DateTime.Now;
            var tcs = new TaskCompletionSource<bool>();
            if (_ghMain.TestingZoneHigh != null &&
            _ghMain.FreshWaterPump != null)
            {
                try
                {
                    _ghMain.FreshWaterPump.TurnOn();
                    _ghMain.TestingZoneHigh.StateChanges.Where(t => t.New.State == "on").FirstOrTimeout(TimeSpan.FromSeconds(secondsToRun)).Subscribe(t =>
                                   {
                                       _ghMain.FreshWaterPump.TurnOff();
                                       if (t.New.State != "TimeOut")
                                       {
                                           TimeSpan remainingTime = DateTime.Now - startTime;
                                           if (remainingTime.TotalSeconds > 1)
                                           {
                                               //this is to let the water drain down;
                                               _app.RunIn(TimeSpan.FromSeconds(10), async () =>
                                                        {
                                                            int secondsForNewRun = (int)remainingTime.TotalSeconds;
                                                            if (secondsToRun > 1)
                                                            {
                                                                _app.LogDebug($"Adding more water for {secondsForNewRun} second");
                                                                await SafelyAddFreshWater(secondsForNewRun);

                                                            }
                                                        });

                                           }
                                       }
                                       tcs.SetResult(true);
                                   });
                }
                catch (Exception ex)
                {
                    _app.LogInformation($"Fill fresh Water safely had an exception of {ex.Message}");
                    _ghMain.FreshWaterPump.TurnOff();
                    tcs.SetResult(true);
                }
            }
            await tcs.Task;
            return true;


        }



    }

}