using ChaarrRescueMission.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaarrRescueMission.Model.Entity.EntityFiltering
{
    class EventFiltering
    {
        public static IList<string> Filter(IList<string> list)
        {
            return list.Where(Event =>
                    Event != Resources.CaptionEventExpeditionEnergyChanged &&
                    Event != Resources.CaptionEventExpeditionMatterChanged &&
                    Event != Resources.CaptionEventPołudnicaEnergyChanged &&
                    Event != Resources.CaptionEventPołudnicaMatterChanged &&
                    Event != Resources.CaptionEventChaarrHatredChanged &&
                    Event != Resources.CaptionEventKnowledgeChanged &&
                    Event != Resources.CaptionEventCrewDeathsChanged &&
                    Event != Resources.CaptionEventSurvivorDeathsChanged &&
                    Event != Resources.CaptionEventUpkeepTime
                ).ToList();
        }
    }
}
