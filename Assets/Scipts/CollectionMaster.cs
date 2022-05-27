using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionMaster : MonoBehaviour
{
    public static CollectionMaster Instance { get; private set; }

    public List<CardInfo> cardCollection = new List<CardInfo>();

    // Ïðè çàïóñêå èãðû ôîðìèðóåòñÿ ñïèñîê âñåõ êàðò
    void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        cardCollection.Clear();

        IAbility sharpVision = new SharpVision();
        IAbility camouflage = new Ñamouflage();
        IAbility fatTissue = new Fat_Tissue();
        IAbility highBodyWeight = new High_Body_Weight();
        IAbility carnivorous = new Carnivorous();

        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue, 0));
        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue, 0));
        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue, 0));
        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue, 0));

        cardCollection.Add(new CardInfo("Ñamouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue, 1));
        cardCollection.Add(new CardInfo("Ñamouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue, 1));
        cardCollection.Add(new CardInfo("Ñamouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue, 1));
        cardCollection.Add(new CardInfo("Ñamouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue, 1));

        cardCollection.Add(new CardInfo("High_Body_Weight-Carnivorous", highBodyWeight, carnivorous, 2));
        cardCollection.Add(new CardInfo("High_Body_Weight-Carnivorous", highBodyWeight, carnivorous, 2));
        cardCollection.Add(new CardInfo("High_Body_Weight-Carnivorous", highBodyWeight, carnivorous, 2));
        cardCollection.Add(new CardInfo("High_Body_Weight-Carnivorous", highBodyWeight, carnivorous, 2));

        cardCollection = Shuffle(cardCollection);
    }

    private List<CardInfo> Shuffle(List<CardInfo> cards)
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

    public void Create(List<int> deck)
    {
        cardCollection.Clear();

        IAbility sharpVision = new SharpVision();
        IAbility camouflage = new Ñamouflage();
        IAbility fatTissue = new Fat_Tissue();
        IAbility highBodyWeight = new High_Body_Weight();
        IAbility carnivorous = new Carnivorous();

        foreach (int cardID in deck)
        {
            if (cardID == 0)
            {
                cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue, cardID));
            }
            if (cardID == 1)
            {
                cardCollection.Add(new CardInfo("Ñamouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue, cardID));
            }
            if (cardID == 2)
            {
                cardCollection.Add(new CardInfo("High_Body_Weight-Carnivorous", highBodyWeight, carnivorous, cardID));
            }
        }

        Debug.Log("after collection creation " + cardCollection.Count);
    }

    public List<int> GetOrders()
    {
        List<int> res = new List<int>();

        foreach (CardInfo card in cardCollection)
        {
            res.Add(card.ID);
        }

        return res;
    }
}

public class SharpVision: IAbility
{
    public void OnPlay(Creature creature)
    {
        creature.sharpVision = true;
    }
    public void OnEnemyPlay() { }
    public void OnDestroy() { }
    public void OnEat() { }
    public void OnUse() { }
    public void OnAttack() { }
    public bool CanDefend(Creature attakingCreature) { return false; }
    public void OnDie() { }
}

public class Ñamouflage : IAbility
{
    public void OnPlay(Creature creature)
    {
        creature.camouflage = true;
    }
    public void OnEnemyPlay() { }
    public void OnDestroy() { }
    public void OnEat() { }
    public void OnUse() { }
    public void OnAttack() { }
    public bool CanDefend(Creature attakingCreature)
    {
        Debug.Log(!attakingCreature.sharpVision + " Ñamouflage");
        return !attakingCreature.sharpVision;
    }
    public void OnDie() { }
}

public class Fat_Tissue : IAbility
{
    public void OnPlay(Creature creature)
    {
        //Debug.Log(this);
    }
    public void OnEnemyPlay() { }
    public void OnDestroy() { }
    public void OnEat() { }
    public void OnUse() { }
    public void OnAttack() { }
    public bool CanDefend(Creature attakingCreature) { return false; }
    public void OnDie() { }
}

public class High_Body_Weight : IAbility
{
    public void OnPlay(Creature creature)
    {
        creature.hunger += 1;
        creature.highBodyWeight = true;
        //Debug.Log(this);
    }
    public void OnEnemyPlay() { }
    public void OnDestroy() { }
    public void OnEat() { }
    public void OnUse() { }
    public void OnAttack() { }
    public bool CanDefend(Creature attakingCreature)
    {
        Debug.Log(!attakingCreature.highBodyWeight + " High_Body_Weight");
        return !attakingCreature.highBodyWeight;
    }
    public void OnDie() { }
}

public class Carnivorous : IAbility
{
    public void OnPlay(Creature creature)
    {
        creature.hunger += 1;
        creature.carnivorous = true;
        creature.canAttack = true;
        //Debug.Log(this);
    }
    public void OnEnemyPlay() { }
    public void OnDestroy() { }
    public void OnEat() { }
    public void OnUse() { }
    public void OnAttack() { }
    public bool CanDefend(Creature attakingCreature) { return false; }
    public void OnDie() { }
}