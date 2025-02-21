using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetCOinInWorld : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsCount;

    private int coins = 0;

    private void Start()
    {

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            coins += PlayerPrefs.GetInt("CollectedCoins_Level" + i, 0);
        }

        coinsCount.text = $"{coins}";
    }
}
