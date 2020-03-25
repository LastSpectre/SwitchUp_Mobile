using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterUpgradeElement : Singleton<CharacterUpgradeElement>
{
    [Header("UI Elements")]
    public Image m_MaterialNeededIcon;
    public TMP_Text m_MaterialNeededText;
    public TMP_Text m_ExpNeededText;
    public Button m_UpgradeButton;
    public Image m_ErrorBanner;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCharacterUpgradeUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCharacterUpgradeUI()
    {
        // choose correct sprite for selected character
        m_MaterialNeededIcon.sprite = UIController.get.m_LastSelectedCharacter.m_UpgradeSprite;

        int tmpEXP = UIController.get.m_LastSelectedCharacter.ExperienceForNextUpgrade();

        // if character cant be upgraded anymore
        if (tmpEXP == -1)
        {
            m_MaterialNeededText.text = " Max upgraded";
            m_ExpNeededText.text = " Max upgraded";

            m_UpgradeButton.GetComponent<ButtonAppearance>().ChangeAppearance(true);
        }
        // if character still can be upgraded
        else
        {
            m_MaterialNeededText.text = " Upgrade Cost: " + UIController.get.m_LastSelectedCharacter.m_Level;
            m_ExpNeededText.text = " Upgrade Cost: " + tmpEXP;

            m_UpgradeButton.GetComponent<ButtonAppearance>().ChangeAppearance(false);
        }
    }
}
