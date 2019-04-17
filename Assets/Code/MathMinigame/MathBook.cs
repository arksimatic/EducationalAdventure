using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class MathBook : MonoBehaviour
{
    [SerializeField]
    private GameObject NumPad;

    [SerializeField]
    private Text calculations;

    [SerializeField]
    private Text answerText;

    private string answer;
    private string s;
    private string[] fields;

    StringReader reader;

    //Scieszka do pliku z zadaniami 
    private readonly string MathPathEasy = "MathBookEasy";
    private readonly string MathPathMedium = "MathBookMedium";
    private readonly string MathPathHard = "MathBookHard";
    private string MathPath = null;

    private void Awake()
    {
        //Sprawdzanie Poziomu trudności
        if(Difficulty.diff == DIFFICULTY.EASY )
        {
            MathPath = Resources.Load<TextAsset>(MathPathEasy).text;
        }
        else if(Difficulty.diff == DIFFICULTY.MEDIUM)
        {
            MathPath = Resources.Load<TextAsset>(MathPathMedium).text;
        }
        else
        {
            MathPath = Resources.Load<TextAsset>(MathPathHard).text;
        }

        reader = new StringReader(MathPath);

        //Aktywacja książki
        calculations.gameObject.SetActive(true);
        NumPad.SetActive(true);
        answerText.gameObject.SetActive(true);
        SeeOnScreen();
        answerText.text = " ";
    }

    //Wyświetlanie zadania + zakonczenie questa
    private void SeeOnScreen()
    {
        char[] delimiter = { '=' };
        s = reader.ReadLine();
       
        //Sprawdzanie czy to już ostatnie zadanie
        if (s != null)
        {
            //Wyświetlanie zadania
            fields = s.Split(delimiter);
            calculations.text = fields[0] + "=" ;
        }
        else
        {
            
            //Zakończenie questa                                
            calculations.gameObject.SetActive(false);
            NumPad.SetActive(false);
            //StartMathButton.gameObject.SetActive(true);
            answerText.gameObject.SetActive(false);
            Debug.Log("Off Math");
            SceneChanger.instance.ChangeSceneOnWin("school_scene");
            reader.Close();
        }
    }

    //Pobieranie odpowiedzi do answerText
    public void TakeAnswer(string score)
    {
            answer += score;
            answerText.text = answer;

        if (answer.Length >= 6)
        {
            answer = answer.Remove(answer.Length - 1, 1);
            answerText.text = answer;
        }
    }

    //Usuwanie ostatniego znaku z odpowiedzi
    public void DeleteLastNumber()
    {
        int count = answer.Length;
        
        if (count > 0 )
        {
            answer = answer.Remove(count - 1, 1);
            answerText.text = answer;
        }
     }

    //Sprawdzanie odpowiedzi
    public void CheckAnswer()
    {
        int answerInt = 0;
        int answerFromFile = 0;

        //Ze stringa do inta
        int.TryParse(fields[1], out answerFromFile);
        int.TryParse(answerText.text, out answerInt);

        //Poprawna odpowiedz
        if (answerFromFile == answerInt)
        {
            SeeOnScreen();
            answer = " ";
            answerText.text = answer;
            Debug.Log("Checked: Good");
        }
        //Błędna odpowiedź
        else
        {
            answerText.text = answer;
            Debug.Log("Checked: Bad");
            answer = " ";
            answerText.text = answer;
        }
    }
}