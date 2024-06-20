using TMPro;
using UnityEngine;

[System.Serializable]
public struct PlayerHealthStats
{
    public TextMeshProUGUI PlayerHealthText;
    [Header("Max health")]
    public int PlayerMaxHealth;
}