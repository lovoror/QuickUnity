using QuickUnity.Events;

namespace QuickUnity.Tests.IntegrationTests
{
    /// <summary>
    /// Class MockEvent.
    /// </summary>
    /// <seealso cref="QuickUnity.Events.Event"/>
    public class MockEvent : Event
    {
        /// <summary>
        /// The test event type.
        /// </summary>
        public const string Test = "Test";

        /// <summary>
        /// Initializes a new instance of the <see cref="MockEvent"/> class.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="context">The context.</param>
        public MockEvent(string eventType, object context = null)
            : base(eventType, context)
        {
        }
    }
}