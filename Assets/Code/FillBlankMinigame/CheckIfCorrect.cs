using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CheckIfCorrect : MonoBehaviour
{
    public InputField Input;
    public Text displayThis = null;
    public static int score = 0;

    public void OnClick()
    {
            if (Input.text == SettingPhrases.m[score+1])
            {
                Debug.Log("Correct answer! "); //give point to kiddo
                SettingPhrases.j[score+1] = SettingPhrases.m[score+1];
                GetComponent<SettingPhrases>().DrawEveryone();
                score += 1;
            if (score > 14)
                {
                    Debug.Log("End of this mini game");
                }
            }
            else
            {
                Debug.Log("Sorry, not this wae..."); //no points for kiddo :<
                //displayThis.text = Input.text;
        }
    }

    // void Update () { displayThis.text = Input.text;}

}         