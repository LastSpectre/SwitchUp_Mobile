using UnityEngine;
using BA_Praxis_Library;

public class Character : MonoBehaviour
{
    public string m_CharacterName;
    [HideInInspector]
    public int m_ExperienceGained;
    [HideInInspector]
    public int m_Level;
    [HideInInspector]
    public int m_UnlockStatus;

    public Sprite m_UpgradeSprite;
    public int m_CharacterID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // get current character stats from database
    public void GetStats()
    {
        ServerResponse sr = UserRequestLibrary.GetCharacterStatsRequest(m_CharacterName);

        m_ExperienceGained = sr.Characters[0].Experience;
        m_Level = sr.Characters[0].Level;
        m_UnlockStatus = sr.Characters[0].UnlockStatus;
    }

    // upgrade character in database
    public ServerResponse UpgradeCharacter()
    {
        return UserRequestLibrary.UpgradeCharacterRequest(m_CharacterName);
    }

    // returns the experience needed to upgrade this character
    public int ExperienceForNextUpgrade()
    {
        if(m_Level == 1)
        {
            return 125;
        }
        else if(m_Level == 2)
        {
            return 250;
        }
        else if(m_Level == 3)
        {
            return 500;
        }
        else if(m_Level == 4)
        {
            return 1000;
        }
        // character is already max level
        else
        {
            return -1;
        }
    }
}
