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

        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue));
        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue));
        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue));
        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue));
        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue));
        cardCollection.Add(new CardInfo("Sharp_Vision-Fat_Tissue-Backimage", sharpVision, fatTissue));
        cardCollection.Add(new CardInfo("Ñamouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue));
        cardCollection.Add(new CardInfo("Ñamouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue));
        cardCollection.Add(new CardInfo("Ñamouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue));
        cardCollection.Add(new CardInfo("Ñamouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue));
        cardCollection.Add(new CardInfo("Ñamouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue));
        cardCollection.Add(new CardInfo("Ñamouflage-Fat_tissue-Backimage_Texture", camouflage, fatTissue));
    }
}

public class SharpVision: IAbility
{
    public void OnPlay()
    {
        Debug.Log(this);
    }
    public void OnEnemyPlay() { }
    public void OnDestroy() { }
    public void OnEat() { }
    public void OnUse() { }
    public void OnAttack() { }
    public void OnDefend() { }
}

public class Ñamouflage : IAbility
{
    public void OnPlay()
    {
        Debug.Log(this);
    }
    public void OnEnemyPlay() { }
    public void OnDestroy() { }
    public void OnEat() { }
    public void OnUse() { }
    public void OnAttack() { }
    public void OnDefend() { }
}

public class Fat_Tissue : IAbility
{
    public void OnPlay()
    {
        Debug.Log(this);
    }
    public void OnEnemyPlay() { }
    public void OnDestroy() { }
    public void OnEat() { }
    public void OnUse() { }
    public void OnAttack() { }
    public void OnDefend() { }
}
