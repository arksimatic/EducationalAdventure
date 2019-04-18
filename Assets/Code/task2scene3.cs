using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


public class task2scene3 : MonoBehaviour
{
    [SerializeField]
    private Text questionText; //maks 258 char

    [SerializeField]
    private List<Button> buttons; 

    StringReader reader;
    private string s;

    private int fileAnswer;
    private int playerAnswer;

    //Path with quest 
    private readonly string questPathEasy = "QuestionEasy";
    private readonly string questPathMedium = "QuestionMedium";
    private readonly string questPathHard = "QuestionHard";
    private string questPath = null;

    private void Awake()
    {
        //check difficulty lvl
       if(Difficulty.diff == DIFFICULTY.EASY)
        {
            questPath = Resources.Load<TextAsset>(questPathEasy).text;
        }
       else if (Difficulty.diff == DIFFICULTY.MEDIUM)
        {
            questPath = Resources.Load<TextAsset>(questPathMedium).text;
        }
        else
        {
            questPath = Resources.Load<TextAsset>(questPathHard).text;
        }

        reader = new StringReader(questPath);

        ReadFromFile();
    }

    //Read from file to quest text , buttons and fileanswer
    private void ReadFromFile()
    {
        //Read to text quest
        s = reader.ReadLine();
        questionText.text = s;
        
        //Read to buttons
        foreach (Button but in buttons )
        {
            s = reader.ReadLine();
            but.GetComponentInChildren<Text>().text = s;
        }

        //Read to fileanswer
        s = reader.ReadLine();
        fileAnswer = int.Parse(s);
        reader.Close();
    }

    public void CheckAnswer()
    {
        if(playerAnswer == fileAnswer)
        {
            SceneChanger.instance.ChangeSceneOnWin("school_scene");
        }
        else
        {
            foreach(Button but in buttons)
            {
                if(but.image.color == Color.yellow)
                {
                    but.image.color = Color.red;
                }
            }
        }
        
    }

    public void GetAnswer(string a)
    {
        playerAnswer = int.Parse(a);
    }
    public void ChangeButton(Button change)
    {
       foreach(Button but in buttons)
        {
            but.image.color = Color.white;
        }

        change.image.color = Color.yellow;
    }
}
