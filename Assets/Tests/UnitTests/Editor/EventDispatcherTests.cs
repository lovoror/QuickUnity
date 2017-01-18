using NUnit.Framework;
using QuickUnity.Events;
using System;

namespace QuickUnity.UnitTests
{
    /// <summary>
    /// Unit test cases for class EventDispatchers.
    /// </summary>
    [TestFixture]
    [Category("EventDispatcherTests")]
    internal class EventDispatcherTests
    {
        /// <summary>
        /// Test event object for unit tests.
        /// </summary>
        /// <seealso cref="QuickUnity.Events.Event"/>
        internal class TestEvent : Event
        {
            /// <summary>
            /// The type of event for test cases.
            /// </summary>
            public const string Test = "Test";

            /// <summary>
            /// The type of event for test cases.
            /// </summary>
            public const string TestB = "TestB";

            /// <summary>
            /// Initializes a new instance of the <see cref="TestEvent"/> class.
            /// </summary>
            /// <param name="eventType">The type of event.</param>
            /// <param name="context">The context object.</param>
            public TestEvent(string eventType, object context = null)
                : base(eventType, context)
            {
            }
        }

        /// <summary>
        /// Test case for method AddEventListener of class EventDispatcher.
        /// </summary>
        [Test]
        public void AddEventListenerTest()
        {
            IEventDispatcher dispatcher = new EventDispatcher();
            Action<TestEvent> TestEventHandler = (testEvent) => { };
            dispatcher.AddEventListener(TestEvent.Test, TestEventHandler);
            Assert.IsTrue(dispatcher.HasEventListener(TestEvent.Test, TestEventHandler));
        }

        /// <summary>
        /// Test case for method DispatchEvent of class EventDispatcher.
        /// </summary>
        [Test]
        public void DispatchEventTest()
        {
            IEventDispatcher dispatcher = new EventDispatcher();
            Action<TestEvent> TestEventHandler = (testEvent) =>
            {
                Assert.IsNotNull(testEvent);
            };
            dispatcher.AddEventListener(TestEvent.Test, TestEventHandler);
            dispatcher.DispatchEvent(new TestEvent(TestEvent.Test));
        }

        /// <summary>
        /// Test case for method HasEventListener of class EventDispatcher.
        /// </summary>
        [Test]
        public void HasEventListenerTest()
        {
            IEventDispatcher dispatcher = new EventDispatcher();
            Action<TestEvent> TestEventHandler = (testEvent) => { };
            dispatcher.AddEventListener(TestEvent.Test, TestEventHandler);
            Assert.IsTrue(dispatcher.HasEventListener(TestEvent.Test, TestEventHandler));
        }

        /// <summary>
        /// Test case for method RemoveEventListener of class EventDispatcher.
        /// </summary>
        [Test]
        public void RemoveEventListenerTest()
        {
            IEventDispatcher dispatcher = new EventDispatcher();
            Action<TestEvent> TestEventHandler = (testEvent) => { };
            dispatcher.AddEventListener(TestEvent.Test, TestEventHandler);
            dispatcher.RemoveEventListener(TestEvent.Test, TestEventHandler);
            Assert.IsFalse(dispatcher.HasEventListener(TestEvent.Test, TestEventHandler));
        }

        /// <summary>
        /// Test case for method RemoveEventListeners of class EventDispatcher.
        /// </summary>
        [Test]
        public void RemoveEventListenersTest()
        {
            IEventDispatcher dispatcher = new EventDispatcher();
            dispatcher.AddEventListener<TestEvent>(TestEvent.Test, ClassTestEventHandler);
            dispatcher.AddEventListener<TestEvent>(TestEvent.TestB, ClassTestEventHandler);
            dispatcher.RemoveEventListeners(this);
            Assert.IsFalse(dispatcher.HasEventListener<TestEvent>(TestEvent.Test, ClassTestEventHandler));
        }

        private void ClassTestEventHandler(TestEvent testEvent)
        {
        }
    }
}