using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingPointer : MonoBehaviour
{
    public Rigidbody2D rb;

    void Update()
    {
        //Note: this should also work on mobile devices
        //TODO: check functionality on mobile devices
        if (Input.GetMouseButtonDown(0))
        {
            Move();
            
        }
       
     
    }

    void Move()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
    
         QuestStarter quest = hitInfo.GetComponent<QuestStarter>();
         if (quest != null)
         {
             quest.Starter(transform.position);

         }
         


    }
}
