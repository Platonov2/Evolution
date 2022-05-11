using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NativeWebSocket;

public class Client : MonoBehaviour
{
    public static Client Instance { get; private set; }

    private WebSocket websocket;
    public bool isReady;

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

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");

            isReady = true;

            if (act == "CREATE")
            {
                CreateRoom("1", "Some name");
            }

            if (act == "ENTER")
            {
                EnterRoom("1", "Some name");
            }
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Broken connection " + e);

            isReady = false;
        };

        websocket.OnMessage += (bytes) =>
        {
            var str = System.Text.Encoding.UTF8.GetString(bytes);

            Debug.Log("get mes: " + str);

            GreetingMessage mes = JsonUtility.FromJson<GreetingMessage>(str);

            if (mes.players.Count == 2) {
                SendInfo(
                    mes.player_id,
                    mes.room_id,
                    Actions.startGame,
                    "");
            }
        };

        await websocket.Connect();
    }

    public void CreateRoom(string playerID, string RoomName)
    {

        GreetingMessage mes = new GreetingMessage(playerID, Actions.createRoom, true);
        mes.room_name = RoomName;
        mes.deck = DeckMaster.Instance.formatCards;

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

    private void SendInfo(string player_id, string room_id, string act, string body)
    {
        Message mes = new Message(player_id, room_id, act, body);

        string strJson = JsonUtility.ToJson(mes);

        Debug.Log("format message in info " + strJson);

        SendWsMessage(strJson);
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
