using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadView : MonoBehaviour
{
    [SerializeField] private new Camera camera;

    //��ħ�� ����
    [SerializeField] private RectTransform rtCompass;
    [SerializeField] private float fov = 0;

    [SerializeField] private float wheelSpeed = 10f;

    [SerializeField] private float roll;
    [SerializeField] private float pitch;
    [SerializeField] private float mouseSpeed = 10f;

    [SerializeField] private float autoTime = 0;

    [SerializeField] private bool isDebugMode = false;

    private void Awake()
    {
        fov = camera.fieldOfView;
    }
    private void Update()
    {
        //���콺 ��ũ������ ���� ī�޶� ȭ�� ���ξƿ�ȿ��
        float wheel = Input.GetAxis("Mouse ScrollWheel");
        if(Mathf.Abs(wheel) > 0)
        {
            Debug.Log("Wheel");
            fov -= wheel * wheelSpeed;
            fov = Mathf.Clamp(fov, 25, 80);//25~80����
            camera.fieldOfView = fov;
        }

        //���콺�� ������ �ִ� ���¿��� �¿�� �������� ȸ���Ѵ�.ī�޶� ȸ���ؾ��Ѵ�.
        if (Input.GetMouseButton(1))
        {
            autoTime = 0;

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            roll -= mouseY * Time.deltaTime * mouseSpeed;//ī�޶� x�� ȸ��(���Ʒ�ȸ��)
            pitch -= mouseX * Time.deltaTime * mouseSpeed;//ī�޶� �¿� ȸ��

            roll = Mathf.Clamp(roll, -30, 60);//���Ʒ� ȸ�� ����
        }
        else
        {
            autoTime += Time.deltaTime;
        }

        if(autoTime >= 5.0f && isDebugMode == false)
        {
            //5�ʰ� ������ �ڵ����� y��ȸ��(�¿�ȸ��)�ǰԲ�
            pitch += Time.deltaTime * 10f;
        }

        if(pitch >= 360f)
        {
            pitch -= 360f;
        }
        else
        {
            pitch += 360f;
        }

        camera.transform.eulerAngles = new Vector3(roll, pitch, 0);//x��ȸ��,y��ȸ�� �ݿ�
        rtCompass.rotation = Quaternion.Euler(0, 0, pitch);//ȭ���� �¿�ȸ��(ī�޶�y��ȸ��) pitch�� �״�� �ݿ� ��ħ��
    }
}
