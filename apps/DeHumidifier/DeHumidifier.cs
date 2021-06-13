using System.Threading.Tasks;
using NetDaemon.Common.Reactive;
using System.Collections.Generic;
using Netdaemon.Generated.Reactive;
using NetDaemon.Common.Reactive.Services;
using System;

// Use unique namespaces for your apps if you going to share with others to avoid
// conflicting names
namespace Greenhouse
{
    /// <summary>
    ///     The NetDaemonApp implements async model API
    ///     This API are deprecated please use the Rx one!
    /// </summary>
    public class DeHumidifierApp : NetDaemonRxApp
    {
        public SensorEntity? humiditySensor { get; set; }
        public SwitchEntity? humidifier { get; set; }
        public override void Initialize()
        {

             //var humidifier = new SwitchEntities(this).Tb4p2;
             //var humiditySensor = (new SensorEntities(this)).GreenhouseInternalHumidity;
            this.RunEvery(TimeSpan.FromMinutes(5), () =>
            {
                if (humiditySensor?.State?.GetType() == typeof(System.Int64))
                {
                    Int64 humidity = humiditySensor.State;
                    if (humidity > 80)
                    {
                        if (humidifier.IsOff())
                        {
                            this.LogInformation("Humiditiy is " + humidity.ToString());
                            humidifier.TurnOn();
                            this.LogInformation("Turned on DeHumidifier");
                        }
                    }
                    else
                    {
                        if (humidifier.IsOn())
                        {
                            this.LogInformation("Humiditiy is " + humidity.ToString());
                            humidifier.TurnOff();
                            this.LogInformation("Turned off DeHumidifier");
                        }
                    }


                }
                else
                {
                    this.LogInformation($"GreenhouseInternalHumidity is not in a valid state. Its current value is {(humiditySensor?.State ?? "Unknown")}");
                }

            });

        }
    }
}

