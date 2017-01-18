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
        /// Test case for method HasEventListeners of class EventDispatcher.
        /// </summary>
        [Test]
        public void HasEventListenersByEventTypeTest()
        {
            IEventDispatcher dispatcher = new EventDispatcher();
            Action<TestEvent> TestEventHandler = (testEvent) => { };
            dispatcher.AddEventListener(TestEvent.Test, TestEventHandler);
            Assert.IsTrue(dispatcher.HasEventListeners(TestEvent.Test));
        }

        /// <summary>
        /// Test case for method HasEventListeners of class EventDispatcher.
        /// </summary>
        [Test]
        public void HasEventListenersByTargetTest()
        {
            IEventDispatcher dispatcher = new EventDispatcher();
            dispatcher.AddEventListener<TestEvent>(TestEvent.Test, ClassTestEventHandler);
            dispatcher.AddEventListener<TestEvent>(TestEvent.Test, ClassTestBEventHandler);
            Assert.IsTrue(dispatcher.HasEventListeners(this));
        }

        /// <summary>
        /// Test case for method HasAnyEventListener of class EventDispatcher.
        /// </summary>
        [Test]
        public void HasAnyEventListenerTest()
        {
            IEventDispatcher dispatcher = new EventDispatcher();
            Action<TestEvent> TestEventHandler = (testEvent) => { };
            dispatcher.AddEventListener(TestEvent.Test, TestEventHandler);
            Assert.IsTrue(dispatcher.HasAnyEventListener());
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
        /// Test case for method RemoveEventListener of class EventDispatcher.
        /// </summary>
        [Test]
        public void RemoveEventListenerByEventTypeTest()
        {
            IEventDispatcher dispatcher = new EventDispatcher();
            dispatcher.AddEventListener<TestEvent>(TestEvent.Test, ClassTestEventHandler);
            dispatcher.AddEventListener<TestEvent>(TestEvent.Test, ClassTestBEventHandler);
            dispatcher.RemoveEventListener(TestEvent.Test);
            Assert.IsFalse(dispatcher.HasEventListeners(TestEvent.Test));
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
            Assert.IsFalse(dispatcher.HasEventListeners(this));
        }

        /// <summary>
        /// Test case for method RemoveAllEventListeners of class EventDispatcher.
        /// </summary>
        [Test]
        public void RemoveAllEventListenersTest()
        {
            IEventDispatcher dispatcher = new EventDispatcher();
            dispatcher.AddEventListener<TestEvent>(TestEvent.Test, ClassTestEventHandler);
            dispatcher.AddEventListener<TestEvent>(TestEvent.TestB, ClassTestBEventHandler);
            dispatcher.RemoveAllEventListeners();
            Assert.IsFalse(dispatcher.HasAnyEventListener());
        }

        /// <summary>
        /// Method for testing.
        /// </summary>
        /// <param name="testEvent">The test event.</param>
        private void ClassTestEventHandler(TestEvent testEvent)
        {
        }

        /// <summary>
        /// Method for testing.
        /// </summary>
        /// <param name="testEvent">The test event.</param>
        private void ClassTestBEventHandler(TestEvent testEvent)
        {
        }
    }
}