using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;



//Class describing difficulty setting
[CreateAssetMenu(fileName = "difficulty", menuName = "Game Data/DifficultySetting")]
public class Difficulty : ScriptableObject
{
    public string name = "";
    public List<SceneCollection> minigames = new List<SceneCollection>();
}


