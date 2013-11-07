using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AmplaEvents.Aggregator.Dispatcher
{
    /// <summary>
    /// Wrapper collection of ListenerWrappers to manage things like 
    /// threadsafe manipulation to the collection, and convenience 
    /// methods to configure the collection
    /// </summary>
    public class ListenerWrapperCollection : IEnumerable<ISelectiveDispatcher>
    {
        private readonly List<ListenerWrapper> listeners = new List<ListenerWrapper>();
        private readonly object syncLock = new object();

        public void RemoveListener(object listener)
        {
            ListenerWrapper listenerWrapper;
            lock (syncLock)
                if (TryGetListenerWrapperByListener(listener, out listenerWrapper))
                    listeners.Remove(listenerWrapper);
        }

        private void RemoveListenerWrapper(ListenerWrapper listenerWrapper)
        {
            lock (syncLock)
                listeners.Remove(listenerWrapper);
        }

        public IEnumerator<ISelectiveDispatcher> GetEnumerator()
        {
            lock (syncLock)
                return listeners.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool ContainsListener(object listener)
        {
            ListenerWrapper listenerWrapper;
            return TryGetListenerWrapperByListener(listener, out listenerWrapper);
        }

        private bool TryGetListenerWrapperByListener(object listener, out ListenerWrapper listenerWrapper)
        {
            lock (syncLock)
                listenerWrapper = listeners.SingleOrDefault(x => x.ListenerInstance == listener);

            return listenerWrapper != null;
        }

        public void AddListener(object listener, bool holdStrongReference, bool supportMessageInheritance)
        {
            lock (syncLock)
            {
                if (ContainsListener(listener))
                    return;

                var listenerWrapper = new ListenerWrapper(listener, RemoveListenerWrapper, holdStrongReference, supportMessageInheritance);
                if (listenerWrapper.Count == 0)
                    throw new ArgumentException("IListener<T> is not implemented", "listener");
                listeners.Add(listenerWrapper);
            }
        }

    }
}