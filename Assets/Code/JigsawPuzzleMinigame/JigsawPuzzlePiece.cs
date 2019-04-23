using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(EventTrigger))]
public class JigsawPuzzlePiece : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    //ID of a matching socket
    public int puzzlePieceID;


    JigsawPuzzleSocket embbededIn;

    Transform container;

    Action changeStateHandler;

    public void SetChangeStateHandler(Action _handler)
    {
        changeStateHandler = _handler;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(container.parent);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Check if this object is overlapping with another object that has JigsawPuzzleSocket script attached
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        RaycastResult result = results.FirstOrDefault(x => x.gameObject.GetComponent<JigsawPuzzleSocket>());


        if (result.gameObject != null)
        {
            if (embbededIn != null)
                embbededIn.Piece = null;
            embbededIn = null;

            //set position to socket position, to achieve "snap" effect
            transform.position = result.gameObject.transform.position;
            embbededIn = result.gameObject.GetComponent<JigsawPuzzleSocket>();
            //If there is already a piece in place, throw it to unused piece container
            if (embbededIn.Piece != null)
            {
                embbededIn.Piece.embbededIn = null;
                embbededIn.Piece.transform.SetParent(container);
            }
            embbededIn.Piece = this;
            changeStateHandler();
        }
        else
        {
            //return object to unused letter container
            transform.SetParent(container);
            if (embbededIn != null)
                embbededIn.Piece = null;
            embbededIn = null;
            changeStateHandler();
        }

    }
}

