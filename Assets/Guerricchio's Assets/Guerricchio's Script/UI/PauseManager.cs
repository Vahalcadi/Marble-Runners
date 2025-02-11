using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject pauseButton;
    public TextMeshProUGUI objectivesText;

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

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        pauseButton.SetActive(!isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }
    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
