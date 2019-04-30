using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Morse_Press : MonoBehaviour
{
    public static int score = 0;
    public static string input;
    public Text InsertedWord;
    public static char[] tempChar = new char[30];

    public void Check()
    {
        if (input == Morse_Output.nonMorseBoi[score].ToString())
        {
            Debug.Log("Correct answer! "); //give point to kiddo
            //SettingPhrases.j[score + 1] = SettingPhrases.m[score + 1];

            tempChar = InsertedWord.text.ToCharArray(); 
            tempChar[score] = Morse_Output.nonMorseBoi[score]; 
            string tempString = new string(tempChar);
            InsertedWord.text = tempString;

            score += 1;

            if (score == Morse_Output.neededScore)
            {
                Debug.Log("End of this mini game");
                //SceneChanger.instance.ChangeSceneOnWin("school_scene");                                                                //dodac scene do ktorej sie wraca po zakonczeniu minigry
            }
        }
        else
        {
            Debug.Log("Sorry, not this wae..."); //no points for kiddo :<
                                                 //displayThis.text = Press.input;
        }
    }

    public void Click()
    {
        input = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name; 
        Debug.Log("Pressed key: " + input);
        Check();
    }

}
