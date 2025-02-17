using System;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    [SerializeField] private List<MeshChanger> Skins;

    private int skinIndex;

    private void Start()
    {

        skinIndex = PlayerPrefs.GetInt("skinList", 0);


        meshFilter = Skins[skinIndex].MeshFilter;
        meshRenderer = Skins[skinIndex].MeshRenderer;
    }
}
[Serializable]
public class MeshChanger
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    public MeshFilter MeshFilter { get => meshFilter; }
    public MeshRenderer MeshRenderer { get => meshRenderer; }
}