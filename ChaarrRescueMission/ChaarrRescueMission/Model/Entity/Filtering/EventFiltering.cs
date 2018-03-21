using ChaarrRescueMission.Properties;
using System.Collections.Generic;
using System.Linq;

namespace ChaarrRescueMission.Model.Entity.Filtering
{
    class EventFiltering
    {
        /// <summary>
        /// Filters events lists and deletes from them unnessecery events.
        /// </summary>
        public static IEnumerable<string> Filter(IEnumerable<string> list)
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
                ).Reverse().ToList();
        }
    }
}
