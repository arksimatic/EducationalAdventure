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
    public static StateSaver instance = null;
    GameSaveState stateData;
    #endregion

    #region MonoBehaviour methods
    private void Awake()
    {
        //Prepare singleton instance
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }


    }

    void Start()
    {
        TryLoadState();
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
    #endregion
}
