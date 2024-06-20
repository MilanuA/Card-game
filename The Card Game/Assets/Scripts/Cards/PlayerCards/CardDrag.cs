using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDrag : MonoBehaviour
{
    #region -----Variables-----
    #region Events
    public delegate void OnDeleteCard();
    public static event OnDeleteCard DeleteCardEvent;
    #endregion

    [Header("The placeholder prefab")]
    [SerializeField] private GameObject placeholderPrefab;
    [Header("Other things")]
    [SerializeField] private StateManager StateManager;
    [SerializeField] private PlayerCard playerCard;

    private CardsListHolder cardsListHolder;
    private GameObject dropZone, instatiatedCard;
    private Transform startPosition;
    private bool isOverDropZone = false, isOverBin = false;
    #endregion

    #region Dragging Methods
    private void Awake() => cardsListHolder = GameObject.FindGameObjectWithTag("ManagerOffice").GetComponent<CardsListHolder>();

    /// <summary> Called when you start dragging the card. </summary>
    public void StartDragging()
    {
        if (!this.enabled) return;

        startPosition = this.transform.parent;

        /// Instantiate a prefab, which holds the position in player's hand.
        instatiatedCard = Instantiate(placeholderPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        instatiatedCard.transform.SetParent(this.transform.parent);
        instatiatedCard.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        this.transform.SetParent(this.transform.root, false);
    }

    public void Drag()
    {
        if (!this.enabled) return;

        transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        int newSiblingIndex = startPosition.childCount;

        for (int i = 0; i < startPosition.childCount; i++)
        {
            if (this.transform.position.x < startPosition.GetChild(i).position.x)
            {
                newSiblingIndex = i;
                if (instatiatedCard.transform.GetSiblingIndex() < newSiblingIndex) newSiblingIndex--;
                break;
            }
        }

        instatiatedCard.transform.SetSiblingIndex(newSiblingIndex);
    }

    /// <summary> When the player stop dragging the card. 
    /// If it's over the dropzone, drop the card in the dropzone.
    /// </summary>
    public void EndDragging() 
    {
        if (!this.enabled) return;


        if (isOverDropZone && DropZone.MaxCards > 0 && playerCard.HasMoney)
        {
            transform.SetParent(dropZone.transform, false);

            StateManager.SwitchState(StateManager.inDropzoneState);

            if (playerCard.CardStats.Tag != CardTag.Ligma)
                cardsListHolder.FriendlyCardsList.Add(this.gameObject);

            Destroy(this);
        }
        else if(isOverBin)
        {
            DeleteCardEvent();

            cardsListHolder.FriendlyCardsList.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            this.transform.SetParent(startPosition);
            this.transform.SetSiblingIndex(instatiatedCard.transform.GetSiblingIndex());
        }

        Destroy(instatiatedCard);
    }
    #endregion

    #region Trigger Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "DropZone":
                dropZone = collision.gameObject;
                isOverDropZone = true;
                break;
            case "Bin":
                isOverBin = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "DropZone":
                dropZone = null;
                isOverDropZone = false;
                break;
            case "Bin":
                isOverBin = false;
                break;
        }
    }
    #endregion
}
