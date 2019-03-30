using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Scene changer. - Change scene with animation - IEnumerator ChangeScene()
/// 
/// ---- SINGLETON ----
/// ---- DontDestroyOnLoad Object ----
/// 
/// </summary>
///BUG: Cutoff will be visible if screen is big
public class SceneChanger : MonoBehaviour
{
    #region Variables & References
    public static SceneChanger instance;
    //Sprite mask for fade in/out anims
    Transform fader;
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
            DontDestroyOnLoad(instance.gameObject);
        }
    }

    private void Start()
    {
        fader = transform.Find("Fader");
    }

    //HACK: Might some small overhead
    void Update()
    {
        transform.position = Camera.main.transform.position;
    }

    #endregion

    #region Public methods
    //Change scene with animation

    public void ChangeScene(string scene)
    {
        StartCoroutine(ChangeSceneCoroutine(scene));
    }
    #endregion

    #region Private methods
    private IEnumerator ChangeSceneCoroutine(string scene)
    {
        yield return StartCoroutine(FadeIn());
        SceneManager.LoadScene(scene);
        yield return StartCoroutine(FadeOut());
        yield return null;

    }

    IEnumerator FadeIn()
    {
        //100 iterations, of 0.01f sec duration, so 1 sec total
        while (fader.localScale.x >= 0 && fader.localScale.y >= 0)
        {
            fader.localScale = new Vector3(fader.localScale.x - 0.01f, fader.localScale.y - 0.01f, fader.localScale.z);
            yield return new WaitForSeconds(0.005f);
        }
        //End with setting xy to 0, to avoid floating point precision problems
        fader.localScale = new Vector3(0, 0, fader.localScale.z);
        yield return null;
    }

    IEnumerator FadeOut()
    {
        while (fader.localScale.x <= 2 && fader.localScale.y <= 2)
        {
            fader.localScale = new Vector3(fader.localScale.x + 0.01f, fader.localScale.y + 0.01f, fader.localScale.z);
            yield return new WaitForSeconds(0.001f);
        }
        //End with setting xy to 1, to avoid floating point precision problems
        fader.localScale = new Vector3(2f, 2f, fader.localScale.z);
        yield return null;
    }
    #endregion

}
