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
    private bool proceeding = false;
    public bool tourEnd = false;


    void Start()
    {

        pathfinding = GameObject.Find("Astar").GetComponent<PathFinding>();
        targetCheck = target.transform.position;
        OnPath(pathfinding.FindPath(transform.position, target.position));
    }

    void Update()
    {
        TargetCheck();
        if (proceeding == false)
        {

            OnPath(pathfinding.FindPath(transform.position, target.position));

        }

    }

    void TargetCheck()
    {
        if (Math.Abs(targetCheck.x - target.position.x) < 0.001f && Math.Abs(targetCheck.y - target.position.y) < 0.001f)
        {
            proceeding = true;
            return;
        }
        proceeding = false;
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
            if (Math.Abs(transform.position.x - (currentWaypoint.x)) < 0.001f && Math.Abs(transform.position.y - (currentWaypoint.y)) < 0.001f)
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
}