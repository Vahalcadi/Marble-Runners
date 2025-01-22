using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject ball;

    private void Awake()
    {
        Instance = this;
    }

    public void RespawnBall()
    {
        ball.transform.position = spawnPoint.position;
        ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }
}
