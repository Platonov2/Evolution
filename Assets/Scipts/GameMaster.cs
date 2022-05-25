using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    public int clientNumber;
    public Player player0;
    public Player player1;
    public Player player2;
    public Player player3;
    public List<Player> players = new List<Player>();

    public KeyCode TestKey;
    private bool working = false;

    void Awake()
    {
        Instance = this;

        if (player0 != null) players.Add(player0);
        if (player1 != null) players.Add(player1);
        if (player2 != null) players.Add(player2);
        if (player3 != null) players.Add(player3);
    }

    void Start()
    {
        // Заполнение колоды
        DeckMaster.Instance.FillAndShaffleDeck(CollectionMaster.Instance.cardCollection);
        FoodBaseMaster.Instance.CreateRedFood(5);
    }

    void Update()
    {
        if (Input.GetKey(TestKey))
        {
            // Каждый игрок берёт по 6 карт в начале игры
            /*foreach (var player in players)
            {
                player.DrawOpenedCards(2);
            }*/

            //Debug.Log(players.Count);
            if (!working)
            {
                StartCoroutine(ExampleCoroutine());
            }
        }
    }

    IEnumerator ExampleCoroutine()
    {
        working = true;
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < players.Count; i++)
        {
            if (i == clientNumber)
            {
                players[i].DrawCardsToPlayer(6);
            }
            else players[i].DrawCardsToOpponent(6);
        }
        working = false;
    }
}
