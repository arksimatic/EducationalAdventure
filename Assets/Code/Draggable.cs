﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Linq;

[RequireComponent(typeof(EventTrigger))]
public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    void Start()
    {



    }

    public void OnBeginDrag(PointerEventData data)
    {

    }

    public void OnDrag(PointerEventData data)
    {
    #if UNITY_EDITOR
        //Since we operate on UI, we can simply set position to screen touch coords
        transform.position = Input.mousePosition;
    #elif UNITY_ANDROID
        //Sanity check
        if(Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            //Since we operate on UI, we can simply set position to screen touch coords
            transform.position = t.position;
        }
    #endif
    }

    public void OnEndDrag(PointerEventData data)
    {



    }




}
