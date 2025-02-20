using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(Rigidbody), typeof(SplineAnimate))]
public class GyroscopicMovement : MonoBehaviour
{
    public static Func<Transform> OnReturnTransform;

    [SerializeField] private MeshRenderer petMeshRenderer;

    private Rigidbody rb;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;

    [Range(0.1f, 10)][SerializeField] private float xMultiplier;
    [Range(0.1f, 10)][SerializeField] private float zMultiplier;
    private Vector3 moveVector;

    [SerializeField] private float aerialTimeBeforeDeath;
    [SerializeField] private Explosion explosionEffect;

    [Header("Shake Jump")]
    [SerializeField] private float yVelocity;
    [SerializeField] private float shakeDetectionThreshold;
    [SerializeField] private float minShakeInterval;

    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;

    private bool IsGrounded;

    private float deathTimer = 0;
    private Coroutine deathCoroutine;

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

        /**
         * Death anim
         * **/
        if (!IsGrounded && rb.useGravity)
        {
            deathTimer += Time.deltaTime;
        }
        else
            ResetDeathTimer();


        /**
         * if needed, create a new vector3 and adjust all the values the InputManager returns
         * xMultiplier and zMultiplier = helper values to reach the desired speed. These values must be greater than zero. These are completely optional 
         * **/
        moveVector = new Vector3(InputManager.Instance.Move().x * xMultiplier, 0, -InputManager.Instance.Move().z * zMultiplier);



        /**
         * Jump logic
         * **/
        /*if (IsGrounded)
        {
            if (InputManager.Instance.Jump().sqrMagnitude >= sqrShakeDetectionThreshold
            && Time.unscaledTime >= timeSinceLastShake + minShakeInterval)
            {
                Jump();
                timeSinceLastShake = Time.unscaledTime;
            }
        }*/

        //rb.linearVelocity = moveVector * speed;
    }

    private void FixedUpdate()
    {
        /**
         * add a force to the rigidbody. Forcemode is completely up to choice. 
         * **/
        rb.AddForce(moveVector * acceleration, ForceMode.Force);
        CheckGround();
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * yVelocity, ForceMode.Impulse);
        //rb.linearVelocity = Vector3.up * yVelocity;
        IsGrounded = false;
    }

    private IEnumerator Die()
    {
        if (deathCoroutine == null)
        {
            //play anim
            explosionEffect.StartExplosion();
            GetComponent<MeshRenderer>().enabled = false;
            petMeshRenderer.enabled = false;
            InputManager.Instance.OnDisable();
            yield return new WaitForSeconds(1);
            petMeshRenderer.enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
            InputManager.Instance.OnEnable();
            GameManager.Instance.RespawnBall();
            deathCoroutine = null;
        }      
    }

    private void CheckGround()
    {
        float yPos = transform.position.y;

        if (transform.position.y > yPos || transform.position.y < yPos)
        {
            IsGrounded = false;
        }
        else
            IsGrounded = true;
    }

    public void ResetDeathTimer()
    {
        deathTimer = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            if (deathTimer > aerialTimeBeforeDeath)
                deathCoroutine = StartCoroutine(Die());
            else
                ResetDeathTimer();

            IsGrounded = true;
        }
    }

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
