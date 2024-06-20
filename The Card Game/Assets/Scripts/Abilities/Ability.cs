using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> All abilities inherits from this class. </summary>
public abstract class Ability : ScriptableObject
{
    /// <summary>
    /// Initialize the ability.
    /// </summary>
    /// <param name="attackedCard">The card which is attacked by the ability.</param>
    /// <param name="attackingCard">The card which is attacking-</param>
    public abstract void InitializeAbility(Card attackedCard, Card attackingCard);
}
