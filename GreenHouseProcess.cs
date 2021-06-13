using System;
using System.Reactive.Linq;
using NetDaemon.Common.Reactive;
using System.Threading.Tasks;
using NetDaemon.Common;
using System.Linq;
using System.Collections.Generic;
using Netdaemon.Generated.Reactive;

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
            return true;
        }

        public async Task<bool> AddNutrientsToCurrentZone()
        {
            GhZone zone = _ghConfig.CurrentZone();
            var nuteFormula = zone.NutrientFormula();
            _app.LogInformation($"Starting adding a dose to Zone {zone.SelectorName}");
            _app.CallService("esphome", _ghMain.NutrientPump1Name, new { target = nuteFormula.pump1 });
            await _app.DelayFor(ConvertStepsToTime(nuteFormula.pump1));
            _app.CallService("esphome", _ghMain.NutrientPump2Name, new { target = nuteFormula.pump2 });
            await _app.DelayFor(ConvertStepsToTime(nuteFormula.pump2));
            _app.CallService("esphome", _ghMain.NutrientPump3Name, new { target = nuteFormula.pump3 });
            await _app.DelayFor(ConvertStepsToTime(nuteFormula.pump3));

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

        public async Task<bool> SendAlert(string title, string descriptions)
        {
            _app.CallService("persistent_notification", "create", new
            {
                title = title,
                message = descriptions
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
                    ise.ReservoirRes.SetOptions(sn);

                }
            }
            else
            {
                await SendAlert("Cannot start Reservoir refil.", $"ReservoirRes state is currently {ise.ReservoirRes.State}");
            }

            return true;

        }

        public async Task<bool> RefillCurrentReservior()
        {
            GhZone currentZone = _ghConfig.CurrentZone();
            if (currentZone.LowWater == null)
            {
                await SendAlert($"Error Refilling Reservior {currentZone.SelectorName}", "Low water is null");
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
                        await SendAlert("Reservoir is in an invalid state for refill", $"Reservoir {zone.SelectorName} has the highwater on but the medium water is off.");
                        throw new Exception("Reservoir in an invalid state");
                    }
                    if (zone.LowWater.IsOff() && (zone.MediumWater.IsOn() || zone.HighWater.IsOn()))
                    {
                        await SendAlert("Reservoir is in an invalid state for refill", $"Reservoir {zone.SelectorName} has the lowWater off but the medium water isOn is {zone.HighWater.IsOn()} and high water isOn is  {zone.HighWater.IsOn()}.");
                        throw new Exception("Reservoir in an invalid state");
                    }

                    _app.LogInformation($"Starting refill of zone {zone.SelectorName}");
                    if (zone.HighWater.IsOff() && refillSucessful)
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
                                            _app.LogInformation("The testing reserviour filled up before we reached the medium water mark on the current zone. It must be pretty full");
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
                await SendAlert($"Error refilling Reserviour {zone.SelectorName}", $"Refill had an exception of { ex.Message}");
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