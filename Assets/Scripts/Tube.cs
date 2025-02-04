using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class Tube : MonoBehaviour
{
    private GameObject ball;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration;
    [SerializeField] private float timeBeforeLaunch;
    [SerializeField] private Transform start;
    [SerializeField] private Transform arrival;

    private Coroutine coroutine;

    //private IEnumerator LaunchBall()
    //{
    //    float t = 0;
    //    float percentage = 0;
    //    ball.GetComponent<Rigidbody>().useGravity = false;
    //    ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

    //    yield return new WaitForSeconds(timeBeforeLaunch);

    //    while (percentage <= 1)
    //    {

    //        t += Time.deltaTime;

    //        percentage = t / duration;
    //        float height = curve.Evaluate(percentage);

    //        Vector3 arrival = new Vector3(this.arrival.transform.position.x, transform.position.y + height, this.arrival.transform.position.z);

    //        ball.transform.position = Vector3.Lerp(start.position, arrival, percentage);

    //        yield return null;
    //    }

    //    ball.GetComponent<Rigidbody>().useGravity = true;
    //    ball = null;
    //    InputManager.Instance.OnEnable();
    //    coroutine = null;
    //}

    private IEnumerator LaunchBall()
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
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && coroutine == null)
        {
            ball = other.gameObject;
            InputManager.Instance.OnDisable();
            coroutine = StartCoroutine(LaunchBall());
        }
    }
}
