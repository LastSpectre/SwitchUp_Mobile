using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BA_Praxis_Library;

public class ExperienceBattleController : Singleton<ExperienceBattleController>
{
    [Header("UI Elements")]
    public Image[] m_PlayerHealthContainers;
    public TMP_Text m_EnemyHealthText;

    // public Button m_AttackButton;
    public TMP_Text m_AttackText;
    public Image m_AttackIcon;
    public Image m_LoadingGif;
    public GameObject m_SwordEffect;

    // Gameplay variables
    [Header("Gameplay Elements")]
    public float m_AttackCooldown = 0.5f;
    public int m_PlayerDamage = 50;
    [HideInInspector]
    public int m_PlayerHealth = 3;
    [HideInInspector]
    public bool m_AttackButtonPressed = false;
    [HideInInspector]
    public bool m_DefenseButtonPressed = false;
    [HideInInspector]
    public bool m_GameOver = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!m_GameOver)
        {
            // update enemy health
            m_EnemyHealthText.text = ExperienceEnemy.get.m_EnemyHealth.ToString();
            if (ExperienceEnemy.get.m_EnemyHealth <= 0)
            {
                ExperienceEnemy.get.m_EnemyAnimator.SetBool("IsDead", true);
            }

            // if player is dead
            if (m_PlayerHealth <= 0)
            {
                m_GameOver = true;

                StartMiniGame.get.m_EndGameScreen.SetActive(true);
                StartMiniGame.get.m_EndGameText.text = "You couldn't beat the Boss this time..\nTry again another time!";
            }
            // player won without getting hit
            else if (ExperienceEnemy.get.m_EnemyHealth <= 0 && m_PlayerHealth == 3)
            {
                m_GameOver = true;

                StartMiniGame.get.m_EndGameScreen.SetActive(true);
                StartMiniGame.get.m_EndGameText.text = $"Congrats! You defeated the Boss!\nYour Character earned 250 Experience!";

                UpdateMaterial.UpdateExperience(250);
            }
            // player won
            else if (ExperienceEnemy.get.m_EnemyHealth <= 0 && m_PlayerHealth > 0)
            {
                m_GameOver = true;

                StartMiniGame.get.m_EndGameScreen.SetActive(true);
                StartMiniGame.get.m_EndGameText.text = $"Congrats! You defeated the Boss!\nYour Character earned 125 Experience!";

                UpdateMaterial.UpdateExperience(125);
            }
        }
    }

    // when player presses attack button
    public void AttackButtonPressed()
    {
        if (!m_DefenseButtonPressed && !m_AttackButtonPressed)
        {
            StartCoroutine(AttackButtonPressedCoroutine());
        }
    }

    // when defense button is pressed
    public void DefenseButtonDown()
    {
        // if player is not already pressing the attack button
        if (!m_AttackButtonPressed)
            m_DefenseButtonPressed = true;
    }

    // when defense button is lifted up
    public void DefenseButtonUp()
    {
        m_DefenseButtonPressed = false;
    }

    // handles player attack
    IEnumerator AttackButtonPressedCoroutine()
    {
        // set attack button to be pressed, and activate loading gif
        m_AttackButtonPressed = true;
        m_AttackText.gameObject.SetActive(false);
        m_AttackIcon.gameObject.SetActive(false);
        m_LoadingGif.gameObject.SetActive(true);
        m_SwordEffect.SetActive(true);

        ExperienceEnemy.get.m_EnemyHealth -= m_PlayerDamage;

        yield return new WaitForSeconds(m_AttackCooldown);

        m_AttackButtonPressed = false;
        m_AttackText.gameObject.SetActive(true);
        m_AttackIcon.gameObject.SetActive(true);
        m_LoadingGif.gameObject.SetActive(false);

        yield break;
    }    
}
