using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoopStage", menuName = "Stage/LoopStage")]
public class LoopStage : ScriptableObject
{
    public delegate void OnDestroyCards(int numberOfCards);
    public static event OnDestroyCards DestroyCards;

    public Stage[] stages;

    public int WaveNumber { get; set; } = 0;

    /// <summary>
    /// 1) Check for the wave number - if it's bigger than the lenght of stages, reset wavenumber to 0
    /// 2) Check if the dropzone isn't full or if they are any cards to spawn
    /// 3) Destory cards in enemy hands based on how many cards are in the current stage
    /// 4) Finally instantiate the card prefab with the card constructor to dropzone
    /// </summary>
    /// <param name="enemyArea">Enemy dropzone</param>
    /// <param name="cardsListHolder"></param>
    /// <param name="enemyCardPrefab">Enemy's card prefab</param>
    public void InitializeWave(GameObject enemyArea, CardsListHolder cardsListHolder, GameObject enemyCardPrefab)
    {    
        if (WaveNumber >= stages.Length) WaveNumber = 0;

        if (DropZone.MaxEnemyCards - stages[WaveNumber].CardsToSpawn.Length < 0
            || stages[WaveNumber].CardsToSpawn.Length <= 0
            || DrawCards.EnemyCardsInHand <= 0) return;


        DestroyCards(stages[WaveNumber].CardsToSpawn.Length);


        for (int i = 0; i < stages[WaveNumber].CardsToSpawn.Length; i++) 
        {
            GameObject enemyCard = Instantiate(enemyCardPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            Enemy tempEnemy = enemyCard.GetComponent<Enemy>();
            tempEnemy.CardStats = stages[WaveNumber].CardsToSpawn[i];
            tempEnemy.SpawnCard();

            cardsListHolder.EnemyCardsList.Add(tempEnemy.gameObject);
            enemyCard.transform.SetParent(enemyArea.transform, false);

            DrawCards.EnemyCardsInHand--;
            DropZone.MaxEnemyCards--;
        }
    }
}

[System.Serializable]
public struct Stage
{
    public CardConstructor[] CardsToSpawn;
}
