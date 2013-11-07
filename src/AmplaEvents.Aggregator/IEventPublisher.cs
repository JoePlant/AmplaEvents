
namespace AmplaEvents.Aggregator
{
    /// <summary>
    /// Interface used to publish messages
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// Publishes the specified message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        void Publish<TMessage>(TMessage message) where TMessage : class;

        /// <summary>
        /// Publishes the message
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        void Publish<TMessage>() where TMessage : class, new();
    }
}