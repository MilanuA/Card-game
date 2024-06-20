using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField healthInput, cardInDeckInput;
    [SerializeField] Button btn;
    public void LoadToScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex);

    #region TEMPORARY CODE
    public void SaveInputs()
    {
        int healthValue = GetParsedInputOrDefault(healthInput, "Health", 30);
        int cardsInDeckValue = GetParsedInputOrDefault(cardInDeckInput, "CardsInDeck", 35);

        PlayerPrefs.SetInt("Health", healthValue);
        PlayerPrefs.SetInt("CardsInDeck", cardsInDeckValue);
        PlayerPrefs.Save();
    }

    private int GetParsedInputOrDefault(TMP_InputField inputField, string key, int defaultValue)
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            return defaultValue;
        }

        return int.Parse(inputField.text);
    }

    public void Changed(string str)
    {
        int.TryParse(str, out int test);

        if (test < 0)
            btn.interactable = false;
        else
            btn.interactable = true;
    }
    #endregion
}
