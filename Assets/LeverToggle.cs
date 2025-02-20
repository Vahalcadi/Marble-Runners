using UnityEngine;

public class LeverToggle : MonoBehaviour
{
    public float toggleSpeed = 2f;
    public float toggleInterval = 2f;
    public float angleOn = -30f;
    public float angleOff = 30f;

    private Quaternion rotationOn;
    private Quaternion rotationOff;
    private float timer = 0f;
    private bool isOn = false;

    void Start()
    {
        rotationOn = Quaternion.Euler(angleOn, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
        rotationOff = Quaternion.Euler(angleOff, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= toggleInterval)
        {
            isOn = !isOn;
            timer = 0f;
        }


        transform.localRotation = Quaternion.Lerp(transform.localRotation, isOn ? rotationOn : rotationOff, toggleSpeed * Time.deltaTime);
    }
}
