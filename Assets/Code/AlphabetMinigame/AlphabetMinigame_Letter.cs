using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class AlphabetMinigame_Letter : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    [SerializeField]
    Text letterDisplay;
    [SerializeField]
    Image backgroundImage;


    public char representedLetter = '!';

    AlphabetMinigame_LetterSocket embbededIn;

    Transform container;

    Action changeStateHandler;

    [SerializeField]
    ParticleSystem[] particles = new ParticleSystem[2];


    //Initialize this letter with character
    public void Init(char character, Transform _container, Action _changedStateHandler)
    {
        letterDisplay.text = character.ToString();
        representedLetter = character;
        container = _container;


        //Randomize some look
        backgroundImage.color = UnityEngine.Random.ColorHSV(0, 1, 0,0.5f,1,1);
        changeStateHandler = _changedStateHandler;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(container.parent);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Check if this object is overlapping with another object that has AlphabetMinigame_LetterSocket script attached
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        RaycastResult result = results.FirstOrDefault(x => x.gameObject.GetComponent<AlphabetMinigame_LetterSocket>());


        if (result.gameObject != null)
        {
            if (embbededIn != null)
                embbededIn.Letter = null;
            embbededIn = null;

            //set position to socket position, to achieve "snap" effect
            transform.position = result.gameObject.transform.position;
            embbededIn = result.gameObject.GetComponent<AlphabetMinigame_LetterSocket>();
            //If there is already a letter in place, throw it to unused letter container
            if(embbededIn.Letter != null)
            {
                embbededIn.Letter.embbededIn = null;
                embbededIn.Letter.transform.SetParent(container);
            }
            embbededIn.Letter = this;
            changeStateHandler();
        }
        else
        {
            //return object to unused letter container
            transform.SetParent(container);
            if (embbededIn != null)
                embbededIn.Letter = null;
            embbededIn = null;
            changeStateHandler();
        }

    }

}
