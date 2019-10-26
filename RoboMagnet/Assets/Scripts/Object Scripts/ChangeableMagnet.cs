using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeableMagnet : MonoBehaviour
{
    public float timer;
    public float waitTime;
    public float distance;
    public string currentMagnet;
    
    public GameObject posMagnet;
    public GameObject negMagnet;

    public Vector2 originPosition;
    public Vector2 arrivingPosition;
    
    void Start()
    {
        originPosition = gameObject.transform.position;
        arrivingPosition = originPosition - new Vector2(0,distance);
        print("anan");
    }
    
    void Update()
    {
        MagnetMovement(posMagnet);
    }

    void MagnetSystem()
    {
        
    }

    void MagnetMovement(GameObject objectToMove)
    {
        bool firstTime = true;
        
        /*switch (currentMagnet)
        {
            case "Positive":
            {
                objectToMove = posMagnet;
                break;
            }
            case "Negative":
            {
                objectToMove = negMagnet;
                break;
            }
        }*/

        if (timer < waitTime)
        {
            objectToMove.transform.position = Vector3.Lerp(originPosition, arrivingPosition, 2.5f * Time.time);
            timer += Time.deltaTime;

            if (timer > waitTime)
            {
                objectToMove.transform.position = Vector3.Lerp(arrivingPosition, originPosition, 2.5f * Time.time);
            }
        }
    }
}
