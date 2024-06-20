using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary> Card class. Here is everything which enemy and player cards have in common.  </summary>
public class Card : MonoBehaviour
{
    [SerializeField] protected Image cardImage;
    [SerializeField] protected TextMeshProUGUI healthText;
    [SerializeField] protected TextMeshProUGUI dmgText;
    [SerializeField] protected GameObject healthObject, attackDmgObject;
    public GameObject FreezedGameobject;
    public CardsListHolder CardsListHolder;

    public int Health { get; set; }
    public int AttackDamage { get; set; }
    public bool IsFreezed { get; set; }
    public CardConstructor CardStats { get; set; }
    public int CoinCost { get; protected set; }
    public bool AbilityReady { get; set; } = true;

    protected int freezedInRound = 0;

    #region Unity Methods
    private void OnEnable()
    {
        AbilityReady = true;
        CardsListHolder = GameObject.FindGameObjectWithTag("ManagerOffice").GetComponent<CardsListHolder>();

        EndRound.EndRoundEvent += UnFreeze;
    }

    private void OnDisable()
    {
        EndRound.EndRoundEvent -= UnFreeze;
    }
    #endregion

    public void CreateCard()
    {
        CoinCost = CardStats.CoinCost;
        AttackDamage = CardStats.AttackDmg;
        Health = CardStats.Health;
        cardImage.sprite = CardStats.Sprite;

        healthText.text = Health.ToString();
        dmgText.text = AttackDamage.ToString();
    }

    /// <summary> Take damage from other card. </summary>
    public void TakeDamage(int dmg, bool isEnemy, Card damagingCard)
    {
        Health -= dmg;

        if (Health <= 0 && isEnemy) 
        {
            CardStats.Events.Killed?.Invoke(damagingCard, this);

            CardsListHolder.EnemyCardsList.Remove(this.gameObject);
            DropZone.MaxEnemyCards++;
            Destroy(this.gameObject);
        }
        else if (Health <= 0 && !isEnemy) 
        {
            CardStats.Events.Killed?.Invoke(damagingCard, this);

            CardsListHolder.FriendlyCardsList.Remove(this.gameObject);
            DropZone.MaxCards++;

            Destroy(this.gameObject);
        }

        healthText.text = Health.ToString();
    }


    public void UpdateDamageText()
    {
        if(AttackDamage > 0) dmgText.text = AttackDamage.ToString();
    }

    /// <summary>Attack other card </summary>
    /// <param name="card">Card which is attacking</param>
    /// <param name="enemyAttack">Is the attacked card an enemy card?</param>
    public void Attack(Card card, bool enemyAttack, Card damagingCard) => card.TakeDamage(AttackDamage, enemyAttack, damagingCard);

    #region Freezing
    public void Freezed(Color color)
    {
        FreezedGameobject.SetActive(true);
        cardImage.color = color;
        freezedInRound = EndRound.Round;
    }

    private void UnFreeze()
    {
        if(EndRound.Round == freezedInRound + 2)
        {
            cardImage.color = Color.white;
            IsFreezed = false;
            FreezedGameobject.SetActive(false);
        }   
    }
    #endregion
}
