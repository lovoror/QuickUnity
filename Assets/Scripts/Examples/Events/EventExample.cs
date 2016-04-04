using UnityEngine;
using QuickUnity.Events;

namespace QuickUnity.Examples.Events
{
    /// <summary>
    /// Example for showing how to use class EventDispatcher.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class EventExample : MonoBehaviour
    {
        /// <summary>
        /// The dispatcher object.
        /// </summary>
        private IEventDispatcher m_dispatcher;

        /// <summary>
        /// Awakes.
        /// </summary>
        private void Awake()
        {
            m_dispatcher = new EventDispatcher();
        }

        /// <summary>
        /// Starts to run.
        /// </summary>
        private void Start()
        {
            Dispatcher dispatcher = GetComponentInChildren<Dispatcher>();
            Observer observer = GetComponentInChildren<Observer>();
            observer.Follow(m_dispatcher);
            dispatcher.Notify(m_dispatcher);
        }
    }
}
