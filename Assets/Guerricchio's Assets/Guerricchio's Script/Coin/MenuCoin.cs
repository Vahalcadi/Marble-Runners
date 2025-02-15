using TMPro;
using UnityEngine;

public class MenuCoin : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;

    private void Start()
    {
        LoadCoinCount();
    }

    private void LoadCoinCount()
    {
        // Carica il numero totale di monete raccolte da PlayerPrefs
        int totalCollectedCoins = 0;

        // Supponiamo che tu abbia 2 livelli, puoi adattare questo ciclo in base al numero di livelli
        for (int levelIndex = 1; levelIndex <= 2; levelIndex++)
        {
            totalCollectedCoins += PlayerPrefs.GetInt("CollectedCoins_Level" + levelIndex, 0);
        }

        // Aggiorna il testo per mostrare il numero totale di monete
        coinText.text = totalCollectedCoins.ToString();
    }
}
