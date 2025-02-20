using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public class Catapult : MonoBehaviour
{

    private SplineAnimate ballSplineAnimate;
    private Rigidbody ballRigidBody;
    private SplineContainer splineContainer;
    [SerializeField] private float timeBeforeLaunch;

    [SerializeField] private SplineAnimate.Method animationMethod = SplineAnimate.Method.Speed;
    [Header("if animationMethod is time")]
    [SerializeField] private float duration = 3;
    [Header("if animationMethod is speed")]
    [SerializeField] private float speed = 15;

    private Coroutine coroutine;


    public IEnumerator LaunchBall()
    {
        yield return new WaitForSeconds(timeBeforeLaunch);
        AudioManager.Instance.PlaySFXDirectly(0, transform);
        ballSplineAnimate.Play();
    }

    private void Start()
    {
        splineContainer = GetComponent<SplineContainer>();
    }

    private void ResetCoroutine()
    {

        ballSplineAnimate.Completed -= ResetCoroutine;

        ballRigidBody.useGravity = true;
        InputManager.Instance.OnEnable();
        coroutine = null;
        ballRigidBody.interpolation = RigidbodyInterpolation.Interpolate;
        ballRigidBody = null;
        ballSplineAnimate.Container = null;
        ballSplineAnimate = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && coroutine == null)
        {
            InputManager.Instance.OnDisable();
            ballRigidBody = other.GetComponent<Rigidbody>();
            ballSplineAnimate = other.GetComponent<SplineAnimate>();

            ballRigidBody.useGravity = false;
            ballSplineAnimate.Container = splineContainer;
            ballRigidBody.linearVelocity = Vector3.zero;
            ballRigidBody.angularVelocity = Vector3.zero;

            ballSplineAnimate.AnimationMethod = animationMethod;

            if (ballSplineAnimate.AnimationMethod == SplineAnimate.Method.Time)
                ballSplineAnimate.Duration = duration;
            else
                ballSplineAnimate.MaxSpeed = speed;

            ballSplineAnimate.ElapsedTime = 0;
            ballRigidBody.interpolation = RigidbodyInterpolation.None;

            ballSplineAnimate.Completed += ResetCoroutine;

            coroutine = StartCoroutine(LaunchBall());
        }
    }
}
