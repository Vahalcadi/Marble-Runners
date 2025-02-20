using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetCoinsAndStarsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI starsCount; 
    [SerializeField] private TextMeshProUGUI coinsCount;
    private int coins = 0;

    private void Start()
    {
        //starsCount.text = $"{PlayerPrefs.GetInt("starsCount", 0)}/{PlayerPrefs.GetInt("starsMaxCount")}";
        //coinsCount.text = $"{PlayerPrefs.GetInt("totalCoins", 0)}";

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) 
        {
            coins += PlayerPrefs.GetInt("CollectedCoins_Level" + i, 0);
        }

        coinsCount.text = $"{coins}";
    }
}
