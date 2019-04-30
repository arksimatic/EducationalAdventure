using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morse_ButtonClick : MonoBehaviour
{

    public void OnClick()
    {
        GameObject g = GameObject.Find("MechanicsM");           // optymalize this
        Morse_Press h = g.GetComponent<Morse_Press>();   //
        h.Click();
    }

}
