using QuickUnity.Patterns;

namespace QuickUnity.Examples.Singleton
{
    /// <summary>
    /// Class TestBehaviourSingletonOne.
    /// </summary>
    public class TestSingletonMonoBehaviourOne : MonoBehaviourSingleton<TestSingletonMonoBehaviourOne>
    {
        private TestSingletonOne m_testOne;

        protected override void Awake()
        {
            base.Awake();
            m_testOne = TestSingletonOne.instance;
        }

        public void Run()
        {
            m_testOne.Run();
        }
    }
}