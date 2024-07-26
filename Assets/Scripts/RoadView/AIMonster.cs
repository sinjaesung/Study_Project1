using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMonster : MonoBehaviour
{
    /*
    1.모든 이동포인트 위치를 찾는다
    2.첫번째 이동포인트의 위치를 알아야하는데, 모든 이동포인트 위치에서 제일 첫 위치로 할당한다
    3.첫번째 이동포인트 위치에 도착하면 두번쨰 이동위치로 변경한다
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
            //trPoints 리스트의 1번째 값을 trTarget에 할당한다.
            trTarget = trPoints[currPointNumber];

            if (currPointNumber <trPoints.Count-1)
            {
                currPointNumber ++;
            }
            else
            {
                currPointNumber = 0;
            }
            Debug.Log("도착했어");
        }
    }
}
