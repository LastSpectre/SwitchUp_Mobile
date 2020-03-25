using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectScript : Singleton<SoundEffectScript>
{
    public AudioSource m_AudioSource;
    public AudioClip m_ButtonClick;
    public float m_ClickVolume;

    // Plays the button sound whenever a button is pressed
    public void PlayButtonSound()
    {
        m_AudioSource.PlayOneShot(m_ButtonClick, m_ClickVolume);
    }
}
