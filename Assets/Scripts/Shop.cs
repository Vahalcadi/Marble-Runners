using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShopItemUI> shopItems;
    public int AvailableCoins { get; private set; }

    private void Awake()
    {
    }

    private void Start()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            AvailableCoins += PlayerPrefs.GetInt("CollectedCoins_Level" + i, 0);
        }
        foreach (ShopItemUI sk in shopItems)
        {
            if (AvailableCoins >= sk.Skin.SkinCost)
            {
                sk.Skin.Unlocked = true;
                sk.UnlockSkin();
            }
        }
    }
}
