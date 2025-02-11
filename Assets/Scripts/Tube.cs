using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public class Tube : MonoBehaviour
{
    private SplineAnimate ballSplineAnimate;
    private Rigidbody ballRigidBody;
    private SplineContainer splineContainer;

    [SerializeField] private SplineAnimate.Method animationMethod = SplineAnimate.Method.Speed;
    [Header("if animationMethod is time")]
    [SerializeField] private float duration = 3;
    [Header("if animationMethod is speed")]
    [SerializeField] private float speed = 15;

    private bool hasStarted;

    private void Start()
    {
        hasStarted = false;
        splineContainer = GetComponent<SplineContainer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasStarted)
        {
            hasStarted = true;
            InputManager.Instance.OnDisable();
            ballRigidBody = other.GetComponent<Rigidbody>();
            ballSplineAnimate = other.GetComponent<SplineAnimate>();

            ballRigidBody.useGravity = false;
            ballSplineAnimate.Container = splineContainer;
            ballRigidBody.linearVelocity = Vector3.zero;
            ballRigidBody.angularVelocity = Vector3.zero;

            if(animationMethod == SplineAnimate.Method.Time)
                ballSplineAnimate.Duration = duration;
            else
                ballSplineAnimate.MaxSpeed = speed;

            ballSplineAnimate.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && hasStarted)
        {
            hasStarted = false;
            ballRigidBody = null;
            ballSplineAnimate = null;
        }
    }
}
