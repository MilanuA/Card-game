using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class CardAttack : Selectable
{
    #region Variables
    public delegate void OnEnemyHealthDamage(int dmg);
    public static event OnEnemyHealthDamage EnemyDamagedEvent;

    [Space(20)]
    [Header("--------------- Setup ---------------")]
    [Space(5)]
    [SerializeField] private PlayerCard card;
    [Header("How much card should be scaled when its selected")]
    [SerializeField] private float scaler;
    [Header("Color of the outline when it can attack")]
    [SerializeField] private Color color;

    private GraphicRaycaster raycast;
    private EventSystem eventSystem;
    private PointerEventData pointerEventData;
    private Outline outline;
    private StateManager stateManager;

    public bool CanAttack { get; set; }
    private int attackDmg;
    #endregion

    #region Unity methods
    protected override void OnEnable()
    {
        EndRound.EndRoundEvent += PlayTheCard;
    }

    protected override void OnDisable()
    {
        EndRound.EndRoundEvent -= PlayTheCard;
    }

    protected override void Awake()
    {
        this.interactable = false;

        outline = this.GetComponent<Outline>();
        eventSystem = GameObject.FindObjectOfType<EventSystem>().GetComponent<EventSystem>();
        raycast = GameObject.FindGameObjectWithTag("EnemyRaycaster").GetComponent<GraphicRaycaster>();
        stateManager = this.GetComponent<StateManager>();
        attackDmg = card.CardStats.AttackDmg;
    }

    private void Update() => CanCardAttack();
    #endregion

    #region Card Events
    public void CardSelected()
    {
        if (!this.enabled)
            return;

        this.transform.localScale = new Vector2(transform.localScale.x + scaler, transform.localScale.y + scaler);
    }


    public void CardUpdated()
    {
        if (!Input.GetMouseButtonDown(0)) 
            return;

        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();

        raycast.Raycast(pointerData, raycastResults);

        foreach (RaycastResult result in raycastResults)
        {
            if (IsEnemyTag(result.gameObject.tag) && CanAttack && !IsLigmaTag(this.tag))
            {
                Enemy enemy = result.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(attackDmg, true, card);

                card.CardStats.Events.WhenPlayed?.Invoke(enemy, card);

                enemy.Attack(card, false, enemy);
                CanAttack = false;
            }
            else if (IsEnemyTag(result.gameObject.tag) && IsLigmaTag(this.tag))
            {
                Enemy enemy = result.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(1000, card, card);
                DropZone.MaxCards++;
                Destroy(this.gameObject);
            }
            else if (result.gameObject.CompareTag("EnemyRaycaster") && CanAttack && CheckDropzone.IsEnemyDropzoneEmpty && !IsLigmaTag(this.tag))
            {
                EnemyDamagedEvent(attackDmg);
                CanAttack = !CanAttack;
            }
        }
    }

    private bool IsEnemyTag(string tag) => tag == "Enemy";

    private bool IsLigmaTag(string tag) => tag == "Ligma";

    public void CardDeselected()
    {
        if (!this.enabled) return;

        this.transform.localScale = new Vector2(1, 1);
    }
    #endregion

    #region Helping Methods
    /// <summary>
    /// If it's the new level, card can attack again.
    /// </summary>
    private void PlayTheCard()
    {
        if (stateManager.currentState == StateManager.inDropzoneState)
            interactable = true;

        CanAttack = true;
    }

    /// <summary>If the card can attack, show outline. If can't attack, hide the outline. </summary>
    private void CanCardAttack()
    {
        if (stateManager.currentState != StateManager.inDropzoneState) return;

        if (CanAttack)
        {
            outline.effectDistance = new Vector2(6, 6);
            outline.effectColor = color;
        }
        else if (!CanAttack)
        {
            this.interactable = false;
            outline.effectDistance = new Vector2(0, 0);
        }
    }
    #endregion
}
