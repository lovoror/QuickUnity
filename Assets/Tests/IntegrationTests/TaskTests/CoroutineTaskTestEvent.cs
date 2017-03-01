namespace QuickUnity.Tests.IntegrationTests
{
    /// <summary>
    /// Event for CoroutineTaskTest.
    /// </summary>
    /// <seealso cref="QuickUnity.Events.Event"/>
    public class CoroutineTaskTestEvent : Events.Event
    {
        /// <summary>
        /// The coroutine execute paused.
        /// </summary>
        public const string CoroutineExecutePaused = "CoroutineExecutePaused";

        /// <summary>
        /// Dispatched whenever coroutine execute complete.
        /// </summary>
        public const string CoroutineExecuteComplete = "CoroutineExecuteComplete";

        /// <summary>
        /// Initializes a new instance of the <see cref="CoroutineTaskTestEvent"/> class.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="context">The context object.</param>
        public CoroutineTaskTestEvent(string eventType, object context = null)
            : base(eventType, context)
        {
        }
    }
}