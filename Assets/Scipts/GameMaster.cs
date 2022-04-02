using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        DeckMaster.Instance.FillDeck(CollectionMaster.Instance.cardCollection);
    }
}
