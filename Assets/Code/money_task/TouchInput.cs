using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{

    void Update()
    {

    //copied from
    //https://forum.unity.com/threads/touching-a-2d-sprite.233483/?fbclid=IwAR219XIQAoBdnBLKYSfympAW4_-nMthlvTpJcx7eH5y5jhvUMDDKqhX4OmU

    #if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

            if (hit)
            {
                hit.transform.gameObject.SendMessage("OnTouchDown");
            }
        }

    #endif

        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);
                if (hit)
                {
                    hit.transform.gameObject.SendMessage("OnTouchDown");
                }
            }
        }

    }
}
