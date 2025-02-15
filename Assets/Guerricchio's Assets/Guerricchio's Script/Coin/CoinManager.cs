using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{

    public static CoinManager Instance { get; private set; }
    public RawImage[] coinImages;  //image array
    [SerializeField] Texture2D coinOff;
    [SerializeField] Texture2D coinOn;

    private int collectedCoins = 0;
    private int totalCoins = 3;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //collectedCoins = PlayerPrefs.GetInt("CollectedCoins", 0);   da tenere nel caso
        collectedCoins = PlayerPrefs.GetInt("CollectedCoins_Level" + SceneManager.GetActiveScene().buildIndex, 0);
        UpdateCoinImages();
    }

    public void CollectCoin(int coinIndex)
    {
        if (coinIndex < coinImages.Length /*&& collectedCoins < totalCoins*/)
        {
            // Se la moneta non è già stata raccolta
            if (coinImages[coinIndex].texture == coinOff)
            {
                coinImages[coinIndex].texture = coinOn;
                collectedCoins++;
                SaveCoins();
            }
        }
    }

    public void ResetCoins()
    {
        collectedCoins = 0;
        foreach (RawImage img in coinImages)
        {
            img.texture = coinOff;
        }
        SaveCoins();
    }

    private void SaveCoins()
    {
        //PlayerPrefs.SetInt("CollectedCoins", collectedCoins);
        PlayerPrefs.SetInt("CollectedCoins_Level" + SceneManager.GetActiveScene().buildIndex, collectedCoins);
        for (int i = 0; i < totalCoins; i++)
        {
            // Salva lo stato di ogni moneta
            int coinState = (coinImages[i].texture == coinOn) ? 1 : 0; // 1 = accesa, 0 = spenta
            //PlayerPrefs.SetInt("CoinState" + i, coinState);
            PlayerPrefs.SetInt("CoinState_Level" + SceneManager.GetActiveScene().buildIndex + "_" + i, coinState);
        }
        PlayerPrefs.Save();
    }
    private void UpdateCoinImages()
    {
        for (int i = 0; i < totalCoins; i++)
        {
            // Carica lo stato di ogni moneta
            // int coinState = PlayerPrefs.GetInt("CoinState" + i, 0); // 0 = spenta, 1 = accesa
            int coinState = PlayerPrefs.GetInt("CoinState_Level" + SceneManager.GetActiveScene().buildIndex + "_" + i, 0); // 0 = spenta, 1 = accesa
            if (coinState == 1)
            {
                coinImages[i].texture = coinOn;


            }
            else
            {
                coinImages[i].texture = coinOff;
            }
        }
    }
}
