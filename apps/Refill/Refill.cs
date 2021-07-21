using System;
using System.Reactive.Linq;
using NetDaemon.Common.Reactive;
using NetDaemon.Generated.Reactive;
using System.Threading.Tasks;
using NetDaemon.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
            LogInformation("Refill started and is ready for a callback");
        }

        [HomeAssistantServiceCall]
        public async Task RefillCurrentZone(dynamic data)
        {

            GhProcedures gh = new GhProcedures(this);
            await gh.RefillCurrentReservior();

        }


        [HomeAssistantServiceCall]
        public async Task RefillWaterTank(dynamic data)
        {
            GhProcedures gh = new GhProcedures(this);
            await gh.RefillMainWaterTank();
        }


        [HomeAssistantServiceCall]
        public async Task RefillSwpCooler(dynamic data)
        {
            GhProcedures gh = new GhProcedures(this);
            await gh.RefillSwampCooler();

        }

        [HomeAssistantServiceCall]
        public async Task RunDumpRutineForCurrentZone(dynamic data)
        {
            GhProcedures gh = new GhProcedures(this);
            await gh.RunOneTankEmptyRunForCurrentZone();

        }


    }
}

