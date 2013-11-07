using System;

namespace AmplaEvents.Aggregator.Dispatcher
{
    internal class WeakListenerReference : ITargetReference
    {
        private readonly WeakReference weakReference;

        public WeakListenerReference(object listener)
        {
            weakReference = new WeakReference(listener);
        }

        public object Target
        {
            get { return weakReference.Target; }
        }
    }
}