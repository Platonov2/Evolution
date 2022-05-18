using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public List<IAbility> abilities;
    public List<GameObject> abilityCards;
    public List<GameObject> foodTokens;
    public int hunger;

    public void Initialize()
    {
        abilities = new List<IAbility>();
        abilityCards = new List<GameObject>();
        foodTokens = new List<GameObject>();
        hunger = 1;
    }

    public void AddAbility(GameObject abilityCard, IAbility ability)
    {
        abilityCards.Add(abilityCard);
        abilities.Add(ability);

        abilityCard.transform.SetParent(transform, true);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        abilityCard.transform.localPosition = new Vector3(0, 0, abilityCards.Count * 1.5f);
        
        ability.OnPlay(this);
    }

    public bool HaveAbility(IAbility ability)
    {
        return abilities.Contains(ability);
    }

    public bool StillHunger()
    {
        return hunger > foodTokens.Count;
    }

    public void Feed(GameObject foodToken)
    {
        foodTokens.Add(foodToken);

        foodToken.transform.SetParent(transform, true);
    }
}
