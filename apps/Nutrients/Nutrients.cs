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
    public class Nutrients : NetDaemonRxApp
    {


        public override void Initialize()
        {
            GhProcedures gh = new GhProcedures(this);
            LogInformation("Nutrients is Starting");
        }

        [HomeAssistantServiceCall]
        public async Task<bool> AddOneDoseToCurrentZone(dynamic data)
        {
            GhProcedures gh = new GhProcedures(this);
            await gh.AddNutrientsToCurrentZone();
            return true;

        }

        [HomeAssistantServiceCall]
        public async Task<bool> AddNutrient(dynamic data)
        {
            int pumpNumber = 0;
            int doses = 0;
            GhProcedures gh = new GhProcedures(this);
            try
            {
                pumpNumber = (int)(double)data.pumpNumber;
                doses = (int)(double)data.doses;
            }
            catch (Exception)
            {
                LogError($"Could not extract pumpNumber and Doses from data. Data is {data}");
            }
            if (pumpNumber > 0 && doses > 0)
            {
                await gh.AddNutrients(pumpNumber, doses);
            }

            return true;

        }



    }
}