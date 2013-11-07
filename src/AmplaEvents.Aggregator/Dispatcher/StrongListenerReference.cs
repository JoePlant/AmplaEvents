namespace AmplaEvents.Aggregator.Dispatcher
{
    internal class StrongListenerReference : ITargetReference
    {
        private readonly object target;

        public StrongListenerReference(object target)
        {
            this.target = target;
        }

        public object Target
        {
            get { return target; }
        }
    }
}