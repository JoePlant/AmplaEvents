namespace AmplaEvents.Aggregator
{
    /// <summary>
    ///     Listener interface to listen to the message
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IListener<in TMessage>
    {
        /// <summary>
        /// When listening to the message the handler is called.
        /// </summary>
        void Handle(TMessage message);
    }
}