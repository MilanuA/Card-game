using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Enemy : Card
{
    private void Awake() 
    {
        healthObject.SetActive(false);
        attackDmgObject.SetActive(false);
        AbilityReady = true;
    }

    /// <summary> Show the info about the enemy card. </summary>
    public void SpawnCard()
    {
        CreateCard();

        healthObject.SetActive(true);
        attackDmgObject.SetActive(true) ;

        gameObject.name = CardStats.name;
        CardStats.Events.Rush?.Invoke(this, this);
    }
}
