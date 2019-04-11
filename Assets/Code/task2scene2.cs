using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class task2scene2 : MonoBehaviour
{
    #region Variebles

    [SerializeField]
    List<GameObject> objects;

    private int[] playerAnswer = new int[5] { 2, 2, 2, 2, 2 };
    private int[] fileAnswer = new int[5];

    private string[] fields;
    private string s;

    StringReader reader;

    private readonly string QAEasy = "QAEasy";
    private readonly string QAMedium = "QAMedium";
    private readonly string QAHard = "QAHard";
    private string QAPath = null;
    #endregion
    private void Awake()
    {
        //Check level of difficulty
        if (Difficulty.diff == DIFFICULTY.EASY)
        {
            QAPath = Resources.Load<TextAsset>(QAEasy).text;
        }
        else if (Difficulty.diff == DIFFICULTY.MEDIUM)
        {
            QAPath = Resources.Load<TextAsset>(QAMedium).text;
        }
        else
        { 
            QAPath = Resources.Load<TextAsset>(QAHard).text;
        }

        reader = new StringReader(QAPath);

        ReadFromFile();
        //Test
        //question1.GetComponentInChildren<Text>().text = "Mam to";
        //question1.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "MAM";
        //question1.GetComponentInChildren<Button.f>().

    }

    private void ReadFromFile()
    {
        char[] delimiter = { '?' };

        int i = 0;

        foreach (GameObject obj in objects)
        {
            s = reader.ReadLine();
            //fields = s.Split(delimiter);
            Debug.Log(s);    //<---                 tu zwraca null
            obj.GetComponentInChildren<Text>().text = s;
            //fileAnswer[i] = int.Parse(fields[1]);
            i++;
        }
        reader.Close();
    }

    #region Button functions 
    public void CheckAnswer()
    {

    }
    //Take string with two numbers, first - nr question     secound - true(1) or false(0)
    public void TakeAnswer(string a)
    {
        int nr, answer;

        //String to int 
        int.TryParse(""+a[0], out nr);
        int.TryParse(""+a[1], out answer);

        //Array wtih player answers 
        playerAnswer[nr-1] = answer;

        Debug.Log("Pytanie nr " + nr + " jest  : " + answer);
        Debug.Log(playerAnswer[nr-1]);
    }
    #region TrueButtons Colors
    public void TrueButtonChangeFalse(Button FalseButton)
    {
        FalseButton.image.color = Color.white;
    }

    public void TrueButtonChangeSelf(Button Self)
    {
        Self.image.color = Color.green;
    }
    #endregion

    #region FalseButton Colors
    public void FalseButtonChangeTrue(Button TrueButton)
    {
        TrueButton.image.color = Color.white;
    }
    public void FalseButtonChangeSelf(Button Self)
    {
        Self.image.color = Color.red;
    }

    #endregion
    #endregion
}
