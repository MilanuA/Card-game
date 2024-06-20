using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Player's move </summary>
public class PlayerRound : BaseState
{
    public override void EnterState(StateManager stateManager)
    {
        if (stateManager.CardDrag != null)
            stateManager.CardDrag.enabled = true;

        stateManager.Attack.enabled = true;
        stateManager.Outline.effectDistance = new Vector2(4, 4);
    }
}

