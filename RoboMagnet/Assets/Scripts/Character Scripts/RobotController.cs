﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class RobotController : MonoBehaviour
{ 
    public float movementSpeed;
    public float leaningLimit;
    public float leaningDuration;

    public bool isGrounded;

    public GameObject head;

    public Rigidbody2D roboRB;
    
    private KeyCode currentlyPressed;
    
    void Start()
    {
        roboRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        LookAtMouse();
    }

    void FixedUpdate()  
    {
        BasicMovement();
    }

    void BasicMovement()
    {
        float movX = Input.GetAxis("Horizontal");

        if (isGrounded)
        {
            roboRB.velocity = new Vector2(movX * movementSpeed, 0);
        }

        LeanTween();
    }

    void LeanTween()
    {
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.DORotate(new Vector3(0,0,-leaningLimit), leaningDuration);
        }

        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.DORotate(new Vector3(0,0,leaningLimit), leaningDuration);
        }
        
        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        }
    }

    void LookAtMouse()
    {
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(head.transform.position); //vector between the object and mouse
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //take the angle between the vectors.

        head.transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward); //head looks forward
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
}
