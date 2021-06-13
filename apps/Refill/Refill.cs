using System;
using System.Reactive.Linq;
using NetDaemon.Common.Reactive;
using Netdaemon.Generated.Reactive;
using System.Threading.Tasks;
using NetDaemon.Common;
using System.Collections.Generic;
using System.Linq;

// Use unique namespaces for your apps if you going to share with others to avoid
// conflicting names
namespace Greenhouse
{
    /// <summary>
    ///     The NetDaemonApp implements async model API
    ///     This API are deprecated please use the Rx one!
    /// </summary>
    public class Refill : NetDaemonRxApp
    {
        public GhZone? CurrentZone { get; set; }
        public GHMain? GHMain { get; set; }

        public override async ValueTask DisposeAsync()
        {
            GhProcedures gh = new GhProcedures(this);
            gh.MakeSureEverythingisOff();

        }
        public override void Initialize()
        {

            LogInformation("Refill started and is ready for a callback");
            // SwitchEntities sw = new SwitchEntities(this);
            // sw.PowerStrip1Outlet3
            // .StateChanges
            // .Where(e => e.New?.State == "on")
            // .Subscribe(async t =>
            // {
            //     this.LogDebug($"PowerStrip1Outlet3 was turned on");
                //     LogInformation("Starting Run");
                //     //GhProcedures gh = new GhProcedures(this);
                //     //bool result = await gh.RefillReservior(6);

                //     //double average = await gh.CreateAverage(2, true);
                //     //this.LogDebug($"The average time is {average}");
                //     //gh.TimeMainWaterPump();

                //     // var currentRun = sw.St1p2.StateChanges.Where(t => t.New.State == "on").FirstOrTimeout(TimeSpan.FromSeconds(5))
                //     // .Subscribe(t =>
                //     // {
                //     //     sw.St1p3.TurnOn();
                //     //     if (t.Item1.State == "TimeOut")
                //     //     {
                //     //         LogDebug("Time Out Happened - But still turning on the Test Switch");
                //     //     }
                //     //     else
                //     //     {
                //     //         LogDebug("Turned the test switch on");
                //     //     }

                //     // });



            // }
            //         );
        }
        [HomeAssistantServiceCall]
        public async Task RefillCurrentZone(dynamic data)
        {

            GhProcedures gh = new GhProcedures(this);
            await gh.RefillCurrentReservior();

        }

        private bool ValidateEntities(GhZone zone)
        {
            List<string> entities = new List<string>();
            return true;

        }

    }
}

