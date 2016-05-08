using UnityEngine;

namespace QuickUnity.Tests.IntegrationTests
{
    /// <summary>
    /// IntegrationTest for class DispatchableMonoBehaviour.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour"/>
    [IntegrationTest.DynamicTestAttribute("DispatchableMonoBehaviourTest")]
    [IntegrationTest.SucceedWithAssertions]
    public class DispatchableMonoBehaviourTest : MonoBehaviour
    {
        /// <summary>
        /// The name of DispatcherMock object.
        /// </summary>
        private const string k_mockObjectName = "DispatcherMock";

        /// <summary>
        /// Start is called just before any of the Update methods is called the first time.
        /// </summary>
        private void Start()
        {
            DispatchableMonoBehaviourTester mock = GameObject.FindObjectOfType<DispatchableMonoBehaviourTester>();

            if (mock)
            {
                mock.AddEventListener<TestEvent>(TestEvent.Test, MockEventTestHandler);
            }
            else
            {
                IntegrationTest.Fail(gameObject, "Can not find DispatchMock component!");
            }
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            DispatchableMonoBehaviourTester mock = GameObject.FindObjectOfType<DispatchableMonoBehaviourTester>();

            if (mock)
                IntegrationTest.Assert(gameObject, mock.HasEventListener<TestEvent>(TestEvent.Test, MockEventTestHandler));
        }

        private void OnDestroy()
        {
            DispatchableMonoBehaviourTester mock = GameObject.FindObjectOfType<DispatchableMonoBehaviourTester>();

            if (mock)
                mock.RemoveEventListener<TestEvent>(TestEvent.Test, MockEventTestHandler);
        }

        /// <summary>
        /// Mocks the event test eventType handler.
        /// </summary>
        /// <param name="mockEvent">The mock event.</param>
        private void MockEventTestHandler(TestEvent mockEvent)
        {
            IntegrationTest.Pass(gameObject);
        }
    }
}