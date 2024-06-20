using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy's move
/// </summary>
public class EnemyRound : BaseState
{
    public override void EnterState(StateManager stateManager)
    {
        if(stateManager.CardDrag != null)
            stateManager.CardDrag.enabled = false;
        
        stateManager.Attack.enabled = false;
        stateManager.Outline.effectDistance = new Vector2(0, 0);
    }
}
