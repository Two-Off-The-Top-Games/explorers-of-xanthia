using System.Collections.Generic;

namespace Events
{
    /// <summary>
    /// You should NOT be using this class directly.
    /// This will require you to supply your own implementation details for firing events.
    /// Use the other event classes in this namespace as they have all of the necessary implementation details for each use case.
    /// </summary>
    public abstract class BaseEvent
    {
        private bool _hasFired = false;

        protected abstract void FireDelegates();
        public void Fire()
        {
            if (_hasFired)
            {
                // TODO: Throw exception?
                return;
            }
            _hasFired = true;

            FireDelegates();
        }
    }

    public abstract class GlobalEvent<T> : BaseEvent where T : GlobalEvent<T>
    {
        public delegate void GlobalEventDelegate(T eventInfo);
        public static event GlobalEventDelegate Listeners;

        public static void RegisterListener(GlobalEventDelegate eventListener)
        {
            Listeners += eventListener;
        }

        public static void DeregisterListener(GlobalEventDelegate eventListener)
        {
            Listeners -= eventListener;
        }

        protected override void FireDelegates()
        {
            if (Listeners == null)
            {
                // Nobody was listening.  Just return.
                return;
            }

            Listeners(this as T);
        }
    }

    /// <summary>
    /// You only need to call the base constructor with the target instance id for the event.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TargetEvent<T> : BaseEvent where T : TargetEvent<T>
    {
        private int _targetInstanceId;

        public TargetEvent(int targetInstanceId)
        {
            _targetInstanceId = targetInstanceId;
        }

        public delegate void EventListener(T eventInfo);
        private static readonly Dictionary<int, EventListener> s_delegates = new Dictionary<int, EventListener>();

        public static void RegisterListener(int delegateId, EventListener eventListener)
        {
            s_delegates.Add(delegateId, eventListener);
        }

        public static void DeregisterListener(int delegateId)
        {
            s_delegates.Remove(delegateId);
        }

        protected override void FireDelegates()
        {
            if (s_delegates.Count == 0)
            {
                // Nobody was listening.  Just return.
                return;
            }

            if (s_delegates.TryGetValue(_targetInstanceId, out var eventListener))
            {
                eventListener(this as T);
            }
        }
    }

    /// <summary>
    /// You only need to call the base constructor with the source instance id for the event.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SourceEvent<T> : BaseEvent where T : SourceEvent<T>
    {
        private int _sourceInstanceId;

        public SourceEvent(int sourceInstanceId)
        {
            _sourceInstanceId = sourceInstanceId;
        }

        public delegate void EventListener(T eventInfo);
        private static readonly List<KeyValuePair<int, EventListener>> s_delegates = new List<KeyValuePair<int, EventListener>>();

        public static void RegisterListener(int delegateId, EventListener eventListener)
        {
            s_delegates.Add(new KeyValuePair<int, EventListener>(delegateId, eventListener));
        }

        public static void DeregisterListener(int delegateId, EventListener eventListener)
        {
            s_delegates.Remove(new KeyValuePair<int, EventListener>(delegateId, eventListener));
        }

        protected override void FireDelegates()
        {
            if (s_delegates.Count == 0)
            {
                // Nobody was listening.  Just return.
                return;
            }

            for (int i = 0; i < s_delegates.Count; i++)
            {
                if (s_delegates[i].Key == _sourceInstanceId)
                {
                    s_delegates[i].Value(this as T);
                }
            }
        }
    }
}