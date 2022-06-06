using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NativeWebSocket;

public class Client : MonoBehaviour
{
    public static Client Instance { get; private set; }

    private WebSocket websocket;

    void Start()
    {
        StartConnection("CREATE");
    }

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (websocket.State == WebSocketState.Open)
        {
            websocket.DispatchMessageQueue();
        }
    }

    async public void StartConnection(string act)
    {
        websocket = new WebSocket("ws://localhost:2567/ws");
        //GameMaster.Instance.StartGame();

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");

            if (act == "CREATE")
            {
                // Заполнение колоды
                CollectionMaster.Instance.Init();

                GameMaster.Instance.current.playerName = "1";
                GameMaster.Instance.current.isHost = true;
                CreateRoom(GameMaster.Instance.current.playerName,
                    GameMaster.Instance.roomName);
            }

            if (act == "ENTER")
            {
                GameMaster.Instance.current.isHost = false;
                EnterRoom(GameMaster.Instance.current.playerName, 
                    GameMaster.Instance.roomId);
            }

            if (act == "REENTER")
            {
                GameMaster.Instance.current.isHost = false;
                ReEnterRoom(GameMaster.Instance.current.playerName,
                    GameMaster.Instance.roomId);
            }
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Broken connection " + e);
        };

        websocket.OnMessage += (bytes) =>
        {
            var str = System.Text.Encoding.UTF8.GetString(bytes);

            Debug.Log("get mes: " + str);


            GreetingMessage gmes = JsonUtility.FromJson<GreetingMessage>(str);

            if ((gmes != null && gmes.players.Count != 0) || gmes.role)
            {
                ProcessGreeting(gmes);
                return;
            }

            Message mes = JsonUtility.FromJson<Message>(str);

            if (mes != null)
            {
                ProcessMessage(mes);
                return;
            }
        };

        await websocket.Connect();
    }

    private void ProcessGreeting(GreetingMessage mes)
    {
        if (mes.action == Actions.startGame)
        {
            GameMaster.Instance.StartGame();
            return;
        }

        if (mes.action == Actions.initPlayer)
        {
            if (GameMaster.Instance.roomId == "")
            {
                GameMaster.Instance.roomId = mes.room_id;
                GameMaster.Instance.roomName = mes.room_name;
            }
            if (GameMaster.Instance.current.ID == "")
            {
                GameMaster.Instance.current.ID = mes.player_id;
            }

            if (GameMaster.Instance.current.ID != mes.player_id && GameMaster.Instance.current.ID != "")
            {
                Debug.Log("setting id to opponent " + mes.player_id);
                GameMaster.Instance.player1.ID = mes.player_id;
                GameMaster.Instance.player1.playerName = mes.player_name;
            }

            if (!GameMaster.Instance.current.isHost)
            {
                CollectionMaster.Instance.Create(mes.deck);
            }
        }

        if (mes.players.Count == 2 && GameMaster.Instance.current.isHost)
        {
            GameMaster.Instance.player1.playerName = mes.players[1];

            Body b = new Body(0, 0, 0, false);
            SendInfo(
                mes.player_id,
                mes.room_id,
                Actions.startGame,
                b);
        }
    }

    private void ProcessMessage(Message mes)
    {
        if (mes.action == Actions.startGame)
        {
            GameMaster.Instance.StartGame();
            return;
        }

        if (mes.action == Actions.takeCard)
        {
            GameMaster.Instance.TakeCards(mes.player_id, mes.cards_num);
        }

        if (mes.action == Actions.placeCard && mes.player_id != GameMaster.Instance.current.ID)
        {
            GameMaster.Instance.player1.PlaceCardToOpponent(mes.body.card_id, mes.body.card_parent, mes.body.card_type, mes.body.is_main_ability);
            return;
        }

        if (mes.action == Actions.feed && mes.player_id != GameMaster.Instance.current.ID)
        {
            GameMaster.Instance.player1.FeedOpponentCreature(mes.body.feed, mes.body.card_parent);
            return;
        }

        if (mes.action == Actions.attack && mes.player_id != GameMaster.Instance.current.ID)
        {
            GameMaster.Instance.player1.AttackCreature(mes.body.card_parent, mes.body.card_id, mes.room_id);
            return;
        }

        if (mes.action == Actions.activatePlayer)
        {
            var creatures = GameMaster.Instance.current.transform.Find("Creatures");
            if (mes.player_id != GameMaster.Instance.current.ID)
            {
                foreach (Transform child in creatures)
                {
                    if (child.gameObject.layer == LayerMask.NameToLayer("EmptyCreature") || child.gameObject.layer == LayerMask.NameToLayer("EmptyCreatureBlocked"))
                    {
                        child.gameObject.layer = LayerMask.NameToLayer("EmptyCreatureBlocked");
                    } else
                    {
                        child.gameObject.layer = LayerMask.NameToLayer("YourCreatureBlocked");
                    }
                }
                creatures.gameObject.layer = LayerMask.NameToLayer("EmptyCreatureBlocked");
            } else
            {
                foreach (Transform child in creatures)
                {
                    if (child.gameObject.layer == LayerMask.NameToLayer("EmptyCreatureBlocked") || child.gameObject.layer == LayerMask.NameToLayer("EmptyCreature"))
                    {
                        child.gameObject.layer = LayerMask.NameToLayer("EmptyCreature");
                    } else
                    {
                        child.gameObject.layer = LayerMask.NameToLayer("YourCreature");
                    }
                }
                creatures.gameObject.layer = LayerMask.NameToLayer("YourCreaturesField");
            }
        }
    }

    public void CreateRoom(string playerID, string RoomName)
    {

        GreetingMessage mes = new GreetingMessage(playerID, Actions.createRoom, true);
        mes.room_name = RoomName;
        mes.deck = CollectionMaster.Instance.GetOrders();

        string strJson = JsonUtility.ToJson(mes);

        Debug.Log("format message in create " + strJson);

        SendWsMessage(strJson);
    }

    public void EnterRoom(string playerID, string RoomId)
    {
        GreetingMessage mes = new GreetingMessage(playerID, Actions.enterRoom, false);
        mes.room_id = RoomId;

        string strJson = JsonUtility.ToJson(mes);

        Debug.Log("format message in enter " + strJson);

        SendWsMessage(strJson);
    }

    public void ReEnterRoom(string playerID, string RoomId)
    {
        GreetingMessage mes = new GreetingMessage(playerID, Actions.reEnterRoom, false);
        mes.room_id = RoomId;

        string strJson = JsonUtility.ToJson(mes);

        Debug.Log("format message in enter " + strJson);

        SendWsMessage(strJson);
    }

    public void SendInfo(string player_id, string room_id, string act, Body body)
    {
        Message mes = new Message(player_id, room_id, act, body);

        string strJson = JsonUtility.ToJson(mes);

        Debug.Log("format message in send info " + strJson);

        SendWsMessage(strJson);

        if (mes.action == Actions.finishGame)
        {
            ConnectionClose();
        }
    }

    public void SendInfo(string player_id, string room_id, string act, int num)
    {
        Message mes = new Message(player_id, room_id, act, null);
        mes.cards_num = num;

        string strJson = JsonUtility.ToJson(mes);

        Debug.Log("format message in send info " + strJson);

        SendWsMessage(strJson);

        if (mes.action == Actions.finishGame)
        {
            ConnectionClose();
        }
    }

    private bool SendWsMessage(string mes)
    {
        if (websocket.State == WebSocketState.Open)
        {
            Debug.Log("sending mes");
            websocket.SendText(mes);

            return true;
        }

        return false;
    }

    private async void ConnectionClose()
    {
        await websocket.Close();
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
