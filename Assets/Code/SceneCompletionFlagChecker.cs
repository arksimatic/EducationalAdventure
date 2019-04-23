using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//Checks minigame completion flags
//TODO: Cutscene support?
public class SceneCompletionFlagChecker : MonoBehaviour
{


    public List<string> flags = new List<string>();
    public string NextSceneName = "";


    // Start is called before the first frame update
    void Start()
    {
        if (flags.All(x => StateSaver.instance.GetFlag(x) == true) == true)
        {
            SceneChanger.instance.ChangeSceneOnWin(NextSceneName);
        }

    }



}
