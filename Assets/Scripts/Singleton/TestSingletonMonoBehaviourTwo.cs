using QuickUnity.Patterns;
using UnityEngine;

namespace QuickUnity.Examples.Singleton
{
    /// <summary>
    /// Class TestBehaviourSingletonTwo.
    /// </summary>
    public class TestSingletonMonoBehaviourTwo : MonoBehaviourSingleton<TestSingletonMonoBehaviourTwo>
    {
        private TestSingletonTwo m_testTwo;

        protected override void Awake()
        {
            base.Awake();
            m_testTwo = TestSingletonTwo.instance;
        }

        public void Run()
        {
            m_testTwo.Run();
        }
    }
}