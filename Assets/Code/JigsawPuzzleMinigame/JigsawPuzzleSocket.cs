using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JigsawPuzzleSocket : MonoBehaviour
{
    int puzzlePieceID;

    //Default value ! means error
    public int ExcpectedID = -1;
    public JigsawPuzzlePiece Piece
    {
        get { return piece; }
        set { piece = value; OnLetterChanged(); }
    }

    private JigsawPuzzlePiece piece;




    void Start()
    {
       
    }

    void OnLetterChanged()
    {
      
    }
}

