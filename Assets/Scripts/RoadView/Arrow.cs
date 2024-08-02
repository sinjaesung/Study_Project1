using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.SceneView;

/*
 * ���� �̵��ϴ� �뵵�� Arrow
   Ŭ���ÿ� ���� ����� ������ ���̵�ƿ��Ǹ� �̵��Ѵ�.
 */

public class Arrow : MonoBehaviour
{
    [SerializeField] private GameObject objMessage;
    [SerializeField] private TMP_Text TextMessage;

    [SerializeField] private Vector3 offset = new Vector3(0, 1, 0);

    [SerializeField] private new string name = string.Empty;

    private bool isHover = false;

    //[SerializeField] private int loadSceneIndex = 5;
    //Arrow�� Ŭ���� �ش� ��������(�ش� Arrow�� �ٹ���ġ�� ī�޶� �̵���Ű�¿��� ����)
    //Arrow�� �̵���Ű�� ���⺤�� x,z���� ���͸� �����صΰ� Ŭ���� �� �������� �̵��ϰԲ�(ī�޶�)
    [SerializeField] private Vector3 move_direction = new Vector3(0, 0, 0);

    [SerializeField] private Camera playerCamera;

    [SerializeField] private int moveSceneIndex = 0;
    [SerializeField] private FadeInOut fadeinout;

    [SerializeField] CameraMoveTest cameraMove;
    private void Start()
    {
        fadeinout = FindObjectOfType<FadeInOut>();
        playerCamera = FindObjectOfType<Camera>();
        cameraMove = playerCamera.GetComponent<CameraMoveTest>();
    }
    private void OnMouseEnter()
    {
        isHover = true;
    }
    private void OnMouseExit()
    {
        isHover = false;
    }

    private void OnMouseUp()
    {
       Debug.Log("Arrow��� Ŭ���� ���!");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�浹 �ε��� ���ü>>" + other.transform.name);
        objMessage.SetActive(true);
        TextMessage.text = $"{name} go to Scene Move";
    }
    private void OnTriggerExit(Collider other)
    {
        objMessage.SetActive(false);
    }
    public void UI_MoveScene_Close()
    {
        objMessage.SetActive(false);
    }
    public void SceneMove()
    {
        Debug.Log("���̵�>>" + moveSceneIndex);
        //SceneManager.LoadScene(moveSceneIndex);
        StartCoroutine(SceneLoad());
    }
    IEnumerator SceneLoad()
    {
        cameraMove.CanControl = false;
        float waitTime = fadeinout.GetFadeTime();
        fadeinout.StartFadeOut();
        yield return new WaitForSeconds(waitTime);
        fadeinout.StartFadeIn();
        yield return new WaitForSeconds(waitTime);
        Debug.Log("fadeinoutȿ���� ��� ���� waitTime�Ŀ� �� ��ȯ" + waitTime);
        SceneManager.LoadScene(moveSceneIndex);
    }

    /*private void Update()
    {
        if(isHover == true)
        {
            //Arrow������ǥ�� ȭ�� ��ũ����ǥ�� �ٲ��ش�.
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + offset);

            //���� ��ġ�� ��ũ����ǥ�� ������ �� message�� ��ġ�� ����
            objMessage.transform.position = screenPos;
        }
    }*/
}
