using QuickUnity.Events;
using QuickUnity.Tasks;
using System.Collections;
using UnityEngine;

namespace QuickUnity.Tests.IntegrationTests
{
    /// <summary>
    /// Sample for testing CoroutineTask.
    /// </summary>
    public class CoroutineTaskSample : EventDispatcher
    {
        /// <summary>
        /// The task object.
        /// </summary>
        private ICoroutineTask m_task;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoroutineTaskSample"/> class.
        /// </summary>
        public CoroutineTaskSample()
            : base()
        {
        }

        /// <summary>
        /// Begins to do something.
        /// </summary>
        public void Begin()
        {
            m_task = new CoroutineTask(CoroutineFunc());
        }

        /// <summary>
        /// Coroutine.
        /// </summary>
        /// <returns>The enumerator object.</returns>
        private IEnumerator CoroutineFunc()
        {
            yield return new WaitForSeconds(2f);

            m_task.Pause();
            DispatchEvent(new CoroutineTaskTestEvent(CoroutineTaskTestEvent.CoroutineExecutePaused, m_task));
            yield return null;

            DispatchEvent(new CoroutineTaskTestEvent(CoroutineTaskTestEvent.CoroutineExecuteComplete, m_task));
        }
    }
}