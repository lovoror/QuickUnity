using QuickUnity.Events;
using UnityEngine;

namespace QuickUnity.Examples.Events
{
    /// <summary>
    /// Event dispatcher object.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class Dispatcher : MonoBehaviour
    {
        /// <summary>
        /// Notifies the specified event.
        /// </summary>
        /// <param name="dispatcher">The event dispatcher.</param>
        public void Notify(IEventDispatcher dispatcher)
        {
            dispatcher.DispatchEvent(new ExampleEvent(ExampleEvent.Example));
        }
    }
}