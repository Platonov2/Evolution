using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionMaster : MonoBehaviour
{
    public static CollectionMaster Instance { get; private set; }

    public List<Card> cardCollection = new List<Card>();

    // Ïðè çàïóñêå èãðû ôîðìèðóåòñÿ ñïèñîê âñåõ êàðò
    void Awake()
    {
        Instance = this;

        cardCollection.Add(new Card("Sharp_Vision-Fat_Tissue-Backimage"));
        cardCollection.Add(new Card("Sharp_Vision-Fat_Tissue-Backimage"));
        cardCollection.Add(new Card("Sharp_Vision-Fat_Tissue-Backimage"));
        cardCollection.Add(new Card("Sharp_Vision-Fat_Tissue-Backimage"));
        cardCollection.Add(new Card("Sharp_Vision-Fat_Tissue-Backimage"));
        cardCollection.Add(new Card("Sharp_Vision-Fat_Tissue-Backimage"));
        cardCollection.Add(new Card("Ñamouflage-Fat_tissue-Backimage_Texture"));
        cardCollection.Add(new Card("Ñamouflage-Fat_tissue-Backimage_Texture"));
        cardCollection.Add(new Card("Ñamouflage-Fat_tissue-Backimage_Texture"));
        cardCollection.Add(new Card("Ñamouflage-Fat_tissue-Backimage_Texture"));
        cardCollection.Add(new Card("Ñamouflage-Fat_tissue-Backimage_Texture"));
        cardCollection.Add(new Card("Ñamouflage-Fat_tissue-Backimage_Texture"));
    }
}
