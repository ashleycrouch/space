using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour
{

    public bool tracking = false;
    public float rotSpeed = 10;
    public float viewAngle = 45;
    public float minAngle = 0;
    public float maxAngle = 180;

    Transform player;
    public bool rotDir = true;
    public float targetAngle;
    public float curAngle;
    
	void Start ()
    {
        curAngle =  transform.localRotation.eulerAngles.z;
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update ()
    {
        curAngle = transform.eulerAngles.z;

        if (tracking)
        {
            
            float angleDif = angleToPlayer();
            //Debug.Log(angleDif);
            
            if (playerInView(angleDif)) //Player is out of view angle
            {
                standardRot();
            }else if (angleDif > rotSpeed * Time.deltaTime)  //Trying to turn too fast
            {
                targetAngle = curAngle + rotSpeed * Time.deltaTime;
            }else if (angleDif - curAngle < -rotSpeed * Time.deltaTime) //Trying to turn too fast
            {
                targetAngle = curAngle - rotSpeed * Time.deltaTime;
            }else
            {
                targetAngle = angleDif + curAngle;
            }

            /*if (angle < viewAngle)
            {

                //Ray2D ray = new Ray2D();
                //ray.origin = transform.position;
            }*/
        }else
            standardRot();

        
        targetAngle = Mathf.Clamp(targetAngle, minAngle, maxAngle);
        if (targetAngle == minAngle)
            rotDir = true;
        else if (targetAngle == maxAngle)
            rotDir = false;
        transform.eulerAngles = new Vector3(0, 0, targetAngle);
        curAngle = targetAngle;
    }

    void standardRot()
    {
        if (rotDir) //Rotating positivly
        {
            targetAngle = curAngle + rotSpeed * Time.deltaTime;
        }else
        {
            targetAngle = curAngle - rotSpeed * Time.deltaTime;
        }
    }

    float angleToPlayer()
    {
        Vector3 posDif = player.transform.position - transform.position;
        float angleDif = (Mathf.Rad2Deg * Mathf.Atan2(posDif.y, posDif.x)) - transform.eulerAngles.z;
        if (angleDif < -90)
            angleDif += 360;
        return angleDif;
    }

    bool playerInView(float angleDif)
    {
        return (angleDif > viewAngle || angleDif < -viewAngle) /* && raycast to player*/;
    }
}
