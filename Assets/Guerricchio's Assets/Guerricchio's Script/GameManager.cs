using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] Transform defaultSpawnPoint;
    private Vector3 currentCheckpoint;

    [SerializeField] GameObject ball;
    private Rigidbody ballRigidBody;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance.gameObject);
        else
            Instance = this;
        ballRigidBody = ball.GetComponent<Rigidbody>();
        currentCheckpoint = defaultSpawnPoint.position;
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }
    public void RespawnBall()
    {
        ball.transform.position = currentCheckpoint;
        ballRigidBody.linearVelocity = Vector3.zero;
        ballRigidBody.angularVelocity = Vector3.zero;
    }
}
