using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��������� � ����������� � ������
public struct Card
{
    public string frontSpritePath;
    public string backSpritePath;

    public Card(string frontSpritePath, string backSpritePath)
    {
        this.frontSpritePath = frontSpritePath;
        this.backSpritePath = backSpritePath;
    }
}

public class CollectionMaster : MonoBehaviour
{
    public Transform cardPrefab;
    private List<Card> cardCollection = new List<Card>();
    public Transform deck;

    // ��� ������� ���� ����������� ������ ��� ����
    void Awake()
    {
        cardCollection.Add(new Card("Sharp_Vision-Fat_Tissue", "BackImage"));
        cardCollection.Add(new Card("Parasite-Carnivorous", "BackImage"));
        cardCollection.Add(new Card("�amouflage-Fat_tissue", "BackImage"));
    }

    void Start()
    {
        FillDeck();
    }

    void Update()
    {

    }

    // ����� ���������� ������ �� ���� � cardCollection
    void FillDeck()
    {
        // Z ���������� ��� ����, ����� ����� ���� ��� ����� "���� �� �����". �� ��������� ��� ����������
        int z = 0;
        foreach (Card card in this.cardCollection)
        {
            CreateCardInstance(card, new Vector3(deck.position.x, deck.position.y, z), Quaternion.identity);

            z -= 10;
        }
    }

    // ����� ���������� ����� ����� �� �����
    // ��� ����� �������� ��������� �������, �������� �������� ��������� �� cardInfo
    void CreateCardInstance(Card cardInfo, Vector3 position, Quaternion rotation)
    {
        // �������� ���������� �������
        var card = Instantiate(this.cardPrefab, position, rotation);
        card.name = cardInfo.frontSpritePath;

        // ����������� ����� � ������.
        // ������ �������� �������� �� ���������� �������� � ��������� ������� ������������ ��������. ������ ������ ���� true
        card.SetParent(deck, true);

        // ���������� �������
        var front = card.Find("Front");
        var back = card.Find("Back");

        SpriteRenderer frontSprite = front.GetComponent<SpriteRenderer>();
        SpriteRenderer backSprite = back.GetComponent<SpriteRenderer>();

        frontSprite.sprite = Resources.Load<Sprite>(cardInfo.frontSpritePath);
        backSprite.sprite = Resources.Load<Sprite>(cardInfo.backSpritePath);
    }
}
