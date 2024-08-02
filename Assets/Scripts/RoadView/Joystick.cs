using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//**(240730_PSB) ���콺�� ��������, ���콺�� ������ ������, ������������
public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    //====================
    // ���̽�ƽ�� ���
    // 1. ���콺�� ������ �ִ� ���¿��� �����̸� ��ƽ�� �����̰� �ϸ�ȴ�.
    // 2. �����ϼ� �ִ� �Ÿ��� �����ؾ��Ѵ�.
    // 3. ���콺�� ������ �ִٰ� ���� ���� ��ġ�� ���ư��� �մϴ�.
    // 4. (�ɼ�) ���콺 �巡�׾� ��� ��ɵ� �����ϵ��� �Ѵ�.
    //====================

    //**(240730_PSB) �����̿��� �� ��ƽ ����
    [SerializeField] private RectTransform rtStick;

    //**(240730_PSB) ������ġ
    [SerializeField] private Vector2 startPos;
    
    //**(240730_PSB) ��ƽ�� �����̿��� �� ũ��� ����
    [SerializeField] private Vector2 dir;

    //======================
    // ���ظ� �������� public���� ����
    //======================
    //**(240730_PSB) ���̽�ƽ�� �����ϴ� ������ �ε�� ���콺 �̵��� ���� �ʰ� �ϱ� ���� Ʈ����
    public bool isDragLock = false;


    private void Start()
    {
        //=================
        // �ʱ�ȭ
        //=================

        startPos = rtStick.position;
    }

    public Vector2 GetDir()
    {
        //**(240730_PSB) dir ���� ���Ϲ޴� �Լ�
        return dir;
    }


    public void OnPointerDown(PointerEventData _eventData)
    {
        Debug.Log("��������");
        isDragLock = true;
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        Debug.Log("������ ������");
        isDragLock = false;
        //**(240730_PSB) ���콺�� ������ ���� �� ó����ġ�� ���ư���.
        rtStick.position = startPos;
        dir = new Vector2(0, 0);
    }

    public void OnDrag(PointerEventData _eventData)
    {
        Debug.Log("������ �����϶�");
        //**(240730_PSB) ���콺�� �巡�� �� ��ġ�� ��ƽ���� �Ҵ��Ѵ�.
        rtStick.position = _eventData.position;
        
        Vector2 drag = _eventData.position - startPos;
        //**(240730_PSB) 40 : �ִ� �Ÿ� ��
        if(drag.magnitude > 40)
        {
            //����ȭ
            drag.Normalize();
            rtStick.position = startPos + drag * 40;
        }

        dir = drag;
    }

}
