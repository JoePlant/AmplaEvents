namespace AmplaEvents.Aggregator.Dispatcher
{
    public interface ISelectiveDispatcher : IDispatcher
    {
        /// <summary>
        ///     Does the Dispatcher handle the TListener?
        /// </summary>
        /// <typeparam name="TListener">The type of the listener.</typeparam>
        /// <returns></returns>
        bool Handles<TListener>() where TListener : class;

        /// <summary>
        ///     Does the Dispatcher handle the message?
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        bool HandlesMessage(object message);
    }
}