using System;
using System.Reactive.Linq;
using NetDaemon.Common.Reactive;
using Netdaemon.Generated.Reactive;
using System.Threading.Tasks;
using NetDaemon.Common;
using System.Collections.Generic;
using System.Linq;

namespace Greenhouse
{
    public class EnvControls : NetDaemonRxApp
    {

        private GhConfig _ghConfig = default!;
        //private GhZone _currentZone;
        private GHMain _ghMain = default!;
        public double? FanOnTemp { get; set; }
        public double? FanOffTemp { get; set; }


        public override void Initialize()
        {
            _ghConfig = new GhConfig(this);
            _ghMain = _ghConfig.GhMain();
            if (FanOnTemp < FanOffTemp)
            {
                LogError($"Fan Off Temp must be lower then Fan On Temp. Fan on temp is {FanOnTemp}. Fan Off temp is {FanOffTemp}");
            }
            LogInformation("EnvControls is Starting");
            RunEvery(TimeSpan.FromMinutes(5), () =>
            {
                if (_ghMain.InternalTemp > FanOnTemp)
                {
                    if (_ghMain.MainFan.IsOff())
                    {
                        LogInformation($"Temp is {_ghMain.InternalTemp} -  Turning on the main Greenhouse Fan");
                        _ghMain.MainFan.TurnOn();
                    }

                    if (_ghMain.InternalTemp > FanOnTemp + 10)
                    {
                        LogInformation($"Temp is {_ghMain.InternalTemp} -  Turning on the swamp cooler");
                        _ghMain.SwampCooler.TurnOn();
                    }
                }
                if (_ghMain.InternalTemp < FanOffTemp)
                {
                    if (_ghMain.MainFan.IsOn())
                    {
                        LogInformation($"Temp is {_ghMain.InternalTemp} -  Turning off the main Greenhouse Fan");
                        _ghMain.MainFan.TurnOff();
                    }
                    if (_ghMain.InternalTemp < FanOffTemp + 10)
                    {
                        LogInformation($"Temp is {_ghMain.InternalTemp} -  Turning off the swamp cooler");
                        _ghMain.SwampCooler.TurnOff();
                    }
                }
            });
        }



    }
}