using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    public int playerNumber;
    public GameObject hand;
    public GameObject creatures;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DrawCards(int numberOfCards)
    {
        var hand = this.transform.Find("Hand");

        for (int i = 0; i < numberOfCards; i++)
        {
            var card = DeckMaster.Instance.GetCard();

            card.transform.SetParent(hand, true);
            card.transform.position = hand.transform.position;
        }
    }
}
