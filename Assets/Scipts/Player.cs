using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    public GameObject hand;
    public GameObject creaturesField;
    public List<GameObject> creatures = new List<GameObject>();
    public GameObject emptyCreaturePrefab;

    public string ID;
    public bool isHost;
    public string playerName;
    public int playerNumber;

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

            // Карты "схлапываются" из-за этой строчки
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

    public void PlaceCardToOpponent(int cardID, int cardParent, int cardType, bool isMainAbility)
    {
        if (cardType == 1)
        {
            Debug.Log("placing creature");
            DrawCreatureToOpponent(cardID);
        } 
        if (cardType == 2) {
            Debug.Log("placing ability");
            DrawAbilityToOpponent(cardID, cardParent, isMainAbility);
        }
    }

    private void DrawAbilityToOpponent(int cardID, int parent, bool isMainAbility)
    {
        var hand = this.transform.Find("Hand");

        foreach (Transform child in hand.transform)
        {
            if (null == child)
                continue;

            Card abilityCard = child.gameObject.GetComponent<Card>();

            if (abilityCard.ID == cardID)
            {
                Debug.Log("find ability with " + cardID);
                SetAbilityToParent(abilityCard, child, cardID, parent, isMainAbility);
                return;
            }
        }
    }

    private void SetAbilityToParent(Card card, Transform child, int cardID, int parent, bool isMainAbility)
    {
        TransformController transformController = child.gameObject.GetComponent<TransformController>();
        transformController.FlipCard();

        card.SyncAbility(isMainAbility);
        IAbility ability = card.GetAbility();

        GameObject abilityCard = new GameObject();

        abilityCard = child.gameObject;

        var creatures = this.transform.Find("Creatures");

        Transform parenCard = creatures.transform.GetChild(parent);
        Card targetCard = parenCard.gameObject.GetComponent<Card>();
        Creature creature = targetCard.GetComponent<Creature>();
        creature.AddAbilityToOpponent(abilityCard, ability);
    }

    private void DrawCreatureToOpponent(int cardID)
    {
        var hand = this.transform.Find("Hand");
        GameObject sourceCard = new GameObject();

        foreach (Transform child in hand.transform)
        {
            if (null == child)
                continue;

            Card card = child.gameObject.GetComponent<Card>();
            if (card.ID == cardID)
            {
                Debug.Log("find creature with " + cardID);
                sourceCard = child.gameObject;
                break;
            }
        }

        creatures.Insert(creatures.Count, sourceCard);

        sourceCard.layer = LayerMask.NameToLayer("OpponentCreature");
        sourceCard.AddComponent<Creature>();
        Creature creatureScript = sourceCard.GetComponent<Creature>();
        creatureScript.Initialize();
        sourceCard.transform.SetParent(creaturesField.transform, true);

        Debug.Log("Created opponent creature");
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
        creatureScript.SetPos();
        sourceCard.transform.SetParent(creaturesField.transform, true);
        sourceCard.transform.SetSiblingIndex(creatures.Count - 2);

        Card card = sourceCard.GetComponent<Card>();

        Body b = new Body(card.ID, 1, -1, false);
        Debug.Log(b.card_type);
        Client.Instance.SendInfo(GameMaster.Instance.current.ID,
                                 GameMaster.Instance.roomId,
                                 Actions.placeCard,
                                 b);

        Debug.Log("Created creature");
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
}
