using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MathQuestStart : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("MathQuestBook", LoadSceneMode.Single);
    }
}
