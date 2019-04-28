using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SettingPhrases : MonoBehaviour
{
    //public Text Word = null;

    public Text Word = null; 


    public static string[] j = new string[150];
    public static string[] m = new string[150];
    public static string word = null;
    public static string txt = null;
    public static int neededScore;

    //TODO: Add new difficulty levels
    public static readonly string[] difficulties = new string[] { "klasa1", "klasa1", "klasa1" };
    //public static string level = difficulties[(int)Difficulty.diff];  //addinational string for more clean code
    public static string level = "klasa1";  //just for testing, delete and turn on the line above

    public void Draw(Text Word)
    {
        string text = Resources.Load<TextAsset>(level).text;
        StringReader reader = new StringReader(text);
        reader.Close();

        char[] charArr = text.ToCharArray();
        word = null;
        int h = 1;
        neededScore = 0;
        foreach (char ch in charArr)
        {
            if(ch=='_')
            {
                neededScore++;
                word = System.String.Format(word + j[h]);
                //Debug.Log(h);
                h++;}
            else
            { word += System.String.Join("", ch); }
        }
        Debug.Log("Needed score" + neededScore);
        Word.text = word;
    }

    public void DrawEveryone()
    {
        Debug.Log("turning on drawing");
        Draw(Word);
    }


    void Start()
    {
        string anwsers = Resources.Load<TextAsset>(level + "odp").text;

        StringReader readerodp = new StringReader(anwsers);
        m[0] = "a";
        for (int i = 1; m[i-1] != null; i++)
        {
            m[i] = readerodp.ReadLine();
            //Debug.Log(i + "m" + m[i]);
        }
        readerodp.Close();

        for (int i = 1; m[i - 1] != null; i++)
        {
            j[i] = "_";
        }

        readerodp.Close();
        Draw(Word);

    }
}