using System;
using System.Reactive.Linq;
using NetDaemon.Common.Reactive;
using NetDaemon.Generated.Reactive;
using System.Threading.Tasks;
using NetDaemon.Common;
using System.Collections.Generic;
using System.Linq;

namespace Greenhouse
{
    public class Nutrients : NetDaemonRxApp
    {
        private bool HoldCurrentZoneIsRunning { get; set; } = false;

        public override void Initialize()
        {
            GhProcedures gh = new GhProcedures(this);
            LogInformation("Nutrients is Starting");
            InputBooleanEntities ibe = new(this);
            ibe.TestingTankHold.StateChanges.Where(t => t.New.State == "on").Subscribe(async e =>
            {
                if (HoldCurrentZoneIsRunning == false)
                {
                    HoldCurrentZoneIsRunning = true;
                    bool completedSuccesfully = await gh.HoldCurrentZone();
                    LogInformation($"Hold currentZone completed with the completedSuccesfully set to {completedSuccesfully}");
                    HoldCurrentZoneIsRunning = false;
                }
            });
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