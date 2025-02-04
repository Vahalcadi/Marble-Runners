using System.Collections;
using UnityEngine;

public class MovingWalls : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float lerpDuration;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Move()
    {

        while (true)
        {
            yield return MoveEachPoint(points[index]);

            if (index < points.Length - 1)
                index++;
            else
                index = 0;
        }
    }

    private IEnumerator MoveEachPoint(Transform point)
    {
        Vector3 startPos = transform.position;
        float t = 0;

        float percentage = 0;

        while (percentage <= 1)
        {
            yield return null;
            t += Time.deltaTime;
            percentage = t / lerpDuration;
            transform.position = Vector3.Lerp(startPos, point.position, percentage);
        }
    }

}
