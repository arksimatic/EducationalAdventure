using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

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

    #region Others functions
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
    }

    private void ReadFromFile()
    {
        char[] delimiter = { '?' };

        int i = 0;

        //Repeat as long as we had fields to show question
        foreach (GameObject obj in objects)
        {
            s = reader.ReadLine();

            //Check count of question
            if (s != null)
            {
                fields = s.Split(delimiter);
                obj.GetComponentInChildren<Text>().text = fields[0] + '?';
                fileAnswer[i] = int.Parse(fields[1]);
                Debug.Log("Odpowiedź pliku " + i + "  : " + fileAnswer[i]);
                i++;
            }
            //if isn't enough, question = base text
            else
            {
                obj.GetComponentInChildren<Text>().text = "Brak pytania, odpowiedź Prawda";
                fileAnswer[i] = 1;
                i++;
            }
        }
        reader.Close();
    }
    #endregion

    #region Button functions 
    //Check how much is Bad answers and set it on Text 
    public void CheckAnswer(Text NrOfBadAnswer)
    {
        int badanswer = 0;

        //How much is bad answers
        for (int i = 0; i < fileAnswer.Length; i++)
        {
            if (fileAnswer[i] != playerAnswer[i])
            {
                badanswer++;
            }
        }

        //If all answers are correct go to sea scene
        if (badanswer == 0)
        {
            SceneManager.LoadScene("ocean_scene", LoadSceneMode.Single);
        }
        else
        {
            if (badanswer == 1)
            {
                NrOfBadAnswer.text = badanswer.ToString() + " Błędna odpowiedź";
            }
            else
            {
                NrOfBadAnswer.text = badanswer.ToString() + " Błędnych odpowiedzi";
            }
        }

    }
    //Take string with two numbers, first - nr question     secound - true(1) or false(0)
    public void TakeAnswer(string a)
    {
        int nr, answer;

        //String to int 
        int.TryParse("" + a[0], out nr);
        int.TryParse("" + a[1], out answer);

        //Array wtih player answers 
        playerAnswer[nr - 1] = answer;

        Debug.Log("Pytanie nr " + nr + " jest  : " + answer);
        Debug.Log(playerAnswer[nr - 1]);
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