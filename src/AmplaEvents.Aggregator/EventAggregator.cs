using System;
using AmplaEvents.Aggregator.Dispatcher;

namespace AmplaEvents.Aggregator
{
    public class EventAggregator : IEventAggregator
    {
        private readonly ListenerWrapperCollection listeners;
        private readonly IDispatcher dispatcher;

        public EventAggregator()
        {
            listeners = new ListenerWrapperCollection();
            dispatcher = new SimpleDispatcher(listeners);
        }

        public void Publish<TMessage>(TMessage message) where TMessage : class
        {
            dispatcher.TryDispatch<TMessage>(message);
        }

        public void Publish<TMessage>() where TMessage : class, new()
        {
            TMessage message = new TMessage();
            dispatcher.TryDispatch<TMessage>(message);
        }

        public IEventSubscriptionManager AddListener(object listener)
        {
            if (listener == null) throw new ArgumentNullException("listener");

            listeners.AddListener(listener, true, false);

            return this;
        }

        public IEventSubscriptionManager AddListener<T>(IListener<T> listener) where T : class
        {
            object listenerObject = listener;
            return AddListener(listenerObject);
        }

        public IEventSubscriptionManager RemoveListener(object listener)
        {
            listeners.RemoveListener(listener);
            return this;
        }
    }
    
}