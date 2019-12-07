using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    Vector2 upperCord;
    Vector2 lowerCord;
    SpringJoint2D spring;
    public Rigidbody2D button;
    public bool buttonPressed = false;
    public GameObject upperDoor;
    public GameObject lowerDoor;

    // Start is called before the first frame update
    void Start()
    {
        upperCord = upperDoor.transform.position + upperDoor.transform.up * 10f;
        lowerCord = lowerDoor.transform.position + lowerDoor.transform.up * -10f;
        spring = gameObject.GetComponent<SpringJoint2D>();
        if (transform.eulerAngles.z == 0 || transform.eulerAngles.z == 180)
        {
            button.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else if (transform.eulerAngles.z == 90 || transform.eulerAngles.z == 270)
        {
            button.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print(spring.reactionForce);
        //print(spring.reactionForce.x);
        //print(transform.eulerAngles.z);
        if (transform.eulerAngles.z == 0)
        {
            if (spring.reactionForce.x > 300f)
            {
                button.constraints = RigidbodyConstraints2D.FreezeAll;
                buttonPressed = true;
            }
        }
        else if(transform.eulerAngles.z == 180)
        {
            if (spring.reactionForce.x < -150f)
            {
                button.constraints = RigidbodyConstraints2D.FreezeAll;
                buttonPressed = true;
            }
        }
        else if (transform.eulerAngles.z == 90)
        {
            if (spring.reactionForce.x < -500f)
            {
                button.constraints = RigidbodyConstraints2D.FreezeAll;
                buttonPressed = true;
            }
        }
        else if(transform.eulerAngles.z == 270)
        {
            if (spring.reactionForce.x > 500f)
            {
                button.constraints = RigidbodyConstraints2D.FreezeAll;
                buttonPressed = true;
            }
        }


        if (buttonPressed)
        {
            upperDoor.transform.position = Vector3.Lerp(upperDoor.transform.position, upperCord, Time.deltaTime * 1);
            lowerDoor.transform.position = Vector3.Lerp(lowerDoor.transform.position, lowerCord, Time.deltaTime * 1);
            upperDoor.GetComponent<BoxCollider2D>().enabled = false;
            lowerDoor.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
