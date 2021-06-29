using System;
using System.Reactive.Linq;
using NetDaemon.Common.Reactive;
using System.Threading.Tasks;
using NetDaemon.Common;
using System.Collections.Generic;
using System.Linq;
using NetDaemon.Generated.Reactive;

namespace Greenhouse
{
    public class PirLights : NetDaemonRxApp
    {
        public double? SunElevation { get; set; }
        public IEnumerable<string>? Lights { get; init;}
        public IEnumerable<string>? PirSensors { get; set; }
        public int LeaveLightsOnForSeconds { get; set; }

        public override void Initialize()
        {
            LogInformation("Pir App Initializing");
            SunEntities sunEntities = new SunEntities(this);
            if (Lights != null && PirSensors != null && SunElevation != null)
            {
                LogInformation("Pir App is running");

                Entities(PirSensors).StateChanges
                        .Where(e => e.New?.State == "on")
                        .Subscribe(e =>
                        {
                            if (sunEntities.Sun.Attribute != null)
                            {
                                if (sunEntities.Sun.Attribute.elevation < SunElevation)
                                {
                                    Entities(Lights).TurnOn();
                                }
                                else
                                {
                                    LogInformation($"Did not turn on the lights because the current sun elevation is {sunEntities.Sun.Attribute.elevation} which is more then {SunElevation}");
                                }
                            }
                            else
                            {
                                LogInformation($"Sun entities is null. Nothing is going to work.");
                            }
                        });



                Entities(PirSensors).StateChanges
                    .Where(e =>
                        (e.New?.State == "off" &&
                        e.Old?.State == "on") || (e.New?.State == "on" &&
                        e.Old?.State == "off")
                    )
                    .NDSameStateFor(TimeSpan.FromSeconds(LeaveLightsOnForSeconds))
                    .Subscribe(e =>
                    {
                        if (e.New.State == "off")
                        {
                            LogInformation("Turning Lights Off");
                            Entities(Lights).TurnOff();
                        }
                    });
            }
            else
            {
                LogInformation("Pir App is not running");
                if (Lights == null)
                {
                    LogInformation("Lights is null pulling from the yaml so the automation could not start.");
                }
                if (PirSensors == null)
                {
                    LogInformation("PirSensors is null pulling from the yaml so the automation could not start.");
                }
                if (SunElevation == null)
                {
                    LogInformation("ElevationEvening is null pulling from the yaml so the automation could not start.");
                }

            }


        }
    }

}
