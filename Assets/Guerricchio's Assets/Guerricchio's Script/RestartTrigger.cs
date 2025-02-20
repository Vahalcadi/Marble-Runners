using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartTrigger : MonoBehaviour
{

    [SerializeField] private List<GameObject> vfxPrefabs = new List<GameObject>();
    [SerializeField] private List<Transform> vfxSpawnPoints = new List<Transform>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < vfxPrefabs.Count; i++)
            {
                if (vfxPrefabs[i] != null && i < vfxSpawnPoints.Count && vfxSpawnPoints[i] != null)
                {
                    Instantiate(vfxPrefabs[i], vfxSpawnPoints[i].position, Quaternion.Euler(-90, 0, 0));
                }
                int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                {
                    AudioManager.Instance.PlaySFXDirectly(11, transform);
                    LoadSceneAsync(nextSceneIndex);
                }
                //int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                //if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                //{
                //    SceneManager.LoadScene(nextSceneIndex);
                //}
            }
        }


    }
    private IEnumerator LoadSceneAsync(int index)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
