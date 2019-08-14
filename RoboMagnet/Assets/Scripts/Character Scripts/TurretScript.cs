using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    #region Raycast Hit Method Variables

    

    #endregion
    
    /*#region Distance Method Variables
    
    [Header("Sound Effects")] 
    public AudioSource audioPlayer;
        
    [Header("Turret Objects")] 
    public GameObject bullet;
    public Transform shootingPoint;
        
    [Header("Turret Settings")] public float totalDistance; 
    public float firingDistance; 
    public float shotForce; 
    public float shotTimer; 
    public float timeToShoot;

    private bool soundPlaying;
        
    #endregion*/
    
    #region Raycast Hit Method
    private void FixedUpdate()
    {
        
    }

    #endregion

    /*#region Distance Between Turret and Robo Method

    void FixedUpdate()
    {
        
        
        if (CheckDistance())
        {
            if (!soundPlaying)
            {
                audioPlayer.Play();
                soundPlaying = true;
            }
            
            shotTimer += Time.deltaTime;
                
            if (shotTimer > timeToShoot)
            {
                SpawnAndThrow(); 
                shotTimer = 0;
            }
    
            if (shotTimer == 0)
            {
                shotTimer += Time.deltaTime;
            }
        }
    
        else
        {
            shotTimer = 0;
            audioPlayer.Stop();
            soundPlaying = false;
        }
    }
        
    private bool CheckDistance()
    {
        totalDistance = Vector3.Distance(gameObject.transform.position, RobotController.Instance.transform.position);
    
        if (totalDistance < firingDistance)
        {
            return true;
        }
    
        return false;
    }
    
    void SpawnAndThrow()
    {
        if (CheckDistance()) 
        {
            GameObject spawnedBullet = Instantiate(bullet, shootingPoint.position,Quaternion.identity);
    
            spawnedBullet.GetComponent<Rigidbody2D>()
                .AddForce(new Vector2(shotForce, RobotController.Instance.transform.position.y * 300));
        }
    }
    
    #endregion*/
}