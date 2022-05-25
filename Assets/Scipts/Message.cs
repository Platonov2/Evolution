using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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

[Serializable]
public class Message
{
    public string player_id;
    public string room_id;
    public string action;
    public int cards_num;
    public Body body;

    public Message(string player_id, string room_id, string act, Body body)
    {
        this.player_id = player_id;
        this.room_id = room_id;
        this.action = act;
        this.body = body;
    }
}

[Serializable]
public class Body
{
    public int card_id;
    public int card_type;
    public int card_parent;
    public bool is_main_ability;

    public Body(int cardID, int cardType, int cardParent, bool is_main_ability)
    {
        this.card_id = cardID;
        this.card_type = cardType;
        this.card_parent = cardParent;
        this.is_main_ability = is_main_ability;
    }
}

public static class Actions
{
    public const string createRoom = "CREATE_ROOM";
    public const string enterRoom = "ENTER_ROOM";
    public const string finishGame = "FINISH_ROOM";
    public const string reEnterRoom = "RE_ENTER_ROOM";

    public const string startGame = "START_GAME";
    public const string placeCard = "PLACE_CARD";
    public const string takeCard = "TAKE_CARD";
    public const string placeFood = "PLACE_FOOD";
    public const string attack = "ATTACK";
    public const string feed = "FEED";

    public const string initPlayer = "INIT_PLAYER";
    public const string activatePlayer = "ACTIVATE_PLAYER";
}
