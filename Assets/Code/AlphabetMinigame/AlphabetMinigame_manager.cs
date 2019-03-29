using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphabetMinigame_manager : MonoBehaviour
{

    List<AlphabetMinigame_LetterSocket> sockets = new List<AlphabetMinigame_LetterSocket>();
    List<AlphabetMinigame_Letter> letters = new List<AlphabetMinigame_Letter>();

    [SerializeField]
    GameObject socketContainer;

    [SerializeField]
    GameObject unsuedLetterContainer;

    [SerializeField]
    GameObject socketPrefab;

    [SerializeField]
    GameObject draggableLetterPrefab;


    void Start()
    {
        //On start, initialize game state




        //1. randomize already filled in letters, count depends on difficulty setting


        //3. Instantiate all letters, those filled in, go to the respective sockets, those not go into unsued letter container
        //Go through A to Z
        for(int i = 65; i <= 90; i++)
        {
            //Instantiate letter sockets
            GameObject letterSocket = Instantiate(socketPrefab, socketContainer.transform);
            letterSocket.GetComponent<AlphabetMinigame_LetterSocket>().ExpectedLetter = (char)i;


            //Instantiate letters
            GameObject letter = Instantiate(draggableLetterPrefab, unsuedLetterContainer.transform);
            letter.GetComponent<AlphabetMinigame_Letter>().Init((char)i, unsuedLetterContainer.transform);
            letters.Add(letter.GetComponent<AlphabetMinigame_Letter>());


        }

    }

    //called when socket values have been updated
    void OnStateChanged()
    { 
       
    }



}
