using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAnime : MonoBehaviour
{
    /*
     ��ư�� ������ ���̵� �ƿ�
    �����ð� ���Ŀ� ���̵� ��
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
