using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    public void DrawCardsToPlayer(int numberOfCards);
    public void DrawCardsToOpponent(int numberOfCards);
}
