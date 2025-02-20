
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private FMODUnity.EventReference[] BGM;
    [SerializeField] private FMODUnity.EventReference[] SFX;
    [SerializeField] private float sfxMinimumDistance = 10;
    [SerializeField] private bool setRandomPitch = false;

    private List<FMOD.Studio.EventInstance> SFXEmitters = new();
    private List<FMOD.Studio.EventInstance> BGMEmitters = new();

    private FMOD.Studio.VCA BGMController;
    private FMOD.Studio.VCA SFXController;

    private Transform playerTransform;

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
        playerTransform = GyroscopicMovement.OnReturnTransform?.Invoke();

        BGMController = FMODUnity.RuntimeManager.GetVCA($"vca:/MUSIC");
        SFXController = FMODUnity.RuntimeManager.GetVCA($"vca:/SFX");

        float BGMValue = PlayerPrefs.GetFloat("MUSIC", 1);
        float SFXValue = PlayerPrefs.GetFloat("SFX", 1);

        BGMController.setVolume(BGMValue);
        SFXController.setVolume(SFXValue);
    }

    public void PlaySFXDirectly(int index, Transform source)
    {
        if (source != null && Vector2.Distance(playerTransform.position, source.position) > sfxMinimumDistance)
            return;

        if (index < SFXEmitters.Count)
        {
            if(setRandomPitch)
                SFXEmitters[index].setPitch(Random.Range(.85f, 1.1f));

            SFXEmitters[index].start();
        }

    }

    public void StopSFXDirectly(int index, FMOD.Studio.STOP_MODE stopMode)
    {
        SFXEmitters[index].stop(stopMode);
    }
}
