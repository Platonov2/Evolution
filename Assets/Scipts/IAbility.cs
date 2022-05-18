using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    public void OnPlay(Creature creature);
    public void OnEnemyPlay();
    public void OnDestroy();
    public void OnEat();
    public void OnUse();
    public void OnAttack();
    public void OnDefend();
}
