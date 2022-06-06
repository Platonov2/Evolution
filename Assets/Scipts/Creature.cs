using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public List<IAbility> abilities;
    public List<GameObject> abilityCards;
    public List<GameObject> foodTokens;

    public int hunger = 1;

    public bool carnivorous = false;
    public bool camouflage = false;
    public bool highBodyWeight = false; 
    public bool sharpVision = false;

    public bool canAttack = false;
    public FoodBaseMaster foodBaseMaster;
    public int pos;

    public void SetPos(int pos)
    {
        this.pos = pos;
    }

    public void Initialize()
    {
        foodBaseMaster = FoodBaseMaster.Instance;
        abilities = new List<IAbility>();
        abilityCards = new List<GameObject>();
        foodTokens = new List<GameObject>();
    }

    public void AddAbilityToOpponent(GameObject abilityCard, IAbility ability)
    {
        abilityCards.Add(abilityCard);
        abilities.Add(ability);

        abilityCard.transform.SetParent(transform, true);
        transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z - 1);
        abilityCard.transform.localPosition = new Vector3(0, abilityCards.Count * 2.0f, abilityCards.Count * 1.5f);

        ability.OnPlay(this);

        Debug.Log("Added ability");
    }

    public void AddAbilityToPlayer(GameObject abilityCard, IAbility ability)
    {
        abilityCards.Add(abilityCard);
        abilities.Add(ability);

        abilityCard.transform.SetParent(transform, true);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        abilityCard.transform.localPosition = new Vector3(0, 0, abilityCards.Count * 1.5f);

        ability.OnPlay(this);

        Card card = abilityCard.GetComponent<Card>();

        Body b = new Body(card.ID, 2, pos, card.isMainAbility);
        Debug.Log(b.card_type);
        Client.Instance.SendInfo(GameMaster.Instance.current.ID,
                                 GameMaster.Instance.roomId,
                                 Actions.placeCard,
                                 b);

        Debug.Log("Added ability");
    }

    private void AddAbility(GameObject abilityCard, IAbility ability)
    {
        abilityCards.Add(abilityCard);
        abilities.Add(ability);

        abilityCard.transform.SetParent(transform, true);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        abilityCard.transform.localPosition = new Vector3(0, 0, abilityCards.Count * 1.5f);
        
        ability.OnPlay(this);

        Card card = abilityCard.GetComponent<Card>();

        Body b = new Body(card.ID, 2, pos, card.isMainAbility);
        Debug.Log(b.card_type);
        Client.Instance.SendInfo(GameMaster.Instance.current.ID,
                                 GameMaster.Instance.roomId,
                                 Actions.placeCard,
                                 b);

        Debug.Log("Added ability");
    }

    public void Die()
    {
        foreach (var ability in abilities)
        {
            ability.OnDie();
        }
    }

    public void Attack(GameObject victimCreature)
    {
        if (canAttack && CanEat(victimCreature))
        {
            FeedBlue(2);
            TransformController transformController = this.GetComponent<TransformController>();
            transformController.DisableHighLiteRed();
            canAttack = false;

            Player playerScript = transform.parent.transform.parent.GetComponent<Player>();
            playerScript.DestroyCreature(victimCreature);
            Destroy(victimCreature);

            Creature targetCreature = victimCreature.GetComponent<Creature>();
            var victimParent = victimCreature.transform.parent.transform.parent;
            Player player = victimParent.GetComponent<Player>();

            Body b = new Body(targetCreature.pos, 0, this.pos, false);
            Client.Instance.SendInfo(GameMaster.Instance.current.ID,
                                     player.ID,
                                     Actions.attack,
                                     b);
        }
    }

    public void AttackOpponent(GameObject victimCreature)
    {
        FeedBlue(2);
        TransformController transformController = this.GetComponent<TransformController>();
        transformController.DisableHighLiteRed();
        canAttack = false;

        Player playerScript = transform.parent.transform.parent.GetComponent<Player>();
        playerScript.DestroyCreature(victimCreature);
        Destroy(victimCreature);
    }

    public bool CanEat(GameObject victimCreature)
    {
        Creature victimController = victimCreature.GetComponent<Creature>();

        bool canDefend = false;

        if (victimController.abilities != null)
        {
            foreach (var ability in victimController.abilities)
            {
                canDefend = ability.CanDefend(this);
            }
        }
        return !canDefend;
    }

    public bool HaveAbility(IAbility ability)
    {
        return abilities.Contains(ability);
    }

    public bool CanAttack()
    {
        return StillHunger() && carnivorous && canAttack;
    }

    public bool StillHunger()
    {
        return hunger > foodTokens.Count;
    }

    public void FeedRed(GameObject foodToken)
    {
        foodTokens.Add(foodToken);

        foodToken.transform.SetParent(transform, true);

        Body b = new Body(-1, -1, pos, false);
        b.feed = 0;
        Client.Instance.SendInfo(GameMaster.Instance.current.ID,
                                 GameMaster.Instance.roomId,
                                 Actions.feed,
                                 b);
    }

    public void FeedRedOpponent(GameObject foodToken)
    {
        Debug.Log("feed opponent red");
        foodTokens.Add(foodToken);
        foodToken.transform.SetParent(transform, true);
        foodToken.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 3 + 2.5f);

        foodToken.transform.SetParent(transform, true);
    }

    public void FeedBlue(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (StillHunger())
            {
                var blueToken = foodBaseMaster.GetBlueFood();
                foodTokens.Add(blueToken);

                Food foodController = blueToken.GetComponent<Food>();

                blueToken.transform.SetParent(transform, true);
                blueToken.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 3 + i * 2.5f);

                /*Body b = new Body(-1, -1, pos, false);
                b.feed = 1;
                Client.Instance.SendInfo(GameMaster.Instance.current.ID,
                                         GameMaster.Instance.roomId,
                                         Actions.feed,
                                         b);*/
            }
            else break;
            //foodController.MoveTo(transform.position);
        }
    }

    public void FeedBlue()
    {
        var blueToken = foodBaseMaster.GetBlueFood();
        foodTokens.Add(blueToken);

        Food foodController = blueToken.GetComponent<Food>();

        blueToken.transform.SetParent(transform, true);
        blueToken.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 1.5f);
    }
}
