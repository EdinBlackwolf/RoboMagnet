using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 velocityVector;
    
    public float smoothingTimeX;
    public float smoothingTimeY;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        FollowRobo();
    }

    void FollowRobo()
    {
        /*float posX = Mathf.SmoothDamp(gameObject.transform.position.x, RobotController.Instance.transform.position.x,
            ref velocityVector.x, smoothingTimeX);
        
        float posY = Mathf.SmoothDamp(gameObject.transform.position.y, RobotController.Instance.transform.position.y,
            ref velocityVector.y, smoothingTimeY);
        
        gameObject.transform.position = new Vector3(posX,posY,-30)*/;
    }
    
}
