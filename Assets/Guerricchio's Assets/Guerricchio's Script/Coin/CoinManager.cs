using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    private int coinsCollected = 0;
    [SerializeField] private TextMeshProUGUI coinText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        UpdateUI();
    }

    public void CollectCoin()
    {
        coinsCollected++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        coinText.text = coinsCollected.ToString() + "/3";

    }
}
