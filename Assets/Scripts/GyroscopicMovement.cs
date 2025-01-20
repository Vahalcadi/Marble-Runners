using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class GyroscopicMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    private Vector3 moveVector;

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

        moveVector = new Vector3(InputManager.Instance.Move().x, 0, -InputManager.Instance.Move().z);
        Debug.Log(moveVector);
        //rb.linearVelocity = moveVector * speed;
    }

    private void FixedUpdate()
    {
        
        rb.AddForce(moveVector * acceleration, ForceMode.Force);
    }
}
