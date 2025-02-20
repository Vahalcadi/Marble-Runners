using UnityEngine;
using UnityEngine.UI;

public class VcaController : MonoBehaviour
{
    private FMOD.Studio.VCA vcaController;
    [SerializeField] private string vcaName;
    private Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vcaController = FMODUnity.RuntimeManager.GetVCA($"vca:/" + vcaName);
        slider = GetComponent<Slider>();

        float volumeValue = PlayerPrefs.GetFloat(vcaName,1);

        //SetVolume(volumeValue);
        slider.value = volumeValue;
    }

    public void SetVolume(float volume)
    {
        vcaController.setVolume(volume);
        PlayerPrefs.SetFloat(vcaName, volume);
    }
}
