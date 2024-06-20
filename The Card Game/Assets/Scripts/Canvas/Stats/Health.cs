using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    #region Variables
    public delegate void EndGame();
    public static event EndGame OnGameEndedEvent;

    [SerializeField] private EnemyHealthStats enemyHealthStats;
    [SerializeField] private PlayerHealthStats playerHealthStats;
    #endregion

    private void OnEnable()
    {
        Wave.DamagePlayerHeatlh += DamagePlayer;
        RushAttack.DamagePlayerHeatlh += DamagePlayer;
        CardAttack.EnemyDamagedEvent += DamageEnemy;
    }

    private void OnDisable()
    {
        Wave.DamagePlayerHeatlh -= DamagePlayer;
        RushAttack.DamagePlayerHeatlh -= DamagePlayer;
        CardAttack.EnemyDamagedEvent -= DamageEnemy;
    }

    private void Awake()
    {
        enemyHealthStats.EnemyMaxHealth = PlayerPrefs.GetInt("Health");
        playerHealthStats.PlayerMaxHealth = PlayerPrefs.GetInt("Health");
        UpdateText();
    }

    private void UpdateText()
    {
        enemyHealthStats.EnemyHealthText.text = $"{enemyHealthStats.EnemyMaxHealth}";
        playerHealthStats.PlayerHealthText.text = $"{playerHealthStats.PlayerMaxHealth}";
    }

    #region Damage the main health
    private void DamagePlayer(int dmg)
    {
        playerHealthStats.PlayerMaxHealth -= dmg;
        UpdateText();

        if (playerHealthStats.PlayerMaxHealth > 0) return;
        OnGameEndedEvent();
    }

    private void DamageEnemy(int dmg)
    {
        enemyHealthStats.EnemyMaxHealth -= dmg;
        UpdateText();

        if (enemyHealthStats.EnemyMaxHealth > 0) return;
        OnGameEndedEvent();
    }
    #endregion
}
