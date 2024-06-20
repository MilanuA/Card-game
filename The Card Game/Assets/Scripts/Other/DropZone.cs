using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    [Header("Max cards in the dropzone")]
    [SerializeField] private int maxCards = 5;
    /// <summary> Max cards in the dropzone. </summary>
    public static int MaxCards { get; set; }
    public static int MaxEnemyCards {get; set; }

    private void Awake()
    {
        MaxCards = maxCards;
        MaxEnemyCards = maxCards;
    }
}
