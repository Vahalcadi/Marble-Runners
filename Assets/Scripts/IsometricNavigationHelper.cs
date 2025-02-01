using UnityEngine;

public static class IsometricNavigationHelper
{

    private static Matrix4x4 isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, -45, 0));

    public static Vector3 AdjustToIsometricPlane(this Vector3 input) => isometricMatrix.MultiplyPoint3x4(input);
    
}
