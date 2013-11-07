
namespace AmplaEvents.Aggregator
{
    /// <summary>
    /// Provides a way to add and remove a listener object from the EventAggregator
    /// </summary>
    public interface IEventSubscriptionManager
    {
        /// <summary>
        /// Adds the given listener object to the EventAggregator.
        /// </summary>
        /// <param name="listener">Object that should be implementing IListener(of T's), this overload is used when your listeners to multiple message types</param>
        /// <returns>Returns the current IEventSubscriptionManager to allow for easy fluent additions.</returns>
        IEventSubscriptionManager AddListener(object listener);

        /// <summary>
        /// Adds the given listener object to the EventAggregator.
        /// </summary>
        /// <typeparam name="T">Listener Message type</typeparam>
        /// <param name="listener"></param>
        /// <returns>Returns the current IEventSubscriptionManager to allow for easy fluent additions.</returns>
        IEventSubscriptionManager AddListener<T>(IListener<T> listener) where T : class;

        /// <summary>
        /// Removes the listener object from the EventAggregator
        /// </summary>
        /// <param name="listener">The object to be removed</param>
        /// <returns>Returnes the current IEventSubscriptionManager for fluent removals.</returns>
        IEventSubscriptionManager RemoveListener(object listener);
    }
}