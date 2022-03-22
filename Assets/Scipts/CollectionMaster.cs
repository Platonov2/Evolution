using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Структура с информацией о картах
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

    // При запуске игры формируется список всх карт
    void Awake()
    {
        cardCollection.Add(new Card("Sharp_Vision-Fat_Tissue", "BackImage"));
        cardCollection.Add(new Card("Parasite-Carnivorous", "BackImage"));
        cardCollection.Add(new Card("Сamouflage-Fat_tissue", "BackImage"));
    }

    void Start()
    {
        FillDeck();
    }

    void Update()
    {

    }

    // Метод заполнения колоды из карт в cardCollection
    void FillDeck()
    {
        // Z изменяется для того, чтобы карты были как будто "друг на друге". Хз насколько это пригодится
        int z = 0;
        foreach (Card card in this.cardCollection)
        {
            CreateCardInstance(card, new Vector3(deck.position.x, deck.position.y, z), Quaternion.identity);

            z -= 10;
        }
    }

    // Метод добавления новой карты на сцену
    // Для этого создаётся экземпляр преваба, которому задаются настройки из cardInfo
    void CreateCardInstance(Card cardInfo, Vector3 position, Quaternion rotation)
    {
        // Создание экземпляра префаба
        var card = Instantiate(this.cardPrefab, position, rotation);
        card.name = cardInfo.frontSpritePath;

        // Перемещение карты в колоду.
        // Второй аргумент отвечает за перерасчёт размеров и координат объекта относительно родителя. Всегда должен быть true
        card.SetParent(deck, true);

        // Добавление текстур
        var front = card.Find("Front");
        var back = card.Find("Back");

        SpriteRenderer frontSprite = front.GetComponent<SpriteRenderer>();
        SpriteRenderer backSprite = back.GetComponent<SpriteRenderer>();

        frontSprite.sprite = Resources.Load<Sprite>(cardInfo.frontSpritePath);
        backSprite.sprite = Resources.Load<Sprite>(cardInfo.backSpritePath);
    }
}
