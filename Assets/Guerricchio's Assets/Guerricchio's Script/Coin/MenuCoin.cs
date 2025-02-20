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
        int totalCollectedCoins = 0;

        for (int levelIndex = 1; levelIndex <= 2; levelIndex++)
        {
            totalCollectedCoins += PlayerPrefs.GetInt("CollectedCoins_Level" + levelIndex, 0);
        }

        // Aggiorna il testo per mostrare il numero totale di monete
        coinText.text = totalCollectedCoins.ToString();
    }
}
