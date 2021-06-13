using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using NetDaemon.Common.Reactive.Services;
using NetDaemon.Common.Reactive;
using NetDaemon.Common;

namespace Netdaemon.Generated.Reactive
{
    public class GeneratedAppBase : NetDaemonRxApp
    {
        public BinarySensorEntities BinarySensor => new(this);
        public SensorEntities Sensor => new(this);
        public WeatherEntities Weather => new(this);
        public SwitchEntities Switch => new(this);
        public AutomationEntities Automation => new(this);
        public DeviceTrackerEntities DeviceTracker => new(this);
        public InputNumberEntities InputNumber => new(this);
        public PersonEntities Person => new(this);
        public ScriptEntity Script => new(this, new string[]{""});
        public InputSelectEntities InputSelect => new(this);
        public ZoneEntities Zone => new(this);
        public CameraEntities Camera => new(this);
        public SunEntities Sun => new(this);
    }

    public partial class InputNumberEntity : NetDaemon.Common.Reactive.Services.RxEntityBase
    {
        public InputNumberEntity(INetDaemonRxApp daemon, IEnumerable<string> entityIds): base(daemon, entityIds)
        {
        }

        public void Reload(dynamic? data = null)
        {
            CallService("input_number", "reload", data, false);
        }

        public void SetValue(dynamic? data = null)
        {
            CallService("input_number", "set_value", data, true);
        }

        public void Increment(dynamic? data = null)
        {
            CallService("input_number", "increment", data, true);
        }

        public void Decrement(dynamic? data = null)
        {
            CallService("input_number", "decrement", data, true);
        }
    }

    public partial class ScriptEntity : NetDaemon.Common.Reactive.Services.ScriptEntity
    {
        public ScriptEntity(INetDaemonRxApp daemon, IEnumerable<string> entityIds): base(daemon, entityIds)
        {
        }

        public void Db1Main(dynamic? data = null)
        {
            CallService("script", "db1_main", data, false);
        }

        public void Db1Refill(dynamic? data = null)
        {
            CallService("script", "db1_refill", data, false);
        }

        public void RefillCurrentBucket(dynamic? data = null)
        {
            CallService("script", "refill_current_bucket", data, false);
        }

        public void AddDoseToCurrentZone(dynamic? data = null)
        {
            CallService("script", "add_dose_to_current_zone", data, false);
        }
    }

    public partial class InputSelectEntity : NetDaemon.Common.Reactive.Services.RxEntityBase
    {
        public InputSelectEntity(INetDaemonRxApp daemon, IEnumerable<string> entityIds): base(daemon, entityIds)
        {
        }

        public void Reload(dynamic? data = null)
        {
            CallService("input_select", "reload", data, false);
        }

        public void SelectOption(dynamic? data = null)
        {
            CallService("input_select", "select_option", data, true);
        }

        public void SelectNext(dynamic? data = null)
        {
            CallService("input_select", "select_next", data, true);
        }

        public void SelectPrevious(dynamic? data = null)
        {
            CallService("input_select", "select_previous", data, true);
        }

        public void SelectFirst(dynamic? data = null)
        {
            CallService("input_select", "select_first", data, true);
        }

        public void SelectLast(dynamic? data = null)
        {
            CallService("input_select", "select_last", data, true);
        }

        public void SetOptions(dynamic? data = null)
        {
            CallService("input_select", "set_options", data, true);
        }
    }

    public partial class BinarySensorEntities
    {
        private readonly NetDaemonRxApp _app;
        public BinarySensorEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public BinarySensorEntity FreshWaterMedium => new(_app, new string[]{"binary_sensor.fresh_water_medium"});
        public BinarySensorEntity PowerStrip1Button => new(_app, new string[]{"binary_sensor.power_strip_1_button"});
        public BinarySensorEntity RemoteUi => new(_app, new string[]{"binary_sensor.remote_ui"});
        public BinarySensorEntity Station3Status => new(_app, new string[]{"binary_sensor.station_3_status"});
        public BinarySensorEntity Db6BucketMedium => new(_app, new string[]{"binary_sensor.db6_bucket_medium"});
        public BinarySensorEntity TestZoneWaterBucketEmpty => new(_app, new string[]{"binary_sensor.test_zone_water_bucket_empty"});
        public BinarySensorEntity FreshWaterFull => new(_app, new string[]{"binary_sensor.fresh_water_full"});
        public BinarySensorEntity BucketEmpty => new(_app, new string[]{"binary_sensor.bucket_empty"});
        public BinarySensorEntity Db2Full => new(_app, new string[]{"binary_sensor.db2_full"});
        public BinarySensorEntity FreshWaterLevelState => new(_app, new string[]{"binary_sensor.fresh_water_level_state"});
        public BinarySensorEntity BucketMedium => new(_app, new string[]{"binary_sensor.bucket_medium"});
        public BinarySensorEntity Db3BucketFull => new(_app, new string[]{"binary_sensor.db3_bucket_full"});
        public BinarySensorEntity Db2Empty => new(_app, new string[]{"binary_sensor.db2_empty"});
        public BinarySensorEntity Db4BucketEmpty => new(_app, new string[]{"binary_sensor.db4_bucket_empty"});
        public BinarySensorEntity ValveClosed => new(_app, new string[]{"binary_sensor.valve_closed"});
        public BinarySensorEntity GreenHousePowerStatus => new(_app, new string[]{"binary_sensor.green_house_power_status"});
        public BinarySensorEntity GreenHouseMain => new(_app, new string[]{"binary_sensor.green_house_main"});
        public BinarySensorEntity Db5BucketEmpty => new(_app, new string[]{"binary_sensor.db5_bucket_empty"});
        public BinarySensorEntity TestZoneResWaterLevelState => new(_app, new string[]{"binary_sensor.test_zone_res_water_level_state"});
        public BinarySensorEntity BucketMedium2 => new(_app, new string[]{"binary_sensor.bucket_medium_2"});
        public BinarySensorEntity Valve2Open => new(_app, new string[]{"binary_sensor.valve_2_open"});
        public BinarySensorEntity Db2Medium => new(_app, new string[]{"binary_sensor.db2_medium"});
        public BinarySensorEntity Db4BucketFull => new(_app, new string[]{"binary_sensor.db4_bucket_full"});
        public BinarySensorEntity FreshWaterEmpty => new(_app, new string[]{"binary_sensor.fresh_water_empty"});
        public BinarySensorEntity FrontLightStatus => new(_app, new string[]{"binary_sensor.front_light_status"});
        public BinarySensorEntity TestZoneWaterBucketMedium => new(_app, new string[]{"binary_sensor.test_zone_water_bucket_medium"});
        public BinarySensorEntity Db4BucketMedium => new(_app, new string[]{"binary_sensor.db4_bucket_medium"});
        public BinarySensorEntity Db6BucketEmpty => new(_app, new string[]{"binary_sensor.db6_bucket_empty"});
        public BinarySensorEntity Db1WaterLevelState => new(_app, new string[]{"binary_sensor.db_1_water_level_state"});
        public BinarySensorEntity FanButton => new(_app, new string[]{"binary_sensor.fan_button"});
        public BinarySensorEntity MotoGStylusIsCharging => new(_app, new string[]{"binary_sensor.moto_g_stylus_is_charging"});
        public BinarySensorEntity FreshResWaterLevelState => new(_app, new string[]{"binary_sensor.fresh_res_water_level_state"});
        public BinarySensorEntity Espcam01Button => new(_app, new string[]{"binary_sensor.espcam01_button"});
        public BinarySensorEntity Db5BucketMedium => new(_app, new string[]{"binary_sensor.db5_bucket_medium"});
        public BinarySensorEntity Station1Status => new(_app, new string[]{"binary_sensor.station_1_status"});
        public BinarySensorEntity Espcam01Pir => new(_app, new string[]{"binary_sensor.espcam01_pir"});
        public BinarySensorEntity PowerStrip1ServerStatus => new(_app, new string[]{"binary_sensor.power_strip_1_server_status"});
        public BinarySensorEntity BackLightStatus => new(_app, new string[]{"binary_sensor.back_light_status"});
        public BinarySensorEntity Db5BucketFull => new(_app, new string[]{"binary_sensor.db5_bucket_full"});
        public BinarySensorEntity Db3BucketMedium => new(_app, new string[]{"binary_sensor.db3_bucket_medium"});
        public BinarySensorEntity Updater => new(_app, new string[]{"binary_sensor.updater"});
        public BinarySensorEntity NftResMedium => new(_app, new string[]{"binary_sensor.nft_res_medium"});
        public BinarySensorEntity Valve2Closed => new(_app, new string[]{"binary_sensor.valve_2_closed"});
        public BinarySensorEntity Db3BucketEmpty => new(_app, new string[]{"binary_sensor.db3_bucket_empty"});
        public BinarySensorEntity FreshWaterBucketFull => new(_app, new string[]{"binary_sensor.fresh_water_bucket_full"});
        public BinarySensorEntity FreshWaterBucketEmpty => new(_app, new string[]{"binary_sensor.fresh_water_bucket_empty"});
        public BinarySensorEntity NftResFull => new(_app, new string[]{"binary_sensor.nft_res_full"});
        public BinarySensorEntity FrontLightButton => new(_app, new string[]{"binary_sensor.front_light_button"});
        public BinarySensorEntity Station4Status => new(_app, new string[]{"binary_sensor.station_4_status"});
        public BinarySensorEntity NftWaterLevelStatus => new(_app, new string[]{"binary_sensor.nft_water_level_status"});
        public BinarySensorEntity Db6BucketFull => new(_app, new string[]{"binary_sensor.db6_bucket_full"});
        public BinarySensorEntity NftResEmpty => new(_app, new string[]{"binary_sensor.nft_res_empty"});
        public BinarySensorEntity ValveOpen => new(_app, new string[]{"binary_sensor.valve_open"});
        public BinarySensorEntity BucketFull => new(_app, new string[]{"binary_sensor.bucket_full"});
        public BinarySensorEntity GhBackLightState => new(_app, new string[]{"binary_sensor.gh_back_light_state"});
        public BinarySensorEntity Db3WaterLevelState => new(_app, new string[]{"binary_sensor.db_3_water_level_state"});
        public BinarySensorEntity FanStatus => new(_app, new string[]{"binary_sensor.fan_status"});
        public BinarySensorEntity Db4WaterLevelState => new(_app, new string[]{"binary_sensor.db_4_water_level_state"});
        public BinarySensorEntity Station4Status2 => new(_app, new string[]{"binary_sensor.station_4_status_2"});
        public BinarySensorEntity BucketFull2 => new(_app, new string[]{"binary_sensor.bucket_full_2"});
        public BinarySensorEntity Station2Status => new(_app, new string[]{"binary_sensor.station_2_status"});
        public BinarySensorEntity TestZoneWaterBucketFull => new(_app, new string[]{"binary_sensor.test_zone_water_bucket_full"});
        public BinarySensorEntity Db5WaterLevelState => new(_app, new string[]{"binary_sensor.db_5_water_level_state"});
        public BinarySensorEntity BucketEmpty2 => new(_app, new string[]{"binary_sensor.bucket_empty_2"});
        public BinarySensorEntity Db2WaterLevelState => new(_app, new string[]{"binary_sensor.db_2_water_level_state"});
        public BinarySensorEntity Db6WaterLevelState => new(_app, new string[]{"binary_sensor.db_6_water_level_state"});
        public BinarySensorEntity FreshWaterBucketMedium => new(_app, new string[]{"binary_sensor.fresh_water_bucket_medium"});
    }

    public partial class SensorEntities
    {
        private readonly NetDaemonRxApp _app;
        public SensorEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public SensorEntity MotoGStylusDoNotDisturbSensor => new(_app, new string[]{"sensor.moto_g_stylus_do_not_disturb_sensor"});
        public SensorEntity OpenweathermapForecastTime => new(_app, new string[]{"sensor.openweathermap_forecast_time"});
        public SensorEntity LocalIp => new(_app, new string[]{"sensor.local_ip"});
        public SensorEntity OpenweathermapForecastTemperature => new(_app, new string[]{"sensor.openweathermap_forecast_temperature"});
        public SensorEntity MotoGStylusBatteryState => new(_app, new string[]{"sensor.moto_g_stylus_battery_state"});
        public SensorEntity OpenweathermapWeatherCode => new(_app, new string[]{"sensor.openweathermap_weather_code"});
        public SensorEntity OpenweathermapRain => new(_app, new string[]{"sensor.openweathermap_rain"});
        public SensorEntity OpenweathermapTemperature => new(_app, new string[]{"sensor.openweathermap_temperature"});
        public SensorEntity OpenweathermapWindSpeed => new(_app, new string[]{"sensor.openweathermap_wind_speed"});
        public SensorEntity Db4Temp => new(_app, new string[]{"sensor.db4_temp"});
        public SensorEntity OpenweathermapWindBearing => new(_app, new string[]{"sensor.openweathermap_wind_bearing"});
        public SensorEntity OpenweathermapSnow => new(_app, new string[]{"sensor.openweathermap_snow"});
        public SensorEntity GreenhouseInternalHumidity => new(_app, new string[]{"sensor.greenhouse_internal_humidity"});
        public SensorEntity MotoGStylusLightSensor => new(_app, new string[]{"sensor.moto_g_stylus_light_sensor"});
        public SensorEntity MotoGStylusAudioSensor => new(_app, new string[]{"sensor.moto_g_stylus_audio_sensor"});
        public SensorEntity MotoGStylusBatteryLevel => new(_app, new string[]{"sensor.moto_g_stylus_battery_level"});
        public SensorEntity Db5Temp => new(_app, new string[]{"sensor.db5_temp"});
        public SensorEntity DailyEnergy => new(_app, new string[]{"sensor.daily_energy"});
        public SensorEntity MotoGStylusBluetoothConnection => new(_app, new string[]{"sensor.moto_g_stylus_bluetooth_connection"});
        public SensorEntity MotoGStylusGeocodedLocation => new(_app, new string[]{"sensor.moto_g_stylus_geocoded_location"});
        public SensorEntity OpenweathermapCloudCoverage => new(_app, new string[]{"sensor.openweathermap_cloud_coverage"});
        public SensorEntity OpenweathermapForecastWindBearing => new(_app, new string[]{"sensor.openweathermap_forecast_wind_bearing"});
        public SensorEntity ExteriorTemperature => new(_app, new string[]{"sensor.exterior_temperature"});
        public SensorEntity OpenweathermapForecastPressure => new(_app, new string[]{"sensor.openweathermap_forecast_pressure"});
        public SensorEntity NetdaemonStatus => new(_app, new string[]{"sensor.netdaemon_status"});
        public SensorEntity OpenweathermapWeather => new(_app, new string[]{"sensor.openweathermap_weather"});
        public SensorEntity GreenhouseInternalTemperature => new(_app, new string[]{"sensor.greenhouse_internal_temperature"});
        public SensorEntity PowerStrip1WifiStatus => new(_app, new string[]{"sensor.power_strip_1_wifi_status"});
        public SensorEntity MotoGStylusBatteryHealth => new(_app, new string[]{"sensor.moto_g_stylus_battery_health"});
        public SensorEntity Db2Temp => new(_app, new string[]{"sensor.db2_temp"});
        public SensorEntity ExteriorHumidity => new(_app, new string[]{"sensor.exterior_humidity"});
        public SensorEntity GhInteralProtectedTemp => new(_app, new string[]{"sensor.gh_interal_protected_temp"});
        public SensorEntity GhPowerR2Voltage => new(_app, new string[]{"sensor.gh_power_r2_voltage"});
        public SensorEntity MonthlyEnergy => new(_app, new string[]{"sensor.monthly_energy"});
        public SensorEntity NftWaterLevelState => new(_app, new string[]{"sensor.nft_water_level_state"});
        public SensorEntity MotoGStylusNextAlarm => new(_app, new string[]{"sensor.moto_g_stylus_next_alarm"});
        public SensorEntity Db6Temp => new(_app, new string[]{"sensor.db6_temp"});
        public SensorEntity MotoGStylusWifiConnection => new(_app, new string[]{"sensor.moto_g_stylus_wifi_connection"});
        public SensorEntity Db1Temp => new(_app, new string[]{"sensor.db1_temp"});
        public SensorEntity MotoGStylusProximitySensor => new(_app, new string[]{"sensor.moto_g_stylus_proximity_sensor"});
        public SensorEntity OpenweathermapPressure => new(_app, new string[]{"sensor.openweathermap_pressure"});
        public SensorEntity OpenweathermapForecastPrecipitation => new(_app, new string[]{"sensor.openweathermap_forecast_precipitation"});
        public SensorEntity OpenweathermapCondition => new(_app, new string[]{"sensor.openweathermap_condition"});
        public SensorEntity TestingZoneTemp => new(_app, new string[]{"sensor.testing_zone_temp"});
        public SensorEntity OpenweathermapForecastWindSpeed => new(_app, new string[]{"sensor.openweathermap_forecast_wind_speed"});
        public SensorEntity Hacs => new(_app, new string[]{"sensor.hacs"});
        public SensorEntity GhPowerR2Current => new(_app, new string[]{"sensor.gh_power_r2_current"});
        public SensorEntity SonoffPowUptime => new(_app, new string[]{"sensor.sonoff_pow_uptime"});
        public SensorEntity MotoGStylusLastReboot => new(_app, new string[]{"sensor.moto_g_stylus_last_reboot"});
        public SensorEntity Db3Temp => new(_app, new string[]{"sensor.db3_temp"});
        public SensorEntity OpenweathermapForecastCondition => new(_app, new string[]{"sensor.openweathermap_forecast_condition"});
        public SensorEntity GhPowerR2Power => new(_app, new string[]{"sensor.gh_power_r2_power"});
        public SensorEntity GhInteralProtectedHumidity => new(_app, new string[]{"sensor.gh_interal_protected_humidity"});
        public SensorEntity OpenweathermapForecastTemperatureLow => new(_app, new string[]{"sensor.openweathermap_forecast_temperature_low"});
        public SensorEntity MotoGStylusStorageSensor => new(_app, new string[]{"sensor.moto_g_stylus_storage_sensor"});
        public SensorEntity MotoGStylusChargerType => new(_app, new string[]{"sensor.moto_g_stylus_charger_type"});
        public SensorEntity OpenweathermapHumidity => new(_app, new string[]{"sensor.openweathermap_humidity"});
        public SensorEntity SonoffPowWifiSignal => new(_app, new string[]{"sensor.sonoff_pow_wifi_signal"});
    }

    public partial class WeatherEntities
    {
        private readonly NetDaemonRxApp _app;
        public WeatherEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public WeatherEntity Home => new(_app, new string[]{"weather.home"});
        public WeatherEntity Openweathermap => new(_app, new string[]{"weather.openweathermap"});
    }

    public partial class SwitchEntities
    {
        private readonly NetDaemonRxApp _app;
        public SwitchEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public SwitchEntity St1p1 => new(_app, new string[]{"switch.st1p1"});
        public SwitchEntity St4p4 => new(_app, new string[]{"switch.st4p4"});
        public SwitchEntity St3p1 => new(_app, new string[]{"switch.st3p1"});
        public SwitchEntity Tbp1 => new(_app, new string[]{"switch.tbp1"});
        public SwitchEntity St2p1 => new(_app, new string[]{"switch.st2p1"});
        public SwitchEntity RestartTestZoneWaterLevel => new(_app, new string[]{"switch.restart_test_zone_water_level"});
        public SwitchEntity Tb4p4 => new(_app, new string[]{"switch.tb4p4"});
        public SwitchEntity St2p2 => new(_app, new string[]{"switch.st2p2"});
        public SwitchEntity St4p2 => new(_app, new string[]{"switch.st4p2"});
        public SwitchEntity GhFrontLightSwitch => new(_app, new string[]{"switch.gh_front_light_switch"});
        public SwitchEntity Fan => new(_app, new string[]{"switch.fan"});
        public SwitchEntity GhProtectedRestart => new(_app, new string[]{"switch.gh_protected_restart"});
        public SwitchEntity St3p4 => new(_app, new string[]{"switch.st3p4"});
        public SwitchEntity St1p4 => new(_app, new string[]{"switch.st1p4"});
        public SwitchEntity RestartTestNodeLevel => new(_app, new string[]{"switch.restart_test_node_level"});
        public SwitchEntity PowerStrip1Outlet2 => new(_app, new string[]{"switch.power_strip_1_outlet_2"});
        public SwitchEntity NetdaemonNetdaemonApp => new(_app, new string[]{"switch.netdaemon_netdaemon_app"});
        public SwitchEntity St4p1 => new(_app, new string[]{"switch.st4p1"});
        public SwitchEntity NetdaemonPirlightsApp => new(_app, new string[]{"switch.netdaemon_pirlights_app"});
        public SwitchEntity St3p2 => new(_app, new string[]{"switch.st3p2"});
        public SwitchEntity St2p4 => new(_app, new string[]{"switch.st2p4"});
        public SwitchEntity GhInteriorAndExteriorTempRestart => new(_app, new string[]{"switch.gh_interior_and_exterior_temp_restart"});
        public SwitchEntity RestartNftWaterLevel => new(_app, new string[]{"switch.restart_nft_water_level"});
        public SwitchEntity Valve2 => new(_app, new string[]{"switch.valve_2"});
        public SwitchEntity RestartDb1WaterLevel => new(_app, new string[]{"switch.restart_db1_water_level"});
        public SwitchEntity Enablepumps => new(_app, new string[]{"switch.enablepumps"});
        public SwitchEntity St2p3 => new(_app, new string[]{"switch.st2p3"});
        public SwitchEntity PowerStrip1Outlet4 => new(_app, new string[]{"switch.power_strip_1_outlet_4"});
        public SwitchEntity St1p3 => new(_app, new string[]{"switch.st1p3"});
        public SwitchEntity St1p2 => new(_app, new string[]{"switch.st1p2"});
        public SwitchEntity Tb4p2 => new(_app, new string[]{"switch.tb4p2"});
        public SwitchEntity GreenHousePower => new(_app, new string[]{"switch.green_house_power"});
        public SwitchEntity RestartFreshWaterLevel => new(_app, new string[]{"switch.restart_fresh_water_level"});
        public SwitchEntity RestartDb5WaterLevel => new(_app, new string[]{"switch.restart_db5_water_level"});
        public SwitchEntity RestartDb4WaterLevel => new(_app, new string[]{"switch.restart_db4_water_level"});
        public SwitchEntity PowerStrip1Usb => new(_app, new string[]{"switch.power_strip_1_usb"});
        public SwitchEntity NetdaemonGrowlightApp => new(_app, new string[]{"switch.netdaemon_growlight_app"});
        public SwitchEntity RestartDb3WaterLevel => new(_app, new string[]{"switch.restart_db3_water_level"});
        public SwitchEntity Valve => new(_app, new string[]{"switch.valve"});
        public SwitchEntity PowerStrip1Outlet3 => new(_app, new string[]{"switch.power_strip_1_outlet_3"});
        public SwitchEntity PowerStrip1Outlet1 => new(_app, new string[]{"switch.power_strip_1_outlet_1"});
        public SwitchEntity Tb4p3 => new(_app, new string[]{"switch.tb4p3"});
        public SwitchEntity St3p3 => new(_app, new string[]{"switch.st3p3"});
        public SwitchEntity St4p3 => new(_app, new string[]{"switch.st4p3"});
        public SwitchEntity RestartDb2WaterLevel => new(_app, new string[]{"switch.restart_db2_water_level"});
        public SwitchEntity RestartFreshResWaterLevel => new(_app, new string[]{"switch.restart_fresh_res_water_level"});
        public SwitchEntity GhBackLight => new(_app, new string[]{"switch.gh_back_light"});
        public SwitchEntity RestartDb6WaterLevel => new(_app, new string[]{"switch.restart_db6_water_level"});
        public SwitchEntity NetdaemonNutrientsApp => new(_app, new string[]{"switch.netdaemon_nutrients_app"});
    }

    public partial class AutomationEntities
    {
        private readonly NetDaemonRxApp _app;
        public AutomationEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public AutomationEntity Test => new(_app, new string[]{"automation.test"});
        public AutomationEntity WriteStepperValueToEsp => new(_app, new string[]{"automation.write_stepper_value_to_esp"});
        public AutomationEntity TurnOffGrowLightInTheMornining => new(_app, new string[]{"automation.turn_off_grow_light_in_the_mornining"});
        public AutomationEntity TurnOnCirculatingFanEver4Hours => new(_app, new string[]{"automation.turn_on_circulating_fan_ever_4_hours"});
        public AutomationEntity HeaterTry2 => new(_app, new string[]{"automation.heater_try_2"});
        public AutomationEntity CheckTemperatureOfDb1Reservoir => new(_app, new string[]{"automation.check_temperature_of_db1_reservoir"});
        public AutomationEntity Db6WateringPump => new(_app, new string[]{"automation.db6_watering_pump"});
        public AutomationEntity TurnOnGrowLight => new(_app, new string[]{"automation.turn_on_grow_light"});
        public AutomationEntity TurnOnGrowLight42 => new(_app, new string[]{"automation.turn_on_grow_light_4_2"});
        public AutomationEntity TurnOnPumpEvery5Minutes => new(_app, new string[]{"automation.turn_on_pump_every_5_minutes"});
        public AutomationEntity Db4WateringPump => new(_app, new string[]{"automation.db4_watering_pump"});
        public AutomationEntity TurnOnNftPump => new(_app, new string[]{"automation.turn_on_nft_pump"});
        public AutomationEntity TurnOnDb2PumpEvery4Hours => new(_app, new string[]{"automation.turn_on_db2_pump_every_4_hours"});
        public AutomationEntity TurnOffGrowLight => new(_app, new string[]{"automation.turn_off_grow_light"});
        public AutomationEntity Db3WateringPump => new(_app, new string[]{"automation.db3_watering_pump"});
        public AutomationEntity Db3WateringPump2 => new(_app, new string[]{"automation.db3_watering_pump_2"});
        public AutomationEntity TurnOnGrowLightDb1And2BeforeSunset => new(_app, new string[]{"automation.turn_on_grow_light_db1_and_2_before_sunset"});
    }

    public partial class DeviceTrackerEntities
    {
        private readonly NetDaemonRxApp _app;
        public DeviceTrackerEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public DeviceTrackerEntity MotoGStylus => new(_app, new string[]{"device_tracker.moto_g_stylus"});
    }

    public partial class InputNumberEntities
    {
        private readonly NetDaemonRxApp _app;
        public InputNumberEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public InputNumberEntity StepperControl => new(_app, new string[]{"input_number.stepper_control"});
    }

    public partial class PersonEntities
    {
        private readonly NetDaemonRxApp _app;
        public PersonEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public PersonEntity NickGelotte => new(_app, new string[]{"person.nick_gelotte"});
    }

    public partial class ScriptEntities
    {
        private readonly NetDaemonRxApp _app;
        public ScriptEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public ScriptEntity Db1Main => new(_app, new string[]{"script.db1_main"});
        public ScriptEntity Db1Refill => new(_app, new string[]{"script.db1_refill"});
        public ScriptEntity AddDoseToCurrentZone => new(_app, new string[]{"script.add_dose_to_current_zone"});
        public ScriptEntity RefillCurrentBucket => new(_app, new string[]{"script.refill_current_bucket"});
    }

    public partial class InputSelectEntities
    {
        private readonly NetDaemonRxApp _app;
        public InputSelectEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public InputSelectEntity ReservoirRes => new(_app, new string[]{"input_select.reservoir_res"});
        public InputSelectEntity Reservoir => new(_app, new string[]{"input_select.reservoir"});
    }

    public partial class ZoneEntities
    {
        private readonly NetDaemonRxApp _app;
        public ZoneEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public ZoneEntity Home => new(_app, new string[]{"zone.home"});
    }

    public partial class CameraEntities
    {
        private readonly NetDaemonRxApp _app;
        public CameraEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public CameraEntity Espcam01 => new(_app, new string[]{"camera.espcam01"});
    }

    public partial class SunEntities
    {
        private readonly NetDaemonRxApp _app;
        public SunEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public SunEntity Sun => new(_app, new string[]{"sun.sun"});
    }
}