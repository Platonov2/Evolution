using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    public int playerNumber;
    public GameObject hand;
    public GameObject creaturesField;
    public List<GameObject> creatures = new List<GameObject>();

    public int creatureCount;

    void Update()
    {
        creatureCount = creatures.Count;
    }

    public void DrawCardsToPlayer(int numberOfCards)
    {
        var hand = this.transform.Find("Hand");

        for (int i = 0; i < numberOfCards; i++)
        {
            var card = DeckMaster.Instance.GetCard();
            //card.transform.rotation = Quaternion.Euler(-90, -90, 90);

            TransformController transformController = card.GetComponent<TransformController>();
            transformController.MoveTo(new Vector3(hand.transform.position.x, hand.transform.position.y, -1));
            transformController.FlipCard();

            card.transform.SetParent(hand, true);
            card.transform.position = hand.transform.position;
            card.transform.position = Vector3.MoveTowards(card.transform.position, new Vector3(hand.transform.position.x, hand.transform.position.y, -1), 10);

            card.layer = LayerMask.NameToLayer("YourHandCard");
        }
    }

    public void DrawCardsToOpponent(int numberOfCards)
    {
        var hand = this.transform.Find("Hand");

        for (int i = 0; i < numberOfCards; i++)
        {
            var card = DeckMaster.Instance.GetCard();

            card.transform.SetParent(hand, true);
            card.transform.position = Vector3.MoveTowards(card.transform.position, new Vector3(hand.transform.position.x, hand.transform.position.y, -1), 10);

            card.layer = LayerMask.NameToLayer("OpponentCard");
        }
    }

    public void CreateCreature(GameObject sourceCard) 
    {
        var emptyLayer = LayerMask.NameToLayer("EmptyCard");

        for (int i = 0; i < creatures.Count; i++)
        {
            if (creatures[i].layer == emptyLayer)
            {
                sourceCard.layer = LayerMask.NameToLayer("YourCreature");
                creatures[i] = sourceCard;
                sourceCard.transform.SetParent(creaturesField.transform, true);
                //Debug.Log("Стало" + creatures.Count);
                return;
            }
        }
        /*
        //Debug.Log(creatureIndex);
        sourceCard.transform.SetParent(creaturesField.transform, true);
        //sourceCard.transform.SetSiblingIndex(creaturePosition);
        //sourceCard.layer = LayerMask.NameToLayer("YourCreature");
        sourceCard.layer = LayerMask.NameToLayer(layerName);
        //creatures.Insert(creatureIndex, sourceCard);
        if (creatures.Count == 0)
            creatures.Insert(0, sourceCard);
        else creatures[creatureIndex] = sourceCard;*/
    }

    public void CreateEmptyCreature(GameObject emptyCard, int creatureIndex)
    {
        creatures.Insert(creatureIndex, emptyCard);
        emptyCard.transform.SetParent(creaturesField.transform, true);
    }

    public int GetLastIndex()
    {
        return creatures.Count;
    }

    /*public void DeleteCreature(int creatureIndex)
    {
        creatures.RemoveAt(creatureIndex);
    }*/

    public void RemoveEmptyCard()
    {
        //Debug.Log("Удаляем");
        var emptyLayer = LayerMask.NameToLayer("EmptyCard");

        for (int i = 0; i < creatures.Count; i++)
        {
            if (creatures[i].layer == emptyLayer)
            {
                creatures.RemoveAt(i);
                //Debug.Log("Стало" + creatures.Count);
                return;
            }
        }
    }

    /*public int GetNearLeftCardIndex(Vector3 position)
    {
        float minDistance = 1000f;
        int minDistanceIndex = 0;

        Debug.Log(minDistance);

        for (int i = 0; i < creatures.Count; i++)
        {
            float distance = Vector3.Distance(position, creatures[i].transform.position);
            Debug.Log(distance);

            if (distance < minDistance)
            {
                minDistance = distance;
                minDistanceIndex = i;
                Debug.Log(minDistance);
            }
            if (distance > minDistance)
            {
                float leftDistance = Vector3.Distance(creatures[i].transform.position, creatures[i - 1].transform.position);
                float rightDistance = Vector3.Distance(creatures[i].transform.position, creatures[i + 1].transform.position);
                Debug.Log("Лево  " + leftDistance);
                Debug.Log("Право " + leftDistance);
                Debug.Log("Индекс " + leftDistance);

                if (leftDistance < rightDistance)
                {
                    Debug.Log("Индекс " + i);
                    return i;
                }
                else
                {
                    Debug.Log("Индекс " + (i - 1));
                    return i - 1;
                }
            }
        }

        Debug.Log("Индекс " + (0));

        return 0;
    }*/
}
