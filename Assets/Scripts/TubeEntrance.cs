using UnityEngine;
using UnityEngine.Splines;

public class TubeEntrance : MonoBehaviour
{
    [SerializeField] private bool startSplineFromEnd;
    private Tube tube;
    void Start()
    {
        tube = GetComponentInParent<Tube>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        SplineAnimate sa = other.GetComponent<SplineAnimate>();

        if (other.CompareTag("Player") && !tube.hasStarted)
        {
            AudioManager.Instance.PlaySFXDirectly(9, transform);
            if (startSplineFromEnd)
                tube.StartTubeLogic(rb, sa, 1);
            else
                tube.StartTubeLogic(rb, sa, 0);
        }
    }
}
