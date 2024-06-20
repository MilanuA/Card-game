using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI moneyText;

    public static int MoneyAmount { get; set; }
    public static int AdditionalMoney { get; set; }
    #endregion

    private void OnEnable()
    {
        EndRound.EndRoundEvent += UpdateRoundMoney;
        InDropzone.InDropzoneEvent += UpdateMoney;
    }

    private void OnDisable()
    {
        EndRound.EndRoundEvent -= UpdateRoundMoney;
        InDropzone.InDropzoneEvent -= UpdateMoney;
    }

    private void Awake() => UpdateRoundMoney();

    private void UpdateRoundMoney()
    {
        MoneyAmount = EndRound.Round;
        moneyText.text = MoneyAmount.ToString();
    }

    public void UpdateMoney(int amount)
    {
        MoneyAmount += amount;
        moneyText.text = MoneyAmount.ToString();
    }
}
