using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    #region Variables
    [Header("Scripts")]
    public PlayerCard PlayerCard;
    public CardDrag CardDrag;    
    public CardAttack Attack;

    [Header("Other things")]
    public Outline Outline;
    public GameObject CoinObject;
    #region States
    public BaseState currentState;

    public static InDeck inDeckState = new();
    public static InDropzone inDropzoneState = new();
    public static GameStart gameStartState = new();
    #endregion

    #region Rounds
    public static BaseState currentRound;

    public static PlayerRound playerRound = new();
    public static EnemyRound enemyRound = new();
    #endregion

    #endregion

    private void OnEnable()
    {
        Timer.TimerEndedEvent += SwitchState;
    }

    private void OnDisable()
    {
        Timer.TimerEndedEvent -= SwitchState;
    }

    private void Start()
    {
        if (EndRound.Round == 1)
            currentState = gameStartState;
        else
            currentState = inDeckState;

        if (EndRound.Round % 2 == 0)
            currentRound = enemyRound;
        else if(EndRound.Round % 2 != 0)
            currentRound = playerRound;

        currentRound.EnterState(this);
        currentState.EnterState(this);
    }

    public void SwitchState(BaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void SwitchRound(BaseState round)
    {
        currentRound = round;
        round.EnterState(this);
    }
}
