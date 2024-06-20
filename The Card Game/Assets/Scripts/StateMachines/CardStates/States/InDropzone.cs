using UnityEngine;

/// <summary> When the card is in the dropzone, so it can attack enemy cards. </summary>
public class InDropzone : BaseState
{
    public delegate void OnDropZone(int cost);
    public static event OnDropZone InDropzoneEvent;

    public override void EnterState(StateManager stateManager)
    {
        InDropzoneEvent(stateManager.PlayerCard.CoinCost * -1);
        stateManager.Outline.effectDistance = new Vector2(0, 0);
        stateManager.Attack.CanAttack = false;
        stateManager.CoinObject.SetActive(false);

        DrawCards.PlayerCardsInHand--;
        DropZone.MaxCards--;
    }
}
