using UnityEngine;

/// <summary> Card's first state. It's only in the Round One and before the timer hits 0.  </summary>
public class GameStart : BaseState
{
    public override void EnterState(StateManager stateManager)
    {
        stateManager.CardDrag.enabled = false;
    }
}
