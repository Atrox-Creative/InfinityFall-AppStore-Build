using System.Collections.Generic;

[System.Serializable]

public class Player {
    public string _id;
    public string name;
    public string token;
    public string creationDate;

    public Player(string id, string playerName, string playerToken)
    {
        _id = id;
        name = playerName;
        token = playerToken;
    }
}
