using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MobileInputExtension
{
    //Return horizontal/vertical distance between the two point
    public static float verticalSwipeVal(Vector2 startPoint, Vector2 endPoint)
    {
        return Mathf.Abs(startPoint.y - endPoint.y);
    }

    public static float horizontalSwipeVal(Vector2 startPoint, Vector2 endPoint)
    {
        return Mathf.Abs(startPoint.x - endPoint.x);
    }
}
