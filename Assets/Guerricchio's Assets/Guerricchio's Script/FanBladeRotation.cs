using UnityEngine;

public class FanBladeRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1000f;

    private void Update()
    {
        transform.Rotate(Vector3.right * rotationSpeed * Time.fixedDeltaTime);
    }
}
