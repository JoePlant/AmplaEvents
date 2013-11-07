using System;
using NUnit.Framework;

namespace AmplaEvents.Aggregator.Dispatcher
{
    [TestFixture]
    public class ListenerWrapperUnitTests : TestFixture
    {
        public class DifferentMessage
        {
        }

        public class Message
        {
        }

        public class InheritedMessage : Message
        {
        }

        public class MessageListener : IListener<Message>
        {
            public Message Message { get; private set; }

            public void Handle(Message message)
            {
                Message = message;
            }
        }

        [Test]
        public void ListenerWithoutMessageInheritance()
        {
            MessageListener listener = new MessageListener();

            bool removed = false;

            Action<ListenerWrapper> remove = (m) =>
                {
                    removed = true;
                };

            ListenerWrapper wrapper = new ListenerWrapper(listener, remove, true, false);

            Assert.That(wrapper, Is.Not.Null);
            Assert.That(wrapper.Count, Is.EqualTo(1));
            Assert.That(removed, Is.False);

            Assert.That(wrapper.TryDispatch<Message>(new Message()), Is.True);
            Assert.That(wrapper.TryDispatch<Message>(new InheritedMessage()), Is.True);
            Assert.That(wrapper.TryDispatch<InheritedMessage>(new InheritedMessage()), Is.False);

            Assert.That(wrapper.TryDispatch<DifferentMessage>(new DifferentMessage()), Is.False);

            Assert.That(wrapper.ListenerInstance, Is.EqualTo(listener));
        }

        [Test]
        public void ListenerWithMessageInheritance()
        {
            MessageListener listener = new MessageListener();

            bool removed = false;

            Action<ListenerWrapper> remove = (m) =>
            {
                removed = true;
            };

            ListenerWrapper wrapper = new ListenerWrapper(listener, remove, true, true);

            Assert.That(wrapper, Is.Not.Null);
            Assert.That(wrapper.Count, Is.EqualTo(1));
            Assert.That(removed, Is.False);


            Assert.That(wrapper.TryDispatch<Message>(new Message()), Is.True);
            Assert.That(wrapper.TryDispatch<Message>(new InheritedMessage()), Is.True);
            Assert.That(wrapper.TryDispatch<InheritedMessage>(new InheritedMessage()), Is.True);

            Assert.That(wrapper.TryDispatch<DifferentMessage>(new DifferentMessage()), Is.False);

            Assert.That(wrapper.ListenerInstance, Is.EqualTo(listener));
        }
    }
}