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

        IAbility sharpVision = new SharpVision();
        IAbility camouflage = new Ñamouflage();
        IAbility fatTissue = new Fat_Tissue();
        IAbility highBodyWeight = new High_Body_Weight();
        IAbility carnivorous = new Carnivorous();
        IAbility parasite = new Parasite();
        IAbility borrowing = new Borrowing();
        IAbility poisonous = new Poisonous();
        IAbility swimming = new Swimming();
        IAbility vivaparous = new Vivaparous();

        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue));
        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue));
        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue));
        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue));
        cardCollection.Add(new CardInfo("Ñamouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue));
        cardCollection.Add(new CardInfo("Ñamouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue));
        cardCollection.Add(new CardInfo("Ñamouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue));
        cardCollection.Add(new CardInfo("Ñamouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue));
        cardCollection.Add(new CardInfo("High_Body_Weight-Carnivorous", highBodyWeight, carnivorous));
        cardCollection.Add(new CardInfo("High_Body_Weight-Carnivorous", highBodyWeight, carnivorous));
        cardCollection.Add(new CardInfo("High_Body_Weight-Carnivorous", highBodyWeight, carnivorous));
        cardCollection.Add(new CardInfo("High_Body_Weight-Carnivorous", highBodyWeight, carnivorous));
        cardCollection.Add(new CardInfo("Parasite-Carnivorous-Backimage", parasite, carnivorous));
        cardCollection.Add(new CardInfo("Parasite-Carnivorous-Backimage", parasite, carnivorous));
        cardCollection.Add(new CardInfo("Parasite-Carnivorous-Backimage", parasite, carnivorous));
        cardCollection.Add(new CardInfo("Parasite-Carnivorous-Backimage", parasite, carnivorous));
        cardCollection.Add(new CardInfo("Borrowing-Fat_Tissue-Backimage", borrowing, fatTissue));
        cardCollection.Add(new CardInfo("Borrowing-Fat_Tissue-Backimage", borrowing, fatTissue));
        cardCollection.Add(new CardInfo("Borrowing-Fat_Tissue-Backimage", borrowing, fatTissue));
        cardCollection.Add(new CardInfo("Borrowing-Fat_Tissue-Backimage", borrowing, fatTissue));
        cardCollection.Add(new CardInfo("Poisonous_Carnivorous-Backimage", poisonous, carnivorous));
        cardCollection.Add(new CardInfo("Poisonous_Carnivorous-Backimage", poisonous, carnivorous));
        cardCollection.Add(new CardInfo("Poisonous_Carnivorous-Backimage", poisonous, carnivorous));
        cardCollection.Add(new CardInfo("Poisonous_Carnivorous-Backimage", poisonous, carnivorous));
        cardCollection.Add(new CardInfo("Swimming", swimming, null));
        cardCollection.Add(new CardInfo("Swimming", swimming, null));
        cardCollection.Add(new CardInfo("Swimming", swimming, null));
        cardCollection.Add(new CardInfo("Swimming", swimming, null));
        cardCollection.Add(new CardInfo("Vivaparous-Swimming-Backimage", vivaparous, swimming));
        cardCollection.Add(new CardInfo("Vivaparous-Swimming-Backimage", vivaparous, swimming));
        cardCollection.Add(new CardInfo("Vivaparous-Swimming-Backimage", vivaparous, swimming));
        cardCollection.Add(new CardInfo("Vivaparous-Swimming-Backimage", vivaparous, swimming));
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
    public void OnEat(Creature creature, Player creatureOwner) { }
    public void OnUse() { }
    public void OnAttack() { }
    public bool CanPlay(Creature choosenCreature)
    {
        return (choosenCreature.gameObject.layer == LayerMask.NameToLayer("YourCreature")) && (!choosenCreature.abilities.Contains(this));
    }
    public bool CanDefend(Creature attakingCreature, Creature defendingCreature) { return false; }
    public void OnDie(GameObject killerCreature) { }
}

public class Ñamouflage : IAbility
{
    public void OnPlay(Creature creature)
    {
        creature.camouflage = true;
    }
    public void OnEnemyPlay() { }
    public void OnDestroy() { }
    public void OnEat(Creature creature, Player creatureOwner) { }
    public void OnUse() { }
    public void OnAttack() { }
    public bool CanPlay(Creature choosenCreature)
    {
        return (choosenCreature.gameObject.layer == LayerMask.NameToLayer("YourCreature")) && (!choosenCreature.abilities.Contains(this));
    }
    public bool CanDefend(Creature attakingCreature, Creature defendingCreature)
    {
        Debug.Log(!attakingCreature.sharpVision + " Ñamouflage");
        return !attakingCreature.sharpVision;
    }
    public void OnDie(GameObject killerCreature) { }
}

public class Fat_Tissue : IAbility
{
    public void OnPlay(Creature creature)
    {
        //Debug.Log(this);
    }
    public void OnEnemyPlay() { }
    public void OnDestroy() { }
    public void OnEat(Creature creature, Player creatureOwner) { }
    public void OnUse() { }
    public void OnAttack() { }
    public bool CanPlay(Creature choosenCreature)
    {
        return (choosenCreature.gameObject.layer == LayerMask.NameToLayer("YourCreature")) && (!choosenCreature.abilities.Contains(this));
    }
    public bool CanDefend(Creature attakingCreature, Creature defendingCreature) { return false; }
    public void OnDie(GameObject killerCreature) { }
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
    public void OnEat(Creature creature, Player creatureOwner) { }
    public void OnUse() { }
    public void OnAttack() { }
    public bool CanPlay(Creature choosenCreature)
    {
        return (choosenCreature.gameObject.layer == LayerMask.NameToLayer("YourCreature")) && (!choosenCreature.abilities.Contains(this));
    }
    public bool CanDefend(Creature attakingCreature, Creature defendingCreature)
    {
        Debug.Log(!attakingCreature.highBodyWeight + " High_Body_Weight");
        return !attakingCreature.highBodyWeight;
    }
    public void OnDie(GameObject killerCreature) { }
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
    public void OnEat(Creature creature, Player creatureOwner) { }
    public void OnUse() { }
    public void OnAttack() { }
    public bool CanPlay(Creature choosenCreature)
    {
        return (choosenCreature.gameObject.layer == LayerMask.NameToLayer("YourCreature")) && (!choosenCreature.abilities.Contains(this));
    }
    public bool CanDefend(Creature attakingCreature, Creature defendingCreature) { return false; }
    public void OnDie(GameObject killerCreature) { }
}

public class Parasite : IAbility
{
    public void OnPlay(Creature creature)
    {
        creature.hunger += 2;
        creature.parasite = true;
    }
    public void OnEnemyPlay() { }
    public void OnDestroy() { }
    public void OnEat(Creature creature, Player creatureOwner) { }
    public void OnUse() { }
    public void OnAttack() { }
    public bool CanPlay(Creature choosenCreature)
    {
        var creatureLayer = choosenCreature.gameObject.layer;
        return (creatureLayer == LayerMask.NameToLayer("YourCreature") || creatureLayer == LayerMask.NameToLayer("OpponentCreature")) && (!choosenCreature.abilities.Contains(this));
    }
    public bool CanDefend(Creature attakingCreature, Creature defendingCreature) { return false; }
    public void OnDie(GameObject killerCreature) { }
}

public class Borrowing : IAbility
{
    public void OnPlay(Creature creature)
    {
        creature.borrowing = true;
    }
    public void OnEnemyPlay() { }
    public void OnDestroy() { }
    public void OnEat(Creature creature, Player creatureOwner) { }
    public void OnUse() { }
    public void OnAttack() { }
    public bool CanPlay(Creature choosenCreature)
    {
        return (choosenCreature.gameObject.layer == LayerMask.NameToLayer("YourCreature")) && (!choosenCreature.abilities.Contains(this));
    }
    public bool CanDefend(Creature attakingCreature, Creature defendingCreature) 
    { 
        return !defendingCreature.StillHunger();
    }
    public void OnDie(GameObject killerCreature) { }
}

public class Poisonous : IAbility
{
    public void OnPlay(Creature creature)
    {
        creature.poisonous = true;
    }
    public void OnEnemyPlay() { }
    public void OnDestroy() { }
    public void OnEat(Creature creature, Player creatureOwner) { }
    public void OnUse() { }
    public void OnAttack() { }
    public bool CanPlay(Creature choosenCreature)
    {
        return (choosenCreature.gameObject.layer == LayerMask.NameToLayer("YourCreature")) && (!choosenCreature.abilities.Contains(this));
    }
    public bool CanDefend(Creature attakingCreature, Creature defendingCreature){ return false; }
    public void OnDie(GameObject killerCreature) 
    {
        if (killerCreature != null)
        {
            Creature CreatureController = killerCreature.GetComponent<Creature>();
            CreatureController.Die(null);
        }
    }
}

public class Swimming : IAbility
{
    public void OnPlay(Creature creature)
    {
        creature.swimming = true;
    }
    public void OnEnemyPlay() { }
    public void OnDestroy() { }
    public void OnEat(Creature creature, Player creatureOwner) { }
    public void OnUse() { }
    public void OnAttack() { }
    public bool CanPlay(Creature choosenCreature)
    {
        return (choosenCreature.gameObject.layer == LayerMask.NameToLayer("YourCreature")) && (!choosenCreature.abilities.Contains(this));
    }
    public bool CanDefend(Creature attakingCreature, Creature defendingCreature)
    {
        return !attakingCreature.swimming;
    }
    public void OnDie(GameObject killerCreature) { }
}

public class Vivaparous : IAbility
{
    public void OnPlay(Creature creature)
    {
        creature.hunger += 1;
    }
    public void OnEnemyPlay() { }
    public void OnDestroy() { }
    public void OnEat(Creature creature, Player creatureOwner) 
    {
        if (!creature.StillHunger())
        {
            Debug.Log(creatureOwner);
            creatureOwner.CreateCreatureAndFeed();
        }
    }
    public void OnUse() { }
    public void OnAttack() { }
    public bool CanPlay(Creature choosenCreature)
    {
        return (choosenCreature.gameObject.layer == LayerMask.NameToLayer("YourCreature")) && (!choosenCreature.abilities.Contains(this));
    }
    public bool CanDefend(Creature attakingCreature, Creature defendingCreature){ return false; }
    public void OnDie(GameObject killerCreature) { }
}