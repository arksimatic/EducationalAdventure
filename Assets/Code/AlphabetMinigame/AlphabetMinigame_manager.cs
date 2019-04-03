using System.Linq;
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
        //Array containing letters that are not in polish alphabet
        char[] forbiddenCharacters = { 'V', 'Q', 'X' };
        //polish letter placement matrix, first element in nested array encodes after which letter
        //the following elements will have socket and letter created
        char[][] placementMatrix = { new char[] {'O', 'Ó'},
        new char[] {'Z', 'Ź', 'Ż'},
        new char[] {'A', 'Ą'},
        new char[] {'E', 'Ę'},
        new char[] {'L', 'Ł'}};


        //Todo: randomize filled up leters
        //1. randomize already filled in letters, count depends on difficulty setting

        Difficulty.diff = DIFFICULTY.HARD;

        //3. Instantiate all letters, those filled in, go to the respective sockets, those not go into unsued letter container
        //Go through A to Z plus add polish letters in between
        for (int i = 65; i <= 90; i++)
        {
            //check for forbidden letters
            if (forbiddenCharacters.Any(x => x == (char)i) == false)
            {
                CreateLetterAndSocket((char)i);

                //if difficulty is higher than EASY and check for placement matrix
                if (Difficulty.diff > DIFFICULTY.EASY && placementMatrix.Any(x => x[0] == (char)i))
                {
                    char[] placements = placementMatrix.First(x => x[0] == (char)i);

                    for (int j = 1; j < placements.Length; j++)
                    {
                        //create additional letters
                        CreateLetterAndSocket(placements[j]);
                    }
                }
            }
        }

        //4. At the end scramble alphabet letters in unused letter container

        for (int i = 0; i < 10; i++)
        {
            foreach (AlphabetMinigame_Letter l in letters)
            {
                l.transform.SetSiblingIndex(UnityEngine.Random.Range(0, letters.Count - 1));
            }
        }



    }



    void CreateLetterAndSocket(char c)
    {
        //Instantiate letter sockets
        GameObject letterSocket = Instantiate(socketPrefab, socketContainer.transform);
        letterSocket.GetComponent<AlphabetMinigame_LetterSocket>().ExpectedLetter = c;
        sockets.Add(letterSocket.GetComponent<AlphabetMinigame_LetterSocket>());

        //Instantiate letters
        GameObject letter = Instantiate(draggableLetterPrefab, unsuedLetterContainer.transform);
        letter.GetComponent<AlphabetMinigame_Letter>().Init(c, unsuedLetterContainer.transform, OnStateChanged);
        letters.Add(letter.GetComponent<AlphabetMinigame_Letter>());

    }


    //called when socket values have been updated
    void OnStateChanged()
    {
        if (sockets.All(x => x.Letter != null) == true)
        {
            if (sockets.All(x => x.Letter.representedLetter == x.ExpectedLetter) == true)
            {
                Win();
            }
        }
    }

    void Win()
    {
        //Todo: Win screen?
        Debug.Log("Win");
        SceneChanger.instance.ChangeSceneOnWin("school_scene");
        StateSaver.instance.SetFlag("minigame_alphabet", true);
    }





}
