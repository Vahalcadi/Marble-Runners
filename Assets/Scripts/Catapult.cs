using System.Collections;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    private GameObject ball;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration;
    [SerializeField] private float height;
    [SerializeField] private float timeBeforeLaunch;
    [SerializeField] private Transform start;
    [SerializeField] private Transform arrival;


    private IEnumerator LaunchBall()
    {
        float t = 0;
        ball.GetComponent<Rigidbody>().useGravity = false;
        ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        Vector3 end = new Vector3(arrival.position.x, arrival.position.y, start.position.z);

        yield return new WaitForSeconds(timeBeforeLaunch);

        while (t < duration)
        {

            t += Time.deltaTime;

            float linearT = t / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0, this.height, heightT);

            ball.transform.position = Vector3.Lerp(start.position, end, linearT) + new Vector3(0, height, 0);

            yield return null;
        }

        ball.GetComponent<Rigidbody>().useGravity = true;
        ball = null;
        InputManager.Instance.OnEnable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ball = other.gameObject;
            InputManager.Instance.OnDisable();
            StartCoroutine(LaunchBall());
        }
    }
}
