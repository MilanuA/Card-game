using UnityEngine;
using UnityEngine.UI;

public class DrawCards : MonoBehaviour
{
    #region -----VARIABLES-----
    [Header("Friendly cards array")]
    [SerializeField] private CardConstructor[] cards;
    [Space(5)]
    [Header("Gameobjects to attach")]
    [SerializeField] private GameObject enemyCardPrefab;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] protected GameObject enemyHand;
    [SerializeField] protected GameObject playerHand; 
    [Space(15)]
    [Header("Card's info")]
    [Tooltip("Max cards in enemy's & player's hand at the beginning")]
    [SerializeField] protected int maxCards;
    [Tooltip("Max cards in player hand")]
    [SerializeField] protected int maxCardsPlayerHand = 9;
    public static int PlayerCardsInHand { get; set; }
    public static int EnemyCardsInHand { get; set; }
    public static bool CanBeChanged { get; set; }
    #endregion

    #region Card methods
    /// <summary> Take the cards from the deck. </summary>
    protected void TakeCards()
    {
        SpawnPlayerCards();
        PlayerCardsInHand++;

        if (EnemyCardsInHand < maxCardsPlayerHand) 
        {
            SpawnEnemyCards();
            EnemyCardsInHand++;
        }
    }

    /// <summary> Spawn enemy cards. </summary>
    private void SpawnEnemyCards()
    {
        GameObject enemyCard = Instantiate(enemyCardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        enemyCard.transform.SetParent(enemyHand.transform, false);
    }

    /// <summary> Spawn player cards. </summary>
    private void SpawnPlayerCards()
    {
        GameObject playerCard = Instantiate(RandomCard(), new Vector3(0, 0, 0), Quaternion.identity);
        playerCard.transform.SetParent(playerHand.transform, false);
    }

    /// <summary> Switch randomly card, which was clicked on. </summary>
    protected void SwitchCard() 
    {
      PlayerCard.CardConstructor = cards[Random.Range(0, cards.Length)];
    }

    /// <summary> Create random card from the friendly card deck  </summary>
    /// <returns> Radomly selected card from the deck</returns>
    private GameObject RandomCard()
    {
        PlayerCard.CardConstructor = cards[Random.Range(0, cards.Length)];
        return cardPrefab;
    }

    /// <summary> Spawn random cards to player & enemy decks at the beginning of the game. </summary>
    /// <param name="startMaxCards">Max cards which could be spawned when the game beggins</param>
    protected void InitializeCards(int startMaxCards)
    {
        EnemyCardsInHand = startMaxCards;
        PlayerCardsInHand = startMaxCards;

        for (int i = 0; i < startMaxCards; i++)
        {
            SpawnPlayerCards();
            SpawnEnemyCards();
        }
    }
    #endregion
}
