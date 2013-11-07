using AmplaEvents.Aggregator;
using AmplaEvents.RecordChanged.Downtime;

namespace AmplaEvents.Listeners.Downtime
{
    public class DowntimeListener : ModuleListener, IListener<DowntimeRecord>
    {
        public void Handle(DowntimeRecord message)
        {
        }
    }
}