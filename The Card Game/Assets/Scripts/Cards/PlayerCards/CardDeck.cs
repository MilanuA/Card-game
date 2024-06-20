using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDeck : DrawCards
{
    #region -----VARIABLES-----
    [Space(20)]
    [Header("Card deck")]
    [SerializeField] private TextMeshProUGUI cardText;
    public int CardsAvailable = 0;
    #endregion

    #region Unity methods
    private void OnEnable()
    { 
        EndRound.EndRoundEvent += TakeCardsFromDeck;
        CardDrag.DeleteCardEvent += CardInBin;
        PlayerCard.NewCardEvent += SwitchCard;
    }

    private void OnDisable() 
    { 
        EndRound.EndRoundEvent -= TakeCardsFromDeck;
        CardDrag.DeleteCardEvent -= CardInBin;
        PlayerCard.NewCardEvent += SwitchCard;
    }

    void Awake()
    {
        CardsAvailable = PlayerPrefs.GetInt("CardsInDeck");
        cardText.text = CardsAvailable.ToString();
        InitializeCards(maxCards);
    }
    #endregion

    /// <summary> Take cards from the deck, unless the player has full hands 
    /// and deck is empty. </summary>
    private void TakeCardsFromDeck()
    {
        if (PlayerCardsInHand >= maxCardsPlayerHand || CardsAvailable <= 0) return;

        CardsAvailable--;
        cardText.text = CardsAvailable.ToString();

        TakeCards();
    }

    /// <summary> When player puts the card in the bin. </summary>
    private void CardInBin()
    {
        PlayerCardsInHand--;
        CardsAvailable++; 
        cardText.text = CardsAvailable.ToString(); 
    }
}
