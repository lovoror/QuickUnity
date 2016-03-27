using QuickUnity.Utilities;
using UnityEngine;

namespace QuickUnity.Example.Utilities
{
    /// <summary>
    /// Example for class LogUtility.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class LogUtilityExample : MonoBehaviour
    {
        #region Messages

        /// <summary>
        /// Awakes.
        /// </summary>
        private void Awake()
        {
            LogUtility.Log("TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest");
            LogUtility.LogWarning("WarningWarningWarningWarningWarningWarningWarning");
            LogUtility.LogError("ErrorErrorErrorErrorErrorErrorErrorErrorError");
            LogUtility.Assert(1 == 2, "WTF?");
        }

        #endregion Messages
    }
}