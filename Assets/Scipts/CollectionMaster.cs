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

        IAbility sharpVision = new SharpVision(0);
        IAbility camouflage = new Ñamouflage(1);
        IAbility fatTissue = new Fat_Tissue(2);
        IAbility highBodyWeight = new High_Body_Weight(3);
        IAbility carnivorous = new Carnivorous(4);

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

        IAbility sharpVision = new SharpVision(0);
        IAbility camouflage = new Ñamouflage(1);
        IAbility fatTissue = new Fat_Tissue(2);

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
                cardCollection.Add(new CardInfo("Ñamouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue, cardID));
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
    public int ID;
    public SharpVision(int ID)
    {
        this.ID = ID;
    }
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
    public int GetAbility() { return ID; }
}

public class Ñamouflage : IAbility
{
    int ID;
    public Ñamouflage(int ID)
    {
        this.ID = ID;
    }
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
    public int GetAbility() { return ID; }
}

public class Fat_Tissue : IAbility
{
    int ID;
    public Fat_Tissue(int ID)
    {
        this.ID = ID;
    }
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
    public int GetAbility() { return ID; }
}

public class High_Body_Weight : IAbility
{
    int ID;
    public High_Body_Weight(int ID)
    {
        this.ID = ID;
    }
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
    int ID;
    public Carnivorous(int ID)
    {
        this.ID = ID;
    }
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