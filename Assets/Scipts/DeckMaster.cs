using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckMaster : MonoBehaviour
{
    public static DeckMaster Instance { get; private set; }

    public GameObject cardPrefab;
    public TMP_Text cardCounter;

    public Stack<GameObject> cards = new Stack<GameObject>();


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




    public void FillAndShaffleDeck(List<Card> cards)
    {
        var shaffledCards = Shuffle(cards);

        int z = 0;
        foreach (Card card in shaffledCards)
        {
            var cardInstance = CreateCardInstance(card, new Vector3(this.transform.position.x, this.transform.position.y, z), this.transform.rotation);//.identity);
            this.cards.Push(cardInstance);

            z -= 1;
        }
    }

    public List<Card> Shuffle(List<Card> cards)
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
        return cards.Pop();
    }


    // ����� ���������� ����� ����� �� �����
    // ��� ����� �������� ��������� �������, �������� �������� ��������� �� cardInfo
    private GameObject CreateCardInstance(Card cardInfo, Vector3 position, Quaternion rotation)
    {
        // �������� ���������� �������
        var card = Instantiate(this.cardPrefab, position, rotation);
        card.name = cardInfo.SpritePath;
        card.transform.Rotate(-90, 0, 0);

        // ����������� ����� � ������.
        // ������ �������� �������� �� ���������� �������� � ��������� ������� ������������ ��������. ������ ������ ���� true
        card.transform.SetParent(this.transform, true);

        // ���������� �������
        card.GetComponent<MeshRenderer>().material = Resources.Load<Material>(cardInfo.SpritePath);

        return card;
    }
}
