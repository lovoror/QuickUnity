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

using QuickUnity.Patterns;

namespace QuickUnity.GameFramework
{
    /// <summary>
    /// GameInstance: high-level manager object for an instance of the running game. Spawned at game
    /// creation and not destroyed until game instance is shut down. Running as a standalone game,
    /// there will be one of these.
    /// </summary>
    /// <seealso cref="QuickUnity.Patterns.SingletonBehaviour{QuickUnity.GameFramework.GameInstance}"/>
    public abstract class GameInstance : SingletonBehaviour<GameInstance>
    {
        /// <summary>
        /// The application pause count number.
        /// </summary>
        private uint m_applicationPauseCount = 0;

        /// <summary>
        /// The GameObject instance.
        /// </summary>
        protected GameWorld m_gameWorld;

        /// <summary>
        /// Gets the GameWorld instance.
        /// </summary>
        /// <value>The GameWorld instance.</value>
        public GameWorld GameWorld
        {
            get
            {
                return m_gameWorld;
            }
        }

        /// <summary>
        /// Initializes this game.
        /// </summary>
        protected virtual void Initialize()
        {
        }

        /// <summary>
        /// Sent to all game objects when the player pauses.
        /// </summary>
        /// <param name="pauseStatus">if set to <c>true</c> [pause status].</param>
        private void OnApplicationPause(bool pauseStatus)
        {
            m_applicationPauseCount++;

            if (m_applicationPauseCount == 1)
            {
                // Dispatch game start event.
                Initialize();
                DispatchEvent(new GameEvent(GameEvent.GameStart));
            }
        }

        /// <summary>
        /// Sent to all game objects before the application is quit.
        /// </summary>
        private void OnApplicationQuit()
        {
            // Dispatch game end event.
            DispatchEvent(new GameEvent(GameEvent.GameEnd));
        }
    }
}