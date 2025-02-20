using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public class Tube : MonoBehaviour
{
    private SplineAnimate ballSplineAnimate;
    private Rigidbody ballRigidBody;
    private SplineContainer splineContainer;

    [SerializeField] private float duration = 3;

    private int startOffset;
    [HideInInspector] public bool hasStarted;

    private void Start()
    {
        hasStarted = false;
        splineContainer = GetComponent<SplineContainer>();
    }

    public void StartTubeLogic(Rigidbody rb, SplineAnimate sa, int startOffset)
    {
        AudioManager.Instance.PlaySFXDirectly(10, null);

        hasStarted = true;
        InputManager.Instance.OnDisable();
        ballRigidBody = rb;
        ballSplineAnimate = sa;

        ballRigidBody.useGravity = false;
        ballSplineAnimate.Container = splineContainer;
        ballRigidBody.linearVelocity = Vector3.zero;
        ballRigidBody.angularVelocity = Vector3.zero;


        ballSplineAnimate.AnimationMethod = SplineAnimate.Method.Time;
        ballSplineAnimate.Duration = duration;

        ballSplineAnimate.ElapsedTime = 0;
        ballRigidBody.interpolation = RigidbodyInterpolation.None;

        ballSplineAnimate.Completed += ResetCoroutine;

        this.startOffset = startOffset;
        StartCoroutine(MoveBall());
    }

    private IEnumerator MoveBall()
    {
        //ballSplineAnimate.StartOffset = startOffset;

        if(startOffset == 0)
            yield return StartToEnd();
        else
            yield return EndToStart();
    }

    private IEnumerator EndToStart()
    {
        float t = 1;
        float percentage = 1;

        while (percentage >= 0)
        {
            yield return null;
            t -= Time.deltaTime;
            percentage = t / duration;

            Vector3 posEvaluated = splineContainer.Spline.EvaluatePosition(percentage);

            ballRigidBody.transform.position = new Vector3(transform.position.x + posEvaluated.x, transform.position.y + posEvaluated.y, transform.position.z + posEvaluated.z);
        }

        ResetCoroutine();
    }

    private IEnumerator StartToEnd()
    {
        float t = 0;
        float percentage = 0;

        while (percentage <= 1)
        {
            yield return null;
            t += Time.deltaTime;
            percentage = t / duration;

            Vector3 posEvaluated = splineContainer.Spline.EvaluatePosition(percentage);

            ballRigidBody.transform.position = new Vector3(transform.position.x + posEvaluated.x, transform.position.y + posEvaluated.y, transform.position.z + posEvaluated.z); 
        }

        ResetCoroutine();
    }

    private void ResetCoroutine()
    {
        AudioManager.Instance.StopSFXDirectly(10, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

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
