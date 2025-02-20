using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private TextMeshProUGUI objectivesText;
    [SerializeField] private GameObject optionsPanel;
    private bool isPaused = false;

    //private void Awake()
    //{
    //    if ((Object.FindObjectsByType<PauseManager>(FindObjectsSortMode.None).Length > 1))
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }
    //    DontDestroyOnLoad(gameObject);
    //}

    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        pauseButton.SetActive(!isPaused);

        if (isPaused)
            AudioManager.Instance.PlaySFXDirectly(13);
        else
            AudioManager.Instance.PlaySFXDirectly(14);

        Time.timeScale = isPaused ? 0f : 1f;
        optionsPanel.SetActive(false);
    }

    public void UpdateObjectives(string[] objectives)
    {
        foreach (string obj in objectives)
        {
            objectivesText.text += "- " + obj + "\n \n";
        }
    }

    public void Menu()
    {
        AudioManager.Instance.PlaySFXDirectly(15);
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
    public void OpenOptions()
    {
        AudioManager.Instance.PlaySFXDirectly(15);
        optionsPanel.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
    public void CloseOptions()
    {
        AudioManager.Instance.PlaySFXDirectly(12);
        optionsPanel.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
