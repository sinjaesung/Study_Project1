using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;
using UnityEngine.SceneManagement;

public class MainRoadMap : MonoBehaviour
{
    [SerializeField] private FadeInOut fadeinout;

    [SerializeField] private int moveSceneIndex = 0;
    void Start()
    {
        fadeinout = FindObjectOfType<FadeInOut>();
    }

    public void SceneMove()
    {
        Debug.Log("���̵�>>" + moveSceneIndex);
        //SceneManager.LoadScene(moveSceneIndex);
        StartCoroutine(SceneLoad());
    }
    IEnumerator SceneLoad()
    {
        float waitTime = fadeinout.GetFadeTime();
        fadeinout.StartFadeOut();
        yield return new WaitForSeconds(waitTime);
        fadeinout.StartFadeIn();
        yield return new WaitForSeconds(waitTime);
        Debug.Log("fadeinoutȿ���� ��� ���� waitTime�Ŀ� �� ��ȯ" + waitTime);
        SceneManager.LoadScene(moveSceneIndex);
    }
}
