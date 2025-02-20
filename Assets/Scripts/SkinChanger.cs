using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private MeshFilter petMesh;
    [SerializeField] private MeshRenderer petMeshRenderer;
    [SerializeField] private MeshFilter marbleMesh;
    [SerializeField] private MeshRenderer marbleMeshRenderer;

    [SerializeField] private MeshChangerSO defaultSkin;
    [SerializeField] private List<MeshChangerSO> Skins;

    private MeshChangerSO skin;
    private StudioEventEmitter eventEmitter;

    private int skinUUID;

    private void Awake()
    {
        skinUUID = PlayerPrefs.GetInt("skinSelected", 9999);

        if (skinUUID == defaultSkin.UUID)
        {
            skin = defaultSkin;
            petMesh.mesh = null;
            petMeshRenderer.materials = null;

            marbleMesh.mesh = skin.marbleMesh;
            marbleMeshRenderer.materials = skin.marbleMaterials;
        }
        else
        {
            skin = Skins.Find(x => x.UUID == skinUUID);
            petMesh.mesh = skin.PetMesh;
            petMeshRenderer.materials = skin.PetMaterials;

            marbleMesh.mesh = skin.marbleMesh;
            marbleMeshRenderer.materials = skin.marbleMaterials;
        }

        eventEmitter = GetComponent<StudioEventEmitter>();

        eventEmitter.EventReference = skin.SoundEvent;
    }

    private void Start()
    {
        eventEmitter.Play();
    }
}
