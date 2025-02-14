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

        //Enable accelerometer sensor only in build
#if UNITY_ANDROID && !UNITY_EDITOR
        UnityEngine.InputSystem.InputSystem.EnableDevice(Accelerometer.current);
#endif
    }

    public Vector3 Move()
    {
        /**
         * read the value as a vector3. It's important to never normalise this vector
         * **/
        Vector3 vector = controls.Player.Movement.ReadValue<Vector3>(); 

        /**
         * rotate the vector about -90 degrees around the x axis. This simulates a device laying down, screen size up
         * **/
        vector = Quaternion.Euler(-90, 0, 0) * vector; 

        /**
         * Extension method to adapt the vector to the isometric plane
         * If you don't need this, you can just return the vector as it is.
         * **/
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
