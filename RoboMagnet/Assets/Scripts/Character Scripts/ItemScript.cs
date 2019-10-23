using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemScript : MonoBehaviour
{
    public bool plus;
    Rigidbody2D rb;
    GameObject rob;
    GameObject armItemFinderPlus;
    GameObject armItemFinderMinus;
    bool magnitezed = false;

    void Start()
    {
        rob = GameObject.Find("Robo");
        rb = GetComponentInParent<Rigidbody2D>();
        armItemFinderPlus = GameObject.Find("ItemHoldingPositionPositive");
        armItemFinderMinus = GameObject.Find("ItemHoldingPositionNegative");
    }

    void Update()
    {

        Magnetized();

    }

    bool Magnetized()
    {
        if (magnitezed == true && Input.GetKey(KeyCode.Mouse0) && plus == true)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
            rb.drag = 0;
            rb.transform.position = Vector3.Lerp(rb.transform.position, armItemFinderPlus.transform.position, Time.deltaTime * 6);
            return true;   
        }

        else if (magnitezed == true && Input.GetKey(KeyCode.Mouse1) && plus == false)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
            rb.drag = 0;
            rb.transform.position = Vector3.Lerp(rb.transform.position, armItemFinderMinus.transform.position, Time.deltaTime * 6);
            return true;
        }
        else
        {
            magnitezed = false;
        }
        return false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Robo"))
        {
            magnitezed = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Robo"))
        {
            magnitezed = true;
        }
    }



}
