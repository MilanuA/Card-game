using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DecreaseAbilities", menuName = "Abilities/DecreaseAbilities")]
public class DecreaseAbilities : Ability
{
    public int DecreaseAttackDamage;
    public int DecreaseHealth;
    [Tooltip("Is the card attacking enemy card?")]
    public bool AttackingEnemy;

    public override void InitializeAbility(Card recivingCard, Card attackingCard)
    {
        if (!attackingCard.AbilityReady) return;

        recivingCard.TakeDamage(DecreaseHealth, AttackingEnemy, attackingCard);
        recivingCard.AttackDamage -= DecreaseAttackDamage;
        recivingCard.UpdateDamageText();

        attackingCard.AbilityReady = false;

    }
}
