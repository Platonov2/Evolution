using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Card
{
    public string frontSpritePath;
    public string backSpritePath;

    public Card(string frontSpritePath)
    {
        this.frontSpritePath = frontSpritePath;
        this.backSpritePath = "BackImage";
    }
}
