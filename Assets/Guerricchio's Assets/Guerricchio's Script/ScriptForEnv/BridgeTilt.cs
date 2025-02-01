using System.Collections;
using UnityEngine;

public class BridgeTilt : MonoBehaviour
{
    [SerializeField] private float tiltAngle = 30f;
    [SerializeField] private float tiltDuration = 0.5f;
    [SerializeField] private float interval = 3f;

    private bool isTilting = false;

    private Quaternion initialRotation;
    private Quaternion targetRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = Quaternion.Euler(tiltAngle, 0f, 0f) * initialRotation; // X axis inclination
        InvokeRepeating(nameof(StartTiltCycle), 0f, interval);
    }

    void StartTiltCycle()
    {
        if (!isTilting)
            StartCoroutine(TiltAndReturn());
    }

    IEnumerator TiltAndReturn()
    {
        isTilting = true;

        //inclination(opening bridge)
        yield return StartCoroutine(RotateOverTime(initialRotation, targetRotation, tiltDuration));

        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(RotateOverTime(targetRotation, initialRotation, tiltDuration));

        isTilting = false;

    }

    IEnumerator RotateOverTime(Quaternion fromRotation, Quaternion toRotation, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(fromRotation, toRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = toRotation;
    }
}
