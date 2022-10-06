using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Model : MonoBehaviour
{
    private Animator animator;
    private int currHurtAnimationIndex = 1;

    public void Init()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayHurtAnimation()
    {
        animator.SetTrigger(" ‹…À" + currHurtAnimationIndex);
        if (currHurtAnimationIndex == 1)
        {
            currHurtAnimationIndex = 2;
        }
        else
        {
            currHurtAnimationIndex = 1;
        }
    }

    public void StopHurtAnimation()
    {
        animator.SetTrigger(" ‹…ÀΩ· ¯");
    }
}
