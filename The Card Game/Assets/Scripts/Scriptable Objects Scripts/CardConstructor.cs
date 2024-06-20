using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="Card", menuName ="Card/CardConstructor")]
public class CardConstructor : ScriptableObject
{
    public AbilitiesEvents Events;
    [Space(10)]
    public string Name;
    public Sprite Sprite;
    [Space(10)]
    public int AttackDmg = 0;
    public int Health = 0;
    public int CoinCost = 0;
    [Space(10)]
    public CardTag Tag;
}

#region Other things
public enum CardTag
{
    Friendly,
    Enemy,
    Ligma
}

[System.Serializable]
public class AbilitiesEvents
{
    public UnityEvent<Card, Card> Killed;
    public UnityEvent<Card, Card> Hurt;
    public UnityEvent<Card, Card> WhenPlayed;
    public UnityEvent<Card, Card> Rush;
}
#endregion