using TMPro;
using UnityEngine;

public class MenuCoin : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;

    private void Start()
    {
        int collectedCoins = PlayerPrefs.GetInt("CollectedCoins", 0);
        coinText.text = collectedCoins.ToString();
    }
}
