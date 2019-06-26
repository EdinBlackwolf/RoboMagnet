using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    public Transform endMagnetPositive;
    public Transform endMagnetNegative;
    public GameObject leftArm;
    public GameObject rightArm;
    bool spotted = false;
    public LayerMask mask;
    Rigidbody2D rb;
    bool inContact;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LeftRay();
        if (inContact)
        {
            print("he");
        }
        if (Input.GetKey(KeyCode.R))
        {
            MagnetMovement();
        }
        if (Input.GetKey(KeyCode.Q) && !inContact)
        {
            transform.parent.position = Vector3.Slerp(transform.parent.position, endMagnetNegative.position, Time.deltaTime * 1f);
        //    if (Vector3.Distance(endMagnetNegative.position, transform.position) < 1)
        //    {
        //        //transform.position = Vector3.Slerp(transform.position, endMagnetNegative.position, Time.deltaTime * 10f);
        //        //transform.parent.position = Vector3.Slerp(transform.parent.position, endMagnetNegative.position, Time.deltaTime * 10f);
        //    }
        //    else
        //    {
        //        MagnetMovement_();
        //    }
        }
        else
        {
            //rb.angularVelocity = 0;
            //print("he");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //if(other.otherCollider.tag == "Positive")
        //{
        //    inContact = true;
        //}
        //else
        //{
        //    inContact = false;
        //}
        if (other.otherCollider.tag == "Negative")
        {
            inContact = true;
        }
        else
        {
            inContact = false;
        }
    }

    void MagnetMovement()
    {
        transform.position = Vector3.Slerp(transform.position, endMagnetPositive.position, Time.deltaTime * 3f);
    }

    void MagnetMovement_()
    {
        //transform.position = Vector3.Slerp(transform.position, endMagnetNegative.position, Time.deltaTime * 3f);
    }

    private void LeftRay()
    {
        Debug.DrawLine(leftArm.transform.position, leftArm.transform.up, Color.green);
        spotted = Physics2D.Linecast(leftArm.transform.position, leftArm.transform.up, 1);

        //RaycastHit2D hitInfo = Physics2D.Raycast(leftArm.transform.position, leftArm.transform.up,10);

        //if (Physics2D.Raycast(ray, out hitInfo, 1.0f, mask, QueryTriggerInteraction.Ignore))
        //{
        //    Debug.DrawLine(ray.origin, hitInfo.point, Color.yellow);
        //    return true;
        //}
        //else
        //{
        //    Debug.DrawLine(ray.origin, ray.origin + ray.direction * 0.5f, Color.blue);
        //    return false;
        //}
        //if (hitInfo.collider != null)
        //{
        //    print("hue");
        //    Debug.DrawLine(leftArm.transform.position, leftArm.transform.up, Color.green);
        //    return true;
        //}
        //else
        //    Debug.DrawLine(leftArm.transform.position, leftArm.transform.up, Color.red);
        //{
        //    return false;

        //}
    }    
}
