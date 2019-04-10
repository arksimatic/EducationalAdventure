using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;


public class GoToPointScript : MonoBehaviour
{


    public Transform target;
    public float speed = 20;
    private PathFinding pathfinding;
    private List<Node> Path = new List<Node>();
    private int targetIndex = 0;
    private Vector3 targetCheck;
    private Vector2[] directions;
    private bool prociding = false;
    public bool tourEnd=false;


    void Start()
    {

        pathfinding = GameObject.Find("Astar").GetComponent<PathFinding>();
        targetCheck = target.transform.position;
        OnPath(pathfinding.FindPath(transform.position, target.position));
    }

    void Update()
    {
        TargetCheck();
        if (prociding == false)
        {

            OnPath(pathfinding.FindPath(transform.position, target.position));

        }

    }

    void TargetCheck()
    {
        if (targetCheck.x == target.position.x && targetCheck.y == target.position.y)
        {
            prociding = true;

        }
        else
            prociding = false;

    }

    public void OnPath(List<Node> newPath)
    {


        
        targetCheck = target.transform.position;
        Path = newPath;
        targetIndex = 0;
        directions = Directions(Path);
        StopCoroutine("FollowPath");
        StartCoroutine("FollowPath");



    }

    public Vector2[] Directions(List<Node> path)// tworzy pkt po ktorych porusza sie postac 
    {
        List<Vector2> waypoints = new List<Vector2>();

        for (int i = 0; i < path.Count; i++)
        {

            waypoints.Add(path[i].worldPosition);

        }

        return waypoints.ToArray();

    }

    IEnumerator FollowPath() /// chodzonko przeskakuje po waypointach
    {

        Vector2 currentWaypoint = directions[0];
        while (true)
        {
            if (transform.position.x == (currentWaypoint.x) && transform.position.y == (currentWaypoint.y))
            {
                targetIndex++;

                if (targetIndex >= directions.Length)
                {
                    tourEnd = true;
                    yield break;
                }

                currentWaypoint = directions[targetIndex];
                targetIndex++;
            }

            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }
    }





    /* 
    void GetCloser(Vector2 Pos, Vector2 Target)// niby fajne ale nie potrzebne jednak wiec skomentowalem
    {

        RaycastHit2D hit = Physics2D.Raycast(Pos, Target,2.0f);
        if (hit.collider == null)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target, speed * Time.deltaTime);
        }


    }
}*/
}