using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class BasicBehaviour : MonoBehaviour
{

    GameObject Hero;
    private Vector3 target;
    bool CloseEnough;

    public void OnTask(Vector3 targetManager)
    {
        CloseEnough = false;
        target = targetManager;
        StopCoroutine("WalkingBehindHero");
        StartCoroutine("WalkingBehindHero");

    }

    IEnumerator WalkingBehindHero()
    {
        while (true)
        {
            Vector2 movementDirection = new Vector2(transform.position.x - target.x, transform.position.y - target.y);
            movementDirection.Normalize();
            if (movementDirection.x < 0)
            {
                
               
                Quaternion angle = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, angle, Time.deltaTime * 20.0f);
            }
 
            else if (movementDirection.x >0 )
            {


                Quaternion angle = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, angle, Time.deltaTime * 20.0f);
            }
            transform.position = Vector3.MoveTowards(transform.position, target, .03f);
          //  }
            yield return null;
        }

    }
    public bool ForParrotAnimation()
    {
        if (Vector3.Distance(transform.position,target) < 1.9f)
        {
            CloseEnough = true;
        }

        return CloseEnough;
    }

 

}
