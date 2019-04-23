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

    Transform parentbuffer = null;

    Transform unusedContainer;

    Action changeStateHandler;

    public void SetUnusedContainer(Transform _unusedContainer)
    {
        unusedContainer = _unusedContainer;
    }

    public void SetChangeStateHandler(Action _handler)
    {
        changeStateHandler = _handler;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentbuffer = transform.parent;
        transform.SetParent(unusedContainer.parent);
    }

    //TODO: Simplify the code, because it is a total fucking mess
    public void OnEndDrag(PointerEventData eventData)
    {
        //Check if this object is overlapping with another object that has JigsawPuzzleSocket script attached
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        RaycastResult result = results.FirstOrDefault(x => x.gameObject.GetComponent<JigsawPuzzleSocket>());

        //If something was hit...
        if (result.gameObject != null)
        {

            //Nullify to ensure no hanging references
            if (embbededIn != null)
            {
                embbededIn.Piece = null;
            }

            embbededIn = result.gameObject.GetComponent<JigsawPuzzleSocket>();

            //If there is already a piece in place, exchange parents
            if (embbededIn.Piece != null)
            {
               
                transform.SetParent(embbededIn.transform);
                transform.position = embbededIn.transform.position;

                embbededIn.Piece.embbededIn = null;
                embbededIn.Piece.transform.SetParent(parentbuffer);
                embbededIn.Piece.transform.position = parentbuffer.transform.position;
                //If it happens that the lastest parent was a puzzle socket, exchange field information too.
                if (parentbuffer.GetComponent<JigsawPuzzleSocket>())
                {
                    parentbuffer.GetComponent<JigsawPuzzleSocket>().Piece = embbededIn.Piece;
                    embbededIn.Piece.embbededIn = parentbuffer.GetComponent<JigsawPuzzleSocket>();
                }

            }
            else
            {
                //set position to socket position, to achieve "snap" effect
                transform.position = result.gameObject.transform.position;
                //set parent
                transform.SetParent(result.gameObject.transform);
            }

            //Set fields
            embbededIn.Piece = this;
        }
        else
        {
            //return object to unused letter container
            transform.SetParent(unusedContainer);
            if (embbededIn != null)
                embbededIn.Piece = null;
            embbededIn = null;
        }

        changeStateHandler();
    }
}

