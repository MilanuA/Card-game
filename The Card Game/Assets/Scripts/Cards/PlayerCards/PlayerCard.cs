using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.Events;

public class PlayerCard : Card
{
    #region -----Variables-----
    public delegate void OnNewCard();
    public static event OnNewCard NewCardEvent;

    [Header("The card prefab")]
    [SerializeField] private GameObject cardPrefab;
    [Header("Show some stuff")]
    [SerializeField] private TextMeshProUGUI moneyText;
    private Outline outline;   
    private StateManager stateManager;

    #region Public variables

    public static CardConstructor CardConstructor { get; set; }
    /// <summary> Has player enough money to put the card in the dropzoen? </summary>
    public bool HasMoney { get; private set; } = false;
    #endregion
    #endregion

    #region Unity Methods
    private void Awake()
    {
        GetComponents();
        SpawnCard();

        if (EndRound.Round == 1 && DrawCards.CanBeChanged)
            this.gameObject.AddComponent<CardChange>();
    }

    private void Update()
    {
        if (StateManager.currentRound == StateManager.playerRound)
            CanBePlayed();
    }
    #endregion

    #region Card helping methods
    /// <summary> 
    /// If it's the first level and card can be changed => 
    /// 1) call the event
    /// 2) change the card constructor 
    /// 3) assign the card info, so the player can see it
    /// </summary>
    public void ChangeCard()
    {
        NewCardEvent();
        SpawnCard();
    }

    /// <summary> Asing info about the card to the card prefab. </summary>
    private void SpawnCard()
    {
        CardStats = CardConstructor;

        CreateCard();

        cardPrefab.name = CardStats.Name;
        cardPrefab.tag = CardStats.Tag.ToString();
        moneyText.text = CardStats.CoinCost.ToString();

        if (CompareTag("Ligma")) 
        {
            healthObject.SetActive(false);
            attackDmgObject.SetActive(false);
        } 
        else 
        {
            healthObject.SetActive(true);
            attackDmgObject.SetActive(true);
        }
    }

    /// <summary>Check if the card can be played, if yes, give it the outline. 
    /// Otherwise disable the outline. </summary>
    private void CanBePlayed()
    { 
        if ((Money.MoneyAmount - CardStats.CoinCost) >= 0 
            && DropZone.MaxCards > 0 
            && stateManager.currentState != StateManager.inDropzoneState)
        {
            HasMoney = true;
            this.outline.effectDistance = new Vector2(4, 4);
        }
        else
        {
            HasMoney = false;
            this.outline.effectDistance = new Vector2(0, 0);
        }
    }
    #endregion

    #region Cache the components
    /// <summary> Cache all the components </summary>
    private void GetComponents()
    {
        CardStats = CardConstructor;
        stateManager = GetComponent<StateManager>();
        outline = GetComponent<Outline>();
    }
    #endregion
}
