using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeck : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject enemyHand;
    #endregion

    private void OnEnable() => LoopStage.DestroyCards += DestroyCards;

    private void OnDisable() => LoopStage.DestroyCards -= DestroyCards;


    private void DestroyCards(int numberOfCards)
    {
        if (enemyHand.transform.childCount <= 0) return;

        for (int i = 0; i < numberOfCards; i++)
           Destroy(enemyHand.transform.GetChild(i).gameObject); 
    }
}
