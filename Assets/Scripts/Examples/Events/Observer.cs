using QuickUnity.Events;
using UnityEngine;

namespace QuickUnity.Examples.Events
{
    /// <summary>
    /// Observer object.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class Observer : MonoBehaviour
    {
        /// <summary>
        /// The event dispatcher.
        /// </summary>
        private IEventDispatcher m_eventDispatcher;

        /// <summary>
        /// Follows the specified event.
        /// </summary>
        /// <param name="dispatcher">The event dispatcher.</param>
        public void Follow(IEventDispatcher dispatcher)
        {
            m_eventDispatcher = dispatcher;
            dispatcher.AddEventListener<ExampleEvent>(ExampleEvent.Example, OnExampleFired);
        }

        /// <summary>
        /// Called when [example event fired].
        /// </summary>
        /// <param name="evnt">The event object.</param>
        private void OnExampleFired(ExampleEvent evnt)
        {
            m_eventDispatcher.RemoveEventListener<ExampleEvent>(ExampleEvent.Example, OnExampleFired);
            Debug.Log("The Example event was fired!");
        }
    }
}