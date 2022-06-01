using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    public void OnPlay(Creature creature);
    public void OnEnemyPlay();
    public void OnDestroy();
    public void OnEat(Creature creature, Player creatureOwner);
    public void OnUse();
    public void OnAttack();
    public bool CanPlay(Creature choosenCreature);
    public bool CanDefend(Creature attakingCreature, Creature defendingCreature);
    public void OnDie(GameObject killerCreature);
}
