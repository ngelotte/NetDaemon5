using System.Collections.Generic;
using NetDaemon.Common.Reactive.Services;
using NetDaemon.Common.Reactive;

namespace NetDaemon.Generated.Reactive
{
    public class GeneratedAppBase : NetDaemonRxApp
    {
        public SensorEntities Sensor => new(this);
        public SwitchEntities Switch => new(this);
        public BinarySensorEntities BinarySensor => new(this);
        public InputNumberEntities InputNumber => new(this);
        public WeatherEntities Weather => new(this);
        public ZoneEntities Zone => new(this);
        public AutomationEntities Automation => new(this);
        public ScriptEntity Script => new(this, new string[] { "" });
        public DeviceTrackerEntities DeviceTracker => new(this);
        public SunEntities Sun => new(this);
        public CameraEntities Camera => new(this);
        public InputSelectEntities InputSelect => new(this);
        public InputBooleanEntities InputBoolean => new(this);
        public PersonEntities Person => new(this);
    }

    public partial class InputNumberEntity : NetDaemon.Common.Reactive.Services.RxEntityBase
    {
        public InputNumberEntity(INetDaemonRxApp daemon, IEnumerable<string> entityIds) : base(daemon, entityIds)
        {
        }

        public void Reload(dynamic? data = null)
        {
            CallService("input_number", "reload", data, false);
        }

        public void SetValue(dynamic? data = null)
        {
            CallService("input_number", "set_value", data, false);
        }

        public void Increment(dynamic? data = null)
        {
            CallService("input_number", "increment", data, false);
        }

        public void Decrement(dynamic? data = null)
        {
            CallService("input_number", "decrement", data, false);
        }
    }

    public partial class ScriptEntity : NetDaemon.Common.Reactive.Services.ScriptEntity
    {
        public ScriptEntity(INetDaemonRxApp daemon, IEnumerable<string> entityIds) : base(daemon, entityIds)
        {
        }

        public void RefillCurrentBucket(dynamic? data = null)
        {
            CallService("script", "refill_current_bucket", data, false);
        }

        public void AddDoseToCurrentZone(dynamic? data = null)
        {
            CallService("script", "add_dose_to_current_zone", data, false);
        }

        public void MovePump1(dynamic? data = null)
        {
            CallService("script", "move_pump_1", data, false);
        }

        public void MovePump2(dynamic? data = null)
        {
            CallService("script", "move_pump_2", data, false);
        }

        public void MovePump3(dynamic? data = null)
        {
            CallService("script", "move_pump_3", data, false);
        }
    }

    public partial class InputSelectEntity : NetDaemon.Common.Reactive.Services.RxEntityBase
    {
        public InputSelectEntity(INetDaemonRxApp daemon, IEnumerable<string> entityIds) : base(daemon, entityIds)
        {
        }

        public void Reload(dynamic? data = null)
        {
            CallService("input_select", "reload", data, false);
        }

        public void SelectOption(dynamic? data = null)
        {
            CallService("input_select", "select_option", data, false);
        }

        public void SelectNext(dynamic? data = null)
        {
            CallService("input_select", "select_next", data, false);
        }

        public void SelectPrevious(dynamic? data = null)
        {
            CallService("input_select", "select_previous", data, false);
        }

        public void SelectFirst(dynamic? data = null)
        {
            CallService("input_select", "select_first", data, false);
        }

        public void SelectLast(dynamic? data = null)
        {
            CallService("input_select", "select_last", data, false);
        }

        public void SetOptions(dynamic? data = null)
        {
            CallService("input_select", "set_options", data, false);
        }
    }

    public partial class SensorEntities
    {
        private readonly NetDaemonRxApp _app;
        public SensorEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public SensorEntity MotoGStylusLastReboot => new(_app, new string[] { "sensor.moto_g_stylus_last_reboot" });
        public SensorEntity MotoGStylusAudioSensor => new(_app, new string[] { "sensor.moto_g_stylus_audio_sensor" });
        public SensorEntity Db1Ec => new(_app, new string[] { "sensor.db1_ec" });
        public SensorEntity OpenweathermapCondition => new(_app, new string[] { "sensor.openweathermap_condition" });
        public SensorEntity Db3Ec => new(_app, new string[] { "sensor.db3_ec" });
        public SensorEntity MotoGStylusWifiConnection => new(_app, new string[] { "sensor.moto_g_stylus_wifi_connection" });
        public SensorEntity OpenweathermapForecastTemperature => new(_app, new string[] { "sensor.openweathermap_forecast_temperature" });
        public SensorEntity SonoffPowWifiSignal => new(_app, new string[] { "sensor.sonoff_pow_wifi_signal" });
        public SensorEntity OpenweathermapCloudCoverage => new(_app, new string[] { "sensor.openweathermap_cloud_coverage" });
        public SensorEntity OpenweathermapHumidity => new(_app, new string[] { "sensor.openweathermap_humidity" });
        public SensorEntity FrontFieldStationStatus => new(_app, new string[] { "sensor.front_field_station_status" });
        public SensorEntity Hacs => new(_app, new string[] { "sensor.hacs" });
        public SensorEntity GhInteralProtectedHumidity => new(_app, new string[] { "sensor.gh_interal_protected_humidity" });
        public SensorEntity SonoffPowUptime => new(_app, new string[] { "sensor.sonoff_pow_uptime" });
        public SensorEntity NetdaemonStatus => new(_app, new string[] { "sensor.netdaemon_status" });
        public SensorEntity BerriesStationStatus => new(_app, new string[] { "sensor.berries_station_status" });
        public SensorEntity Db1Temp => new(_app, new string[] { "sensor.db1_temp" });
        public SensorEntity OpenweathermapForecastWindBearing => new(_app, new string[] { "sensor.openweathermap_forecast_wind_bearing" });
        public SensorEntity WaterTemperature => new(_app, new string[] { "sensor.water_temperature" });
        public SensorEntity MotoGStylusBluetoothConnection => new(_app, new string[] { "sensor.moto_g_stylus_bluetooth_connection" });
        public SensorEntity MotoGStylusLightSensor => new(_app, new string[] { "sensor.moto_g_stylus_light_sensor" });
        public SensorEntity Db2Temp => new(_app, new string[] { "sensor.db2_temp" });
        public SensorEntity S20StationStatus => new(_app, new string[] { "sensor.s20_station_status" });
        public SensorEntity S23StationStatus => new(_app, new string[] { "sensor.s23_station_status" });
        public SensorEntity OpenweathermapPrecipitationKind => new(_app, new string[] { "sensor.openweathermap_precipitation_kind" });
        public SensorEntity ExteriorHumidity => new(_app, new string[] { "sensor.exterior_humidity" });
        public SensorEntity GhPowerR2Current => new(_app, new string[] { "sensor.gh_power_r2_current" });
        public SensorEntity Db5Ec => new(_app, new string[] { "sensor.db5_ec" });
        public SensorEntity PowerStripMidWifiStatus => new(_app, new string[] { "sensor.power_strip_mid_wifi_status" });
        public SensorEntity GhInteralProtectedTemp => new(_app, new string[] { "sensor.gh_interal_protected_temp" });
        public SensorEntity OpenweathermapRain => new(_app, new string[] { "sensor.openweathermap_rain" });
        public SensorEntity S15StationStatus => new(_app, new string[] { "sensor.s15_station_status" });
        public SensorEntity OpenweathermapTemperature => new(_app, new string[] { "sensor.openweathermap_temperature" });
        public SensorEntity Db6Ec => new(_app, new string[] { "sensor.db6_ec" });
        public SensorEntity OpenweathermapForecastPrecipitationProbability => new(_app, new string[] { "sensor.openweathermap_forecast_precipitation_probability" });
        public SensorEntity S08StationStatus => new(_app, new string[] { "sensor.s08_station_status" });
        public SensorEntity S21StationStatus => new(_app, new string[] { "sensor.s21_station_status" });
        public SensorEntity OpenweathermapWeatherCode => new(_app, new string[] { "sensor.openweathermap_weather_code" });
        public SensorEntity MonthlyEnergy => new(_app, new string[] { "sensor.monthly_energy" });
        public SensorEntity Db4Ec => new(_app, new string[] { "sensor.db4_ec" });
        public SensorEntity OpenweathermapFeelsLikeTemperature => new(_app, new string[] { "sensor.openweathermap_feels_like_temperature" });
        public SensorEntity OpensprinklerRainDelayStopTime => new(_app, new string[] { "sensor.opensprinkler_rain_delay_stop_time" });
        public SensorEntity S14StationStatus => new(_app, new string[] { "sensor.s14_station_status" });
        public SensorEntity ExteriorTemperature => new(_app, new string[] { "sensor.exterior_temperature" });
        public SensorEntity OpenweathermapForecastCondition => new(_app, new string[] { "sensor.openweathermap_forecast_condition" });
        public SensorEntity BleVoltageGhTempExternal => new(_app, new string[] { "sensor.ble_voltage_gh_temp_external" });
        public SensorEntity S17StationStatus => new(_app, new string[] { "sensor.s17_station_status" });
        public SensorEntity Db5Temp => new(_app, new string[] { "sensor.db5_temp" });
        public SensorEntity BleTemperatureGhIndoorTemp => new(_app, new string[] { "sensor.ble_temperature_gh_indoor_temp" });
        public SensorEntity SwpCoolWaterLevelState => new(_app, new string[] { "sensor.swp_cool_water_level_state" });
        public SensorEntity NatureSprinklerStationStatus => new(_app, new string[] { "sensor.nature_sprinkler_station_status" });
        public SensorEntity NftEc => new(_app, new string[] { "sensor.nft_ec" });
        public SensorEntity S18StationStatus => new(_app, new string[] { "sensor.s18_station_status" });
        public SensorEntity FrontFlowerSprinklerStationStatus => new(_app, new string[] { "sensor.front_flower_sprinkler_station_status" });
        public SensorEntity NftWaterLevelState2 => new(_app, new string[] { "sensor.nft_water_level_state_2" });
        public SensorEntity GreenhouseInternalHumidity => new(_app, new string[] { "sensor.greenhouse_internal_humidity" });
        public SensorEntity PowerStripTestZoneWifiStatus => new(_app, new string[] { "sensor.power_strip_test_zone_wifi_status" });
        public SensorEntity OpensprinklerFlowRate => new(_app, new string[] { "sensor.opensprinkler_flow_rate" });
        public SensorEntity OpenweathermapWeather => new(_app, new string[] { "sensor.openweathermap_weather" });
        public SensorEntity MotoGStylusNextAlarm => new(_app, new string[] { "sensor.moto_g_stylus_next_alarm" });
        public SensorEntity OpenweathermapForecastTime => new(_app, new string[] { "sensor.openweathermap_forecast_time" });
        public SensorEntity GhPowerR2Voltage => new(_app, new string[] { "sensor.gh_power_r2_voltage" });
        public SensorEntity MotoGStylusStorageSensor => new(_app, new string[] { "sensor.moto_g_stylus_storage_sensor" });
        public SensorEntity OpenweathermapForecastPrecipitation => new(_app, new string[] { "sensor.openweathermap_forecast_precipitation" });
        public SensorEntity MotoGStylusBatteryState => new(_app, new string[] { "sensor.moto_g_stylus_battery_state" });
        public SensorEntity OpenweathermapUvIndex => new(_app, new string[] { "sensor.openweathermap_uv_index" });
        public SensorEntity DailyEnergy => new(_app, new string[] { "sensor.daily_energy" });
        public SensorEntity WellPTsAndAAStationStatus => new(_app, new string[] { "sensor.well_p_ts_and_a_a_station_status" });
        public SensorEntity OpenweathermapSnow => new(_app, new string[] { "sensor.openweathermap_snow" });
        public SensorEntity NftTemp => new(_app, new string[] { "sensor.nft_temp" });
        public SensorEntity HomeAssistantBeansStationStatus => new(_app, new string[] { "sensor.home_assistant_beans_station_status" });
        public SensorEntity OpenweathermapPressure => new(_app, new string[] { "sensor.openweathermap_pressure" });
        public SensorEntity GreenhouseTemp => new(_app, new string[] { "sensor.greenhouse_temp" });
        public SensorEntity Db2Ec => new(_app, new string[] { "sensor.db2_ec" });
        public SensorEntity BleHumidityGhIndoorTemp => new(_app, new string[] { "sensor.ble_humidity_gh_indoor_temp" });
        public SensorEntity MotoGStylusBatteryTemperature => new(_app, new string[] { "sensor.moto_g_stylus_battery_temperature" });
        public SensorEntity BleVoltageGhIndoorTemp => new(_app, new string[] { "sensor.ble_voltage_gh_indoor_temp" });
        public SensorEntity OpenweathermapForecastCloudCoverage => new(_app, new string[] { "sensor.openweathermap_forecast_cloud_coverage" });
        public SensorEntity OpenweathermapForecastPressure => new(_app, new string[] { "sensor.openweathermap_forecast_pressure" });
        public SensorEntity GreenhouseInternalTemperature => new(_app, new string[] { "sensor.greenhouse_internal_temperature" });
        public SensorEntity MotoGStylusDoNotDisturbSensor => new(_app, new string[] { "sensor.moto_g_stylus_do_not_disturb_sensor" });
        public SensorEntity SideBackGrassStationStatus => new(_app, new string[] { "sensor.side_back_grass_station_status" });
        public SensorEntity MotoGStylusGeocodedLocation => new(_app, new string[] { "sensor.moto_g_stylus_geocoded_location" });
        public SensorEntity OpenweathermapForecastWindSpeed => new(_app, new string[] { "sensor.openweathermap_forecast_wind_speed" });
        public SensorEntity PowerStrip1WifiStatus => new(_app, new string[] { "sensor.power_strip_1_wifi_status" });
        public SensorEntity BackBushesStationStatus => new(_app, new string[] { "sensor.back_bushes_station_status" });
        public SensorEntity Db3Temp => new(_app, new string[] { "sensor.db3_temp" });
        public SensorEntity EcSensor => new(_app, new string[] { "sensor.ec_sensor" });
        public SensorEntity MotoGStylusChargerType => new(_app, new string[] { "sensor.moto_g_stylus_charger_type" });
        public SensorEntity BleBatteryGhIndoorTemp => new(_app, new string[] { "sensor.ble_battery_gh_indoor_temp" });
        public SensorEntity MotoGStylusBatteryLevel => new(_app, new string[] { "sensor.moto_g_stylus_battery_level" });
        public SensorEntity MotoGStylusBatteryHealth => new(_app, new string[] { "sensor.moto_g_stylus_battery_health" });
        public SensorEntity PowerStrip1WifiStatus2 => new(_app, new string[] { "sensor.power_strip_1_wifi_status_2" });
        public SensorEntity NftWaterLevelState => new(_app, new string[] { "sensor.nft_water_level_state" });
        public SensorEntity OpensprinklerLastRun => new(_app, new string[] { "sensor.opensprinkler_last_run" });
        public SensorEntity Db4Temp => new(_app, new string[] { "sensor.db4_temp" });
        public SensorEntity S07StationStatus => new(_app, new string[] { "sensor.s07_station_status" });
        public SensorEntity WonderStrawberriesStationStatus => new(_app, new string[] { "sensor.wonder_strawberries_station_status" });
        public SensorEntity GhPowerR2Power => new(_app, new string[] { "sensor.gh_power_r2_power" });
        public SensorEntity OpenweathermapDewPoint => new(_app, new string[] { "sensor.openweathermap_dew_point" });
        public SensorEntity S19StationStatus => new(_app, new string[] { "sensor.s19_station_status" });
        public SensorEntity MotoGStylusProximitySensor => new(_app, new string[] { "sensor.moto_g_stylus_proximity_sensor" });
        public SensorEntity LocalIp => new(_app, new string[] { "sensor.local_ip" });
        public SensorEntity S24StationStatus => new(_app, new string[] { "sensor.s24_station_status" });
        public SensorEntity SnapPeasAndSunflowersStationStatus => new(_app, new string[] { "sensor.snap_peas_and_sunflowers_station_status" });
        public SensorEntity S22StationStatus => new(_app, new string[] { "sensor.s22_station_status" });
        public SensorEntity OpenweathermapWindSpeed => new(_app, new string[] { "sensor.openweathermap_wind_speed" });
        public SensorEntity OpenweathermapWindBearing => new(_app, new string[] { "sensor.openweathermap_wind_bearing" });
        public SensorEntity OpensprinklerWaterLevel => new(_app, new string[] { "sensor.opensprinkler_water_level" });
        public SensorEntity BleBatteryGhTempExternal => new(_app, new string[] { "sensor.ble_battery_gh_temp_external" });
        public SensorEntity S16StationStatus => new(_app, new string[] { "sensor.s16_station_status" });
        public SensorEntity OpenweathermapForecastTemperatureLow => new(_app, new string[] { "sensor.openweathermap_forecast_temperature_low" });
        public SensorEntity BleTemperatureGhTempExternal => new(_app, new string[] { "sensor.ble_temperature_gh_temp_external" });
        public SensorEntity FruitTreesStationStatus => new(_app, new string[] { "sensor.fruit_trees_station_status" });
        public SensorEntity Db6Temp => new(_app, new string[] { "sensor.db6_temp" });
        public SensorEntity GreenhouseHumidity => new(_app, new string[] { "sensor.greenhouse_humidity" });
        public SensorEntity BleHumidityGhExternal => new(_app, new string[] { "sensor.ble_humidity_gh_external" });
    }

    public partial class SwitchEntities
    {
        private readonly NetDaemonRxApp _app;
        public SwitchEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public SwitchEntity St3p1 => new(_app, new string[] { "switch.st3p1" });
        public SwitchEntity RestartEcSensor => new(_app, new string[] { "switch.restart_ec_sensor" });
        public SwitchEntity RestartTestZoneWaterLevel => new(_app, new string[] { "switch.restart_test_zone_water_level" });
        public SwitchEntity Valve2 => new(_app, new string[] { "switch.valve_2" });
        public SwitchEntity FrontFieldStationEnabled => new(_app, new string[] { "switch.front_field_station_enabled" });
        public SwitchEntity S17StationEnabled => new(_app, new string[] { "switch.s17_station_enabled" });
        public SwitchEntity FruitTreesStationEnabled => new(_app, new string[] { "switch.fruit_trees_station_enabled" });
        public SwitchEntity Tb4p2 => new(_app, new string[] { "switch.tb4p2" });
        public SwitchEntity St2p4 => new(_app, new string[] { "switch.st2p4" });
        public SwitchEntity RestartDb1WaterLevel => new(_app, new string[] { "switch.restart_db1_water_level" });
        public SwitchEntity SideBackGrassStationEnabled => new(_app, new string[] { "switch.side_back_grass_station_enabled" });
        public SwitchEntity FirstHalfProgramEnabled => new(_app, new string[] { "switch.first_half_program_enabled" });
        public SwitchEntity St2p2 => new(_app, new string[] { "switch.st2p2" });
        public SwitchEntity PowerStripNeOutlet1 => new(_app, new string[] { "switch.power_strip_ne_outlet_1" });
        public SwitchEntity PowerStripTestZoneOutlet2 => new(_app, new string[] { "switch.power_strip_test_zone_outlet_2" });
        public SwitchEntity PowerStripTestZoneOutlet4 => new(_app, new string[] { "switch.power_strip_test_zone_outlet_4" });
        public SwitchEntity S19StationEnabled => new(_app, new string[] { "switch.s19_station_enabled" });
        public SwitchEntity PowerStrip1Usb => new(_app, new string[] { "switch.power_strip_1_usb" });
        public SwitchEntity PowerStrip1Outlet3 => new(_app, new string[] { "switch.power_strip_1_outlet_3" });
        public SwitchEntity NetdaemonNutrientsApp => new(_app, new string[] { "switch.netdaemon_nutrients_app" });
        public SwitchEntity St4p3 => new(_app, new string[] { "switch.st4p3" });
        public SwitchEntity NetdaemonRefillApp => new(_app, new string[] { "switch.netdaemon_refill_app" });
        public SwitchEntity NetdaemonDehumidifierApp => new(_app, new string[] { "switch.netdaemon_dehumidifier_app" });
        public SwitchEntity SnapPeasAndSunflowersStationEnabled => new(_app, new string[] { "switch.snap_peas_and_sunflowers_station_enabled" });
        public SwitchEntity St1p3 => new(_app, new string[] { "switch.st1p3" });
        public SwitchEntity S22StationEnabled => new(_app, new string[] { "switch.s22_station_enabled" });
        public SwitchEntity PowerStrip1Outlet32 => new(_app, new string[] { "switch.power_strip_1_outlet_3_2" });
        public SwitchEntity SecondHalfProgramEnabled => new(_app, new string[] { "switch.second_half_program_enabled" });
        public SwitchEntity HomeAssistantBeansStationEnabled => new(_app, new string[] { "switch.home_assistant_beans_station_enabled" });
        public SwitchEntity GhFrontLightSwitch => new(_app, new string[] { "switch.gh_front_light_switch" });
        public SwitchEntity RestartDb4WaterLevel => new(_app, new string[] { "switch.restart_db4_water_level" });
        public SwitchEntity PowerStrip1Outlet22 => new(_app, new string[] { "switch.power_strip_1_outlet_2_2" });
        public SwitchEntity St3p2 => new(_app, new string[] { "switch.st3p2" });
        public SwitchEntity PowerStrip1Outlet4 => new(_app, new string[] { "switch.power_strip_1_outlet_4" });
        public SwitchEntity S14StationEnabled => new(_app, new string[] { "switch.s14_station_enabled" });
        public SwitchEntity St1p2 => new(_app, new string[] { "switch.st1p2" });
        public SwitchEntity NetdaemonPirlightsApp => new(_app, new string[] { "switch.netdaemon_pirlights_app" });
        public SwitchEntity BackBushesStationEnabled => new(_app, new string[] { "switch.back_bushes_station_enabled" });
        public SwitchEntity PowerStripMidUsb => new(_app, new string[] { "switch.power_strip_mid_usb" });
        public SwitchEntity S21StationEnabled => new(_app, new string[] { "switch.s21_station_enabled" });
        public SwitchEntity PowerStrip1Outlet1 => new(_app, new string[] { "switch.power_strip_1_outlet_1" });
        public SwitchEntity St1p4 => new(_app, new string[] { "switch.st1p4" });
        public SwitchEntity Tb4p4 => new(_app, new string[] { "switch.tb4p4" });
        public SwitchEntity WellPTsAndAAStationEnabled => new(_app, new string[] { "switch.well_p_ts_and_a_a_station_enabled" });
        public SwitchEntity S07StationEnabled => new(_app, new string[] { "switch.s07_station_enabled" });
        public SwitchEntity St3p3 => new(_app, new string[] { "switch.st3p3" });
        public SwitchEntity PowerStripNeOutlet3 => new(_app, new string[] { "switch.power_strip_ne_outlet_3" });
        public SwitchEntity PowerStripTestZoneUsb => new(_app, new string[] { "switch.power_strip_test_zone_usb" });
        public SwitchEntity RestartDb3WaterLevel => new(_app, new string[] { "switch.restart_db3_water_level" });
        public SwitchEntity PowerStripMidOutlet4 => new(_app, new string[] { "switch.power_strip_mid_outlet_4" });
        public SwitchEntity FrontFlowerSprinklerStationEnabled => new(_app, new string[] { "switch.front_flower_sprinkler_station_enabled" });
        public SwitchEntity PowerStripNeOutlet4 => new(_app, new string[] { "switch.power_strip_ne_outlet_4" });
        public SwitchEntity Tbp1 => new(_app, new string[] { "switch.tbp1" });
        public SwitchEntity PowerStripTestZoneOutlet1 => new(_app, new string[] { "switch.power_strip_test_zone_outlet_1" });
        public SwitchEntity S18StationEnabled => new(_app, new string[] { "switch.s18_station_enabled" });
        public SwitchEntity RestartDb5WaterLevel => new(_app, new string[] { "switch.restart_db5_water_level" });
        public SwitchEntity RestartFreshWaterLevel => new(_app, new string[] { "switch.restart_fresh_water_level" });
        public SwitchEntity St2p1 => new(_app, new string[] { "switch.st2p1" });
        public SwitchEntity NetdaemonNetdaemonApp => new(_app, new string[] { "switch.netdaemon_netdaemon_app" });
        public SwitchEntity S08StationEnabled => new(_app, new string[] { "switch.s08_station_enabled" });
        public SwitchEntity PowerStripNeOutlet2 => new(_app, new string[] { "switch.power_strip_ne_outlet_2" });
        public SwitchEntity S20StationEnabled => new(_app, new string[] { "switch.s20_station_enabled" });
        public SwitchEntity PowerStripMidOutlet2 => new(_app, new string[] { "switch.power_strip_mid_outlet_2" });
        public SwitchEntity PowerStrip1Usb2 => new(_app, new string[] { "switch.power_strip_1_usb_2" });
        public SwitchEntity PowerStripMidOutlet3 => new(_app, new string[] { "switch.power_strip_mid_outlet_3" });
        public SwitchEntity PowerStrip1Outlet12 => new(_app, new string[] { "switch.power_strip_1_outlet_1_2" });
        public SwitchEntity GhBackLight => new(_app, new string[] { "switch.gh_back_light" });
        public SwitchEntity PowerStrip1Outlet2 => new(_app, new string[] { "switch.power_strip_1_outlet_2" });
        public SwitchEntity GhProtectedRestart => new(_app, new string[] { "switch.gh_protected_restart" });
        public SwitchEntity BerriesStationEnabled => new(_app, new string[] { "switch.berries_station_enabled" });
        public SwitchEntity PowerStripNeUsb => new(_app, new string[] { "switch.power_strip_ne_usb" });
        public SwitchEntity PowerStripMidOutlet1 => new(_app, new string[] { "switch.power_strip_mid_outlet_1" });
        public SwitchEntity Enablepumps => new(_app, new string[] { "switch.enablepumps" });
        public SwitchEntity RestartFreshResWaterLevel => new(_app, new string[] { "switch.restart_fresh_res_water_level" });
        public SwitchEntity St4p4 => new(_app, new string[] { "switch.st4p4" });
        public SwitchEntity S23StationEnabled => new(_app, new string[] { "switch.s23_station_enabled" });
        public SwitchEntity OpensprinklerEnabled => new(_app, new string[] { "switch.opensprinkler_enabled" });
        public SwitchEntity PowerStrip1Outlet42 => new(_app, new string[] { "switch.power_strip_1_outlet_4_2" });
        public SwitchEntity S16StationEnabled => new(_app, new string[] { "switch.s16_station_enabled" });
        public SwitchEntity RestartDb6WaterLevel => new(_app, new string[] { "switch.restart_db6_water_level" });
        public SwitchEntity PowerStripTestZoneOutlet3 => new(_app, new string[] { "switch.power_strip_test_zone_outlet_3" });
        public SwitchEntity RestartNftWaterLevel2 => new(_app, new string[] { "switch.restart_nft_water_level_2" });
        public SwitchEntity RestartDb2WaterLevel => new(_app, new string[] { "switch.restart_db2_water_level" });
        public SwitchEntity NetdaemonGrowlightApp => new(_app, new string[] { "switch.netdaemon_growlight_app" });
        public SwitchEntity Fan => new(_app, new string[] { "switch.fan" });
        public SwitchEntity St2p3 => new(_app, new string[] { "switch.st2p3" });
        public SwitchEntity St4p2 => new(_app, new string[] { "switch.st4p2" });
        public SwitchEntity GhInteriorAndExteriorTempRestart => new(_app, new string[] { "switch.gh_interior_and_exterior_temp_restart" });
        public SwitchEntity NatureSprinklerStationEnabled => new(_app, new string[] { "switch.nature_sprinkler_station_enabled" });
        public SwitchEntity RestartSwpCoolWaterLevel => new(_app, new string[] { "switch.restart_swp_cool_water_level" });
        public SwitchEntity St3p4 => new(_app, new string[] { "switch.st3p4" });
        public SwitchEntity St4p1 => new(_app, new string[] { "switch.st4p1" });
        public SwitchEntity RestartNftWaterLevel => new(_app, new string[] { "switch.restart_nft_water_level" });
        public SwitchEntity S24StationEnabled => new(_app, new string[] { "switch.s24_station_enabled" });
        public SwitchEntity GreenHousePower => new(_app, new string[] { "switch.green_house_power" });
        public SwitchEntity St1p1 => new(_app, new string[] { "switch.st1p1" });
        public SwitchEntity WonderStrawberriesStationEnabled => new(_app, new string[] { "switch.wonder_strawberries_station_enabled" });
        public SwitchEntity NetdaemonEnvcontrolsApp => new(_app, new string[] { "switch.netdaemon_envcontrols_app" });
        public SwitchEntity Valve => new(_app, new string[] { "switch.valve" });
        public SwitchEntity Tb4p3 => new(_app, new string[] { "switch.tb4p3" });
        public SwitchEntity S15StationEnabled => new(_app, new string[] { "switch.s15_station_enabled" });
    }

    public partial class BinarySensorEntities
    {
        private readonly NetDaemonRxApp _app;
        public BinarySensorEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public BinarySensorEntity TestZoneWaterBucketEmpty => new(_app, new string[] { "binary_sensor.test_zone_water_bucket_empty" });
        public BinarySensorEntity PowerStrip1Button => new(_app, new string[] { "binary_sensor.power_strip_1_button" });
        public BinarySensorEntity FreshWaterMedium => new(_app, new string[] { "binary_sensor.fresh_water_medium" });
        public BinarySensorEntity NftResFull => new(_app, new string[] { "binary_sensor.nft_res_full" });
        public BinarySensorEntity NftResEmpty => new(_app, new string[] { "binary_sensor.nft_res_empty" });
        public BinarySensorEntity S18StationRunning => new(_app, new string[] { "binary_sensor.s18_station_running" });
        public BinarySensorEntity SideBackGrassStationRunning => new(_app, new string[] { "binary_sensor.side_back_grass_station_running" });
        public BinarySensorEntity S07StationRunning => new(_app, new string[] { "binary_sensor.s07_station_running" });
        public BinarySensorEntity SwpResFull => new(_app, new string[] { "binary_sensor.swp_res_full" });
        public BinarySensorEntity SnapPeasAndSunflowersStationRunning => new(_app, new string[] { "binary_sensor.snap_peas_and_sunflowers_station_running" });
        public BinarySensorEntity FruitTreesStationRunning => new(_app, new string[] { "binary_sensor.fruit_trees_station_running" });
        public BinarySensorEntity S14StationRunning => new(_app, new string[] { "binary_sensor.s14_station_running" });
        public BinarySensorEntity GhBackLightState => new(_app, new string[] { "binary_sensor.gh_back_light_state" });
        public BinarySensorEntity Db6BucketFull => new(_app, new string[] { "binary_sensor.db6_bucket_full" });
        public BinarySensorEntity Db3BucketMedium => new(_app, new string[] { "binary_sensor.db3_bucket_medium" });
        public BinarySensorEntity RemoteUi => new(_app, new string[] { "binary_sensor.remote_ui" });
        public BinarySensorEntity SwpResEmpty => new(_app, new string[] { "binary_sensor.swp_res_empty" });
        public BinarySensorEntity WonderStrawberriesStationRunning => new(_app, new string[] { "binary_sensor.wonder_strawberries_station_running" });
        public BinarySensorEntity Station4Status2 => new(_app, new string[] { "binary_sensor.station_4_status_2" });
        public BinarySensorEntity Espcam01Button => new(_app, new string[] { "binary_sensor.espcam01_button" });
        public BinarySensorEntity Db6WaterLevelState => new(_app, new string[] { "binary_sensor.db_6_water_level_state" });
        public BinarySensorEntity FanStatus => new(_app, new string[] { "binary_sensor.fan_status" });
        public BinarySensorEntity ValveOpen => new(_app, new string[] { "binary_sensor.valve_open" });
        public BinarySensorEntity FreshWaterEmpty => new(_app, new string[] { "binary_sensor.fresh_water_empty" });
        public BinarySensorEntity S22StationRunning => new(_app, new string[] { "binary_sensor.s22_station_running" });
        public BinarySensorEntity OpensprinklerRainDelayActive => new(_app, new string[] { "binary_sensor.opensprinkler_rain_delay_active" });
        public BinarySensorEntity Db5BucketFull => new(_app, new string[] { "binary_sensor.db5_bucket_full" });
        public BinarySensorEntity Station3Status => new(_app, new string[] { "binary_sensor.station_3_status" });
        public BinarySensorEntity Db6BucketEmpty => new(_app, new string[] { "binary_sensor.db6_bucket_empty" });
        public BinarySensorEntity Db2Medium => new(_app, new string[] { "binary_sensor.db2_medium" });
        public BinarySensorEntity Station1Status => new(_app, new string[] { "binary_sensor.station_1_status" });
        public BinarySensorEntity TestZoneResWaterLevelState => new(_app, new string[] { "binary_sensor.test_zone_res_water_level_state" });
        public BinarySensorEntity FreshWaterLevelState => new(_app, new string[] { "binary_sensor.fresh_water_level_state" });
        public BinarySensorEntity Espcam01Pir => new(_app, new string[] { "binary_sensor.espcam01_pir" });
        public BinarySensorEntity Db2WaterLevelState => new(_app, new string[] { "binary_sensor.db_2_water_level_state" });
        public BinarySensorEntity TestZoneWaterBucketFull => new(_app, new string[] { "binary_sensor.test_zone_water_bucket_full" });
        public BinarySensorEntity FreshWaterBucketFull => new(_app, new string[] { "binary_sensor.fresh_water_bucket_full" });
        public BinarySensorEntity OpensprinklerSensor2Active => new(_app, new string[] { "binary_sensor.opensprinkler_sensor_2_active" });
        public BinarySensorEntity BackLightStatus => new(_app, new string[] { "binary_sensor.back_light_status" });
        public BinarySensorEntity Valve2Open => new(_app, new string[] { "binary_sensor.valve_2_open" });
        public BinarySensorEntity OpensprinklerSensor1Active => new(_app, new string[] { "binary_sensor.opensprinkler_sensor_1_active" });
        public BinarySensorEntity NatureSprinklerStationRunning => new(_app, new string[] { "binary_sensor.nature_sprinkler_station_running" });
        public BinarySensorEntity Db1WaterLevelState => new(_app, new string[] { "binary_sensor.db_1_water_level_state" });
        public BinarySensorEntity BucketMedium => new(_app, new string[] { "binary_sensor.bucket_medium" });
        public BinarySensorEntity Db2Empty => new(_app, new string[] { "binary_sensor.db2_empty" });
        public BinarySensorEntity FreshWaterBucketEmpty => new(_app, new string[] { "binary_sensor.fresh_water_bucket_empty" });
        public BinarySensorEntity FirstHalfProgramRunning => new(_app, new string[] { "binary_sensor.first_half_program_running" });
        public BinarySensorEntity S20StationRunning => new(_app, new string[] { "binary_sensor.s20_station_running" });
        public BinarySensorEntity Db3BucketEmpty => new(_app, new string[] { "binary_sensor.db3_bucket_empty" });
        public BinarySensorEntity NftWaterLevelStatus => new(_app, new string[] { "binary_sensor.nft_water_level_status" });
        public BinarySensorEntity NftResEmpty2 => new(_app, new string[] { "binary_sensor.nft_res_empty_2" });
        public BinarySensorEntity S16StationRunning => new(_app, new string[] { "binary_sensor.s16_station_running" });
        public BinarySensorEntity S24StationRunning => new(_app, new string[] { "binary_sensor.s24_station_running" });
        public BinarySensorEntity Db3BucketFull => new(_app, new string[] { "binary_sensor.db3_bucket_full" });
        public BinarySensorEntity WellPTsAndAAStationRunning => new(_app, new string[] { "binary_sensor.well_p_ts_and_a_a_station_running" });
        public BinarySensorEntity S15StationRunning => new(_app, new string[] { "binary_sensor.s15_station_running" });
        public BinarySensorEntity S23StationRunning => new(_app, new string[] { "binary_sensor.s23_station_running" });
        public BinarySensorEntity S17StationRunning => new(_app, new string[] { "binary_sensor.s17_station_running" });
        public BinarySensorEntity BerriesStationRunning => new(_app, new string[] { "binary_sensor.berries_station_running" });
        public BinarySensorEntity Db4BucketFull => new(_app, new string[] { "binary_sensor.db4_bucket_full" });
        public BinarySensorEntity FrontLightButton => new(_app, new string[] { "binary_sensor.front_light_button" });
        public BinarySensorEntity SwpResMedium => new(_app, new string[] { "binary_sensor.swp_res_medium" });
        public BinarySensorEntity BucketEmpty => new(_app, new string[] { "binary_sensor.bucket_empty" });
        public BinarySensorEntity BackBushesStationRunning => new(_app, new string[] { "binary_sensor.back_bushes_station_running" });
        public BinarySensorEntity SwpCoolWaterLevelStatus => new(_app, new string[] { "binary_sensor.swp_cool_water_level_status" });
        public BinarySensorEntity Station4Status => new(_app, new string[] { "binary_sensor.station_4_status" });
        public BinarySensorEntity HomeAssistantBeansStationRunning => new(_app, new string[] { "binary_sensor.home_assistant_beans_station_running" });
        public BinarySensorEntity FrontLightStatus => new(_app, new string[] { "binary_sensor.front_light_status" });
        public BinarySensorEntity BucketFull => new(_app, new string[] { "binary_sensor.bucket_full" });
        public BinarySensorEntity SecondHalfProgramRunning => new(_app, new string[] { "binary_sensor.second_half_program_running" });
        public BinarySensorEntity FrontFlowerSprinklerStationRunning => new(_app, new string[] { "binary_sensor.front_flower_sprinkler_station_running" });
        public BinarySensorEntity Db4BucketMedium => new(_app, new string[] { "binary_sensor.db4_bucket_medium" });
        public BinarySensorEntity Db4WaterLevelState => new(_app, new string[] { "binary_sensor.db_4_water_level_state" });
        public BinarySensorEntity Valve2Closed => new(_app, new string[] { "binary_sensor.valve_2_closed" });
        public BinarySensorEntity Db2Full => new(_app, new string[] { "binary_sensor.db2_full" });
        public BinarySensorEntity Db5WaterLevelState => new(_app, new string[] { "binary_sensor.db_5_water_level_state" });
        public BinarySensorEntity Db5BucketMedium => new(_app, new string[] { "binary_sensor.db5_bucket_medium" });
        public BinarySensorEntity FreshWaterFull => new(_app, new string[] { "binary_sensor.fresh_water_full" });
        public BinarySensorEntity Station2Status => new(_app, new string[] { "binary_sensor.station_2_status" });
        public BinarySensorEntity Db4BucketEmpty => new(_app, new string[] { "binary_sensor.db4_bucket_empty" });
        public BinarySensorEntity GreenHousePowerStatus => new(_app, new string[] { "binary_sensor.green_house_power_status" });
        public BinarySensorEntity NftWaterLevelStatus2 => new(_app, new string[] { "binary_sensor.nft_water_level_status_2" });
        public BinarySensorEntity Db5BucketEmpty => new(_app, new string[] { "binary_sensor.db5_bucket_empty" });
        public BinarySensorEntity FanButton => new(_app, new string[] { "binary_sensor.fan_button" });
        public BinarySensorEntity NftResMedium => new(_app, new string[] { "binary_sensor.nft_res_medium" });
        public BinarySensorEntity Updater => new(_app, new string[] { "binary_sensor.updater" });
        public BinarySensorEntity FrontFieldStationRunning => new(_app, new string[] { "binary_sensor.front_field_station_running" });
        public BinarySensorEntity Db3WaterLevelState => new(_app, new string[] { "binary_sensor.db_3_water_level_state" });
        public BinarySensorEntity GreenHouseMain => new(_app, new string[] { "binary_sensor.green_house_main" });
        public BinarySensorEntity NftResMedium2 => new(_app, new string[] { "binary_sensor.nft_res_medium_2" });
        public BinarySensorEntity PowerStrip1ServerStatus => new(_app, new string[] { "binary_sensor.power_strip_1_server_status" });
        public BinarySensorEntity NftResFull2 => new(_app, new string[] { "binary_sensor.nft_res_full_2" });
        public BinarySensorEntity FreshWaterBucketMedium => new(_app, new string[] { "binary_sensor.fresh_water_bucket_medium" });
        public BinarySensorEntity Db6BucketMedium => new(_app, new string[] { "binary_sensor.db6_bucket_medium" });
        public BinarySensorEntity S19StationRunning => new(_app, new string[] { "binary_sensor.s19_station_running" });
        public BinarySensorEntity FreshResWaterLevelState => new(_app, new string[] { "binary_sensor.fresh_res_water_level_state" });
        public BinarySensorEntity S08StationRunning => new(_app, new string[] { "binary_sensor.s08_station_running" });
        public BinarySensorEntity MotoGStylusIsCharging => new(_app, new string[] { "binary_sensor.moto_g_stylus_is_charging" });
        public BinarySensorEntity S21StationRunning => new(_app, new string[] { "binary_sensor.s21_station_running" });
        public BinarySensorEntity TestZoneWaterBucketMedium => new(_app, new string[] { "binary_sensor.test_zone_water_bucket_medium" });
        public BinarySensorEntity ValveClosed => new(_app, new string[] { "binary_sensor.valve_closed" });
    }

    public partial class InputNumberEntities
    {
        private readonly NetDaemonRxApp _app;
        public InputNumberEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public InputNumberEntity Nutrientunitstoadd => new(_app, new string[] { "input_number.nutrientunitstoadd" });
    }

    public partial class WeatherEntities
    {
        private readonly NetDaemonRxApp _app;
        public WeatherEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public WeatherEntity Home => new(_app, new string[] { "weather.home" });
        public WeatherEntity Openweathermap => new(_app, new string[] { "weather.openweathermap" });
    }

    public partial class ZoneEntities
    {
        private readonly NetDaemonRxApp _app;
        public ZoneEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public ZoneEntity Home => new(_app, new string[] { "zone.home" });
    }

    public partial class AutomationEntities
    {
        private readonly NetDaemonRxApp _app;
        public AutomationEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public AutomationEntity Db1WateringPump => new(_app, new string[] { "automation.db1_watering_pump" });
        public AutomationEntity TurnOffGrowLight => new(_app, new string[] { "automation.turn_off_grow_light" });
        public AutomationEntity TurnOffGrowLightInTheMornining => new(_app, new string[] { "automation.turn_off_grow_light_in_the_mornining" });
        public AutomationEntity WriteStepperValueToEsp => new(_app, new string[] { "automation.write_stepper_value_to_esp" });
        public AutomationEntity Db3WateringPump => new(_app, new string[] { "automation.db3_watering_pump" });
        public AutomationEntity Db2WateringPump => new(_app, new string[] { "automation.db2_watering_pump" });
        public AutomationEntity Test => new(_app, new string[] { "automation.test" });
        public AutomationEntity Db3WateringPump2 => new(_app, new string[] { "automation.db3_watering_pump_2" });
        public AutomationEntity TurnOnGrowLightDb1And2BeforeSunset => new(_app, new string[] { "automation.turn_on_grow_light_db1_and_2_before_sunset" });
        public AutomationEntity CheckTemperatureOfDb1Reservoir => new(_app, new string[] { "automation.check_temperature_of_db1_reservoir" });
        public AutomationEntity TurnOnGrowLight => new(_app, new string[] { "automation.turn_on_grow_light" });
        public AutomationEntity HeaterTry2 => new(_app, new string[] { "automation.heater_try_2" });
        public AutomationEntity TurnOnGrowLight42 => new(_app, new string[] { "automation.turn_on_grow_light_4_2" });
        public AutomationEntity Db4WateringPump => new(_app, new string[] { "automation.db4_watering_pump" });
        public AutomationEntity TurnOnCirculatingFanEver4Hours => new(_app, new string[] { "automation.turn_on_circulating_fan_ever_4_hours" });
        public AutomationEntity TurnOnNftPump => new(_app, new string[] { "automation.turn_on_nft_pump" });
        public AutomationEntity TurnOnDb2PumpEvery4Hours => new(_app, new string[] { "automation.turn_on_db2_pump_every_4_hours" });
        public AutomationEntity Db6WateringPump => new(_app, new string[] { "automation.db6_watering_pump" });
    }

    public partial class ScriptEntities
    {
        private readonly NetDaemonRxApp _app;
        public ScriptEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public ScriptEntity MovePump3 => new(_app, new string[] { "script.move_pump_3" });
        public ScriptEntity MovePump2 => new(_app, new string[] { "script.move_pump_2" });
        public ScriptEntity MovePump1 => new(_app, new string[] { "script.move_pump_1" });
        public ScriptEntity AddDoseToCurrentZone => new(_app, new string[] { "script.add_dose_to_current_zone" });
        public ScriptEntity RefillCurrentBucket => new(_app, new string[] { "script.refill_current_bucket" });
    }

    public partial class DeviceTrackerEntities
    {
        private readonly NetDaemonRxApp _app;
        public DeviceTrackerEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public DeviceTrackerEntity MotoGStylus => new(_app, new string[] { "device_tracker.moto_g_stylus" });
    }

    public partial class SunEntities
    {
        private readonly NetDaemonRxApp _app;
        public SunEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public SunEntity Sun => new(_app, new string[] { "sun.sun" });
    }

    public partial class CameraEntities
    {
        private readonly NetDaemonRxApp _app;
        public CameraEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public CameraEntity Espcam01 => new(_app, new string[] { "camera.espcam01" });
    }

    public partial class InputSelectEntities
    {
        private readonly NetDaemonRxApp _app;
        public InputSelectEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public InputSelectEntity ReservoirRes => new(_app, new string[] { "input_select.reservoir_res" });
    }

    public partial class InputBooleanEntities
    {
        private readonly NetDaemonRxApp _app;
        public InputBooleanEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public InputBooleanEntity TestingTankHold => new(_app, new string[] { "input_boolean.testing_tank_hold" });
    }

    public partial class PersonEntities
    {
        private readonly NetDaemonRxApp _app;
        public PersonEntities(NetDaemonRxApp app)
        {
            _app = app;
        }

        public PersonEntity NickGelotte => new(_app, new string[] { "person.nick_gelotte" });
    }
}