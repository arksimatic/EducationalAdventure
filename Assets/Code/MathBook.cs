using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class MathBook : MonoBehaviour
{
    
    //[SerializeField]
    //private Button StartMathButton;

    [SerializeField]
    private GameObject NumPad;

    [SerializeField]
    private Text calculations;

    [SerializeField]
    private Text answerText;

    private string answer;
    private string s;
    private string[] fields;

    StreamReader reader;

    //Scieszka do pliku z zadaniami                                   <--Dodać różne poziomy trudności 
    private string MathPath = "Assets/Resources/MathBook.txt";

    private void Awake()
    {
        
        reader = new StreamReader(MathPath);

        //Aktywacja książki
        calculations.gameObject.SetActive(true);
        NumPad.SetActive(true);
        answerText.gameObject.SetActive(true);
        SeeOnScreen();
        answerText.text = " ";
    }

    //Start event
    public void ClickOnBook()
    {
        //Aktywacja answerText, NumPad, calculationText
        calculations.gameObject.SetActive(true);
        NumPad.SetActive(true);
        //StartMathButton.gameObject.SetActive(false);
        answerText.gameObject.SetActive(true);

        Debug.Log("Is Clicked");
        SeeOnScreen();
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
            //Zakończenie questa                                <------ zmiana sceny spowrotem
            calculations.gameObject.SetActive(false);
            NumPad.SetActive(false);
            //StartMathButton.gameObject.SetActive(true);
            answerText.gameObject.SetActive(false);
            Debug.Log("Off Math");
            SceneManager.LoadScene("school_scene", LoadSceneMode.Single);
            reader.Close();
        }
    }

    //Pobieranie odpowiedzi do answerText
    public void TakeAnswer(string score)
    {
        answer += score;
        answerText.text = answer;
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