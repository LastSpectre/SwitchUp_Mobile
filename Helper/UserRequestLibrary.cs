using BA_Praxis_Library;
using TMPro;

public static class UserRequestLibrary
{
    // creates a request to log the user in
    public static ServerResponse LoginRequest(TMP_Text _name, TMP_Text _password)
    {
        // create new client
        Client client = new Client();

        UserRequest currentRequest = new UserRequest()
        {
            RequestType = EUserRequestType.USER_LOGIN,
            Username = _name.text.Remove(_name.text.Length - 1),
            Password = _password.text.Remove(_password.text.Length - 1)
        };

        // get response from server with new request
        ServerResponse response = client.RunRequest(PlayerData.IP, PlayerData.Port, currentRequest);

        return response;
    }

    public static ServerResponse GetCharacterStatsRequest(string _charName)
    {
        // create new client
        Client client = new Client();

        UserRequest currentRequest = new UserRequest()
        {
            RequestType = EUserRequestType.CHAR_GET,
            Username = PlayerData.playerName,
            Password = PlayerData.playerPassword,
            PayloadChar = new BA_Praxis_Library.Character() { Name = _charName }
        };

        // get response from the server with new request
        ServerResponse response = client.RunRequest(PlayerData.IP, PlayerData.Port, currentRequest);

        return response;
    }

    public static ServerResponse UpgradeCharacterRequest(string _charName)
    {
        // create new client
        Client client = new Client();

        UserRequest currentRequest = new UserRequest()
        {
            RequestType = EUserRequestType.CHAR_UPGRADE,
            Username = PlayerData.playerName,
            Password = PlayerData.playerPassword,
            PayloadChar = new BA_Praxis_Library.Character() { Name = _charName }
        };

        // get response from the server with new request
        ServerResponse response = client.RunRequest(PlayerData.IP, PlayerData.Port, currentRequest);

        return response;
    }

    public static ServerResponse UpdateResourcesRequest(Resources resources)
    {
        // create new client
        Client client = new Client();

        UserRequest currentRequest = new UserRequest()
        {
            RequestType = EUserRequestType.UPDATE_RESOURCES,
            Username = PlayerData.playerName,
            Password = PlayerData.playerPassword,
            PayloadResources = resources
        };

        ServerResponse response = client.RunRequest(PlayerData.IP, PlayerData.Port, currentRequest);

        return response;
    }

    public static ServerResponse GetResourcesRequest()
    {
        // create new client
        Client client = new Client();

        UserRequest currentRequest = new UserRequest()
        {
            RequestType = EUserRequestType.GET_RESOURCES,
            Username = PlayerData.playerName,
            Password = PlayerData.playerPassword
        };

        ServerResponse response = client.RunRequest(PlayerData.IP, PlayerData.Port, currentRequest);

        return response;
    }

    public static ServerResponse UpdateExperience(BA_Praxis_Library.Character _char)
    {
        // create new client
        Client client = new Client();

        UserRequest currentRequest = new UserRequest()
        {
            RequestType = EUserRequestType.CHAR_EXP_GAIN,
            Username = PlayerData.playerName,
            Password = PlayerData.playerPassword,
            PayloadChar = _char
        };

        ServerResponse response = client.RunRequest(PlayerData.IP, PlayerData.Port, currentRequest);

        return response;
    }
}
