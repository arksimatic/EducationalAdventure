using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int value;
    private bool active = false;
    private GameObject controller;
    private Color defaultColor;
    private Color clickedColor = new Color(0.45f, 0.83f, 0.45f);


    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        defaultColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    void OnTouchDown()
    {
        if (!active)
        {
            controller.SendMessage("IncreaseSum", value);
            //Some visual feedback to when a coin is clicked...
            gameObject.GetComponent<SpriteRenderer>().color = clickedColor;            
        }
        else
        {
            controller.SendMessage("DecreaseSum", value);
            //...and unclicked.
            gameObject.GetComponent<SpriteRenderer>().color = defaultColor;           
        }
        active = !active;
    }

    
}
