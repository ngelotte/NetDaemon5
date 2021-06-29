using System;
using System.Reactive.Linq;
using NetDaemon.Common.Reactive;
using System.Collections.Generic;
using NetDaemon.Generated.Reactive;

namespace Greenhouse
{
    public class GrowLightApp : NetDaemonRxApp
    {
        public double? ElevationEvening { get; set; }
        public double? ElevationMorning { get; set; }
        public string? NightEndTime { get; set; }
        public string? MorningStartTime { get; set; }
        public IEnumerable<string>? GrowLights { get; set; }
        public override void Initialize()
        {
            InitNightTime(this);
            InitTurnOnLightsInTheMorning(this);
        }

        private void InitNightTime(NetDaemonRxApp app)
        {
            if (NightEndTime != null && ElevationEvening != null && GrowLights != null)
            {
                SunEntities sunEntities = new SunEntities(app);
                sunEntities.Sun
                    .StateAllChanges
                    .Where(e =>
                        e.New?.Attribute?.elevation <= ElevationEvening &&
                        e.New?.Attribute?.rising == false &&
                        e.Old?.Attribute?.elevation > ElevationEvening
                        )
                    .Subscribe(s =>
                    {
                        DateTime currentTime = DateTime.Now;
                        DateTime todaysEndTime = currentTime.Date.Add(TimeSpan.Parse(NightEndTime));
                        if (todaysEndTime >= DateTime.Now.AddMinutes(1))
                        {
                            LogInformation($"Turning on the Lights at {DateTime.Now}");
                            Entities(GrowLights).TurnOn();
                        }
                        else
                        {
                            LogInformation($"Skipping turning on the Lights because it is {currentTime} but Night End Time is {todaysEndTime}");
                        }
                    });
            }
            if (NightEndTime != null && GrowLights != null)
            {
                app.RunDaily(NightEndTime, () =>
                                {
                                    Entities(GrowLights).TurnOff();
                                    LogInformation($"Turning off the Lights at {DateTime.Now}");
                                });
            }


        }

        private void InitTurnOnLightsInTheMorning(NetDaemonRxApp app)
        {
            SunEntities sunEntities = new SunEntities(app);
            if (MorningStartTime != null && ElevationMorning != null && GrowLights != null)
            {                app.RunDaily(MorningStartTime, () =>
                                {
                                    if (sunEntities?.Sun?.Attribute != null)
                                    {
                                        if (sunEntities.Sun.Attribute.elevation < ElevationMorning)
                                        {
                                            LogInformation($"Turning on the Lights at {DateTime.Now}");
                                            Entities(GrowLights).TurnOn();

                                        }
                                        else
                                        {
                                            LogInformation($"Skipping turning on the Lights because the sun is at  {sunEntities.Sun.Attribute.elevation} but it needs to be below {ElevationMorning}");
                                        }

                                    }
                                    else
                                    {
                                        LogInformation($"Sun attributes is null. So not turning on the lights because we dont know where the sun is.");
                                    }

                                }
                        );


            }
            //
            if (MorningStartTime != null && ElevationMorning != null && GrowLights != null)
            {
                sunEntities.Sun
                            .StateAllChanges
                            .Where(e =>
                                e.New?.Attribute?.elevation >= ElevationMorning &&
                                e.New?.Attribute?.rising == true &&
                                e.Old?.Attribute?.elevation < ElevationMorning
                                )
                            .Subscribe(s =>
                            {
                                Entities(GrowLights).TurnOff();
                                LogInformation($"Turning off the Lights at {DateTime.Now}");
                            }

                        );
            }


        }
    }
}

