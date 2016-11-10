using QuickUnity.Timers;
using UnityEngine;

namespace QuickUnity.Tests.IntegrationTests
{
    /// <summary>
    /// Integration test of TimerGroup.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour"/>
    [IntegrationTest.DynamicTestAttribute("TimerTests")]
    [IntegrationTest.SucceedWithAssertions]
    public class TimerGroupTest : MonoBehaviour
    {
        private ITimer m_skillACDTimer;

        private ITimer m_skillBCDTimer;

        private ITimerGroup m_skillCDTimerGroup;

        /// <summary>
        /// Start is called just before any of the Update methods is called the first time.
        /// </summary>
        private void Start()
        {
            m_skillACDTimer = new Timer(1, 3, true, true, false);
            m_skillACDTimer.AddEventListener<TimerEvent>(TimerEvent.Timer, OnSkillACDTimer);
            m_skillBCDTimer = new Timer(1, 4, true, true, false);
            m_skillBCDTimer.AddEventListener<TimerEvent>(TimerEvent.Timer, OnSkillBCDTimer);
            m_skillCDTimerGroup = new TimerGroup("SkillCDTimers", true, m_skillACDTimer, m_skillBCDTimer);
            m_skillCDTimerGroup.AddEventListener<TimerGroupEvent>(TimerGroupEvent.TimerGroupReset, OnSkillTimersReset);
            TimerManager.instance.AddTimerGroup(m_skillCDTimerGroup);
            Invoke("ResetAllSkills", 2.5f);
        }

        /// <summary>
        /// Called when [destroy].
        /// </summary>
        private void OnDestroy()
        {
            if (m_skillACDTimer != null)
            {
                m_skillACDTimer.RemoveEventListener<TimerEvent>(TimerEvent.Timer, OnSkillACDTimer);
                m_skillACDTimer = null;
            }

            if (m_skillBCDTimer != null)
            {
                m_skillBCDTimer.RemoveEventListener<TimerEvent>(TimerEvent.Timer, OnSkillBCDTimer);
                m_skillBCDTimer = null;
            }

            if (m_skillCDTimerGroup != null)
            {
                TimerManager.instance.RemoveTimerGroup(m_skillCDTimerGroup);
                m_skillCDTimerGroup.RemoveEventListener<TimerGroupEvent>(TimerGroupEvent.TimerGroupReset, OnSkillTimersReset);
                m_skillCDTimerGroup = null;
            }
        }

        /// <summary>
        /// Resets all skills.
        /// </summary>
        private void ResetAllSkills()
        {
            m_skillCDTimerGroup.Reset();
        }

        private void OnSkillACDTimer(TimerEvent timerEvent)
        {
            Debug.LogFormat("Skill A is cooling down: {0}!", m_skillACDTimer.currentCount);
        }

        private void OnSkillBCDTimer(TimerEvent timerEvent)
        {
            Debug.LogFormat("Skill B is cooling down: {0}!", m_skillBCDTimer.currentCount);
        }

        /// <summary>
        /// Called when [skill timers reset].
        /// </summary>
        /// <param name="timerGroupEvent">The timer group event.</param>
        private void OnSkillTimersReset(TimerGroupEvent timerGroupEvent)
        {
            Debug.Log("All skills are ready!");
            IntegrationTest.Pass(gameObject);
        }
    }
}