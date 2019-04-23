using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JigsawPuzzleManager : MonoBehaviour
{
    //Image that will be devided into puzzles
    [SerializeField]
    Texture2D inputImage;


    //Describes how many puzzles there will be in x and y axises
    [SerializeField]
    Vector2Int jigsawPuzzleCount;

    [SerializeField]
    Transform unusedPuzzlePieceContainer;

    [SerializeField]
    Transform puzzleSocketContainer;

    [SerializeField]
    GameObject socketPrefab;

    [SerializeField]
    GameObject piecePrefab;

    // Start is called before the first frame update
    void Start()
    {
        //calculate one piece's texture size
        Vector2Int texSize = new Vector2Int(Mathf.FloorToInt((float)inputImage.width / (float)jigsawPuzzleCount.x),
        Mathf.FloorToInt((float)inputImage.height / (float)jigsawPuzzleCount.y));

        //calculate piece's UI element size
        Rect containerBounds = puzzleSocketContainer.GetComponent<RectTransform>().rect;
        GridLayoutGroup containerLayout = puzzleSocketContainer.GetComponent<GridLayoutGroup>();

        Vector2 objSize = new Vector2((containerBounds.width - containerLayout.padding.left - containerLayout.padding.right) / jigsawPuzzleCount.x,
           (containerBounds.height - containerLayout.padding.bottom - containerLayout.padding.top) / jigsawPuzzleCount.y);
        containerLayout.cellSize = objSize;

        //take the input image and devide it into puzzle pieces
        for (int i = 0; i < jigsawPuzzleCount.x; i++)
        {
            for (int j = 0; j < jigsawPuzzleCount.y; j++)
            {
                //Create puzzle piece
                GameObject puzzlePiece = Instantiate(piecePrefab);
                puzzlePiece.GetComponent<JigsawPuzzlePiece>().puzzlePieceID = (i + 1) * (j + 1);
                puzzlePiece.transform.SetParent(unusedPuzzlePieceContainer);
                //get portion of a main picture to assign it as a texture for our new piece
                Texture2D pieceTex = new Texture2D(texSize.x, texSize.y);
                pieceTex.SetPixels(inputImage.GetPixels(i * texSize.x, j * texSize.y,
                texSize.x, texSize.y));
                pieceTex.Apply();

                //Set texture for piece
                puzzlePiece.GetComponent<Image>().sprite = Sprite.Create(pieceTex, new Rect(0.0f, 0.0f, pieceTex.width, pieceTex.height), new Vector2(0.5f, 0.5f));


                //Create puzzle socket
                GameObject puzzleSocket = Instantiate(socketPrefab);
                puzzleSocket.transform.SetParent(puzzleSocketContainer);



            }
        }


    }

}
