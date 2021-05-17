namespace Events
{
    public abstract class Event<T> where T : Event<T>
    {
        private bool HasFired;
        public delegate void EventListener(T eventInfo);
        public static event EventListener Listeners;

        public static void RegisterListener(EventListener listener)
        {
            Listeners += listener;
        }

        public static void DeregisterListener(EventListener listener)
        {
            Listeners -= listener;
        }

        public void Fire()
        {
            if (HasFired)
            {
                // TODO: Throw exception?
                return;
            }
            HasFired = true;

            if (Listeners == null)
            {
                // Nobody was listening.  Just return.
                return;
            }

            Listeners(this as T);
        }
    }
}