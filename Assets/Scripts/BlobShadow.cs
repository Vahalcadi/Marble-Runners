using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlobShadow : MonoBehaviour
{
    Transform _transform;
    private Vector3 offset;
    [SerializeField] private Transform playerTransform;
    private DecalProjector projector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _transform = transform;
        offset = _transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position = playerTransform.localPosition + offset;
    }

    private void FixedUpdate()
    {
    }
}
