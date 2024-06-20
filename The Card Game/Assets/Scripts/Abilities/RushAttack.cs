using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RushAttack", menuName = "Abilities/RushAttack")]

public class RushAttack : Ability
{
    public delegate void OnPlayerHeatlh(int dmg);
    public static event OnPlayerHeatlh DamagePlayerHeatlh;

    private int random;

    public override void InitializeAbility(Card takingDamage, Card attackingCard)
    {
        switch (attackingCard.CardStats.Tag)
        {
            case CardTag.Friendly:
                Enemy(takingDamage);
                break;
            case CardTag.Enemy:
                Friendly(takingDamage);
                break;
            default:
                Debug.LogError("Rush attack failed");
                break;
        }
    }

    private void Friendly(Card takingDamage)
    {
        if (takingDamage.CardsListHolder.FriendlyCardsList.Count <= 0)
        {
            DamagePlayerHeatlh(takingDamage.CardStats.AttackDmg);
            return;
        }

        random = Random.Range(0, takingDamage.CardsListHolder.FriendlyCardsList.Count);
        takingDamage.Attack(takingDamage.CardsListHolder.FriendlyCardsList[random].GetComponent<Card>(), false, null);
    }

    private void Enemy(Card takingDamage)
    {
        if (takingDamage.CardsListHolder.EnemyCardsList.Count <= 0) return;

        random = Random.Range(0, takingDamage.CardsListHolder.EnemyCardsList.Count);
        takingDamage.Attack(takingDamage.CardsListHolder.EnemyCardsList[random].GetComponent<Card>(), true, null);
    }
}
