using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class A_I : MonoBehaviour
{


    public GameObject target; //the enemy's target
    private bool move=false;
    private Rigidbody rb;
    public float rotSpeed = 20f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
       /*
        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.fixedDeltaTime);*/

        if (Mathf.Abs(target.transform.position.x - transform.position.x) + Mathf.Abs(target.transform.position.y - transform.position.y) > 3)
        {
            move = true;
        }

        else if (Mathf.Abs(target.transform.position.x - transform.position.x)+ Mathf.Abs(target.transform.position.y - transform.position.y) < 2)
        {
            move = false;
        }

        if (move == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, .03f);
        }
    }

    void Interaction()
    {

    }
}