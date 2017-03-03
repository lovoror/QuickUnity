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

using QuickUnity.Events;
using UnityEngine;

namespace QuickUnity.Audio
{
    /// <summary>
    /// Apply event dispatcher for AudioSource component.
    /// </summary>
    /// <seealso cref="QuickUnity.Events.BehaviourEventDispatcher"/>
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceEventDispatcher : BehaviourEventDispatcher
    {
        /// <summary>
        /// The AudioSource component.
        /// </summary>
        protected AudioSource m_audioSource;

        /// <summary>
        /// Gets the AudioSource component.
        /// </summary>
        /// <value>The AudioSource component.</value>
        public AudioSource audioSource
        {
            get
            {
                if (!m_audioSource)
                {
                    m_audioSource = GetComponent<AudioSource>();
                }

                return m_audioSource;
            }
        }

        /// <summary>
        /// This function is called when the MonoBehaviour will be destroyed.
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();

            CancelInvoke();
        }

        /// <summary>
        /// Play the audio source.
        /// </summary>
        public void PlayAudio()
        {
            if (audioSource && audioSource.clip)
            {
                audioSource.Play();
                Invoke("OnPlayComplete", audioSource.clip.length);
            }
        }

        /// <summary>
        /// Plays the audio by setting audio clip.
        /// </summary>
        /// <param name="clip">The audio clip.</param>
        public void PlayAudio(AudioClip clip)
        {
            if (audioSource && clip)
            {
                audioSource.clip = clip;
                audioSource.Play();
                Invoke("OnPlayComplete", clip.length);
            }
        }

        /// <summary>
        /// Called when the AudioSource component [play complete].
        /// </summary>
        private void OnPlayComplete()
        {
            DispatchEvent(new AudioSourceEvent(AudioSourceEvent.PlayComplete, this));
        }
    }
}