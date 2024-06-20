using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardChange : MonoBehaviour, IPointerClickHandler
{
    private UnityAction unityAction;
    private PlayerCard card;

    private void OnEnable() => Timer.TimerEndedEvent += Delete;

    private void Awake()
    {
        card = this.gameObject.GetComponent<PlayerCard>();
        unityAction += card.ChangeCard;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (unityAction == null) return;

        unityAction.Invoke();
        unityAction -= card.ChangeCard;
        Destroy(this);
    }

    private void Delete(BaseState state) => Destroy(this);
}
