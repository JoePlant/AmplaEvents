namespace AmplaEvents.Aggregator.Dispatcher
{
    public interface IMethodHandler
    {
        bool TryHandle<TListener>(object target, object message) where TListener : class;
    }
}