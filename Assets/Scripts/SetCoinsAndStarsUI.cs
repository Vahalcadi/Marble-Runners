using TMPro;
using UnityEngine;

public class SetCoinsAndStarsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI starsCount; 
    [SerializeField] private TextMeshProUGUI coinsCount;

    private void Start()
    {
        //starsCount.text = $"{PlayerPrefs.GetInt("starsCount", 0)}/{PlayerPrefs.GetInt("starsMaxCount")}";
        //coinsCount.text = $"{PlayerPrefs.GetInt("totalCoins", 0)}";
    }
}
