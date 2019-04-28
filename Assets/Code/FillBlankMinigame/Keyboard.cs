using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    string[] letter = { "A", "Ą", "B", "C", "Ć", "D", "E", "Ę", "F", "G", "H", "I", "J", "K", "L", "Ł", "M", "N", "Ń", "O", "Ó", "P", "R", "S", "Ś", "T", "U", "W", "X", "Y", "Z", "Ż", "Ź", "CZ", "RZ", "SZ", "DZ", "DŻ", "DŹ"};
    GameObject[] button = new GameObject[50];
    Text[] text = new Text[50];
    public GameObject canvas;
    public GameObject prefab;
    public float x = 0f, y = 0f, z = 0f;

    void Start()
    {
        int i = 0;
        foreach (string value in letter)
        {
            button[i] = Instantiate(prefab, transform.position, transform.rotation);
            button[i].transform.SetParent(canvas.transform);
            button[i].name = value;
            text[i] = button[i].GetComponentInChildren<Text>();
            text[i].text = value;

            i++;
        }
    }
}
