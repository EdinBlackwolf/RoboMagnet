using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MagnetScript : MonoBehaviour
{
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject Llimit;
    public GameObject Rlimit;
    public GameObject holdLeft;
    public GameObject holdRight;
    public float pullForce;
    Rigidbody2D rb;
    bool inContact;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        magnetManager();
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
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (!inContact)
                {
                    transform.position = Vector3.Slerp(transform.position, hitInfo.transform.position, Time.deltaTime * pullForce);
                    rb.constraints = RigidbodyConstraints2D.None;
                }
                else if (inContact)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                }
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.None;
            }
        }
        else if(hitInfo.collider != null && hitInfo.collider.tag == "Positive")
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                hitInfo.collider.GetComponentInChildren<PointEffector2D>().enabled = true;
            }
        }
        else if (hitInfoSmall.collider != null && hitInfoSmall.collider.tag == "SmallItem")
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //hitInfoSmall.collider.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                //hitInfoSmall.transform.position = Vector3.Slerp(hitInfoSmall.transform.position, holdLeft.transform.position, Time.deltaTime * 100);
                hitInfoSmall.transform.DOMove(holdLeft.transform.position, 0.001f);
            }
            else
            {
                //hitInfoSmall.collider.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }
        }
    }
    private void RightRay()
    {
        RaycastHit2D hitInfo;
        Debug.DrawLine(rightArm.transform.position, Rlimit.transform.position, Color.magenta);
        hitInfo = Physics2D.Linecast(rightArm.transform.position, Rlimit.transform.position, 1 << LayerMask.NameToLayer("Magnet"));
        if (hitInfo.collider != null && hitInfo.collider.tag == "Positive")
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                if (!inContact)
                {
                    transform.position = Vector3.Slerp(transform.position, hitInfo.transform.position, Time.deltaTime * pullForce);
                    rb.constraints = RigidbodyConstraints2D.None;
                }
                else if (inContact)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                }
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.None;
            }
        }
        else if (hitInfo.collider != null && hitInfo.collider.tag == "Negative")
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                hitInfo.collider.GetComponentInChildren<PointEffector2D>().enabled = true;
            }
        }
    }

    void magnetManager()
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
            PointEffector2D[] effector = FindObjectsOfType<PointEffector2D>();
            foreach (PointEffector2D o in effector)
            {
                if (o.gameObject.scene == gameObject.scene && o != null)
                {
                    o.enabled = false;
                }
            }
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
}
