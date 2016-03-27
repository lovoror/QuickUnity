using QuickUnity.Events;

namespace QuickUnity.Examples.Event
{
    /// <summary>
    /// Class EventExample.
    /// </summary>
    public class EventExample : MonoBehaviourEventDispatcher
    {
        /// <summary>
        /// The event for test
        /// </summary>
        public const string TestEvent = "test";

        /// <summary>
        /// Starts this sInstance.
        /// </summary>
        private void Start()
        {
            Invoke("Dispatch", 2.0f);
        }

        /// <summary>
        /// Dispatches this sInstance.
        /// </summary>
        private void Dispatch()
        {
            DispatchEvent(new Events.Event(TestEvent));
        }
    }
}