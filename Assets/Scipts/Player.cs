using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    public int playerNumber;
    public GameObject hand;
    public GameObject creaturesField;
    public List<GameObject> creatures = new List<GameObject>();
    public GameObject emptyCreaturePrefab;
    public GameObject cardPrefab;

    public int creatureCount;

    void Start()
    {
        if (playerNumber == 0)
            CreateEmptyCreature();
    }

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

            //  арты "схлапываютс€" из-за этой строчки
            //transformController.MoveTo(new Vector3(hand.transform.position.x, hand.transform.position.y, -1));

            card.transform.SetParent(hand, true);

            transformController.FlipCard();

            //card.transform.position = hand.transform.position;
            //card.transform.position = Vector3.MoveTowards(card.transform.position, new Vector3(hand.transform.position.x, hand.transform.position.y, -1), 10);

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
            //card.transform.position = Vector3.MoveTowards(card.transform.position, new Vector3(hand.transform.position.x, hand.transform.position.y, -1), 10);

            card.layer = LayerMask.NameToLayer("OpponentCard");
        }
    }

    public void CreateCreature(GameObject sourceCard)
    {
        TransformController transformController = sourceCard.GetComponent<TransformController>();
        
        transformController.FlipCard();

        creatures.Insert(creatures.Count - 1, sourceCard);

        sourceCard.layer = LayerMask.NameToLayer("YourCreature");
        sourceCard.AddComponent<Creature>();
        Creature creatureScript = sourceCard.GetComponent<Creature>();
        creatureScript.Initialize();
        sourceCard.transform.SetParent(creaturesField.transform, true);
        sourceCard.transform.SetSiblingIndex(creatures.Count - 2);
    }

    public void CreateCreatureAndFeed()
    {
        var card = Instantiate(this.cardPrefab);
        //card.transform.Rotate(-90, 90, 90);

        creatures.Insert(creatures.Count - 1, card);

        card.layer = LayerMask.NameToLayer("YourCreature");

        card.AddComponent<Creature>();
        Creature creatureScript = card.GetComponent<Creature>();
        creatureScript.Initialize();
        creatureScript.FeedBlue(1);

        card.transform.SetParent(creaturesField.transform, true);
        card.transform.SetSiblingIndex(creatures.Count - 2);
    }

    public void DestroyCreature(GameObject creature)
    {
        creatures.Remove(creature);
    }

    public void CreateEmptyCreature()
    {
        var emptyCreature = Instantiate(emptyCreaturePrefab);

        creatures.Add(emptyCreature);
        emptyCreature.transform.position = creaturesField.transform.position;
        emptyCreature.transform.SetParent(creaturesField.transform, true);
    }

    public int CalculateScore()
    {
        int finalScore = 0;
        foreach (var creature in creatures)
        {
            Creature creatureController = creature.GetComponent<Creature>();

            finalScore += 2; // +2 за выжившее существо
            finalScore += creatureController.abilities.Count; // +1 за каждое свойство
            finalScore += creatureController.hunger - 1; // +1 за каждое увеличение потребности в еде
        }

        return finalScore;
    }
}
