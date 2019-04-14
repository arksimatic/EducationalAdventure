using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SettingPhrases : MonoBehaviour
{
    public Text Word = null;
    public static string[] j = new string[150];
    public static string[] m = new string[150];
    public static string word = null;
    public static string txt = null;


    //TODO: Add new difficulty levels
    public static readonly string[] difficulties = new string[] { "klasa1", "klasa1", "klasa1" };



    public static void Draw(Text Word)
    {
        string text = Resources.Load<TextAsset>(difficulties[(int)Difficulty.diff]).text;
        StringReader reader = new StringReader(text);
        reader.Close();

        char[] charArr = text.ToCharArray();
        word = null;
        int h = 1;
        foreach (char ch in charArr)
        {
            if(ch=='_')
            {
                word = System.String.Format(word + j[h]);
                Debug.Log(h);
                h++;}
            else
            { word += System.String.Join("", ch); }
        }

        Word.text = word;
    }

    public void DrawEveryone()
    {
        Draw(Word);
    }


    void Start()
    {


        string anwsers = Resources.Load<TextAsset>(difficulties[(int)Difficulty.diff] + "odp").text;



        StringReader readerodp = new StringReader(anwsers);
        m[0] = "a";
        for (int i = 1; m[i-1] != null; i++)
        {
            m[i] = readerodp.ReadLine();
            Debug.Log(i + "m");
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