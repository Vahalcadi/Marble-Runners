using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject MainTitle;
    [SerializeField] private GameObject WorldSelection;
    [SerializeField] private GameObject[] LevelSelections;
    [SerializeField] private GameObject Shop;
    [SerializeField] private GameObject Settings;

    private void Start()
    {

    }

    public void OpenWorldSelection()
    {
        MainTitle.SetActive(false);
        WorldSelection.SetActive(true);
        Shop.SetActive(false);
        Settings.SetActive(false);
        foreach (GameObject go in LevelSelections)
            go.SetActive(false);

        AudioManager.Instance.PlaySFXDirectly(15);
    }

    public void OpenLevelSelection(int world)
    {
        MainTitle.SetActive(false);
        WorldSelection.SetActive(false);
        Shop.SetActive(false);
        Settings.SetActive(false);
        foreach (GameObject go in LevelSelections)
            go.SetActive(false);

        LevelSelections[world].SetActive(true);
        AudioManager.Instance.PlaySFXDirectly(15);
    }

    public void OpenShop()
    {
        MainTitle.SetActive(false);
        WorldSelection.SetActive(false);
        Shop.SetActive(true);
        Settings.SetActive(false);
        foreach (GameObject go in LevelSelections)
            go.SetActive(false);

        AudioManager.Instance.PlaySFXDirectly(15);
    }

    public void OpenSetting()
    {
        MainTitle.SetActive(false);
        WorldSelection.SetActive(false);
        Shop.SetActive(false);
        Settings.SetActive(true);
        foreach (GameObject go in LevelSelections)
            go.SetActive(false);

        AudioManager.Instance.PlaySFXDirectly(15);
    }
}
