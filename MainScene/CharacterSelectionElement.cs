using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSelectionElement : MonoBehaviour
{
    [Header("UI Elements")]
    public Image m_CharacterIcon;
    public TMP_Text m_CharacterNameText;
    public TMP_Text m_CharacterLevelText;
    public Button m_SelectCharacterButton;
    public Image m_CheckmarkIcon;
    public TMP_Text m_ExperienceText;
    public Image m_ExperienceBarImage;

    [Header("Other")]
    public Character m_Character;

    private void Awake()
    {
        m_Character.GetStats();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUIStatus();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateUIStatus()
    {
        // get current stats from database
        m_Character.GetStats();

        // if character unlocked
        if (m_Character.m_UnlockStatus != 0)
        {
            // get correct sprite
            m_CharacterIcon.sprite = UIController.get.GetCorrectSprite(m_Character.m_CharacterName);

            m_CharacterNameText.text = m_Character.m_CharacterName;

            m_CharacterLevelText.text = "Lv." + m_Character.m_Level.ToString();

            // if this is the current selected character
            if (m_Character.m_CharacterName == UIController.get.m_LastSelectedCharacter.m_CharacterName)
            {
                m_SelectCharacterButton.GetComponent<ButtonAppearance>().ChangeAppearance(true);
                m_CheckmarkIcon.gameObject.SetActive(true);
            }
            // is not the selected character
            else
            {
                m_SelectCharacterButton.GetComponent<ButtonAppearance>().ChangeAppearance(false);
                m_CheckmarkIcon.gameObject.SetActive(false);
            }

            // current exp from selected character
            float expTMP = m_Character.m_ExperienceGained;
            
            // exp current character needs to be upgraded
            float maxEXP = m_Character.ExperienceForNextUpgrade();
            
            // if character is already fully upgraded
            if (maxEXP == -1)
            {
                m_ExperienceText.text = expTMP.ToString() + "/" + "Max";
                m_ExperienceBarImage.rectTransform.sizeDelta = new Vector2(480.0f, 130.0f);
            }
            // if character still can be upgraded
            else
            {
                m_ExperienceText.text = expTMP.ToString() + "/" + maxEXP.ToString() + " XP";

                // clamp current exp for rect scale
                float rectX = Mathf.Clamp(expTMP, 0, maxEXP);

                m_ExperienceBarImage.rectTransform.sizeDelta = new Vector2(480.0f * (rectX / maxEXP), 130.0f);
            }

        }
        // if character not unlocked
        else
        {
            // get sprite
            m_CharacterIcon.sprite = UIController.get.GetCorrectSprite("NotUnlocked");

            m_CharacterNameText.text = "Not Unlocked";

            m_CharacterLevelText.text = "Lv.X";

            m_SelectCharacterButton.GetComponent<ButtonAppearance>().ChangeAppearance(true);
            m_CheckmarkIcon.gameObject.SetActive(false);

            m_ExperienceText.text = "0/0 XP";

            m_ExperienceBarImage.rectTransform.sizeDelta = new Vector2(0.0f, 130.0f);
        }

    }

    // changes the current selected character to the one attached to the button clicked
    public void ChangeCurrentCharacterSelected()
    {
        UIController.get.UpdateCharacterSelection(m_Character);
    }
}
