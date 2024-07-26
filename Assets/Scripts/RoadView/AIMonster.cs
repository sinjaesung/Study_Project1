using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMonster : MonoBehaviour
{
    /*
    1.��� �̵�����Ʈ ��ġ�� ã�´�
    2.ù��° �̵�����Ʈ�� ��ġ�� �˾ƾ��ϴµ�, ��� �̵�����Ʈ ��ġ���� ���� ù ��ġ�� �Ҵ��Ѵ�
    3.ù��° �̵�����Ʈ ��ġ�� �����ϸ� �ι��� �̵���ġ�� �����Ѵ�
     */
    [Header("Components")]
    [SerializeField] private  List<Transform> trPoints = new List<Transform>();

    [SerializeField] private Transform trTarget;

    [Header("Information")]
    [SerializeField] private int currPointNumber = 0;//0,1,2,3,0,1,2,3,....

    Vector3 forward = new Vector3(0, 0, 1);

    private void Start()
    {
        trTarget = trPoints[currPointNumber];
    }
    private void Update()
    {
        //Vector3 pos = (trPoint.position - transform.position).normalized;

        transform.position = Vector3.MoveTowards(transform.position, trTarget.position, Time.deltaTime * 2f);

        float distance_value = Vector3.Distance(transform.position, trTarget.position);
        Debug.Log("Distance value:" + distance_value);

        if(distance_value <= 0.1f)
        {
            //trPoints ����Ʈ�� 1��° ���� trTarget�� �Ҵ��Ѵ�.
            trTarget = trPoints[currPointNumber];

            if (currPointNumber <trPoints.Count-1)
            {
                currPointNumber ++;
            }
            else
            {
                currPointNumber = 0;
            }
            Debug.Log("�����߾�");
        }
    }
}
