using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraOffset;
    public float offsetSpeed;
    public float cameraSize;

    private void Start()
    {
        gameObject.transform.position = RobotController.Instance.transform.position - new Vector3(0,0,cameraSize);
    }

    private void FixedUpdate()
    {
        FollowRobo();
    }

    void FollowRobo()
    {
        Vector3 roboPos = new Vector3(RobotController.Instance.transform.position.x,
            RobotController.Instance.transform.position.y,gameObject.transform.position.z);
        
        if (RobotController.Instance.isGrounded)
        {
            if (RobotController.Instance.isGoingRight)
            {
                roboPos = new Vector3(roboPos.x + cameraOffset,gameObject.transform.position.y,gameObject.transform.position.z);
            }
            
            if (RobotController.Instance.isGoingLeft)
            {
                roboPos = new Vector3(roboPos.x - cameraOffset,gameObject.transform.position.y,gameObject.transform.position.z);
            }
        }
        
        gameObject.transform.position =
            Vector3.Lerp(gameObject.transform.position, roboPos, offsetSpeed * Time.deltaTime);
    }
}
