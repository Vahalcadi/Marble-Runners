using TMPro;
using UnityEngine;

public class ShopItemUI : MonoBehaviour
{
    [field: SerializeField] public MeshChangerSO Skin { get; private set; }
    [SerializeField] private GameObject priceTag;
    [SerializeField] private TextMeshProUGUI cost;
    private void Awake()
    {
        Skin = Instantiate(Skin);
        cost.text = $"{Skin.SkinCost}";
    }

    public void SelectSkin()
    {
        if (Skin.Unlocked)
            PlayerPrefs.SetInt("skinSelected", Skin.UUID);

        AudioManager.Instance.PlaySFXDirectly(15);
    }

    public void UnlockSkin()
    {
        priceTag.SetActive(false);
    }
}
