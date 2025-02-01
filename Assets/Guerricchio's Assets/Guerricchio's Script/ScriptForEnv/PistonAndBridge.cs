using UnityEngine;

public class PistonAndBridge : MonoBehaviour
{
    [Header("Bridge")]
    public Transform bridge;
    public float tiltAngle = 30f;
    public float tiltDuration = 0.5f;

    [Header("Piston")]
    public Transform piston;
    public float liftHeight = 2f;
    public float liftDuration = 0.5f;

    [Header("Interval")]
    public float interval = 3f;

    private Vector3 initialPistonPosition;
    private Vector3 liftedPistonPosition;
    private Quaternion initialBridgeRotation;
    private Quaternion targetBridgeRotation;

    private bool isAnimating = false;

    private void Start()
    {
        initialBridgeRotation = bridge.rotation;
        targetBridgeRotation = Quaternion.Euler(tiltAngle, 0f, 0f) * initialBridgeRotation;

        initialPistonPosition = piston.position;
        liftedPistonPosition = new Vector3(piston.position.x, piston.position.y + liftHeight, piston.position.z);

        InvokeRepeating(nameof(StartPistonCycle), 0f, interval);
    }

    private void StartPistonCycle()
    {
        if (!isAnimating)
            StartCoroutine(PistonAndBridgeCycle());
    }

    private System.Collections.IEnumerator PistonAndBridgeCycle()
    {
        isAnimating = true;

        yield return StartCoroutine(LiftPiston(liftDuration));

        yield return StartCoroutine(RotateBridge(initialBridgeRotation, targetBridgeRotation, tiltDuration));

        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(LiftPiston(liftDuration, reverse: true));
        yield return StartCoroutine(RotateBridge(targetBridgeRotation, initialBridgeRotation, tiltDuration));

        isAnimating = false;
    }

    private System.Collections.IEnumerator LiftPiston(float duration, bool reverse = false)
    {
        float elapsed = 0f;

        Vector3 startPosition = reverse ? liftedPistonPosition : initialPistonPosition;
        Vector3 endPosition = reverse ? initialPistonPosition : liftedPistonPosition;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            piston.position = Vector3.Lerp(startPosition, endPosition, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        piston.position = endPosition;
    }

    private System.Collections.IEnumerator RotateBridge(Quaternion fromRotation, Quaternion toRotation, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            bridge.rotation = Quaternion.Slerp(fromRotation, toRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        bridge.rotation = toRotation;
    }
}
