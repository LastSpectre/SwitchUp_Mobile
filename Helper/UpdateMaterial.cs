using BA_Praxis_Library;

public static class UpdateMaterial
{
    public static void UpdateResourcesForCurrentSelectedCharacter(int materialsGained)
    {
        int currentCharID = PlayerData.currentSelectedCharacter;

        ServerResponse sr = UserRequestLibrary.GetResourcesRequest();

        Resources resources = sr.Resources;

        // if character is adventurer
        if (currentCharID == 0)
        {
            resources.NeutralMaterial += materialsGained;
        }

        // if character is bandit | red ogre | werewolf
        else if (currentCharID == 1 || currentCharID == 5 || currentCharID == 9)
        {
            resources.DarkMaterial += materialsGained;
        }

        // if character is golem | satyr | yeti
        else if (currentCharID == 2 || currentCharID == 6 || currentCharID == 10)
        {
            resources.AncientMaterial += materialsGained;
        }

        // if character is wasp | rat | mandrake
        else if (currentCharID == 8 || currentCharID == 4 || currentCharID == 3)
        {
            resources.NatureMaterial += materialsGained;
        }

        // if character is shade
        else if (currentCharID == 7)
        {
            resources.ChaosMaterial += materialsGained;
        }

        UserRequestLibrary.UpdateResourcesRequest(resources);
    }

    public static void UpdateExperience(int _expToAdd)
    {
        int currentCharID = PlayerData.currentSelectedCharacter;

        string characterName = "";

        switch (currentCharID)
        {
            case 0:
                characterName = "Adventurer";
                break;
            case 1:
                characterName = "Bandit";
                break;
            case 2:
                characterName = "Golem";
                break;
            case 3:
                characterName = "Mandrake";
                break;
            case 4:
                characterName = "Rat";
                break;
            case 5:
                characterName = "Red Ogre";
                break;
            case 6:
                characterName = "Satyr";
                break;
            case 7:
                characterName = "Shade";
                break;
            case 8:
                characterName = "Wasp";
                break;
            case 9:
                characterName = "Werewolf";
                break;
            case 10:
                characterName = "Yeti";
                break;

            default:
                break;
        }

        // get current stats from database
        ServerResponse sr = UserRequestLibrary.GetCharacterStatsRequest(characterName);

        // copy stats
        BA_Praxis_Library.Character charTMP = sr.Characters[0];

        //set name
        charTMP.Name = characterName;

        // add experience
        charTMP.Experience += _expToAdd;

        // update experience
        UserRequestLibrary.UpdateExperience(charTMP);
    }
}
