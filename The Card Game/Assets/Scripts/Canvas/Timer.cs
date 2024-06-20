using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public delegate void OnTimerEnded(BaseState state);
    public static event OnTimerEnded TimerEndedEvent;

    #region -----VARIABLES-----
    [Header("Time in which player can change cards")]
    [SerializeField] private float maxTime;
    [Space(30)]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject timerGameobject;
    [SerializeField] private GameObject enemyArea;
    [SerializeField] private Button endRoundButton;

    private float minutes;
    private float seconds;
    #endregion

    private void Awake() 
    {
        DrawCards.CanBeChanged = true;
        endRoundButton.interactable = false;
    }

    void FixedUpdate()
    {
        if (maxTime > 0.2) CountDown();
        else if (maxTime < 0.2) TimerEnded();
    }

    #region Timer Methods
    /// <summary> Count to 0 and show the time left in string </summary>
    private void CountDown()
    {
        maxTime -= Time.deltaTime;
        minutes = Mathf.FloorToInt(maxTime / 60);
        seconds = Mathf.FloorToInt(maxTime % 60);

        timerText.text = string.Format("Time left: {0:00}:{1:00}", minutes, seconds);
    }

    /// <summary> After the timer counted down to 0, enable the button and call the event.  </summary>
    private void TimerEnded()
    {
        TimerEndedEvent(StateManager.inDeckState);
        enemyArea.SetActive(true);
        endRoundButton.interactable = true;
        DrawCards.CanBeChanged = false;
        Destroy(timerGameobject);
        Destroy(this);
    }
    #endregion
}
