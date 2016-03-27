/*
 *	The MIT License (MIT)
 *
 *	Copyright (c) 2016 Jerry Lee
 *
 *	Permission is hereby granted, free of charge, to any person obtaining a copy
 *	of this software and associated documentation files (the "Software"), to deal
 *	in the Software without restriction, including without limitation the rights
 *	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *	copies of the Software, and to permit persons to whom the Software is
 *	furnished to do so, subject to the following conditions:
 *
 *	The above copyright notice and this permission notice shall be included in all
 *	copies or substantial portions of the Software.
 *
 *	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *	SOFTWARE.
 */

using QuickUnity;
using QuickUnity.Patterns;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ConditionalAttribute = System.Diagnostics.ConditionalAttribute;

namespace QuickUnity.Utilities
{
    /// <summary>
    /// Collection of logging methods for different severity of logs.
    /// Debug logs only occur in DEBUG mode.
    /// </summary>
    public static class LogUtility
    {
        /// <summary>
        /// Derived exception class allowing us to override the stacktrace,
        /// and jump to the actual assert etc in unity editor.
        /// </summary>
        /// <seealso cref="System.Exception" />
        public sealed class OverrideException : Exception
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="OverrideException"/> class.
            /// </summary>
            /// <param name="message">The message.</param>
            public OverrideException(string message)
                : base(message, null)
            {
            }

            /// <summary>
            /// Gets the stack trace.
            /// </summary>
            /// <value>
            /// The stack trace.
            /// </value>
            public override string StackTrace
            {
                get
                {
                    string stacktrace = base.StackTrace;

                    if (!string.IsNullOrEmpty(stacktrace) && stacktrace.IndexOf('\n') != -1)
                        stacktrace = stacktrace.Substring(stacktrace.IndexOf('\n'));

                    return stacktrace;
                }
            }
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        [ConditionalAttribute("DEBUG")]
        public static void Log(string message)
        {
            Debug.Log(message);
            LogToScreen("<color=black><b>[Info]: </b>" + message + "</color>");
        }

        /// <summary>
        /// Logs the warning message.
        /// </summary>
        /// <param name="message">The message.</param>
        [ConditionalAttribute("DEBUG")]
        public static void LogWarning(string message)
        {
            Debug.LogWarning(message);
            LogToScreen("<color=yellow><b>[Warning]: </b>" + message + "</color>");
        }

        /// <summary>
        /// Logs the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        [ConditionalAttribute("DEBUG")]
        public static void LogError(string message)
        {
            Debug.LogError(message);
            LogToScreen("<color=red><b>[Warning]: </b>" + message + "</color>");
        }

        /// <summary>
        /// If the condition is false then an error is thrown and execution
        /// stopped. Used for issues that cannot be recovered from or that
        /// highlights issues in usage. Called in DEBUG mode only.
        /// </summary>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="message">The message.</param>
        /// <exception cref="QuickUnity.Utilities.LogUtility.OverrideException"></exception>
        [ConditionalAttribute("DEBUG")]
        public static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                OverrideException exception = new OverrideException(message);
                LogToScreen("<color=red><b>[Exception]: </b>" + exception.Message + "\n" + exception.StackTrace + "</color>");
                Debug.Assert(condition, message);
            }
        }

        /// <summary>
        /// Logs message to screen.
        /// </summary>
        /// <param name="message">The message.</param>
        [ConditionalAttribute("DEBUG")]
        private static void LogToScreen(string message)
        {
            UIScreenLog view = UIScreenLog.instance;

            if (view)
                view.Log(message);
        }
    }

    /// <summary>
    /// UI for displaying debug messages on screen.
    /// </summary>
    /// <seealso cref="QuickUnity.Patterns.MonoBehaviourSingleton{QuickUnity.Utilities.UIScreenLog}" />
    public sealed class UIScreenLog : MonoBehaviourSingleton<UIScreenLog>
    {
        /// <summary>
        /// The UI screen log view.
        /// </summary>
        private GameObject m_uiScreenLogView;

        /// <summary>
        /// The text field.
        /// </summary>
        private Text m_textField;

        /// <summary>
        /// The clear button.
        /// </summary>
        private Button m_clearButton;

        /// <summary>
        /// The close button.
        /// </summary>
        private Button m_closeButton;

        #region Message Functions

        /// <summary>
        /// Update per frame.
        /// </summary>
        private void Update()
        {
            if (Input.touchCount > 2 || Input.GetKeyUp(KeyCode.Escape))
            {
                if (m_uiScreenLogView.activeSelf)
                    HideLogView();
                else
                    ShowLogView();
            }
        }

        #endregion Message Functions

        #region Public Functions

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            if (!EventSystem.current)
                CreateEventSystem();

            if (!m_uiScreenLogView)
                CreateView();

            if (m_textField)
                m_textField.text += message + "\n";
        }

        /// <summary>
        /// Shows the log view.
        /// </summary>
        public void ShowLogView()
        {
            if (m_uiScreenLogView)
                m_uiScreenLogView.SetActive(true);
        }

        /// <summary>
        /// Hides the log view.
        /// </summary>
        public void HideLogView()
        {
            if (m_uiScreenLogView)
                m_uiScreenLogView.SetActive(false);
        }

        #endregion Public Functions

        #region Private Functions

        /// <summary>
        /// Creates the event system GameObject.
        /// </summary>
        private void CreateEventSystem()
        {
            UnityEngine.Object obj = Resources.Load("UI/EventSystem");
            Instantiate(obj);
        }

        /// <summary>
        /// Creates the screen log view GameObject.
        /// </summary>
        private void CreateView()
        {
            UnityEngine.Object obj = Resources.Load("UI/UIScreenLog");
            m_uiScreenLogView = Instantiate(obj) as GameObject;
            m_textField = m_uiScreenLogView.GetComponentInChildrenByName<Text>("TextField");
            m_clearButton = m_uiScreenLogView.GetComponentInChildrenByName<Button>("ClearButton");

            if (m_clearButton)
                m_clearButton.onClick.AddListener(OnClearButtonClick);

            m_closeButton = m_uiScreenLogView.GetComponentInChildrenByName<Button>("CloseButton");

            if (m_closeButton)
                m_closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        /// <summary>
        /// Called when [clear button click].
        /// </summary>
        private void OnClearButtonClick()
        {
            if (m_textField)
                m_textField.text = "";
        }

        /// <summary>
        /// Called when [close button click].
        /// </summary>
        private void OnCloseButtonClick()
        {
            HideLogView();
        }

        #endregion Private Functions
    }
}