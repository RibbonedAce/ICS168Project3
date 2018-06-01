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
    /// <returns></returns>
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
    /// Replace listeners with new actions on a button
    /// </summary>
    /// <param name="b">The button to use</param>
    /// <param name="actions">The actions to replace with</param>
    public static void ReplaceListeners(this Button b, UnityEngine.Events.UnityAction[] actions)
    {
        b.onClick.RemoveAllListeners();
        foreach (UnityEngine.Events.UnityAction a in actions)
        {
            b.onClick.AddListener(a);
        }
    }
	#endregion
	
	#region Coroutines
	
	#endregion
}