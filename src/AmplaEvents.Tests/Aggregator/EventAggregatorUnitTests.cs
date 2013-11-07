using System;
using NUnit.Framework;

namespace AmplaEvents.Aggregator
{
    [TestFixture]
    public class EventAggregatorUnitTests : TestFixture
    {
        private IEventAggregator eventAggregator;

        public class Message
        {
            
        }

        public class EmptyMessageListener : IListener<Message>
        {
            public void Handle(Message message)
            {
            }
        }

        public class MessageListener : IListener<Message>
        {
            public Message Handled { get; private set; }

            public void Handle(Message message)
            {
                Handled = message;
            }
        }

        protected override void OnSetUp()
        {
            base.OnSetUp();
            eventAggregator = new EventAggregator();
        }

        protected override void OnTearDown()
        {
            eventAggregator = null;
            base.OnTearDown();
        }

        [Test]
        public void Register_null()
        {
            Assert.Throws<ArgumentNullException>(() => eventAggregator.AddListener<object>(null));
        }

        [Test]
        public void Register()
        {
            eventAggregator.AddListener(new EmptyMessageListener());
        }

        [Test]
        public void Unregister_null()
        {
            Assert.DoesNotThrow(() => eventAggregator.RemoveListener(null));
        }

        [Test]
        public void RegisterAndUnRegisterThenPublish()
        {
            MessageListener handler = new MessageListener();

            eventAggregator.AddListener(handler);
            eventAggregator.RemoveListener(handler);

            eventAggregator.Publish(new Message());

            Assert.That(handler.Handled, Is.Null);
        }

        [Test]
        public void PublishTMessage()
        {
            MessageListener listener = new MessageListener();
            eventAggregator.AddListener(listener);

            eventAggregator.Publish<Message>();
            Assert.That(listener.Handled, Is.Not.Null);
        }

        [Test]
        public void Publish_WithNoHandlers()
        {
            eventAggregator.Publish(new Message());
        }

        [Test]
        public void PublishWithHandler()
        {
            MessageListener listener = new MessageListener();
            eventAggregator.AddListener(listener);
            
            eventAggregator.Publish(new Message());

            Assert.That(listener.Handled, Is.Not.Null);
        }

        [Test]
        public void PublishWithMultipleHandlers()
        {
            MessageListener listener1 = new MessageListener();
            MessageListener listener2 = new MessageListener(); 
            eventAggregator.AddListener(listener1).AddListener(listener2);

            eventAggregator.Publish(new Message());

            Assert.That(listener1.Handled, Is.Not.Null);
            Assert.That(listener2.Handled, Is.Not.Null);
        }
    }
}