using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStarter : MonoBehaviour
{
    public GoToPointScript hero;
    public PathfindingPointer point;
    private Vector3 OldPos;
   
    public void Starter(Vector3 pointer)
    {
        
        hero.tourEnd = false;
        StopCoroutine("Waiting");
        StartCoroutine("Waiting");
        OldPos = pointer;


    }

    

    IEnumerator Waiting()
    {
        while(true){

            if (OldPos.x == point.transform.position.x && OldPos.y == point.transform.position.y)
            {



                if (hero.tourEnd == true)
                {

                   
                    SceneManager.LoadScene("Quest", LoadSceneMode.Single);
                    yield break;
                }
            }

            yield return null;
        }
    }

}