using UnityEngine;

public class PistonUpDown : MonoBehaviour
{
    [SerializeField] private float moveDistance = 2f;
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 startPos;
    private bool isMovingUp = false;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (isMovingUp)
        {
            float newY = Mathf.MoveTowards(transform.position.y, startPos.y + moveDistance, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(startPos.x, newY, startPos.z);

            if (transform.position.y >= startPos.y + moveDistance)
                isMovingUp = false;
        }
        else if (transform.position.y > startPos.y)
        {
            float newY = Mathf.MoveTowards(transform.position.y, startPos.y, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(startPos.x, newY, startPos.z);
        }
    }

    public void ActivatePiston()
    {
        isMovingUp = true;
    }

}
