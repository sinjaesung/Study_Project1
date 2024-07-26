using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAnime : MonoBehaviour
{
    /*
     버튼을 누르면 페이드 아웃
    일정시간 이후에 페이드 인
    */
    [SerializeField] private Animator fadeAnimator;

    public void AutoFadeEvent()
    {
        fadeAnimator.ResetTrigger("FadeIn");
        fadeAnimator.SetTrigger("FadeOut");

        //Invoke("AutoFadeIn", 3.0f);
    }

    public void AutoFadeIn()
    {
        fadeAnimator.ResetTrigger("FadeOut");
        fadeAnimator.SetTrigger("FadeIn");
    }
}
