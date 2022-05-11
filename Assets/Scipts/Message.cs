using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreetingMessage
{
    public string player_id;
    public string player_name;
    public List<string> players;
    public string room_id;
    public string room_name;
    public bool role;
    public List<int> deck;
    public List<int> cards;
    public string action;

    public GreetingMessage(string name, string act, bool role)
    {
        player_name = name;
        action = act;
        this.role = role;
    }
}

public class PlayerInfo
{
    string player_name;
}

public class Message
{
    public string player_id;
    public string room_id;
    public string action;
    public string body;

    public Message(string player_id, string room_id, string act, string body)
    {
        this.player_id = player_id;
        this.room_id = room_id;
        this.action = act;
        this.body = body;
    }
}

public static class Actions
{
    public const string createRoom = "CREATE_ROOM";
    public const string enterRoom = "ENTER_ROOM";
    public const string startGame = "START_GAME";
    public const string finishGame = "FINISH_ROOM";
    public const string reEnterRoom = "RE_ENTER_ROOM";

    public const string placeCard = "PLACE_CARD";

    public const string initPlayer = "INIT_PLAYER";
    public const string activatePlayer = "ACTIVATE_PLAYER";
}
