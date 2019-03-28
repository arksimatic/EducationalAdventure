using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;


[RequireComponent(typeof(EventTrigger))]
public class Dragable : MonoBehaviour
{
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        trigger.triggers.Add(entry);


    }

    public void OnDrag(PointerEventData data)
    {
        #if UNITY_EDITOR
        transform.position = Input.mousePosition;
        #endif

    }



}
