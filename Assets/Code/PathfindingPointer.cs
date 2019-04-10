using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingPointer : MonoBehaviour
{
    public Rigidbody2D rb;

    void Update()
    {
        if (Input.GetButtonDown("Point"))
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
        
    
         SceneStarter quest = hitInfo.GetComponent<SceneStarter>();
         if (quest != null)
         {
             quest.Starter(transform.position);

         }
         


    }
}
