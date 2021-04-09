using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacingMovement : MonoBehaviour
{
#if UNITY_IOS || UNITY_ANDROID
    public Camera cam;
    protected Plane Plane;
    
    [Header("Move")]
    public bool Move;
    public float DecreaseObjectMovespeed = 1;
    public bool inverseMove;
    public float moveSpacing;
    public float moveThreshold;
    public bool moveAfterSwipeEnd;

    [Header("Enlarge")]
    public bool Enlarge;
    public float DecreaseObjectEnlarge = 1;
    public bool inverseEnlarge;

    [Header("Rotation")]
    public bool Rotate;
    public float DecreaseObjectRotationSpeed = 1;
    public bool inverseRotation;

    private Vector2 startTouchPos;
    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;
    }
    

    private void Update()
    {
        
        //Update Plane
        if (Input.touchCount >= 1)
            Plane.SetNormalAndPosition(transform.up, transform.position);

        var Delta1 = Vector3.zero;
        var Delta2 = Vector3.zero;

        //Move Object
        #region Move
        if (Input.touchCount == 1 && Move)
        {
            Touch currentTouch = Input.GetTouch(0);
            Vector2 currentTouchPos;

            //TOUCH PHASE [BEGAN]
            //Register first and current touch
            if (currentTouch.phase == TouchPhase.Began)
            {
                startTouchPos = currentTouch.position;
            }

            //TOUCH PHASE [MOVED]

            if (currentTouch.phase == TouchPhase.Moved)
            {
                if (!moveAfterSwipeEnd)
                {
                    currentTouchPos = currentTouch.position;
                    checkSwipe(currentTouchPos);
                }
              

            }

            if (currentTouch.phase == TouchPhase.Ended)
            {
                currentTouchPos = currentTouch.position;
                checkSwipe(currentTouchPos);
            }
        }
        #endregion

        //Enlarge & rotate function
        if (Input.touchCount >= 2)
        {
            //Calculate Fingers
            #region FingerCalculation
            //Ray between last frame finger and current
            var pos1 = PlanePosition(Input.GetTouch(0).position);
            var pos2 = PlanePosition(Input.GetTouch(1).position);
            var pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
            var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

            //calc zoom
            var zoom = Vector3.Distance(pos1, pos2) /
                       Vector3.Distance(pos1b, pos2b);

            //edge case
            if (zoom == 0 || zoom > 10)
                return;

            #endregion

            //Enlarge function
            #region Enlarge
            if (Enlarge)
            {
                if (inverseEnlarge)
                {
                    if (zoom < 1)
                    {
                        gameObject.transform.localScale = gameObject.transform.localScale + (Vector3.one * zoom / (20 * DecreaseObjectEnlarge));
                    }
                    else if (zoom > 1)
                    {
                        gameObject.transform.localScale = gameObject.transform.localScale - (Vector3.one * zoom / (20 * DecreaseObjectEnlarge));
                    }
                }
                else if (!inverseEnlarge)
                {
                    if (zoom < 1)
                    {
                        gameObject.transform.localScale = gameObject.transform.localScale - (Vector3.one * zoom / (20 * DecreaseObjectEnlarge));
                    }
                    else if (zoom > 1)
                    {
                        gameObject.transform.localScale = gameObject.transform.localScale + (Vector3.one * zoom / (20 * DecreaseObjectEnlarge));
                    }
                }

            }
            #endregion

            //Rotation Function
            #region Rotation
            if (Rotate && pos2b != pos2)
            {
                if (inverseEnlarge)
                {
                    gameObject.transform.RotateAround(gameObject.transform.position, Plane.normal, (Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, Plane.normal) / DecreaseObjectRotationSpeed));
                }
                else if (!inverseEnlarge)
                {
                    gameObject.transform.RotateAround(gameObject.transform.position, Plane.normal, -(Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, Plane.normal) / DecreaseObjectRotationSpeed));
                }
            }
            #endregion
        }

    }

    private void checkSwipe(Vector2 currentTouchPos)
    {
        //Register Swipe Value
        float verticalSwipeVal = MobileInputExtension.verticalSwipeVal(startTouchPos, currentTouchPos);
        float horizontalSwipeVal = MobileInputExtension.horizontalSwipeVal(startTouchPos, currentTouchPos);
        //Check if Vertical swipe
        if (verticalSwipeVal > moveThreshold && verticalSwipeVal > horizontalSwipeVal)
        {
            //Up
            if (currentTouchPos.y - startTouchPos.y > 0)
            {
                gameObject.transform.Translate(new Vector3(0,0, moveSpacing), Space.World);
            }
            //Down
            else if (currentTouchPos.y - startTouchPos.y < 0)
            {
                gameObject.transform.Translate(new Vector3(0, 0, -moveSpacing), Space.World);
            }

            //Re-registering startTouchPos
            startTouchPos = currentTouchPos;
        }

        //Check if Horizontal swipe
        else if (horizontalSwipeVal > moveThreshold && horizontalSwipeVal > verticalSwipeVal)
        {
            //Right
            if (currentTouchPos.x - startTouchPos.x > 0)
            {
                gameObject.transform.Translate(new Vector3(moveSpacing, 0, 0), Space.World);
            }
            //Left
            else if (currentTouchPos.x - startTouchPos.x < 0)
            {
                gameObject.transform.Translate(new Vector3(-moveSpacing, 0, 0), Space.World);
            }
            startTouchPos = currentTouchPos;
        }

        //No Movement at-all
        else
        {
            Debug.Log("No Swipe!");
        }
    }


    //Returns Vector3 distance between first and final finger position
    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //not moved
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;


        //delta
        var rayBefore = cam.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = cam.ScreenPointToRay(touch.position);
        if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
        {
            return inverseMove ? (rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow)) : -(rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow));

        }

        //not on plane
        return Vector3.zero;
    }
   
    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //position
        var rayNow = cam.ScreenPointToRay(screenPos);
        if (Plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }

    //public switch Functions
    public void SwitchMove()
    {
        Move = !Move;
    }
    public void SwitchEnlarge()
    {
        Enlarge = !Enlarge;
    }
    public void SwitchRotate()
    {
        Rotate = !Rotate;
    }
#endif
}
