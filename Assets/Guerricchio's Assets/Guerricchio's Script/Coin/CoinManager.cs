using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{

    public static CoinManager Instance { get; private set; }
    public RawImage[] coinImages;  //image array
    [SerializeField] Texture2D coinOff;
    [SerializeField] Texture2D coinOn;

    private int collectedCoins = 0;

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
    }

    public void CollectCoin()
    {
        if (collectedCoins < coinImages.Length)
        {
            coinImages[collectedCoins].texture = coinOn;
            collectedCoins++;
        }
    }

    public void ResetCoins()
    {
        collectedCoins = 0;
        foreach (RawImage img in coinImages)
        {
            img.texture = coinOff;
        }
    }
}
