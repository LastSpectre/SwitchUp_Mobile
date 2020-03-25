using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExperienceEnemy : Singleton<ExperienceEnemy>
{
    public int m_EnemyHealth = 1000;
    [HideInInspector]
    public Animator m_EnemyAnimator;
    public Vector2 m_AttackTimer = new Vector2(0, 0);
    public AudioClip m_HitSound;
    public float m_HitSoundVolume;

    private bool m_calculateNewTimer = true;

    // Start is called before the first frame update
    void Start()
    {
        m_EnemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_calculateNewTimer && StartMiniGame.get.m_StartGame && !ExperienceBattleController.get.m_GameOver)
        {
            StartCoroutine(CalcEnemyAttack());
        }
    }

    // enemy attacks
    public void EnemyAttack()
    {
        // if player is not defending and still has hp left
        if (!ExperienceBattleController.get.m_DefenseButtonPressed && ExperienceBattleController.get.m_PlayerHealth > 0)
        {
            ExperienceBattleController.get.m_PlayerHealth--;

            ExperienceBattleController.get.m_PlayerHealthContainers[ExperienceBattleController.get.m_PlayerHealth].gameObject.SetActive(false);

        }
        m_calculateNewTimer = true;
    }

    // enemy dies
    public void EnemyDeath()
    {
        Destroy(gameObject);
    }

    IEnumerator CalcEnemyAttack()
    {
        // stop queueing new attacks
        m_calculateNewTimer = false;

        // calculate time for next attack
        float attackTimer = Random.Range(m_AttackTimer.x, m_AttackTimer.y);

        yield return new WaitForSeconds(attackTimer);

        m_EnemyAnimator.SetTrigger("Attack");

        yield break;
    }

    public void PlayHitSound()
    {
        SoundEffectScript.get.m_AudioSource.PlayOneShot(m_HitSound, m_HitSoundVolume);
    }
}
