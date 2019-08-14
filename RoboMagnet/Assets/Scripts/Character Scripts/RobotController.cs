using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine.Serialization;

public class RobotController : MonoBehaviour
{ 
    public float movementSpeed;
    public float leaningLimit;
    public float leaningDuration;
    public float wheelSpeed;
    private float _distanceGround;
 
    public bool isGoingRight;
    public bool isGoingLeft;
    public bool isGrounded;
    
    public GameObject head;
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject wheel;

    public Rigidbody2D roboRB;

    private Collider2D _wheelCol;
    
    #region Singleton

    public static RobotController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    void Start()
    {
        roboRB = GetComponent<Rigidbody2D>();

       // _wheelCol = wheel.GetComponent<Collider2D>();
    }

    void Update()
    {
        LookAtMouse();
        ArmMovement();
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

        if (movX > 0)
        {
            isGoingRight = true;
        }
        else
        {
            isGoingRight = false;
        }

        if (movX < 0)
        {
            isGoingLeft = true;
        }
        else
        {
            isGoingLeft = false;
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) //this is here to eliminate the delay of the movX.
        {
            isGoingLeft = false;
            isGoingRight = false;
        }
        
        LeanTween();
        WheelTurn();
        
    }
    
    void WheelTurn()
    {
        if (Input.GetKey(KeyCode.D))
        {
            wheel.transform.localEulerAngles += new Vector3(0,0,-wheelSpeed); //turns right.
        }

        if (Input.GetKey(KeyCode.A))
        {
            wheel.transform.localEulerAngles += new Vector3(0,0,wheelSpeed); //turns left.
        }
    }

    void LeanTween()
    {
        if (isGoingRight)
        {
            
        }
        else if (isGoingLeft)
        {
            
        }
        else
        {
            
        }
    }

    void LookAtMouse()
    {
        var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(head.transform.position); //vector between the object and mouse
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //take the angle between the vectors.

        head.transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward); //head looks forward
    }

    void ArmMovement()
    {
        if (Input.GetButton("Fire1"))
        {
            var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(leftArm.transform.position);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            leftArm.transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
        }

        if (Input.GetButton("Fire2"))
        {
            var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(rightArm.transform.position);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            rightArm.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
