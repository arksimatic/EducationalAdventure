using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    [SerializeField]
    string scene_name;

    public void OnClick()
    {
        SceneChanger.instance.ChangeScene(scene_name);

    }
}
