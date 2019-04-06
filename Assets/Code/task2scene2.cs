using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class task2scene2 : MonoBehaviour
{
    #region Variebles
    [SerializeField]
    GameObject question1;

    [SerializeField]
    GameObject question2;

    [SerializeField]
    GameObject question3;

    [SerializeField]
    GameObject question4;

    [SerializeField]
    GameObject question5;

    private int[] playerAnswer = new int[5] { 2, 2, 2, 2, 2 };
    private int[] fileAnswer = new int[5];

    private string Question; 

    StringReader reader;

    private readonly string QAEasy = "QAEassy";
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

        //Test
        question1.GetComponentInChildren<Text>().text = "Mam to";
        question1.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "MAM";
        //question1.GetComponentInChildren<Button.f>().

    }

    private void ReadFromFile()
    {

    }

    #region Button functions 
    public void CheckAnswer()
    {

    }
    //Take string with two numbers, first - nr question     secound - true(1) or false(0)
    public void TakeAnswer(string a)
    {
        int nr, answer;

        int.TryParse(""+a[0], out nr);
        int.TryParse(""+a[1], out answer);
        //nr--;

        playerAnswer[nr] = answer;

        Debug.Log("Pytanie nr " + nr + " jest  : " + answer);
        Debug.Log(playerAnswer[nr]);
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
