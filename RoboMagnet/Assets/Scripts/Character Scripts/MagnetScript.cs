using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagnetScript : MonoBehaviour
{
    PointEffector2D LCollider;
    PointEffector2D RCollider;
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject Llimit;
    public GameObject Rlimit;
    public GameObject holdLeft;
    public GameObject holdRight;
    public BoxCollider2D LeftCollider;

    public Transform ColliderTransform;

    public float pullForce;
    public float pushForce;
    Rigidbody2D rb;
    bool inContact;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        LCollider = GetComponentInChildren<PointEffector2D>();
        RCollider = GetComponentInChildren<PointEffector2D>();
    }

    void Update()
    {
        MagnetManager();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Negative" || other.gameObject.tag == "Positive")
        {
            inContact = true;
        }
        else
        {
            inContact = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "SmallItem")
        {
            ColliderTransform.transform.GetComponent<Rigidbody2D>().gravityScale = 0;
            print("0");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SmallItem")
        {
            ColliderTransform.transform.GetComponent<Rigidbody2D>().gravityScale = 1;
            print("1");
        }
    }

    private void LeftRay()
    {
        RaycastHit2D hitInfo;
        RaycastHit2D hitInfoSmall;
        Debug.DrawLine(leftArm.transform.position, Llimit.transform.position, Color.green);
        hitInfo = Physics2D.Linecast(leftArm.transform.position,Llimit.transform.position, 1 << LayerMask.NameToLayer("Magnet"));
        hitInfoSmall = Physics2D.Linecast(leftArm.transform.position, Llimit.transform.position, 1 << LayerMask.NameToLayer("Small"));
        if (hitInfo.collider != null && hitInfo.collider.tag == "Negative")
        {
            hitInfo.collider.GetComponentInChildren<PointEffector2D>().forceMagnitude = pullForce;
            hitInfo.collider.GetComponentInChildren<PointEffector2D>().enabled = true;
        }
        else if(hitInfo.collider != null && hitInfo.collider.tag == "Positive")
        {
            hitInfo.collider.GetComponentInChildren<PointEffector2D>().forceMagnitude = pushForce;
            hitInfo.collider.GetComponentInChildren<PointEffector2D>().enabled = true;
        }
        else if (hitInfo.collider == null)
        {
            deactivateMangets();
        }
        if (hitInfoSmall.collider != null && hitInfoSmall.collider.tag == "SmallItem")
        {

            if (Input.GetKey(KeyCode.Mouse0))
            {
                
            }
        }
        else if(hitInfoSmall.collider == null)
        {
            //print("detach");
            //holdLeft.transform.DetachChildren();
        }

    }
    private void RightRay()
    {
        RaycastHit2D hitInfo;
        Debug.DrawLine(rightArm.transform.position, Rlimit.transform.position, Color.magenta);
        hitInfo = Physics2D.Linecast(rightArm.transform.position, Rlimit.transform.position, 1 << LayerMask.NameToLayer("Magnet"));
        if (hitInfo.collider != null && hitInfo.collider.tag == "Positive")
        {
            hitInfo.collider.GetComponentInChildren<PointEffector2D>().forceMagnitude = pullForce;
            hitInfo.collider.GetComponentInChildren<PointEffector2D>().enabled = true;        
        }
        else if (hitInfo.collider != null && hitInfo.collider.tag == "Negative")
        {
            hitInfo.collider.GetComponentInChildren<PointEffector2D>().forceMagnitude = pushForce;
            hitInfo.collider.GetComponentInChildren<PointEffector2D>().enabled = true;
        }
        else if (hitInfo.collider == null)
        {
            deactivateMangets();
        }
    }

    void MagnetManager()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1))
        {
            RightRay();
            LeftRay();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            LeftRay();
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            RightRay();
        }
        else
        {
            deactivateMangets();
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void deactivateMangets()
    {
        PointEffector2D[] effector = FindObjectsOfType<PointEffector2D>();
        foreach (PointEffector2D o in effector)
        {
            if (o.gameObject.scene == gameObject.scene && o != null)
            {
                o.enabled = false;
            }
        }
    }
}
