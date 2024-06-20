using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GiveGold", menuName = "Abilities/GiveGold")]
public class GiveGold : Ability
{
    public int MoneyAmount;

    public override void InitializeAbility(Card attackedCard, Card attackingCard) => EndRound.AdditionalMoney += MoneyAmount;
}
