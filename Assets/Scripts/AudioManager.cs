using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private EventReference[] BGM;
    [SerializeField] private EventReference[] SFX;

    private List<EventInstance> SFXEmitters;
    private List<EventInstance> BGMEmitters;

    /*[SerializeField] private int bgmStartIndex;
    [SerializeField] private bool playAtStart;*/

    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;


        foreach (var bgm in BGM)
            BGMEmitters.Add(RuntimeManager.CreateInstance(bgm));

        foreach (var sfx in SFX)
            SFXEmitters.Add(RuntimeManager.CreateInstance(sfx));
    }

    public void PlaySFXDirectly(int index)
    {
        SFXEmitters[index].start();
    }

    public void StopSFXDirectly(int index, FMOD.Studio.STOP_MODE stopMode)
    {
        SFXEmitters[index].stop(stopMode);
    }
}
