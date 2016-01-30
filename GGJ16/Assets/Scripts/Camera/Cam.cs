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
    bool rotDir = true;
    public float targetAngle;
    public float curAngle;
    
	void Start ()
    {
        curAngle =  transform.localRotation.eulerAngles.z;
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update ()
    {
        if (tracking)
        {
            Vector3 posDif = player.transform.position - transform.position;
            float angleDif = (Mathf.Rad2Deg * Mathf.Atan2(posDif.y, posDif.x)) - transform.eulerAngles.z;
            Debug.Log(angleDif);

            if (angleDif > viewAngle || angleDif + 360 < viewAngle)    //Player is out of view angle
            {
                standardRot();
            }   //Player is not visable to camera
            else if (angleDif + transform.eulerAngles.z > maxAngle)
            {
                rotDir = false;
                targetAngle = maxAngle;
            }
            else if (angleDif + transform.eulerAngles.z < minAngle)
            {
                rotDir = true;
                targetAngle = minAngle;
            }else if(angleDif > rotSpeed * Time.deltaTime)
            {
                targetAngle = curAngle + rotSpeed * Time.deltaTime;
            }
            else if (angleDif - curAngle < -rotSpeed * Time.deltaTime)
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
        }
        else
            standardRot();
        

        transform.eulerAngles = new Vector3(0, 0, targetAngle);
        curAngle = targetAngle;
    }

    void standardRot()
    {
        if (rotDir) //Rotating positivly
        {
            targetAngle = curAngle + rotSpeed * Time.deltaTime;
            if (targetAngle >= maxAngle)
            {
                targetAngle = maxAngle;
                rotDir = !rotDir;
            }
        }else
        {
            targetAngle = curAngle - rotSpeed * Time.deltaTime;
            if (targetAngle <= minAngle)
            {
                targetAngle = minAngle;
                rotDir = !rotDir;
            }
        }
    }
}
