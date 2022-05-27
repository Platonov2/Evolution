using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;


public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    public int clientNumber;
    public string roomId;
    public string roomName;

    public Player current;
    public Player player1;
    public Player player2;
    public Player player3;
    public List<Player> players = new List<Player>();

    void Awake()
    {
        Instance = this;

        roomName = "Some name";

        clientNumber = 0;
    }

    void Start()
    {

    }

    public void StartGame()
    {
        players.Add(current);
        players.Add(player1);

        // Заполнение колоды
        DeckMaster.Instance.FillDeck(CollectionMaster.Instance.cardCollection);
        FoodBaseMaster.Instance.CreateRedFood(5);

        Client.Instance.SendInfo(GameMaster.Instance.current.ID,
                                 GameMaster.Instance.roomId,
                                 Actions.takeCard,
                                 5);
    }

    public void TakeCards(string playerID, int num)
    {
        if (playerID == current.ID)
        {
            Debug.Log("give cards to player");
            players[clientNumber].DrawCardsToPlayer(num);
        }
        else
        {
            Debug.Log("give cards to opponent");
            players[1].DrawCardsToOpponent(num);
            //var pnum = FindPlayer(playerID);
            //players[pnum].DrawCardsToOpponent(num);
        }
    }

    private int FindPlayer(string playerID)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].ID == playerID)
            {
                return i;
            }
        }

        return -1;
    }
}
