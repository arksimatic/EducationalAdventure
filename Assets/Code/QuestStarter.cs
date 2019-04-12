using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestStarter : MonoBehaviour
{
    public GoToPointScript hero;
    public PathfindingPointer point;
    [SerializeField]
    private string scene_name;
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
        while (true)
        {
            if (Math.Abs(OldPos.x - point.transform.position.x) < 0.001f && Math.Abs(OldPos.y - point.transform.position.y) < 0.001f)
            {
                if (hero.tourEnd == true)
                {
                    SceneChanger.instance.ChangeScene(scene_name);
                    yield break;
                }
            }
            yield return null;
        }
    }

}