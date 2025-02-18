using System;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    [SerializeField] private MeshChangerSO defaultSkin;
    [SerializeField] private List<MeshChangerSO> Skins;

    private int skinUUID;

    private void Start()
    {

        skinUUID = PlayerPrefs.GetInt("skinSelected", Skins.Count);

        if (skinUUID == Skins.Count)
        {
            meshFilter.mesh = defaultSkin.Mesh;
            meshRenderer.materials = defaultSkin.Materials;
        }

        meshFilter.mesh = Skins.Find(x => x.UUID == skinUUID).Mesh;
        meshRenderer.materials = Skins.Find(x => x.UUID == skinUUID).Materials;
    }
}
