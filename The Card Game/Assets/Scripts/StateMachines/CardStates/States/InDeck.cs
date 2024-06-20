using UnityEngine;

/// <summary> When the card is in the "player's hand" </summary>
public class InDeck : BaseState
{   
    public override void EnterState(StateManager stateManager)
    {
        if (EndRound.Round != 1) return;
        stateManager.CardDrag.enabled = true;
    }
}
