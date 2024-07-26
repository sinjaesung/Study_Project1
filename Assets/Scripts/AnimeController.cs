using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        FadeOutEvent();
    }
     public void FadeOutEvent()
    {
        anim.ResetTrigger("FadeIn");
        anim.SetTrigger("FadeOut");
    }
    public void FadeInEvent()
    {
        anim.ResetTrigger("FadeOut");
        anim.SetTrigger("FadeIn");
    }
}
