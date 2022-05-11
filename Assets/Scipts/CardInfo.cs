using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInfo
{
    public string SpritePath;

    public IAbility mainAbility;
    public IAbility additionalAbility;

    public CardInfo(string SpritePath, IAbility mainAbility, IAbility additionalAbility)
    {
        this.SpritePath = SpritePath;
        this.mainAbility = mainAbility;
        this.additionalAbility = additionalAbility;
    }
}
