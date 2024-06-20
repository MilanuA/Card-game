using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Holds the list of the cards in the dropzone.  </summary>
public class CardsListHolder : MonoBehaviour
{
    public List<GameObject> FriendlyCardsList = new();
    public List<GameObject> EnemyCardsList = new();
}
