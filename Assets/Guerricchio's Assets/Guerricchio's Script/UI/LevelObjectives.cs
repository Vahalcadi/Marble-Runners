using UnityEngine;

public class LevelObjectives : MonoBehaviour
{
    public string[] objectives;

    private void Start()
    {
        Object.FindFirstObjectByType<PauseManager>()?.UpdateObjectives(objectives);
    }
}
