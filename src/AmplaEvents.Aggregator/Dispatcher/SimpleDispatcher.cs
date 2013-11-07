using System.Linq;

namespace AmplaEvents.Aggregator.Dispatcher
{
    public class SimpleDispatcher : IDispatcher
    {
        private readonly ListenerWrapperCollection listeners;

        public SimpleDispatcher(ListenerWrapperCollection listeners)
        {
            this.listeners = listeners;
        }

        public bool TryDispatch<TListener>(object message) where TListener : class
        {
            bool called = false;
            foreach (ISelectiveDispatcher o in listeners.Where(o => o.Handles<TListener>() || o.HandlesMessage(message)))
            {
                called |= o.TryDispatch<TListener>(message);
            }
            return called;
        }
    }
}