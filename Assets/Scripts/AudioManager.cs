
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private FMODUnity.EventReference[] BGM;
    [SerializeField] private FMODUnity.EventReference[] SFX;

    private List<FMOD.Studio.EventInstance> SFXEmitters = new();
    private List<FMOD.Studio.EventInstance> BGMEmitters = new();

    private FMOD.Studio.VCA BGMController;
    private FMOD.Studio.VCA SFXController;


    /*[SerializeField] private int bgmStartIndex;
    [SerializeField] private bool playAtStart;*/
    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;


        foreach (var bgm in BGM)
            BGMEmitters.Add(FMODUnity.RuntimeManager.CreateInstance(bgm));

        foreach (var sfx in SFX)
            SFXEmitters.Add(FMODUnity.RuntimeManager.CreateInstance(sfx));        
    }

    private void Start()
    {
        BGMController = FMODUnity.RuntimeManager.GetVCA($"vca:/MUSIC");
        SFXController = FMODUnity.RuntimeManager.GetVCA($"vca:/SFX");

        float BGMValue = PlayerPrefs.GetFloat("MUSIC", 1);
        float SFXValue = PlayerPrefs.GetFloat("SFX", 1);

        BGMController.setVolume(BGMValue);
        SFXController.setVolume(SFXValue);
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
