

using NetDaemon.Common.Reactive;
using NetDaemon.Common.Reactive.Services;
using NetDaemon.Generated.Reactive;
using System.Collections.Generic;

namespace Greenhouse
{

    public class GHMain
    {
        public BinarySensorEntity? TestingZoneLow { get; set; }
        public BinarySensorEntity? TestingZoneMedium { get; set; }
        public BinarySensorEntity? TestingZoneHigh { get; set; }
        public SwitchEntity MainFan { get; set; } = default!;
        public SwitchEntity? FreshWaterPump { get; set; }
        public decimal SecondsFromLowToMediumOnTestingZone { get; set; }
        public string NutrientPump1Name { get; set; } = "";
        public string NutrientPump2Name { get; set; } = "";
        public string NutrientPump3Name { get; set; } = "";
        public SwitchEntity Dehumidfier { get; set; } = default!;
        public SwitchEntity SwampCooler { get; set; } = default!;
        //These need to just become ordered lists.
        public List<SensorEntity> InternalTempSensors { get; set; } = new List<SensorEntity>();
        public List<SensorEntity> ExternalTempSensors { get; set; } = new List<SensorEntity>();
        public List<SensorEntity> ExternalHumiditySensors { get; set; } = new List<SensorEntity>();
        public List<SensorEntity> InternalHumidtySensors { get; set; } = new List<SensorEntity>();

        public double? InternalTemp
        {
            get
            {

                foreach (var sensor in InternalTempSensors)
                {
                    return TryAndGetValue(sensor);
                }
                return null;

            }
        }
        public double? ExternalTemp
        {
            get
            {

                foreach (var sensor in ExternalTempSensors)
                {
                    return TryAndGetValue(sensor);
                }
                return null;

            }
        }

        public double? InternalHumidity
        {
            get
            {

                foreach (var sensor in InternalHumidtySensors)
                {
                    return TryAndGetValue(sensor);
                }
                return null;

            }
        }

        public double? ExternalHumidty
        {
            get
            {

                foreach (var sensor in ExternalHumiditySensors)
                {
                    return TryAndGetValue(sensor);
                }
                return null;

            }
        }

        private double? TryAndGetValue(SensorEntity sensorEntity)
        {
            if (!sensorEntity.IsUnknown() && sensorEntity != null)
            {
                var currentType = sensorEntity.State?.GetType();
                if (currentType == typeof(double) || currentType == typeof(decimal) || currentType == typeof(int) || currentType == typeof(System.Int64))
                {
                    return (double)sensorEntity.State;
                }

            }
            return null;
        }
    }
    public class GhZone
    {
        public SwitchEntity? WateringPump { get; set; }
        public SwitchEntity? TestingPump { get; set; }
        public BinarySensorEntity? LowWater { get; set; }
        public BinarySensorEntity? MediumWater { get; set; }
        public BinarySensorEntity? HighWater { get; set; }
        public NutrientMixes NutrientMix { get; set; }
        public string SelectorName { get; set; }
        public decimal SecondsFromLowToMediumOnTestingZone { get; set; }
        public decimal SecondsFromHighToMediumOnBucket { get; set; }

        public GhZone()
        {
            SelectorName = "";
        }

        public (int pump1, int pump2, int pump3) NutrientFormula()
        {
            int pump1 = 0;
            int pump2 = 0;
            int pump3 = 0;
            int baseStepCount = 300;
            switch (NutrientMix)
            {
                case NutrientMixes.CuttingsAndSeedlings:
                    pump1 = baseStepCount / 4;
                    pump2 = baseStepCount / 4;
                    pump3 = baseStepCount / 4;
                    break;
                case NutrientMixes.MildVegatativeGrowth:
                    pump1 = baseStepCount;
                    pump2 = baseStepCount;
                    pump3 = baseStepCount;
                    break;
                case NutrientMixes.AggressiveVegetativeGrowth:
                    pump1 = baseStepCount * 3;
                    pump2 = baseStepCount * 2;
                    pump3 = baseStepCount;
                    break;
                case NutrientMixes.TransitionToBloom:
                    pump1 = baseStepCount * 2;
                    pump2 = baseStepCount * 2;
                    pump3 = baseStepCount * 2;
                    break;
                case NutrientMixes.BloomingAndRipening:
                    pump1 = baseStepCount * 1;
                    pump2 = baseStepCount * 2;
                    pump3 = baseStepCount * 3;
                    break;
            }

            return (pump1, pump2, pump3);

        }

    }

    public enum NutrientMixes
    {
        CuttingsAndSeedlings = 1,
        MildVegatativeGrowth = 2,
        AggressiveVegetativeGrowth = 3,
        TransitionToBloom = 4,
        BloomingAndRipening = 5

    }

    public class GhConfig
    {
        private NetDaemonRxApp _app;
        public GhConfig(NetDaemonRxApp app)
        {
            _app = app;
        }

        public GHMain GhMain()
        {
            BinarySensorEntities bse = new BinarySensorEntities(_app);
            SwitchEntities sw = new SwitchEntities(_app);
            GHMain gHMain = new GHMain();
            gHMain.TestingZoneLow = bse.TestZoneWaterBucketEmpty;
            gHMain.TestingZoneMedium = bse.TestZoneWaterBucketMedium;
            gHMain.TestingZoneHigh = bse.TestZoneWaterBucketFull;
            gHMain.FreshWaterPump = sw.Tb4p4;
            gHMain.SecondsFromLowToMediumOnTestingZone = 8.68M;
            gHMain.Dehumidfier = new(_app, new string[] { "switch.tb4p2" });
            gHMain.SwampCooler = new(_app, new string[] { "switch.st4p3" });
            gHMain.NutrientPump1Name = "peristalticnutrients_nutrientpump1";
            gHMain.NutrientPump2Name = "peristalticnutrients_nutrientpump2";
            gHMain.NutrientPump3Name = "peristalticnutrients_nutrientpump3";
            gHMain.InternalTempSensors.Add(new(_app, new string[] { "sensor.ble_temperature_gh_indoor_temp" }));
            gHMain.InternalTempSensors.Add(new(_app, new string[] { "sensor.gh_interal_protected_temp" }));
            gHMain.ExternalTempSensors.Add(new(_app, new string[] { "sensor.ble_temperature_gh_temp_external" }));
            gHMain.ExternalTempSensors.Add(new(_app, new string[] { "sensor.exterior_temperature" }));
            gHMain.InternalHumidtySensors.Add(new(_app, new string[] { "sensor.ble_humidity_gh_indoor_temp" }));
            gHMain.InternalHumidtySensors.Add(new(_app, new string[] { "sensor.gh_interal_protected_humidity" }));
            gHMain.ExternalHumiditySensors.Add(new(_app, new string[] { "sensor.ble_humidity_gh_external" }));
            gHMain.ExternalHumiditySensors.Add(new(_app, new string[] { "sensor.exterior_humidity" }));
            gHMain.MainFan = new(_app, new string[] { "switch.fan" });

            return gHMain;

        }

        public GhZone CurrentZone()
        {
            GhZone CurrentZone = new GhZone();
            SwitchEntities sw = new SwitchEntities(_app);
            BinarySensorEntities bse = new BinarySensorEntities(_app);
            InputSelectEntities ise = new InputSelectEntities(_app);
            if (ise.ReservoirRes.State == "DB1")
            {
                CurrentZone.LowWater = bse.BucketEmpty;
                CurrentZone.MediumWater = bse.BucketMedium;
                CurrentZone.HighWater = bse.BucketFull;
                CurrentZone.WateringPump = sw.St2p3;
                CurrentZone.SelectorName = "DB1";
                CurrentZone.TestingPump = sw.St2p1;
                CurrentZone.SecondsFromLowToMediumOnTestingZone = 10.57M;
                CurrentZone.SecondsFromHighToMediumOnBucket = 31.88M; ///Copied from Zone 2
                CurrentZone.NutrientMix = NutrientMixes.BloomingAndRipening;
                return CurrentZone;
            }
            if (ise.ReservoirRes.State == "DB2")
            {
                CurrentZone.LowWater = bse.Db2Empty;
                CurrentZone.MediumWater = bse.Db2Medium;
                CurrentZone.HighWater = bse.Db2Full;
                CurrentZone.WateringPump = sw.St2p2;
                CurrentZone.SelectorName = "DB2";
                CurrentZone.TestingPump = sw.St2p4;
                CurrentZone.SecondsFromLowToMediumOnTestingZone = 8.868M;
                CurrentZone.SecondsFromHighToMediumOnBucket = 31.88M;
                CurrentZone.NutrientMix = NutrientMixes.BloomingAndRipening;
                return CurrentZone;
            }
            if (ise.ReservoirRes.State == "DB3")
            {
                CurrentZone.LowWater = bse.Db3BucketEmpty;
                CurrentZone.MediumWater = bse.Db3BucketMedium;
                CurrentZone.HighWater = bse.Db3BucketFull;
                CurrentZone.WateringPump = sw.St3p2;
                CurrentZone.SelectorName = "DB3";
                CurrentZone.TestingPump = sw.St3p4;
                CurrentZone.SecondsFromLowToMediumOnTestingZone = 10.477M;
                CurrentZone.SecondsFromHighToMediumOnBucket = 44.66M;
                CurrentZone.NutrientMix = NutrientMixes.BloomingAndRipening;
                return CurrentZone;
            }
            if (ise.ReservoirRes.State == "DB4")
            {
                CurrentZone.LowWater = bse.Db4BucketEmpty;
                CurrentZone.MediumWater = bse.Db4BucketMedium;
                CurrentZone.HighWater = bse.Db4BucketFull;
                CurrentZone.WateringPump = sw.St3p1;
                CurrentZone.SelectorName = "DB4";
                CurrentZone.TestingPump = sw.St3p3;
                CurrentZone.SecondsFromLowToMediumOnTestingZone = 10.896M;
                CurrentZone.SecondsFromHighToMediumOnBucket = 41.53M;
                CurrentZone.NutrientMix = NutrientMixes.BloomingAndRipening;
                return CurrentZone;
            }
            if (ise.ReservoirRes.State == "DB5")
            {
                CurrentZone.LowWater = bse.Db5BucketEmpty;
                CurrentZone.MediumWater = bse.Db5BucketMedium;
                CurrentZone.HighWater = bse.Db5BucketFull;
                CurrentZone.WateringPump = sw.St1p1;
                CurrentZone.SelectorName = "DB5";
                CurrentZone.TestingPump = sw.St1p2;
                CurrentZone.SecondsFromLowToMediumOnTestingZone = 11.08M;
                CurrentZone.SecondsFromHighToMediumOnBucket = 45.45M;
                CurrentZone.NutrientMix = NutrientMixes.BloomingAndRipening;
                return CurrentZone;
            }
            if (ise.ReservoirRes.State == "DB6")
            {
                CurrentZone.LowWater = bse.Db6BucketEmpty;
                CurrentZone.MediumWater = bse.Db6BucketMedium;
                CurrentZone.HighWater = bse.Db6BucketFull;
                CurrentZone.WateringPump = sw.St1p3;
                CurrentZone.SelectorName = "DB6";
                CurrentZone.TestingPump = sw.St1p4;
                CurrentZone.SecondsFromHighToMediumOnBucket = 51.656M;
                CurrentZone.SecondsFromLowToMediumOnTestingZone = 12.465M;
                CurrentZone.NutrientMix = NutrientMixes.BloomingAndRipening;
                return CurrentZone;
            }
            if (ise.ReservoirRes.State == "NFT")
            {
                CurrentZone.LowWater = bse.NftResEmpty;
                CurrentZone.MediumWater = bse.NftResMedium;
                CurrentZone.HighWater = bse.NftResFull;
                CurrentZone.WateringPump = null;
                CurrentZone.SelectorName = "NFT";
                CurrentZone.TestingPump = null;
                CurrentZone.SecondsFromHighToMediumOnBucket = 0;
                CurrentZone.SecondsFromLowToMediumOnTestingZone = 0;
                CurrentZone.NutrientMix = NutrientMixes.MildVegatativeGrowth;
                return CurrentZone;
            }
            else
            {
                _app.LogError("No Zone is currently Selected");
                return CurrentZone;
            }


        }
    }


}