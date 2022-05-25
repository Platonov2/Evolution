using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckMaster : MonoBehaviour
{
    public static DeckMaster Instance { get; private set; }

    public GameObject cardPrefab;
    public TMP_Text cardCounter;
    public float speed;

    public Stack<GameObject> cards = new Stack<GameObject>();
    public List<int> formatCards = new List<int>();

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        // ���������� �������� ���� � ������
        if (cards != null)
            cardCounter.text = "���-�� ����: " + cards.Count.ToString();
    }

    public void FillAndShaffleDeck(List<CardInfo> cards)
    {
        var shaffledCards = Shuffle(cards);

        int z = 0;
        foreach (CardInfo card in shaffledCards)
        {
            var cardInstance = CreateCardInstance(card, new Vector3(this.transform.position.x, this.transform.position.y, z), this.transform.rotation);//.identity);
            this.cards.Push(cardInstance);

            formatCards.Add(card.ID);

            z -= 1;
        }
    }

    public List<CardInfo> Shuffle(List<CardInfo> cards)
    {
        System.Random random = new System.Random();

        for (int i = cards.Count - 1; i >= 1; i--)
        {
            int j = random.Next(i + 1);

            var temp = cards[j];
            cards[j] = cards[i];
            cards[i] = temp;
        }

        return cards;
    }

    public GameObject GetCard()
    {
        var card = cards.Pop();

        //card.transform.position = Vector3.MoveTowards(card.transform.position, new Vector3(card.transform.position.x, card.transform.position.y, -50), speed);

        return card;
    }


    // ����� ���������� ����� ����� �� �����
    // ��� ����� �������� ��������� �������, �������� �������� ��������� �� cardInfo
    private GameObject CreateCardInstance(CardInfo cardInfo, Vector3 position, Quaternion rotation)
    {
        // �������� ���������� �������
        var card = Instantiate(this.cardPrefab, position, rotation);
        card.name = cardInfo.SpritePath;

        Card cardScript = card.GetComponent<Card>();
        cardScript.SetCardInfo(cardInfo);

        //card.AddComponent<Card>();

        card.transform.Rotate(-90, 90, 90);

        // ����������� ����� � ������.
        // ������ �������� �������� �� ���������� �������� � ��������� ������� ������������ ��������. ������ ������ ���� true
        card.transform.SetParent(this.transform, true);

        // ���������� �������
        card.GetComponent<MeshRenderer>().material = Resources.Load<Material>(cardInfo.SpritePath);

        return card;
    }
}
