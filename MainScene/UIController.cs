using System.Collections.Generic;
using UnityEngine;
using BA_Praxis_Library;
using TMPro;

public class UIController : Singleton<UIController>
{
    [Header("UI Elements")]
    public TMP_Text m_PlayerName;

    [Header("Character Sprites")]
    public Sprite[] m_CharacterSelectionSprites;
    private Dictionary<string, Sprite> m_CharacterSpriteDictionary = new Dictionary<string, Sprite>();
    
    [HideInInspector]
    public Character m_LastSelectedCharacter;
    public List<CharacterSelectionElement> m_AvailableCharacters;

    private void Awake()
    {
        // add all character sprites to the character dictionary
        m_CharacterSpriteDictionary.Add("Adventurer", m_CharacterSelectionSprites[0]);
        m_CharacterSpriteDictionary.Add("Bandit", m_CharacterSelectionSprites[1]);
        m_CharacterSpriteDictionary.Add("Golem", m_CharacterSelectionSprites[2]);
        m_CharacterSpriteDictionary.Add("Mandrake", m_CharacterSelectionSprites[3]);
        m_CharacterSpriteDictionary.Add("Rat", m_CharacterSelectionSprites[4]);
        m_CharacterSpriteDictionary.Add("Red Ogre", m_CharacterSelectionSprites[5]);
        m_CharacterSpriteDictionary.Add("Satyr", m_CharacterSelectionSprites[6]);
        m_CharacterSpriteDictionary.Add("Shade", m_CharacterSelectionSprites[7]);
        m_CharacterSpriteDictionary.Add("Wasp", m_CharacterSelectionSprites[8]);
        m_CharacterSpriteDictionary.Add("Werewolf", m_CharacterSelectionSprites[9]);
        m_CharacterSpriteDictionary.Add("Yeti", m_CharacterSelectionSprites[10]);
        m_CharacterSpriteDictionary.Add("NotUnlocked", m_CharacterSelectionSprites[11]);

        // 
        m_LastSelectedCharacter = m_AvailableCharacters[PlayerData.currentSelectedCharacter].m_Character;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerName.text = PlayerData.playerName;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // returns the correct sprite from the dictionary
    public Sprite GetCorrectSprite(string _charName)
    {
        return m_CharacterSpriteDictionary[_charName];
    }

    // updates the last character selected + UI
    public void UpdateCharacterSelection(Character _char)
    {
        // update char
        m_LastSelectedCharacter = _char;
        PlayerData.currentSelectedCharacter = m_LastSelectedCharacter.m_CharacterID;

        // update UI from each char
        foreach (CharacterSelectionElement cse in m_AvailableCharacters)
        {
            cse.UpdateUIStatus();
        }

        CharacterUpgradeElement.get.UpdateCharacterUpgradeUI();
    }

    // try to upgrade current selected character
    public void UpgradeCharacter()
    {
        ServerResponse sr = m_LastSelectedCharacter.UpgradeCharacter();

        if(sr.ResponseType == EServerResponseType.ERROR)
        {
            CharacterUpgradeElement.get.m_ErrorBanner.gameObject.SetActive(true);
            Invoke("DisableErrorBanner", 2.0f);
        }
        else if(sr.ResponseType == EServerResponseType.OK)
        {
            // Update Character Selection Elements
            foreach (CharacterSelectionElement cse in m_AvailableCharacters)
            {
                cse.UpdateUIStatus();
            }

            // Update CharacterUpgrade Element
            CharacterUpgradeElement.get.UpdateCharacterUpgradeUI();

            // Update Resource Window
            ResourcesWindow.get.UpdateResources();
        }
    }

    // disable error banner
    void DisableErrorBanner()
    {
        CharacterUpgradeElement.get.m_ErrorBanner.gameObject.SetActive(false);
    }
}
