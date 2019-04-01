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
    Transform GratzText;
    float scale = 1000f;
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
        GratzText = transform.Find("GratzText");
        //Scale fader according to screen size
        //To get whole circle out of screen bounds, R needs to be equal to sqrt(1/4b^2 + 1/4a^2) from pythagorean principle
        //Then to get scale -> R / 1/2b (1/2b since the circle radius is equal to screen height at the beggining
        //Remember that this method will work only in landscape mode (though it wont be actually a problem, because our game is supposed to be played in
        //landscape mode...)
        float R = Mathf.Sqrt(0.25f * Screen.height * Screen.height + 0.25f * Screen.width * Screen.width);
        scale = R / (0.5f * Screen.height);

        //set scale
        fader.transform.localScale = new Vector3(scale, scale, 1);
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

    //Change scene with on win animation
    public void ChangeSceneOnWin(string scene)
    {
        StartCoroutine(ChangeSceneOnWinCoroutine(scene));
    }   

    #endregion

    #region Private methods
    private IEnumerator ChangeSceneCoroutine(string scene)
    {
        yield return StartCoroutine(FadeIn(fader, scale));
        SceneManager.LoadScene(scene);
        yield return StartCoroutine(FadeOut(fader, scale));
        yield return null;
    }

    private IEnumerator ChangeSceneOnWinCoroutine(string scene)
    {
        yield return FadeOut(GratzText, 1f);
        GratzText.GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(3f);
        yield return FadeIn(GratzText, 1f);
        yield return ChangeSceneCoroutine(scene);
        yield return null;
    }

    IEnumerator FadeIn(Transform obj, float MaxScale)
    {
        //100 iterations, of 0.01f sec duration, so 1 sec total
        //TODO: move MaxScale / 100f somewhere else, to avoid constant recalculation (though it should not have almost any effect on performance)
        while (obj.localScale.x >= 0 && obj.localScale.y >= 0)
        {
            obj.localScale = new Vector3(obj.localScale.x - (MaxScale / 100f), obj.localScale.y - (MaxScale / 100f), obj.localScale.z);
            yield return new WaitForSeconds(0.01f);
        }
        //End with setting xy to 0, to avoid floating point precision problems
        obj.localScale = new Vector3(0, 0, obj.localScale.z);
        yield return null;
    }

    IEnumerator FadeOut(Transform obj, float MaxScale)
    {
        while (obj.localScale.x <= MaxScale && obj.localScale.y <= MaxScale)
        {
            obj.localScale = new Vector3(obj.localScale.x + (MaxScale / 100f), obj.localScale.y + (MaxScale / 100f), obj.localScale.z);
            yield return new WaitForSeconds(0.01f);
        }
        //End with setting xy to 1, to avoid floating point precision problems
        obj.localScale = new Vector3(MaxScale, MaxScale, obj.localScale.z);
        yield return null;
    }
    #endregion

}
