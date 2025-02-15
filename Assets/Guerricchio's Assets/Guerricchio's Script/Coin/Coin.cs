using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    private static readonly Vector3 rotationAxis = new Vector3(1, 1, 0).normalized;

    public int coinIndex;

    [SerializeField] private Material transparentMaterial;
    private Material originalMaterial;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("Componente Renderer non trovato sul GameObject Coin.");
            return;
        }

        originalMaterial = renderer.material;
    }

    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager.Instance.CollectCoin(coinIndex);
            Destroy(gameObject);
        }
    }

    public void SetTransparent()
    {
        if (transparentMaterial == null)
        {
            Debug.LogError("Il materiale trasparente non è assegnato nello script Coin.");
            return;
        }

        // Cambia il materiale in quello trasparente
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = transparentMaterial; // Imposta il materiale trasparente
        }
    }
}

