using QuickUnity.Tasks;
using System.Collections;
using UnityEngine;

namespace QuickUnity.Tests.IntegrationTests
{
    /// <summary>
    /// Integration test of coroutine task.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour"/>
    [IntegrationTest.DynamicTest("TaskTests")]
    [IntegrationTest.SucceedWithAssertions]
    [IntegrationTest.Timeout(10)]
    public class CoroutineTaskTest : MonoBehaviour
    {
        /// <summary>
        /// Start is called just before any of the Update methods is called the first time.
        /// </summary>
        private void Start()
        {
            CoroutineTaskSample sample = new CoroutineTaskSample();

            sample.AddEventListener<CoroutineTaskTestEvent>(CoroutineTaskTestEvent.CoroutineExecutePaused, (testEvent) =>
            {
                Debug.Log("Task is paused!");

                ICoroutineTask task = testEvent.context as ICoroutineTask;

                if (task != null)
                {
                    StartCoroutine(ResumeTask(task));
                }
            });

            sample.AddEventListener<CoroutineTaskTestEvent>(CoroutineTaskTestEvent.CoroutineExecuteComplete, (testEvent) =>
            {
                Debug.Log("Task is complete!");

                IntegrationTest.Pass(gameObject);
            });

            Debug.Log("Task start!");
            sample.Begin();
        }

        private IEnumerator ResumeTask(ICoroutineTask task)
        {
            yield return new WaitForSeconds(2f);
            Debug.Log("Task resume!");
            task.Resume();
        }
    }
}