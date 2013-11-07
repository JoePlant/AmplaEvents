using System;
using System.Collections.Generic;
using System.Reflection;

namespace AmplaEvents.Aggregator.Dispatcher
{
    public class HandleMethodWrapper : IMethodHandler
    {
        private readonly Type listenerInterface;
        private readonly Type registeredMessageType;
        private readonly MethodInfo handlerMethod;
        private readonly bool supportMessageInheritance;
        private readonly Dictionary<Type, bool> supportedMessageTypes = new Dictionary<Type, bool>();

        public HandleMethodWrapper(MethodInfo handlerMethod, Type listenerInterface, Type registeredMessageType, bool supportMessageInheritance)
        {
            this.handlerMethod = handlerMethod;
            this.listenerInterface = listenerInterface;
            this.registeredMessageType = registeredMessageType;
            this.supportMessageInheritance = supportMessageInheritance;
            supportedMessageTypes[registeredMessageType] = true;
        }

        public bool Handles<TMessage>() where TMessage : class
        {
            return listenerInterface == typeof(IListener<TMessage>);
        }

        public bool HandlesMessage(object message)
        {
            if (message == null)
            {
                return false;
            }

            bool canHandle;
            Type messageType = message.GetType();
            bool previousMessageType = supportedMessageTypes.TryGetValue(messageType, out canHandle);
            if (!previousMessageType && supportMessageInheritance)
            {
                canHandle = TypeHelper.IsAssignableFrom(registeredMessageType, messageType);
                supportedMessageTypes[messageType] = canHandle;
            }
            return canHandle;
        }

        public bool TryHandle<TMessage>(object target, object message) where TMessage : class
        {
            bool wasHandled = false;
            if (target != null)
            {
                if (Handles<TMessage>() || HandlesMessage(message))
                {
                    handlerMethod.Invoke(target, new[] {message});
                    wasHandled = true;
                }
            }
            return wasHandled;
        }
    }
}