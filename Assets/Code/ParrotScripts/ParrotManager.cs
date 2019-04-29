using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ParrotManager : MonoBehaviour
{
    public Transform target;

    Vector3 targetPosition, parrotOldPosition;
    BasicBehaviour basic;
    LookingForSpot looking;
    bool isTargetMoving = false;
    bool isProcessingBehaviour;
    private float timer = 0.0f;
    public bool isParrotMoving = false;
    public Animator animator;
    public SpriteRenderer[] parrotSpritesMoving;
    public SpriteRenderer parrotSpriteStaying;
    void Awake()
    {
      
        looking = GetComponent<LookingForSpot>();
        basic = GetComponent<BasicBehaviour>();
        basic.OnTask(target.position);
        targetPosition = target.position;
        parrotOldPosition = transform.position;
        //ParrotSprites();
    }

    private void FixedUpdate()
    {
        ParrotCheck();
        HeroCheck();
        if (isTargetMoving == true && isParrotMoving == true)
        {
            basic.OnTask(target.position);
            timer = 0;
        }
        else if (isTargetMoving == true && isParrotMoving == false)
        {
            timer += Time.fixedDeltaTime;
            if (timer > 40 * Time.fixedDeltaTime)
            {
                basic.OnTask(target.position);
            }
                
            
        }


        else if (isTargetMoving == false)
        {
            looking.OnTask(target.position);
        }
        //ParrotSprites();
        Animation();
    }
    void HeroCheck()
    {
        targetPosition.x -= target.position.x;
        targetPosition.y -= target.position.y;
        if (targetPosition.x==0.0f && targetPosition.y==0.0f)
        {
            isTargetMoving = false;
            
        }
        else
            isTargetMoving = true;
        
        targetPosition = target.position;

    }
    void TryFoundTask()
    {
      
    }
    public void OnTaskFound()
    {
        
    }
    void ParrotCheck()
    {
        parrotOldPosition.x -= transform.position.x;
        parrotOldPosition.y -= transform.position.y;
        if (parrotOldPosition.x == 0.0f && parrotOldPosition.y == 0.0f)
        {
            isParrotMoving = false;

        }
        else
            isParrotMoving = true;

        parrotOldPosition = transform.position;

    }

    void ParrotSprites()
    {

        if (isParrotMoving == true)
        {
            foreach (SpriteRenderer t in parrotSpritesMoving)
            {
                if (t.enabled == true)
                    continue;
                t.enabled = true;

            }
            parrotSpriteStaying.enabled = false;
        }
        else if (isParrotMoving == false)
        {
            foreach (SpriteRenderer t in parrotSpritesMoving)
            {
                if (t.enabled == false)
                    continue;
                t.enabled = false;

            }
            parrotSpriteStaying.enabled = true;
        }
        
    }
    void Animation()
    {
        float dist = Vector3.Distance(transform.position, looking.GetClosestSpot(transform.position));
        if (isTargetMoving == true)
        {
            animator.SetBool("moving",true);
        }
        if (isTargetMoving == false && basic.ForParrotAnimation()==true)
        {
            animator.SetBool("moving",false);
            

        }

    }

}
