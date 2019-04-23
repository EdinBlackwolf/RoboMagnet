using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public Camera cam;
    
    public Rigidbody2D rb;
    public Animator animator;
    
    public bool isGrounded;
 
    public int headRotSpeed;

    private const int STATE_IDLE = 0;
    private const int STATE_WALK = 1;
    
    //public GameObject head;
    
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MovementMethod();
    }

    void MovementMethod()
    {
        /*mousePos = cam.WorldToScreenPoint(Input.mousePosition);
            head.transform.LookAt(mousePos);*/
        
        if(Input.GetKey(KeyCode.W) && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector2.up * 1300);
        }

        if (Input.GetKey(KeyCode.D) && isGrounded)
        {
            rb.velocity = Vector2.right * 20;
            transform.localRotation = Quaternion.Euler(0,0,0);
            
        }
        
        if (Input.GetKey(KeyCode.A) && isGrounded) 
        {
            rb.velocity = Vector2.left * 20;
            transform.localRotation = Quaternion.Euler(0,180,0);
        }
    }

    void AnimationState(int animState)
    {
        switch (animState)
        {
            case STATE_IDLE:
                animator.SetInteger("state",STATE_IDLE);
                break;
            
            case STATE_WALK:
                animator.SetInteger("state",STATE_WALK);
                break;
        }
    }
        
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }
}
