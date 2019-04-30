using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Morse_Output : MonoBehaviour
{
    //public Text Word = null;

    public Text Word = null;
    public Text InsertedWord = null;
    public Text Info = null;
    public static int neededScore=0;

    //TODO: Add new difficulty levels
    public static readonly string[] difficulties = new string[] { "Morse1", "Morse2", "Morse3" };
    //public static string level = difficulties[(int)Difficulty.diff];  //addinational string for more clean code
    public static string level = "Morse1";  //just for testing, delete and turn on the line above


    public static char[] nonMorseBoi = new char[60];
    public static string[] morseBoi = new string[60];
    public static string[] instruction = new string[60];

    public void Set()
    {
        string text = Resources.Load<TextAsset>(level).text;
        Debug.Log(text);
        StringReader reader = new StringReader(text);
        reader.Close();

        string odp = Resources.Load<TextAsset>(level+"odp").text;
        Debug.Log(odp);
        StringReader reader2 = new StringReader(odp);
        reader2.Close();

        char[] charArr = text.ToCharArray();
        nonMorseBoi = odp.ToCharArray();

        int i = 0;
        foreach (char ch in charArr)
        {
            if (ch == '.' || ch == '-')
            {
                morseBoi[i] = System.String.Format(morseBoi[i] + ch);     //Debug.Log("oprawiam " + ch);
            }
            else if(ch == ' ')
            {
                i++;                                             //Debug.Log("spacja");
            }
            else if(ch == '/')
            {
                morseBoi[i] = System.String.Format(morseBoi[i] + ' ');
            }
            else
            {
                nonMorseBoi[i] = ch;                            //Debug.Log("literka: " + ch);
            }
        }

        foreach (char ch in nonMorseBoi)
        {
            neededScore++;
            InsertedWord.text = InsertedWord.text + '_';
        }
                                                                 Debug.Log("neededScore: " + neededScore);
        i = 0;
        Word.text = text;


        Info.text = "A  • —\r\n" +
            "B — • • •\r\n" +
            "C — • — •\r\n" +
            "D — • •\r\n" +
            "E •\r\n" +
            "F • • — •\r\n" +
            "G — — •\r\n" +
            "H • • • •\r\n" +
            "I • •\r\n" +
            "J • — — —\r\n" +
            "K — • —\r\n" +
            "L • — • •\r\n" +
            "M — —\r\n" +
            "N — •\r\n" +
            "O — — —\r\n" +
            "P • — — •\r\n" +
            "Q — — • —\r\n" +
            "R • — •\r\n" +
            "S • • •\r\n" +
            "T —\r\n" +
            "U • • —\r\n" +
            "V • • • —\r\n" +
            "W • — —\r\n" +
            "X — • • —\r\n" +
            "Y — • — —\r\n" +
            "Z — — • •";

    }

    public void DrawInstruction()
    {
        string text = Resources.Load<TextAsset>("Morse").text;
        Debug.Log(text);
        StringReader reader = new StringReader(text);
        reader.Close();

    }

    void Start()
    {
        Set();
    }
}