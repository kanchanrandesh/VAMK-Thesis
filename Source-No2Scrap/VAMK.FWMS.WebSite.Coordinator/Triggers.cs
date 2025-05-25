using System;
using Quartz;

namespace VAMK.FWMS.WebSite
{
    internal class Triggers
    {
        internal static ITrigger TimeTrigger()
        {
            //Runs Event 20 Second
            return TriggerBuilder.Create()
                   .WithIdentity("trigger1", "trigger1")
                   .StartNow().WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(30)
                        .RepeatForever())
                    .Build();

            //For Testing
            //return TriggerBuilder.Create()
            //       .WithIdentity("trigger1", "trigger1")
            //       .StartNow().WithSimpleSchedule(x => x
            //            .WithIntervalInSeconds(1))
            //        .Build();
        }
    }
}