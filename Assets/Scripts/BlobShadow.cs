using UnityEngine;

public class BlobShadow : MonoBehaviour
{
    Transform _transform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform playerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position = playerTransform.position + offset;
    }

    private void FixedUpdate()
    {
    }
}
