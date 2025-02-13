using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public class Tube : MonoBehaviour
{
    private SplineAnimate ballSplineAnimate;
    private Rigidbody ballRigidBody;
    private SplineContainer splineContainer;

    private Spline spline;

    [SerializeField] private float duration = 3;

    [Header("check the boolean only if the tube is a two-way tube")]
    [SerializeField] private bool isTwoWay = false;
    [SerializeField] private int startingSpline; //the index of the first-way spline to use
    [SerializeField] private int endingSpline; //the index of the last-way spline to use

    private bool alternate;

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

            ballSplineAnimate.AnimationMethod = SplineAnimate.Method.Time;
            ballSplineAnimate.Duration = duration;

            ballSplineAnimate.ElapsedTime = 0;
            ballRigidBody.interpolation = RigidbodyInterpolation.None;

            ballSplineAnimate.Completed += ResetCoroutine;
            if (!isTwoWay)
                ballSplineAnimate.Play();        
            else
                StartCoroutine(TwoWayCoroutine());
        }
    }

    private void AlternateSplines()
    {
        if (alternate)
        {
            spline = splineContainer.Splines[endingSpline];
            alternate = false;
        }
        else
        {
            spline = splineContainer.Splines[startingSpline];
            alternate = true;
        }
        
    }

    private IEnumerator TwoWayCoroutine()
    {
        AlternateSplines();
        float t = 0;
        float percentage = 0;

        while (percentage <= 1)
        {
            yield return null;
            t += Time.deltaTime;
            percentage = t / duration;

            Vector3 posEvaluated = spline.EvaluatePosition(percentage);

            ballRigidBody.transform.position = new Vector3(transform.position.x + posEvaluated.x, transform.position.y + posEvaluated.y, transform.position.z + posEvaluated.z); 
        }

        ResetCoroutine();
    }

    private void ResetCoroutine()
    {
        ballSplineAnimate.Completed -= ResetCoroutine;

        ballRigidBody.useGravity = true;
        InputManager.Instance.OnEnable();
        hasStarted = false;
        ballRigidBody.interpolation = RigidbodyInterpolation.Interpolate;
        ballRigidBody = null;
        ballSplineAnimate.Container = null;
        ballSplineAnimate = null;
    }
}
