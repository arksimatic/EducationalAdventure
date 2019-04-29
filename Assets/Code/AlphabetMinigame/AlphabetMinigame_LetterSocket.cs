using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphabetMinigame_LetterSocket : MonoBehaviour
{


    //Default value ! means error
    public char ExpectedLetter = '!';
    public AlphabetMinigame_Letter Letter
    { 
        get { return letter; }
        set { letter = value; OnLetterChanged(); }
    }

    private AlphabetMinigame_Letter letter;

    [SerializeField]
    ParticleSystem[] particles = new ParticleSystem[2];

    List<Image> images = new List<Image>();

    void Start()
    {
        images = transform.Find("line").GetComponentsInChildren<Image>().ToList();
    }

    void OnLetterChanged()
    {
        //check if letter is matching, if not colorize socket on red
        //If empty set to white


        if(Letter == null)
        {
            images.ForEach(x => x.color = Color.white);
        }
        else if(Letter.representedLetter == ExpectedLetter)
        {
            images.ForEach(x => x.color = Color.green);
            particles[0].Play();
        }
        else if (Letter.representedLetter != ExpectedLetter)
        {
            images.ForEach(x => x.color = Color.red);
            particles[1].Play();

        }
    }
}
