using UnityEngine;

public class ButtonLoopScript : MonoBehaviour
{
    public float pressDepth = 0.1f;
    public float pressSpeed = 0.1f;
    public float pressInterval = 2f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timer = 0f;
    private bool goingDown = true;

    void Start()
    {
        startPosition = transform.localPosition;
        targetPosition = startPosition - new Vector3(0, pressDepth, 0);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= pressInterval)
        {
            if (goingDown)
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, pressSpeed * Time.deltaTime);
            else
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPosition, pressSpeed * Time.deltaTime);

            if (transform.localPosition == targetPosition || transform.localPosition == startPosition)
            {
                goingDown = !goingDown;
                timer = 0f;
            }
        }
    }
}
