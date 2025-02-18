using UnityEngine;

[CreateAssetMenu]
public class MeshChangerSO : ScriptableObject
{
    [field: SerializeField] public int UUID { get; private set; }

    [SerializeField] private Mesh mesh;
    [SerializeField] private Material[] materials;
    [field: SerializeField] public int SkinCost { get; private set; }
    public bool Unlocked { get; set; } = false;

    public Mesh Mesh { get => mesh; }
    public Material[] Materials { get => materials; }
}
