using System;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(Rigidbody), typeof(SplineAnimate))]
public class GyroscopicMovement : MonoBehaviour
{
    public static Func<Transform> OnReturnTransform;

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

        rb.maxLinearVelocity = maxSpeed; //set the max speed the rigidbody can reach
        sqrShakeDetectionThreshold = Mathf.Pow(shakeDetectionThreshold, 2); //this is for the jump logic
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Screen.orientation == ScreenOrientation.Portrait)
            moveVector = new Vector3(InputManager.Instance.Move().x * xMultiplier, 0, -InputManager.Instance.Move().z * zMultiplier);
       else
            moveVector = new Vector3(InputManager.Instance.Move().z * zMultiplier, 0, InputManager.Instance.Move().x * xMultiplier);*/




        /**
         * if needed, create a new vector3 and adjust all the values the InputManager returns
         * xMultiplier and zMultiplier = helper values to reach the desired speed. These values must be greater than zero. These are completely optional 
         * **/
        moveVector = new Vector3(InputManager.Instance.Move().x * xMultiplier, 0, -InputManager.Instance.Move().z * zMultiplier);



        /**
         * Jump logic
         * **/
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
        /**
         * add a force to the rigidbody. Forcemode is completely up to choice. 
         * **/
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

    private Transform ReturnTransform() => transform;

    private void OnEnable()
    {
        OnReturnTransform += ReturnTransform;
    }

    private void OnDisable()
    {
        OnReturnTransform -= ReturnTransform;
    }
}
