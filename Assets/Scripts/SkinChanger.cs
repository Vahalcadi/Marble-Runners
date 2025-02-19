using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    [SerializeField] private MeshChangerSO defaultSkin;
    [SerializeField] private List<MeshChangerSO> Skins;

    private MeshChangerSO skin;
    private StudioEventEmitter eventEmitter;

    private int skinUUID;

    private void Awake()
    {
        skinUUID = PlayerPrefs.GetInt("skinSelected", 9999);

        if (skinUUID == 9999)
        {
            skin = defaultSkin;
            meshFilter.mesh = skin.Mesh;
            meshRenderer.materials = skin.Materials;
        }
        else
        {
            skin = Skins.Find(x => x.UUID == skinUUID);
            meshFilter.mesh = skin.Mesh;
            meshRenderer.materials = skin.Materials;
        }

        eventEmitter = GetComponent<StudioEventEmitter>();

        eventEmitter.EventReference = skin.SoundEvent;
    }

    private void Start()
    {
        eventEmitter.Play();
    }
}
