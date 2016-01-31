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
    public float maxDist = 100;
    public Material mat;
    public LayerMask layerMask;

    Transform player;
    public bool rotDir = true;
    float targetAngle;
    float curAngle;

    BoxCollider2D[] boxes;
    PolygonCollider2D[] colliders;

    MeshFilter viewCone;

    void Start()
    {
        curAngle = transform.localRotation.eulerAngles.z;
        minAngle += curAngle;
        maxAngle += curAngle;
        player = GameObject.FindGameObjectWithTag("Player").transform;


        boxes = FindObjectsOfType<BoxCollider2D>();
        colliders = FindObjectsOfType<PolygonCollider2D>();

        GameObject go = new GameObject("View Cone");
        //go.transform.parent = transform;
        viewCone = go.AddComponent<MeshFilter>();
        go.AddComponent<MeshRenderer>().material = mat;
    }

    void Update()
    {
        curAngle = transform.eulerAngles.z;

        if (playerInView(angleTo(player.position)))
            player.GetComponent<Player>().isWatched = true;

        if(stationary)
        {
            drawView();
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

        drawView();
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
        return ((hit.collider != null && hit.collider.tag == "Player") && (angleDif > viewAngle || angleDif < -viewAngle) && (hit.distance <= maxDist));
    }

    void drawView()
    {

        int numVerts = 3;
        int numTris;
        Vector3[] verts = new Vector3[numVerts];
        //Vector2[] uv = new Vector2[numVerts];
        int[] tris = new int[numVerts];


        List<Vector3> worldPoints = new List<Vector3>();

        foreach (PolygonCollider2D col in colliders)
        {
            Vector2[] points = col.points;

            for (int i = 0; i < points.Length; i++)    //Vector3 vert in col.points)
            {
                points[i] = col.transform.TransformPoint(points[i]); //transform to global space
                float angle = angleTo(points[i]);
                RaycastHit2D hit = Physics2D.Linecast(transform.position, points[i], layerMask);
                if ((angle - viewAngle <= 0 || angle + viewAngle <= 0) && (/*hit.point != null &&*/ hit.fraction >= .9))
                {
                    worldPoints.Add(points[i]);
                }
            }
        }

        // Add verts from boxCollider2Ds
        foreach(BoxCollider2D box in boxes)
        {
            Vector2 size = box.size;
            Vector3 center = new Vector3(box.offset.x, box.offset.y, 0f);
            Vector3 worldPos = box.transform.TransformPoint(box.offset);

            float top = worldPos.y + (size.y / 2f);
            float bot = worldPos.y - (size.y / 2f);
            float left = worldPos.x - (size.x / 2f);
            float right = worldPos.x + (size.x / 2f);

            Vector3 tl = new Vector3(left, top, worldPos.z);
            Vector3 tr = new Vector3(right, top, worldPos.z);
            Vector3 bl = new Vector3(left, bot, worldPos.z);
            Vector3 br = new Vector3(right, bot, worldPos.z);
            
            float angle = angleTo(tl);
            RaycastHit2D hit = Physics2D.Linecast(transform.position, tl, layerMask);
            if ((angle - viewAngle <= 0 || angle + viewAngle <= 0) && (/*hit.point != null &&*/ hit.fraction >= .9))
            {
                worldPoints.Add(tl);
            }

            angle = angleTo(tr);
            hit = Physics2D.Linecast(transform.position, tr, layerMask);
            if ((angle - viewAngle <= 0 || angle + viewAngle <= 0) && (/*hit.point != null &&*/ hit.fraction >= .9))
            {
                worldPoints.Add(tr);
            }

            angle = angleTo(bl);
            hit = Physics2D.Linecast(transform.position, bl, layerMask);
            if ((angle - viewAngle <= 0 || angle + viewAngle <= 0) && (/*hit.point != null &&*/ hit.fraction >= .9))
            {
                worldPoints.Add(bl);
            }

            angle = angleTo(br);
            hit = Physics2D.Linecast(transform.position, br, layerMask);
            if ((angle - viewAngle <= 0 || angle + viewAngle <= 0) && (/*hit.point != null &&*/ hit.fraction >= .9))
            {
                worldPoints.Add(br);
            }
        }


        worldPoints.Sort((x, y) => { return (int)(angleTo(y) - angleTo(x)); });

        //Raycast past points to find point behind it
        List<Vector3> temp = new List<Vector3>();
        for (int i = 0; i < worldPoints.Count; i++)
        {
            Vector3 p;
            float angle = angleTo(worldPoints[i]) + transform.eulerAngles.z;
            Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f).normalized;
            RaycastHit2D hit = Physics2D.Raycast(worldPoints[i] + dir * .001f, dir, layerMask /*maxDist - Vector3.Distance(transform.position, worldPoints[i])*/);
            //Debug.DrawRay(worldPoints[i], dir);
            p = hit.point;
            //Debug.Log(worldPoints[i] + " -> " + p);
            if (p == Vector3.zero)
            {
                p = new Vector3((transform.position.x + maxDist) * Mathf.Cos(angle * Mathf.Deg2Rad), (transform.position.y + maxDist) * Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            }
            temp.Add(p);
        }

        for (int i = 0; i < temp.Count; i++)
        {
            worldPoints.Insert(i * 2 + 1, temp[i]);
        }







        // Edge of view
        float a = (transform.eulerAngles.z + viewAngle);
        Vector2 direction = new Vector3(Mathf.Cos(a * Mathf.Deg2Rad), Mathf.Sin(a * Mathf.Deg2Rad), 0f).normalized;

        Vector3 point = Physics2D.Raycast(transform.position, direction, maxDist, layerMask).point;
        if (point == Vector3.zero)
        {
            point = new Vector3(transform.position.x + maxDist * Mathf.Cos(a * Mathf.Deg2Rad), transform.position.y + maxDist * Mathf.Sin(a * Mathf.Deg2Rad), 0);
        }
        worldPoints.Insert(0, point);

        a = (transform.eulerAngles.z - viewAngle);
        direction = new Vector3(Mathf.Cos(a * Mathf.Deg2Rad), Mathf.Sin(a * Mathf.Deg2Rad), 0f).normalized;

        point = Physics2D.Raycast(transform.position, direction, maxDist, layerMask).point;
        if (point == Vector3.zero)
        {
            point = new Vector3(transform.position.x + maxDist * Mathf.Cos(a * Mathf.Deg2Rad), transform.position.y + maxDist * Mathf.Sin(a * Mathf.Deg2Rad), 0);
        }
        worldPoints.Add(point);

        //Debug.Log(worldPoints.Count);


        numTris = worldPoints.Count - 1;
        tris = new int[numTris * 3];

        worldPoints.Insert(0, transform.position);
        verts = worldPoints.ToArray();
        /*foreach(Vector3 v in verts)
        {
            Debug.Log(v.ToString());
        }*/
        for (int i = 0; i < numTris; i++)
        {
            //Debug.DrawRay(transform.position, worldPoints[i] - transform.position);
            //Debug.Log(i + " " + angleTo(worldPoints[i]) + " " + worldPoints[i]);
            tris[i*3] = 0;
            tris[i*3 + 1] = (i + 1);
            tris[i*3 + 2] = (i + 2);
        }




        /*
        verts[0] = transform.position;
        verts[1] = new Vector3(maxDist * (Mathf.Cos(Mathf.Deg2Rad * (transform.eulerAngles.x + viewAngle)) + transform.position.y), maxDist * (Mathf.Sin(Mathf.Deg2Rad * (transform.eulerAngles.y + viewAngle)) + transform.position.y), 0);
        verts[2] = new Vector3(maxDist * (Mathf.Cos(Mathf.Deg2Rad * (transform.eulerAngles.x - viewAngle)) + transform.position.y), maxDist * (Mathf.Sin(Mathf.Deg2Rad * (transform.eulerAngles.y - viewAngle)) + transform.position.y), 0);

        Debug.Log("1: " + verts[1].ToString());
        Debug.Log("2: " + verts[2].ToString());

        Debug.DrawLine(transform.position, verts[1]);
        Debug.DrawLine(transform.position, verts[2]);

        uv[0] = new Vector2(verts[0].x, verts[0].y);
        uv[1] = new Vector2(verts[1].x, verts[1].y);
        uv[1] = new Vector2(verts[2].x, verts[2].y);

        tris[0] = 0;
        tris[1] = 1;
        tris[2] = 2;
        */

        Mesh mesh = new Mesh();
        mesh.vertices = verts;
       // mesh.uv = uv;
        mesh.triangles = tris;
        viewCone.mesh = mesh;
    }
}
