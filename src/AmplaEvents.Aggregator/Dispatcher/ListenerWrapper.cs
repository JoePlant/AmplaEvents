using System;
using System.Collections.Generic;
using System.Linq;

namespace AmplaEvents.Aggregator.Dispatcher
{
    public class ListenerWrapper : ISelectiveDispatcher
    {
        private const string handleMethodName = "Handle";

        private readonly Action<ListenerWrapper> onRemoveCallback;
        private readonly List<HandleMethodWrapper> handlers = new List<HandleMethodWrapper>();
        private readonly ITargetReference reference;

        public ListenerWrapper(object listener, Action<ListenerWrapper> onRemoveCallback, bool holdReferences, bool supportMessageInheritance)
        {
            this.onRemoveCallback = onRemoveCallback;

            if (holdReferences)
                reference = new StrongListenerReference(listener);
            else
                reference = new WeakListenerReference(listener);

            var listenerInterfaces = TypeHelper.GetBaseInterfaceType(listener.GetType())
                                               .Where(w => TypeHelper.DirectlyClosesGeneric(w, typeof(IListener<>)));
           
            foreach (var listenerInterface in listenerInterfaces)
            {
                var messageType = TypeHelper.GetFirstGenericType(listenerInterface);
                var handleMethod = TypeHelper.GetMethod(listenerInterface, handleMethodName);

                HandleMethodWrapper handler = new HandleMethodWrapper(handleMethod, listenerInterface, messageType, supportMessageInheritance);
                handlers.Add(handler);
            }
        }

        public object ListenerInstance
        {
            get { return reference.Target; }
        }

        public bool Handles<TMessage>() where TMessage : class
        {
            return handlers.Aggregate(false, (current, handler) => current | handler.Handles<TMessage>());
        }

        public bool HandlesMessage(object message)
        {
            return message != null && handlers.Aggregate(false, (current, handler) => current | handler.HandlesMessage(message));
        }

        public int Count
        {
            get { return handlers.Count; }
        }

        public bool TryDispatch<TListener>(object message) where TListener : class
        {
            var target = reference.Target;
            if (target == null)
            {
                onRemoveCallback(this);
                return false;
            }

            return handlers.Aggregate(false,
                (current, handler) => current | handler.TryHandle<TListener>(target, message));
        }
    }
}