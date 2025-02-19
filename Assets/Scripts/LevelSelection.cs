using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private string[] scenes;
    [SerializeField] private Button[] buttons;

    public void StartLevel(int index)
    {
        AudioManager.Instance.PlaySFXDirectly(16);
        if (index < scenes.Length)
            StartCoroutine(LoadSceneAsync(index));
    }

    private IEnumerator LoadSceneAsync(int index)
    {

        foreach (var button in buttons)
            button.interactable = false;
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenes[index]);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
