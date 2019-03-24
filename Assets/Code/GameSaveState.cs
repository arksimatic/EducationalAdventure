using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A data model for game saves
/// </summary>
[Serializable]
public struct GameSaveState
{

    //Save flags that will toggle when player completes a task
    //for example, Flags["TASK_COUNTRY_1"] = true will mean that player completed first
    //task in a country map.
    public Dictionary<string, bool> Flags;

    //Last scene that was loaded before exiting the game, so we can put player back in the place where he/she left off
    public string lastScene;
}
