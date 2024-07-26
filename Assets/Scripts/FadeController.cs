using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] private float durationTime = 0;

    [SerializeField] private Image imgFade;

    void DebugRaySystem()
    {
        /*
         * ��������
         * ������ ������
         * ������ ������
         * */

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 3.0f);
        }
    }

    private void Start()
    {
        StartCoroutine("R_FadeOut");
    }


    private IEnumerator R_FadeOut()
    {
        yield return null;

        Color color = imgFade.color;

        float elapsedTime = 0;
        while(elapsedTime < durationTime)
        {
            yield return null;//1frame������ ����

            //�����ð��� �����ϴٰ� ���� ���ص� �ð��� �ɶ� ����Ŵ�.
            elapsedTime += Time.deltaTime;
            //Debug.Log("elapsedTime" + elapsedTime);

            color.a = elapsedTime / durationTime;
            imgFade.color = color;
        }
        Debug.Log("����" + elapsedTime);

        color.a = 1.0f;
        imgFade.color = color;
        yield return new WaitForSeconds(3.0f);
        StartCoroutine("R_FadeIn");
    }

    private IEnumerator R_FadeIn()
    {
        yield return null;

        Color color = imgFade.color;

        float elapsedTime = 0;
        while (elapsedTime < durationTime)
        {
            yield return null;//1frame������ ����

            //�����ð��� �����ϴٰ� ���� ���ص� �ð��� �ɶ� ����Ŵ�.
            elapsedTime += Time.deltaTime;
            //Debug.Log("elapsedTime" + elapsedTime);

            color.a = 1.0f -(elapsedTime / durationTime);
            imgFade.color = color;
        }
        Debug.Log("����" + elapsedTime);

        color.a = 0f;
        imgFade.color = color;
    }

}
