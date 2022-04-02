using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckMaster : MonoBehaviour
{
    public static DeckMaster Instance { get; private set; }

    public GameObject cardPrefab;
    public TMP_Text cardCounter;

    private List<Card> cards = null;


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

    // ������� ���� �������������� ������ ��� �������, ��� �� ������� ���� ��������
    void OnMouseDown()
    {
        Debug.Log(1);
        Shuffle();
    }




    public void FillDeck(List<Card> cards)
    {
        this.cards = cards;

        int z = 0;
        foreach (Card card in cards)
        {
            CreateCardInstance(card, new Vector3(this.transform.position.x, this.transform.position.y, z), Quaternion.identity);

            z -= 1;
        }
    }

    public void Shuffle()
    {
        System.Random random = new System.Random();

        for (int i = cards.Count - 1; i >= 1; i--)
        {
            int j = random.Next(i + 1);

            var temp = cards[j];
            cards[j] = cards[i];
            cards[i] = temp;
        }
    }




    // ����� ���������� ����� ����� �� �����
    // ��� ����� �������� ��������� �������, �������� �������� ��������� �� cardInfo
    private void CreateCardInstance(Card cardInfo, Vector3 position, Quaternion rotation)
    {
        // �������� ���������� �������
        var card = Instantiate(this.cardPrefab.transform, position, rotation);
        card.name = cardInfo.frontSpritePath;

        // ����������� ����� � ������.
        // ������ �������� �������� �� ���������� �������� � ��������� ������� ������������ ��������. ������ ������ ���� true
        card.SetParent(this.transform, true);

        // ���������� �������
        var front = card.Find("Front");
        var back = card.Find("Back");

        SpriteRenderer frontSprite = front.GetComponent<SpriteRenderer>();
        SpriteRenderer backSprite = back.GetComponent<SpriteRenderer>();

        frontSprite.sprite = Resources.Load<Sprite>(cardInfo.frontSpritePath);
        backSprite.sprite = Resources.Load<Sprite>(cardInfo.backSpritePath);
    }
}
