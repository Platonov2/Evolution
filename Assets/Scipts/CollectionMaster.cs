using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionMaster : MonoBehaviour
{
    public static CollectionMaster Instance { get; private set; }

    public List<CardInfo> cardCollection = new List<CardInfo>();

    // ��� ������� ���� ����������� ������ ���� ����
    void Awake()
    {
        Instance = this;

        IAbility sharpVision = new SharpVision();
        IAbility camouflage = new �amouflage();
        IAbility fatTissue = new Fat_Tissue();
        IAbility highBodyWeight = new High_Body_Weight();
        IAbility carnivorous = new Carnivorous();

        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue));
        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue));
        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue));
        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue));
        cardCollection.Add(new CardInfo("�amouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue));
        cardCollection.Add(new CardInfo("�amouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue));
        cardCollection.Add(new CardInfo("�amouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue));
        cardCollection.Add(new CardInfo("�amouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue));
        cardCollection.Add(new CardInfo("High_Body_Weight-Carnivorous", highBodyWeight, carnivorous));
        cardCollection.Add(new CardInfo("High_Body_Weight-Carnivorous", highBodyWeight, carnivorous));
        cardCollection.Add(new CardInfo("High_Body_Weight-Carnivorous", highBodyWeight, carnivorous));
        cardCollection.Add(new CardInfo("High_Body_Weight-Carnivorous", highBodyWeight, carnivorous));
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

public class �amouflage : IAbility
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
        Debug.Log(!attakingCreature.sharpVision + " �amouflage");
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