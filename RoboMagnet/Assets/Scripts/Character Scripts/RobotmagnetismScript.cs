using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotmagnetismScript : MonoBehaviour
{
    public bool Positive;
    public bool Negative;
    public PointEffector2D velocityPositive;
    public PointEffector2D velocityNegative;
    public ChildColliderScript pointing;
    public ChildColliderScript pointing2;
    public GameObject leftArm;
    public GameObject rightArm;




    private void Start()
    {
        //pointing.x = hue;
        leftArm.SetActive(true);
        rightArm.SetActive(true);
    }
   

    void Update()
    {
            if (Negative == true && pointing.x ==1)
            {
                velocityPositive.forceMagnitude = -30.0f;
            }
            else if (Negative == false || pointing.x < 1)
            {
                velocityPositive.forceMagnitude = 0.0f;
            }
            if (Positive == true && pointing2.x == 1)
            {
                velocityPositive.forceMagnitude = 30.0f;
            }
            if (Positive == true && pointing2.x == 2)
            {
                velocityNegative.forceMagnitude = -30.0f;
            }
            else if (Positive == false || pointing2.x < 2)
            {
                velocityNegative.forceMagnitude = 0.0f;
            }
            if (Negative == true && pointing.x == 2)
            {
                velocityNegative.forceMagnitude = 30.0f;
            }
            //else if (Positive == false)
            //{
            //     //velocity.forceMagnitude = 0.0f;
            //}
            Debug.Log(pointing2.x);
    }

    void OnTriggerEnter2D(Collider2D other)
        {
        if (other.gameObject.tag == "Positive")
        {
            
        }
    }
    //void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.gameObject.tag == "Negative")
    //    {
    //        if (Negative == true)
    //        {
    //            velocity.forceMagnitude = 0.0f;
    //        }
    //        if (Positive == true)
    //        {
    //            velocity.forceMagnitude = 0.0f;
    //        }
    //    }
    //}
}

