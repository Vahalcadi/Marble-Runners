using UnityEngine;

[CreateAssetMenu]
public class MeshChangerSO : ScriptableObject
{
    [field: SerializeField] public int UUID { get; private set; }

    [SerializeField] private Mesh petMesh;
    [SerializeField] private Material[] petMaterials;
    [field: SerializeField] public Mesh marbleMesh { get; private set; }
    [field: SerializeField] public Material[] marbleMaterials { get; private set; }

    [field: SerializeField] public int SkinCost { get; private set; }
    [field: SerializeField] public FMODUnity.EventReference SoundEvent { get; private set; }
    public bool Unlocked { get; set; } = false;

    public Mesh PetMesh { get => petMesh; }
    public Material[] PetMaterials { get => petMaterials; }
}
