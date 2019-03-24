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
            JSONdata = File.ReadAllText("/home/mado/save.dat");
            stateData = JsonUtility.FromJson<GameSaveState>(JSONdata);
            SceneManager.LoadScene(stateData.lastScene);
        }
        catch (IOException ioe)
        {
            //if IO error occured, load default scene and create new stateData
            SceneManager.LoadScene(0);
            stateData = new GameSaveState();
            Debug.Log(ioe.Message);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
        finally
        {

        }
    }

    public void TrySaveCurrentState()
    {
        stateData.lastScene = SceneManager.GetActiveScene().name;
        string JSONdata = JsonUtility.ToJson(stateData);
        StreamWriter fs = null;
        try
        {
            fs = File.AppendText("/home/mado/save.dat");
            fs.WriteLine(JSONdata);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            if (fs != null)
                fs.Close();
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
