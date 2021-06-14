using System;
using System.Reactive.Linq;
using NetDaemon.Common.Reactive;
using System.Threading;
using NetDaemon.Common;
using System.Threading.Tasks;
using NetDaemon.Common.Reactive.Services;
using System.Collections.Generic;
using Netdaemon.Generated.Reactive;

// Use unique namespaces for your apps if you going to share with others to avoid
// conflicting names
namespace Greenhouse
{

    public static class GreenhouseExtensions
    {

        public static bool IsOn(this BinarySensorEntity bse)
        {
            return (bse?.State ?? "Unknown") == "on";
        }
        public static bool IsOff(this BinarySensorEntity bse)
        {
            return (bse?.State ?? "Unknown") == "off";
        }

        public static bool IsUnknown(this BinarySensorEntity bse)
        {
            return bse.State == null;
        }
        public static bool IsUnknown(this SensorEntity bse)
        {
            return bse.State == null;
        }

        public static bool IsOn(this SwitchEntity se)
        {
            return (se?.State ?? "Unknown") == "on";
        }
        public static bool IsOff(this SwitchEntity se)
        {
            return (se?.State ?? "Unknown") == "off";
        }
        public static bool IsUnknown(this SwitchEntity se)
        {
            return se.State == null;
        }

        public static IObservable<(EntityState Old, EntityState New)> FirstOrTimeout(this IObservable<(EntityState Old, EntityState New)> observable, TimeSpan timeout)
        {
              return observable.Timeout(timeout, Observable.Return((new NetDaemon.Common.EntityState() { State = "TimeOut" }, new NetDaemon.Common.EntityState() { State = "TimeOut" }))).Take(1);
       }

        public static async Task<bool> DelayFor(this NetDaemonRxApp app, TimeSpan timeSpan)
        {
            var tcs = new TaskCompletionSource<bool>();
            app.RunIn(timeSpan, () =>
                {
                    tcs.SetResult(true);
                });
            await tcs.Task;
            return true;
        }


    }

}

// namespace NetDaemon.Common.Reactive.Services
// {
    
//         public partial class BinarySensorEntity : RxEntityBase
//     {
//         public BinarySensorEntity(INetDaemonRxApp daemon, IEnumerable<string> entityIds) : base(daemon, entityIds)
//         {
//         }
//         public bool IsOn => State == "on";
//         public bool IsOff => State == "off";
//         public bool IsUnknown => State == null;
//     }

//     public partial class SwitchEntity : RxEntityBase
//     {
//         public SwitchEntity(INetDaemonRxApp daemon, IEnumerable<string> entityIds) : base(daemon, entityIds)
//         {
//         }
//         public bool IsOn => State == "on";

//         public bool IsOff => State == "off";
//         public bool IsUnknown => State == null;
//     }
// }