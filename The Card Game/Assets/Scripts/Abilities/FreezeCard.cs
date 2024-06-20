using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FreezeCard", menuName = "Abilities/FreezeCard")]
public class FreezeCard : Ability
{
    public Color freezedColor;

    public override void InitializeAbility(Card attackedCard, Card attackingCard)
    {
        attackedCard.IsFreezed = true;
        attackedCard.Freezed(freezedColor);
    }
}
