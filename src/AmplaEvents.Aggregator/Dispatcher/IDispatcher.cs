namespace AmplaEvents.Aggregator.Dispatcher
{
    public interface IDispatcher
    {
        /// <summary>
        ///     Attempts to Dispatch the message to the listern
        /// </summary>
        /// <typeparam name="TListener">The type of the listener.</typeparam>
        /// <param name="message">The message.</param>
        /// <returns>
        ///     Was the message dispatched to a listener?
        /// </returns>
        bool TryDispatch<TListener>(object message) where TListener : class;
    }
}