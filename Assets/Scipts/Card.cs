using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Card
{
    public string SpritePath;

    public Card(string SpritePath)
    {
        this.SpritePath = SpritePath;
    }
}
