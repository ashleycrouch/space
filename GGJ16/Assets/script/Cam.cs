using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cam : MonoBehaviour
{
    public bool tracking = false;
    public bool stationary = false;
    public float rotSpeed = 10;
    public float viewAngle = 45;
    public float minAngle = 0;
    public float maxAngle = 180;
    public LayerMask layerMask;

    Transform player;
    public bool rotDir = true;
    float targetAngle;
    float curAngle;

    void Start()
    {
        curAngle = transform.localRotation.eulerAngles.z;
        minAngle += curAngle;
        maxAngle += curAngle;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        curAngle = transform.eulerAngles.z;

        if (playerInView(angleTo(player.position)))
            player.GetComponent<Player>().isWatched = true;

        if(stationary)
        {
            return;
        }else if (tracking)
        {

            float angleDif = angleTo(player.position);

            if (playerInView(angleDif)) //Player is out of view angle
            {
                standardRot();
            }
            else if (angleDif > rotSpeed * Time.deltaTime)  //Trying to turn too fast
            {
                targetAngle = curAngle + rotSpeed * Time.deltaTime;
            }
            else if (angleDif - curAngle < -rotSpeed * Time.deltaTime) //Trying to turn too fast
            {
                targetAngle = curAngle - rotSpeed * Time.deltaTime;
            }
            else
            {
                targetAngle = angleDif + curAngle;
            }
        }
        else
            standardRot();


        if (targetAngle <= minAngle && targetAngle > 45)
        {
            rotDir = true;
                targetAngle = minAngle;
        }
		else if (targetAngle >= maxAngle || targetAngle <= 45)
        {
            rotDir = false;
			if (maxAngle == 360)
				targetAngle = maxAngle - .1f;
        }
        transform.eulerAngles = new Vector3(0, 0, targetAngle);
    }

    void standardRot()
    {
        if (rotDir) //Rotating positivly
        {
            targetAngle = curAngle + rotSpeed * Time.deltaTime;
        }
        else
        {
            targetAngle = curAngle - rotSpeed * Time.deltaTime;
        }
    }

    float angleTo(Vector3 target)
    {
        Vector3 posDif = target - transform.position;
        float angleDif = (Mathf.Rad2Deg * Mathf.Atan2(posDif.y, posDif.x)) - transform.eulerAngles.z;
        if (angleDif < -90)
            angleDif += 360;
        return angleDif;
    }

    bool playerInView(float angleDif)
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.position, layerMask, layerMask);
        return ((hit.collider != null && hit.collider.tag == "Player") && (angleDif > viewAngle || angleDif < -viewAngle));
    }

}
