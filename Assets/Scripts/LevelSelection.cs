using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private string[] scenes;

    public void StartLevel(int index)
    {
        if (index < scenes.Length)
            SceneManager.LoadScene(scenes[index]);
    }
}
