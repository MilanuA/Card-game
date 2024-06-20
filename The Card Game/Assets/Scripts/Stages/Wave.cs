using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary> Taking care of enemy waves each round.  </summary>
public class Wave : MonoBehaviour
{
    public delegate void OnPlayerHeatlh(int dmg);
    public static event OnPlayerHeatlh DamagePlayerHeatlh;

    [SerializeField] private LoopStage stage;
    [Space(15)]
    [SerializeField] private GameObject enemyCardPrefab;
    [SerializeField] private GameObject enemyArea;
    [SerializeField] private EndRound round;
    [SerializeField] private CardsListHolder cardsListHolder;


    private void OnEnable()
    {
        EndRound.EnemyRoundEvent += EnemyWave;
        stage.WaveNumber = 0;
    }

    private void OnDisable()
    {
        EndRound.EnemyRoundEvent -= EnemyWave;
    }


    #region Wave methods
    public void EnemyWave() => StartCoroutine(EnemyWaveCoroutine());

    /// <summary> 
    /// 1) Destroy cards in the enemy deck
    /// 2) Attack random player card
    /// 3) Spawn enemy cards in the wave
    /// 4) Start next round
    /// </summary>
    IEnumerator EnemyWaveCoroutine()
    {
        yield return new WaitForSeconds(.5f);
        AttackPlayer();
        yield return new WaitForSeconds(.5f);
        SpawnWave();
        yield return new WaitForSeconds(.5f);
        stage.WaveNumber++;

        round.NextRound();
    }

    /// <summary> 
    /// 1) Attack player's cards, which are in the dropzone
    /// 2) Spawn the enemy cards
    /// </summary>

    private void SpawnWave() => stage.InitializeWave(enemyArea, cardsListHolder, enemyCardPrefab);

    private async void AttackPlayer()
    {
     
        for (int i = 0; i < cardsListHolder.EnemyCardsList.Count; i++)
        {
            Enemy attackingEnemy = cardsListHolder.EnemyCardsList[i].GetComponent<Enemy>();

            if (attackingEnemy.IsFreezed) return;

            else if (cardsListHolder.FriendlyCardsList.Count <= 0)
            {
                DamagePlayerHeatlh(attackingEnemy.AttackDamage);
            }
            else if (cardsListHolder.FriendlyCardsList.Count > 0)
            {
                PlayerCard tempCard = cardsListHolder.FriendlyCardsList[Random.Range(0, cardsListHolder.FriendlyCardsList.Count)].GetComponent<PlayerCard>();
                attackingEnemy.CardStats.Events.WhenPlayed?.Invoke(tempCard, attackingEnemy);
                await Task.Delay(200);
                if (tempCard == null) return;
                attackingEnemy.Attack(tempCard, false, attackingEnemy);
                attackingEnemy.TakeDamage(tempCard.AttackDamage, tempCard, tempCard);
            }
        }
    }
    #endregion
}
