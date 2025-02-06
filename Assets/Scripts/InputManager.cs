using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private PlayerControls controls;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;

        controls = new PlayerControls();

        InputSystem.EnableDevice(Accelerometer.current);
    }

    public Vector3 Move()
    {
        Vector3 vector = controls.Player.Movement.ReadValue<Vector3>();
       
        vector = Quaternion.Euler(-90, 0, 0) * vector;

        return vector.AdjustToIsometricPlane();
    }

    public Vector3 Jump()
    {
        Vector3 vector = controls.Player.Jump.ReadValue<Vector3>();

        vector = Quaternion.Euler(-90, 0, 0) * vector;

        return vector;
    }

    public void OnEnable()
    {
        controls.Enable();
    }

    public void OnDisable()
    {
        controls.Disable();
    }
}
