using UnityEngine;

public class AirFlowObstacles : MonoBehaviour
{
    [SerializeField] private Vector3 windDirection = Vector3.forward;
    [SerializeField] private float windStrength = 10f;

    private Rigidbody playerRb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerRb = other.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerRb = null;
        }
    }

    private void FixedUpdate()
    {
        if (playerRb != null)
        {

            playerRb.AddForce(windDirection.normalized * windStrength, ForceMode.Force);
        }
    }
}
