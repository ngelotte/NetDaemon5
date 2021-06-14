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

        public IEnumerable<string>? ActiveReservoirs { get; set; }


        public override async ValueTask DisposeAsync()
        {
            GhProcedures gh = new GhProcedures(this);
            gh.MakeSureEverythingisOff();
            LogInformation("Made sure everythign is off");

        }
        public override void Initialize()
        {
            GhProcedures gh = new GhProcedures(this);
            LogInformation("Refill started and is ready for a callback");
            RunIn(TimeSpan.FromSeconds(20), async () =>
            {

                await gh.RefillAllReserviors(ActiveReservoirs.ToList());
            });
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

