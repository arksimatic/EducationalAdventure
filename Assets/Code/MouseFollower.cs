using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
        public float movementForce = 126f;

        private Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D does not exist");
            }

        }



        public float rotSpeed = 2f;




        void FixedUpdate()
        {
            UnityEngine.Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UnityEngine.Vector3 pos = transform.position;

            UnityEngine.Vector2 directionToMove = new UnityEngine.Vector2(0, 0);
            if (pos.y < touchPoint.y)
            {
                directionToMove.y += touchPoint.y - pos.y;
            }
            else if (pos.y > touchPoint.y)
            {
                directionToMove.y -= pos.y - touchPoint.y;
            }

            if (pos.x < touchPoint.x)
            {
                directionToMove.x += touchPoint.x - pos.x;
            }
            else if (pos.x > touchPoint.x)
            {
                directionToMove.x -= pos.x - touchPoint.x;
            }

            rb.velocity = new UnityEngine.Vector2(0, 0);
            rb.AddForce(directionToMove * movementForce, ForceMode2D.Force);
        }
    }
