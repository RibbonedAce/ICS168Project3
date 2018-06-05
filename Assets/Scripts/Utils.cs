using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Axis
{
    X,
    Y,
    Z
}

public delegate void TargetGO(GameObject g);

public static class Utils 
{
    #region Methods
    /// <summary>
    /// Move a vector to a certain axis within a certain direction vector
    /// </summary>
    /// <param name="v">The vector3 to use</param>
    /// <param name="axis">The axis to set</param>
    /// <param name="value">The value to set the axis at</param>
    /// <param name="direction">The vector direction to move along</param>
    /// <returns>Returns the modified vector</returns>
	public static Vector3 MoveToAxisPosition(this Vector3 v, Axis axis, float value, Vector3 direction)
    {
        float m;
        switch (axis)
        {
            case Axis.X:
                if (v.x != value && direction.x == 0f)
                {
                    Debug.LogError("Direction is parallel to axis");
                }
                m = (value - v.x) / direction.x;
                return v + m * direction; 
            case Axis.Y:
                if (v.y != value && direction.y == 0f)
                {
                    Debug.LogError("Direction is parallel to axis");
                }
                m = (value - v.y) / direction.y;
                return v + m * direction;
            case Axis.Z:
                if (v.z != value && direction.z == 0f)
                {
                    Debug.LogError("Direction is parallel to axis");
                }
                m = (value - v.z) / direction.z;
                return v + m * direction;
        }
        return Vector3.zero;
    }

    /// <summary>
    /// Set the anchors of a rect transform
    /// </summary>
    /// <param name="t">The rect transform to use</param>
    /// <param name="min">The minimum of x and y</param>
    /// <param name="max">The maximum of x and y</param>
    public static void SetAnchors(this RectTransform t, Vector2 min, Vector2 max)
    {
        t.anchorMin = min;
        t.anchorMax = max;
        t.anchoredPosition = Vector2.zero;
        t.sizeDelta = Vector2.zero;
    }

    /// <summary>
    /// Move the given rigidbody's position by a set amount
    /// </summary>
    /// <param name="r">The rigidbody to use</param>
    /// <param name="pos">How much to move the position by</param>
    public static void MovePositionBy(this Rigidbody r, Vector3 pos)
    {
        r.MovePosition(r.position + pos);
    }
	#endregion
	
	#region Coroutines
    /// <summary>
    /// Call a function after a set amount of time
    /// </summary>
    /// <param name="t">The function to perform</param>
    /// <param name="g">The game object to pass when called</param>
    /// <param name="time">The seconds to wait before calling</param>
    /// <returns></returns>
	public static IEnumerator DoAfterTime(TargetGO t, GameObject g, float time)
    {
        yield return new WaitForSeconds(time);
        t(g);
    }
	#endregion
}