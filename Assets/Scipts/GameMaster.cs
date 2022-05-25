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

    public KeyCode TestKey;
    private bool working = false;

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

        // ���������� ������
        DeckMaster.Instance.FillAndShaffleDeck(CollectionMaster.Instance.cardCollection);
        FoodBaseMaster.Instance.CreateRedFood(5);
    }

    void Update()
    {
        if (Input.GetKey(TestKey))
        {
            // ������ ����� ���� �� 6 ���� � ������ ����
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

        Client.Instance.SendInfo(GameMaster.Instance.current.ID,
                                 GameMaster.Instance.roomId,
                                 Actions.takeCard,
                                 5);

        /*players[clientNumber].DrawCardsToPlayer(5);

        for (int i = 0; i < players.Count; i++)
        {
            if (i == clientNumber)
            {
                Debug.Log("draw cards for player " + players[i].ID);
                players[i].DrawCardsToPlayer(5);
            }
            else
            {
                Debug.Log("draw cards for opponent " + players[i].ID);
                players[i].DrawCardsToOpponent(5);
            }
        }*/
        working = false;
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
