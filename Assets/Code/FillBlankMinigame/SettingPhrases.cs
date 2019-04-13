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
    public static void Draw(Text Word)
    {
        string path = "Assets/Resources/klasa1.txt";
        StreamReader reader = new StreamReader(path);
        txt = File.ReadAllText(path);
        reader.Close();

        /*Word.text = System.String.Format(
            "Po t{0}m ciem{1}ym b{2}ru \r\n" +
            "Kuku{3}e{4}ka kuka, \r\n" +
            "Z ran{5} a do wie{6}ora \r\n" +
            "Gniaz{7}ka sobi{8} sz{9}ka. \r\n" +
            "Ku{10}u!K{11}ku! \r\n" +
            "Gnia{12}dka so{13}ie szu{14}a. ",
            j[0], j[1], j[2], j[3], j[4], j[5], j[6], j[7], j[8], j[9], j[10], j[11], j[12], j[13], j[14]);*/

        char[] charArr = txt.ToCharArray();
        word = null;
        int h = 1;
        foreach (char ch in charArr)
        {
            //if(ch=='/')
                //{ word += System.String.Join("", "\r\n"); }
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
        //!File.Exists(textFile)



        string pathodp = "Assets/Resources/klasa1odp.txt";
        StreamReader readerodp = new StreamReader(pathodp);
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