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
using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Apply effects for AudioSource component.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioSourceEffects : MonoBehaviourBase
{
    /// <summary>
    /// The AudioSource component.
    /// </summary>
    protected AudioSource m_audioSource;

    /// <summary>
    /// Called when script receive message Start.
    /// </summary>
    protected override void OnStart()
    {
        base.OnStart();

        m_audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        StopAllCoroutines();

        if (m_audioSource)
        {
            m_audioSource.Stop();
        }
    }

    /// <summary>
    /// This can be used in place of "play" when it is desired to fade in the sound over time.
    /// </summary>
    /// <param name="fadeInDuration">Duration of the fade in.</param>
    /// <param name="fadeVolume">The fade volume.</param>
    /// <param name="startPosition">The start position.</param>
    /// <param name="completeCallback">The complete callback function.</param>
    public void FadeIn(float fadeInDuration = 0.0f, float fadeVolume = 1.0f, float startPosition = 0.0f, Action completeCallback = null)
    {
        if (m_audioSource)
        {
            m_audioSource.volume = 0.0f;
            m_audioSource.time = startPosition;
            m_audioSource.Play();

            if (fadeInDuration <= 0.0f)
            {
                m_audioSource.volume = fadeVolume;
            }
            else
            {
                StartCoroutine(ApplyFadeIn(fadeInDuration, fadeVolume, completeCallback));
            }
        }
    }

    /// <summary>
    /// This is used in place of "stop" when it is desired to fade the volume of the sound before stopping.
    /// </summary>
    /// <param name="fadeOutDuration">Duration of the fade out.</param>
    /// <param name="fadeVolume">The fade volume.</param>
    /// <param name="completeCallback">The complete callback function.</param>
    public void FadeOut(float fadeOutDuration = 0.0f, float fadeVolume = 0.0f, Action completeCallback = null)
    {
        if (m_audioSource)
        {
            if (fadeOutDuration <= 0.0f)
            {
                m_audioSource.volume = fadeVolume;
            }
            else
            {
                StartCoroutine(ApplyFadeOut(fadeOutDuration, fadeVolume, completeCallback));
            }
        }
    }

    /// <summary>
    /// Applies the fade in effect.
    /// </summary>
    /// <param name="fadeInDuration">Duration of the fade in.</param>
    /// <param name="fadeVolume">The fade volume.</param>
    /// <param name="completeCallback">The complete callback function.</param>
    /// <returns>The enumerator of this coroutine.</returns>
    private IEnumerator ApplyFadeIn(float fadeInDuration = 0.0f, float fadeVolume = 1.0f, Action completeCallback = null)
    {
        float startVolume = m_audioSource.volume;

        while (m_audioSource.volume < fadeVolume)
        {
            m_audioSource.volume += startVolume * Time.deltaTime / fadeInDuration;
            yield return null;
        }

        m_audioSource.volume = fadeVolume;

        if (completeCallback != null)
        {
            completeCallback.Invoke();
        }
    }

    /// <summary>
    /// Applies the fade out effect.
    /// </summary>
    /// <param name="fadeOutDuration">Duration of the fade out.</param>
    /// <param name="fadeVolume">The fade volume.</param>
    /// <param name="completeCallback">The complete callback function.</param>
    /// <returns>The enumerator of this coroutine.</returns>
    private IEnumerator ApplyFadeOut(float fadeOutDuration = 0.0f, float fadeVolume = 0.0f, Action completeCallback = null)
    {
        float startVolume = m_audioSource.volume;

        while (m_audioSource.volume > fadeVolume)
        {
            m_audioSource.volume -= startVolume * Time.deltaTime / fadeOutDuration;
            yield return null;
        }

        m_audioSource.volume = fadeVolume;
        m_audioSource.Stop();

        if (completeCallback != null)
        {
            completeCallback.Invoke();
        }
    }
}