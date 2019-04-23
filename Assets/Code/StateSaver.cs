using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Saves/loads game state
/// 
/// ---- SINGLETON ----
/// ---- DontDestroyOnLoad Object ----
/// 
/// </summary>
public class StateSaver : MonoBehaviour
{

    #region Variables & References
    public static StateSaver instance;
    GameSaveState stateData;
    #endregion

    #region MonoBehaviour methods
    private void Awake()
    {
      
        //Prepare singleton instance
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            stateData = new GameSaveState();
            DontDestroyOnLoad(instance.gameObject);
        }
    }

    void Start()
    {
        TryLoadState();
    }

    void Update()
    {

    }

    //Before app will exit, save state
    void OnApplicationQuit()
    {
        TrySaveCurrentState();
    }

    #endregion

    #region Public Methods
    public void TryLoadState()
    {
        string JSONdata = "";
        try
        {
            //HACK: despite having try/catch block, the statements in if crash whole editor on linux
            if (File.Exists(Application.persistentDataPath + "/save.dat"))
            {
                JSONdata = File.ReadAllText(Application.persistentDataPath + "/save.dat");
                stateData = JsonUtility.FromJson<GameSaveState>(JSONdata);
            }
            else
            {
                throw new IOException("Save file not found! Loading default scene...");
            }
        }
        catch (IOException ioe)
        {
            //if IO error occured, load default scene and create new stateData
            stateData = new GameSaveState();
            Debug.Log(ioe.Message);
            return;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return;
        }

        SceneManager.LoadScene(stateData.lastScene);
    }

    public void TrySaveCurrentState()
    {
        stateData.lastScene = SceneManager.GetActiveScene().name;
        string JSONdata = JsonUtility.ToJson(stateData);
        try
        {
            File.WriteAllText(Application.persistentDataPath + "/save.dat", JSONdata);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void SetFlag(string flagname, bool value)
    {
        if (stateData.Flags.ContainsKey(flagname))
        {
            stateData.Flags[flagname] = value;
        }
        else
        {
            stateData.Flags.Add(flagname, value);
        }

    }


    /// <summary>
    /// Gets the flag value.
    /// </summary>
    /// <returns><c>true</c>, if flag was gotten, <c><paramref name="defaultFailureReturn"/></c> otherwise.</returns>
    /// <param name="flagname">Name of the flag of which value should be returned</param>
    /// <param name="defaultFailureReturn">Sets whichever value should be returned on retrive failure</param>
    public bool GetFlag(string flagname, bool defaultFailureReturn = false)
    {
        if(stateData != null)
        {
            if (stateData.Flags.ContainsKey(flagname))
            {
                return stateData.Flags[flagname];
            }
            else
            {
                Debug.LogWarning("Requested flag: " + flagname + " does not exist.");
                return defaultFailureReturn;
            }
        }
        else
        {

            Debug.LogError("StateSaver: StateData is null!");
            return defaultFailureReturn;
        }
    }
    #endregion
}
