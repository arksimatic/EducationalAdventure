using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingForSpot : MonoBehaviour
{
    BasicBehaviour basic;
    public Transform[] spots;
    private Vector3 target, distance,holding;
    private void Awake()
    {
        basic = GetComponent<BasicBehaviour>();
    }
    public void OnTask(Vector3 targetManager)
    {
        

        basic.OnTask(GetClosestSpot(targetManager));


    }
  
    public Vector3 GetClosestSpot(Vector3 target)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        
        foreach (Transform t in spots)
        {
            float dist = Vector3.Distance(t.position, target);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin.position;
    }
}

