using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    Patrol,
    Wait,
    Chase,
    Jump
}

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

    [SerializeField] private PlayerCube playerCube;

    [Header("Information")]
    [SerializeField] private int currPointNumber = 0;//0,1,2,3,0,1,2,3,....
    [SerializeField] private int dirNumber = 1;


    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpPower = 2.0f;
    [SerializeField] private float elapsedTIme = 0;//대기 경과시간
    [SerializeField] private float waitTIme = 1.0f;//1초 대기 변수

    [SerializeField] private float jumpTime = 0;
    [SerializeField] private float JumpInterval = 5.0f;
    [SerializeField] private float landTime = 0;//착지 시간 변수
    [SerializeField] private float landInterval = 0.2f;//착지 대기 변수

    [SerializeField] private MonsterState monsterState = MonsterState.Patrol;//몬스터 현재 상태

    Vector3 forward = new Vector3(0, 0, 1);//이동방향

    private void Start()
    {
        trTarget = trPoints[currPointNumber];
    }

  
    public void AIMoveSystem()
    {
        //Vector3 pos = (trPoint.position - transform.position).normalized;

        transform.position = Vector3.MoveTowards(transform.position, trTarget.position, Time.deltaTime * 2f);

        float distance_value = Vector3.Distance(transform.position, trTarget.position);
        Debug.Log("Distance value:" + distance_value);

        if (distance_value <= 0.1f)
        {
            //trPoints 리스트의 1번째 값을 trTarget에 할당한다.
            //변수를 하나 만든다. 특정상황에서 -1이되는 변수, 변수 초기값 1로 설정,
            //이동 포인트가 마지막값이 된다면 -1로 변경

            currPointNumber += dirNumber;

            //이동포인트가 최대값을 벗어나면 방향을 바꾼다.
            if (currPointNumber >= trPoints.Count)
            {
                currPointNumber = trPoints.Count - 1;
                dirNumber = -1;
            }
            else if (currPointNumber < 0)
            {
                currPointNumber = 0;
                dirNumber = 1;
            }
        }
    }
    private void SetPatrol()
    {
        Debug.Log("정찰");

        /*
         기능구현
        1.이동포인트를 가져오는 기능
        2.이동포인트에 도착했는지 판별 기능
        3.이동하는 기능
        4.도착하면 대기 상태로 전환 기능
        */
        trTarget = trPoints[currPointNumber];

        float dis = Vector3.Distance(transform.position, trTarget.position);//도착여부판별

        if(dis > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, trTarget.position,
                Time.deltaTime * moveSpeed);
        }
        else
        {
            //도착했으면
            monsterState = MonsterState.Wait;
            elapsedTIme = 0;

            //대기시간을 랜덤으로 설정
            waitTIme = Random.Range(1.0f, 3.0f);
        }
    }
    private void SetWait()
    {
        /*
         * 1.도착하면 1초 정도 대기 기능(딜레이), elapsedTime변수,waitTime변수
         * 2.다음 이동포인트 찾는 기능
         * 3.정찰 상태로 전환하는 기능
         */
        Debug.Log("대기");

        elapsedTIme += Time.deltaTime;//시간경과

        if(elapsedTIme >= waitTIme)
        {
            //대기경과시간이 대기시간에 도달했을때 다음 이동포인트를 찾는다. 

            //trPoints 리스트의 1번째 값을 trTarget에 할당한다.
            //변수를 하나 만든다. 특정상황에서 -1이되는 변수, 변수 초기값 1로 설정,
            //이동 포인트가 마지막값이 된다면 -1로 변경

            //이동속도도 랜덤으로 5~10의 값
            moveSpeed = Random.Range(5, 10);

            currPointNumber += dirNumber;

            //이동포인트가 최대값을 벗어나면 방향을 바꾼다.
            if (currPointNumber >= trPoints.Count)
            {
                currPointNumber = trPoints.Count - 1;
                dirNumber = -1;
            }
            else if (currPointNumber < 0)
            {
                currPointNumber = 0;
                dirNumber = 1;
            }

            //정찰상태로 전환
            monsterState = MonsterState.Patrol;
            //elapsedTIme = 0;
        }
    }
    private void SetChase()
    {
        Debug.Log("추적기능");
        //기능구현
        /*
         플레이어를 알아야한다. 
        컬라이더를 통한 충돌판정으로 플레이어 감지
        플레이어와의 거리차이를 통한 플레이어 감지
        플레이어를 따라가는 이동
        추적상태일때만 멀어지면 정찰 상태로 전환
         */
        transform.position = Vector3.MoveTowards(transform.position, playerCube.transform.position,
              Time.deltaTime * moveSpeed);

        float playerPos = Vector3.Distance(transform.position, playerCube.transform.position);
        if(playerPos > 5.0f)
        {
            monsterState = MonsterState.Wait;
            elapsedTIme = 0;
        }
    }
    private void SetJump()
    {
        /*
         * 
         * 타이머기능 5초
         * 점프기능 
         * 착지기능,착지 딜레이
         * 착지가 되었으면 정찰 상태 전환
         */

        landTime += Time.deltaTime;
        if(landTime >= landInterval)
        {
            //이때 착지기능을 실행한다.
            transform.position += new Vector3(0, -1, 0) * jumpPower;
            monsterState = MonsterState.Patrol;
        }
    }

    public void PatrolEvent()
    {
        monsterState = MonsterState.Patrol;
    }
    public void WaitEvent()
    {
        monsterState = MonsterState.Wait;
    }
    public void ChaseEvent()
    {
        monsterState = MonsterState.Chase;

    }

    private void Update()
    {
        //일정간격으로 점프
        jumpTime += Time.deltaTime;
        if(jumpTime >= JumpInterval)
        {
            jumpTime = 0;
            landTime = 0;
            //점프기능
            transform.position += new Vector3(0, 1, 0) * jumpPower;
            monsterState = MonsterState.Jump;
        }

        float playerDis = Vector3.Distance(transform.position, playerCube.transform.position);
        if(playerDis< 3.0f)
        {
            monsterState = MonsterState.Chase;
        }

        switch (monsterState)
        {
            case MonsterState.Patrol:
                //정찰상태
                SetPatrol();
                break;

            case MonsterState.Wait:
                //대기상태
                SetWait();
                break;

            case MonsterState.Chase:
                //추적상태
                SetChase();
                break;

            case MonsterState.Jump:
                //점프상태
                SetJump();
                break;
        }
    }
}
