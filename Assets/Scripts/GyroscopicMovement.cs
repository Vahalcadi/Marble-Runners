using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GyroscopicMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    private Vector3 moveVector;

    [Header("Shake Jump")]
    [SerializeField] private float yVelocity;
    [SerializeField] private float shakeDetectionThreshold;
    [SerializeField] private float minShakeInterval;

    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;

    private bool IsGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.maxLinearVelocity = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        sqrShakeDetectionThreshold = Mathf.Pow(shakeDetectionThreshold, 2);
        moveVector = new Vector3(InputManager.Instance.Move().x, 0, -InputManager.Instance.Move().z);

        if (IsGrounded)
        {
            if (InputManager.Instance.Jump().sqrMagnitude >= sqrShakeDetectionThreshold
            && Time.unscaledTime >= timeSinceLastShake + minShakeInterval)
            {
                Jump();
                timeSinceLastShake = Time.unscaledTime;
            }
        }   

        //rb.linearVelocity = moveVector * speed;
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveVector * acceleration, ForceMode.Force);
    }

    private void Jump()
    {
        //rb.AddForce(Vector3.up * yVelocity, ForceMode.Impulse);
        rb.linearVelocity = Vector3.up * yVelocity;
        IsGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            IsGrounded = true;
    }

    /*private void OnCollisionExit(Collision collision)
    {

        if (collision.collider.CompareTag("Ground"))
            IsGrounded = false;
    }*/
}
