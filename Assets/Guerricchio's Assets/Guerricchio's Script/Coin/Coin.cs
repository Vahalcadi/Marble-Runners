using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    private static readonly Vector3 rotationAxis = new Vector3(1, 1, 0).normalized;


    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager.Instance.CollectCoin();
            Destroy(gameObject);
        }
    }
}

