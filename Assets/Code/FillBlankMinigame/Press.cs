using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Press : MonoBehaviour
{
    public static int score = 0;
    public static string input;


    public void Check()
    {
        //Debug.Log(SettingPhrases.m[score + 1]);
        if (input.ToLower() == SettingPhrases.m[score + 1])
        {
            Debug.Log("Correct answer! "); //give point to kiddo
            SettingPhrases.j[score + 1] = SettingPhrases.m[score + 1];


            GameObject g = GameObject.Find("Mechanics");           // optymalize this
            SettingPhrases h = g.GetComponent<SettingPhrases>();   //
            h.DrawEveryone();                                      //

            score += 1;

            if (score == SettingPhrases.neededScore)
            {
                Debug.Log("End of this mini game");
                SceneChanger.instance.ChangeSceneOnWin("school_scene");
            }
        }
        else
        {
            Debug.Log("Sorry, not this wae..."); //no points for kiddo :<
                                                 //displayThis.text = Press.input;
        }
    }



    public void OnButtonClick()
    {
        input = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("got letter " + input);
        Check();
    }

}
