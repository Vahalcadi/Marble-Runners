using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    [SerializeField] private Image[] coins;

    [SerializeField] private Sprite coinOff;
    [SerializeField] private Sprite coinOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        UpdateCoinImages();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCoinImages()
    {
        for (int i = 0; i < 3; i++)
        {
            // Carica lo stato di ogni moneta
            // int coinState = PlayerPrefs.GetInt("CoinState" + i, 0); // 0 = spenta, 1 = accesa
            int coinState = PlayerPrefs.GetInt("CoinState_Level" + levelIndex + "_" + i, 0); // 0 = spenta, 1 = accesa
            if (coinState == 1)
                coins[i].sprite = coinOn;
            else
                coins[i].sprite = coinOff;
        }
    }
}
