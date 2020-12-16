using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AuxMethods
{
    /// Normalize an angle, guaranteeing it to be between -pi and pi 
    public static float NormAngle(float angle)
    {
        while (angle < -Mathf.PI) angle += Mathf.PI * 2f;
        while (angle > Mathf.PI) angle -= Mathf.PI * 2f;
        return angle;
    }

    public static Vector2 NormAngle(Vector2 angle)
    {
        while (angle.x < -Mathf.PI) angle.x += Mathf.PI * 2f;
        while (angle.x > Mathf.PI) angle.x -= Mathf.PI * 2f;

        while (angle.y < -Mathf.PI) angle.y += Mathf.PI * 2f;
        while (angle.y > Mathf.PI) angle.y -= Mathf.PI * 2f;

        return angle;
    }
}
