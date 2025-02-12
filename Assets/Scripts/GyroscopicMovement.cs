using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(Rigidbody), typeof(SplineAnimate))]
public class GyroscopicMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;

    [Range(0.1f, 10)][SerializeField] private float xMultiplier;
    [Range(0.1f, 10)][SerializeField] private float zMultiplier;
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
        sqrShakeDetectionThreshold = Mathf.Pow(shakeDetectionThreshold, 2);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Screen.orientation == ScreenOrientation.Portrait)
            moveVector = new Vector3(InputManager.Instance.Move().x * xMultiplier, 0, -InputManager.Instance.Move().z * zMultiplier);
       else
            moveVector = new Vector3(InputManager.Instance.Move().z * zMultiplier, 0, InputManager.Instance.Move().x * xMultiplier);*/

        moveVector = new Vector3(InputManager.Instance.Move().x * xMultiplier, 0, -InputManager.Instance.Move().z * zMultiplier);

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
        rb.AddForce(Vector3.up * yVelocity, ForceMode.Impulse);
        //rb.linearVelocity = Vector3.up * yVelocity;
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
