using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndRound : MonoBehaviour
{
    #region -----Variables-----
    public delegate void OnStartNextRound();
    public static event OnStartNextRound EndRoundEvent;

    public delegate void OnEnemyRound();
    public static event OnEnemyRound EnemyRoundEvent;

    public delegate void GameEnded();
    public static event GameEnded OnGameEndedEvent;

    [SerializeField] private Button playerButton;
    [SerializeField] private Money money;
    [SerializeField] private CardDeck cardeck;

    private StateManager[] stateManagers;
    public static int Round { get; set; }
    public static int AdditionalMoney { get; set; }
    #endregion

    private void Awake() => Round = 1;

    /// <summary> Set the game to the next round. </summary>
    public void NextRound()
    {
        if (cardeck.CardsAvailable <= 0 && DrawCards.PlayerCardsInHand <= 0 && CheckDropzone.IsPlayerDropzoneEmpty)
        {
            OnGameEndedEvent();
            return;
        }
           
        stateManagers = FindObjectsOfType<StateManager>();
        EndRoundEvent();
        Round++;

        if (Round % 2 == 0) EnemyRound();
        else PlayerRound();
    }

    #region Rounds
    private void PlayerRound()
    {
        for (int i = 0; i < stateManagers.Length; i++)
            stateManagers[i].SwitchRound(StateManager.playerRound);

        playerButton.interactable = true;

        money.UpdateMoney(AdditionalMoney);
        AdditionalMoney = 0;
    }

    private void EnemyRound()
    {
        for (int i = 0; i < stateManagers.Length; i++)
            stateManagers[i].SwitchRound(StateManager.enemyRound);

        EnemyRoundEvent();

        playerButton.interactable = false;
    }
    #endregion
}
