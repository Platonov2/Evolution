using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    public Player player1;
    public Player player2;
    public Player player3;
    public Player player4;
    public List<Player> players = new List<Player>();

    void Awake()
    {
        Instance = this;

        if (player1 != null) players.Add(player1);
        if (player2 != null) players.Add(player2);
        if (player3 != null) players.Add(player3);
        if (player4 != null) players.Add(player4);
    }

    void Start()
    {
        // Заполнение колоды
        DeckMaster.Instance.FillAndShaffleDeck(CollectionMaster.Instance.cardCollection);

        // Каждый игрок берёт по 6 карт в начале игры
        foreach (var player in players)
        {
            player.DrawCards(2);
        }
    }
}
