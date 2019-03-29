using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Scales this gameobject to fit camera viewport
//Needs to have 
public class SceneScaler : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // First find a center for your bounds.
        Vector3 center = Vector3.zero;

        foreach (Transform child in transform)
        {
            if(child.GetComponent<SpriteRenderer>() != null)
            center += child.GetComponent<SpriteRenderer>().bounds.center;
        }
        center /= transform.childCount; //center is average center of children

        //Now you have a center, calculate the bounds by creating a zero sized 'Bounds', 
        Bounds bounds = new Bounds(center, Vector3.zero);

        foreach (SpriteRenderer renderers in GetComponentsInChildren<SpriteRenderer>())
        {
            //include only visible gameobjects
            bounds.Encapsulate(renderers.bounds);
        }

        //Rescale this gameobject to match camera rect size

        Vector2 topleft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 bottomright = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        Bounds cameraSize = new Bounds(Camera.main.transform.position, Vector2.zero);
        cameraSize.Encapsulate(topleft);
        cameraSize.Encapsulate(bottomright);

        Vector2 scaleMultiplier = new Vector2(cameraSize.size.x / bounds.size.x, cameraSize.size.y / bounds.size.y);


        this.transform.localScale *= scaleMultiplier;


    }
}
