using TMPro;
using UnityEngine;

[System.Serializable]
public struct EnemyHealthStats
{
    public TextMeshProUGUI EnemyHealthText;
    [Header("Max health")]
    public int EnemyMaxHealth;
}