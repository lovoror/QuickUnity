using QuickUnity.Events;

namespace QuickUnity.Examples.Events
{
    /// <summary>
    /// The example event object.
    /// </summary>
    /// <seealso cref="QuickUnity.Events.Event" />
    public class ExampleEvent : Event
    {
        /// <summary>
        /// The example event type.
        /// </summary>
        public const string Example = "Example";

        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleEvent"/> class.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="context">The context object.</param>
        public ExampleEvent(string eventType, object context = null)
            :base(eventType, context)
        {

        }
    }
}