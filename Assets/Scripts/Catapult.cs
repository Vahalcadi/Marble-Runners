using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public class Catapult : MonoBehaviour
{
    /*private GameObject ball;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration;
    //[SerializeField] private float height;
    [SerializeField] private float timeBeforeLaunch;
    [SerializeField] private Transform start;
    [SerializeField] private Transform arrival;

    private Coroutine coroutine;*/

    //private IEnumerator LaunchBall()
    //{
    //    float t = 0;
    //    ball.GetComponent<Rigidbody>().useGravity = false;
    //    ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

    //    Vector3 end = new Vector3(arrival.position.x, arrival.position.y, start.position.z);

    //    yield return new WaitForSeconds(timeBeforeLaunch);

    //    while (t < duration)
    //    {

    //        t += Time.deltaTime;

    //        float linearT = t / duration;
    //        float heightT = curve.Evaluate(linearT);

    //        float height = Mathf.Lerp(0, this.height, heightT);

    //        ball.transform.position = Vector3.Lerp(start.position, end, linearT) + new Vector3(0, height, 0);

    //        yield return null;
    //    }

    //    ball.GetComponent<Rigidbody>().useGravity = true;
    //    ball = null;
    //    InputManager.Instance.OnEnable();
    //}

    /*private IEnumerator LaunchBall()
    {
        float t = 0;
        float percentage = 0;
        ball.GetComponent<Rigidbody>().useGravity = false;
        ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        yield return new WaitForSeconds(timeBeforeLaunch);

        Vector3 startPosition = start.position;
        Vector3 endPosition = this.arrival.transform.position;

        while (percentage <= 1)
        {
            t += Time.deltaTime;
            percentage = t / duration;

            float height = curve.Evaluate(percentage);

            float x = Mathf.Lerp(startPosition.x, endPosition.x, percentage);
            float z = Mathf.Lerp(startPosition.z, endPosition.z, percentage);

            Vector3 currentPosition = new Vector3(x, startPosition.y + height, z);

            ball.transform.position = currentPosition;

            yield return null;
        }

        ball.GetComponent<Rigidbody>().useGravity = true;
        ball = null;
        InputManager.Instance.OnEnable();
        coroutine = null;
    }*/

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && coroutine == null)
        {
            ball = other.gameObject;
            InputManager.Instance.OnDisable();
            coroutine = StartCoroutine(LaunchBall());
        }
    }*/

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
        ballSplineAnimate.Play();
    }

    private void Start()
    {
        splineContainer = GetComponent<SplineContainer>();
    }

    private void ResetCoroutine()
    {
        ballSplineAnimate.Completed -= ResetCoroutine;
        ballRigidBody = null;
        ballSplineAnimate = null;
        coroutine = null;
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

            if (animationMethod == SplineAnimate.Method.Time)
                ballSplineAnimate.Duration = duration;
            else
                ballSplineAnimate.MaxSpeed = speed;

            ballSplineAnimate.Completed += ResetCoroutine;

            coroutine = StartCoroutine(LaunchBall());
        }
    }
}
