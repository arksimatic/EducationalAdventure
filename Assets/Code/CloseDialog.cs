using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDialog : MonoBehaviour
{
    [SerializeField]
    GameObject objToDestroy;

    public void OnClick()
    {
        Destroy(objToDestroy);
    }
}
