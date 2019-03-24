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
///BUG: Cutoff will be always visible if screen width-height ratio is bigger than 16:9
public class SceneChanger : MonoBehaviour
{
    #region Variables & References
    public static SceneChanger instance;
    //Sprite mask for fade in/out anims
    Transform mask;
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

    private void Start()
    {
        mask = transform.Find("Mask");
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
        yield return StartCoroutine( FadeIn());
        SceneManager.LoadScene(scene);
        yield return StartCoroutine(FadeOut());
        yield return null;

    }

    IEnumerator FadeIn()
    {
        //100 iterations, of 0.01f sec duration, so 1 sec total
        while (mask.localScale.x >= 0 && mask.localScale.y >= 0)
        {
            mask.localScale = new Vector3(mask.localScale.x - 0.01f, mask.localScale.y - 0.01f, mask.localScale.z);
            yield return new WaitForSeconds(0.01f);
        }
        //End with setting xy to 0, to avoid floating point precision problems
        mask.localScale = new Vector3(0, 0, mask.localScale.z);
        yield return null;
    }

    IEnumerator FadeOut()
    {
        while (mask.localScale.x <= 1 && mask.localScale.y <= 1)
        {
            mask.localScale = new Vector3(mask.localScale.x + 0.01f, mask.localScale.y + 0.01f, mask.localScale.z);
            yield return new WaitForSeconds(0.01f);
        }
        //End with setting xy to 1, to avoid floating point precision problems
        mask.localScale = new Vector3(1, 1, mask.localScale.z);
        yield return null;
    }
    #endregion

}
