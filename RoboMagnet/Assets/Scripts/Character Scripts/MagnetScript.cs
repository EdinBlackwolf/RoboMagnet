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
        else if (hitInfoSmall.collider != null && hitInfoSmall.collider.tag == "SmallItem")
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Vector3.Distance(hitInfoSmall.transform.position, holdLeft.transform.position) > 1)
                {
                    print("Grabbed");                    
                    //hitInfoSmall.transform.DOMove(holdLeft.transform.position, 0.1f);
                    //hitInfoSmall.collider.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    hitInfoSmall.transform.position = Vector3.Slerp(hitInfoSmall.transform.position, holdLeft.transform.position, Time.deltaTime * 50);
                }

                //hitInfoSmall.collider.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                //hitInfoSmall.transform.position = Vector3.Slerp(hitInfoSmall.transform.position, holdLeft.transform.position, Time.deltaTime * 2);
                else
                {
                    hitInfoSmall.transform.position = holdLeft.transform.position;
                    //hitInfoSmall.transform.parent = holdLeft.transform.parent;
                }
            }
            else
            {
                //hitInfoSmall.collider.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }
        }
        LCollider.enabled = true;
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
