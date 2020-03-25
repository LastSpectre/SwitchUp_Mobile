using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public AudioClip m_SwordAttackSound;
    public float m_SwordSoundVolume;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 
    public void DeactivateSwordEffect()
    {
        gameObject.SetActive(false);
    }

    public void PlayHitSound()
    {
        SoundEffectScript.get.m_AudioSource.PlayOneShot(m_SwordAttackSound, m_SwordSoundVolume);
    }
}
