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
    1.��� �̵�����Ʈ ��ġ�� ã�´�
    2.ù��° �̵�����Ʈ�� ��ġ�� �˾ƾ��ϴµ�, ��� �̵�����Ʈ ��ġ���� ���� ù ��ġ�� �Ҵ��Ѵ�
    3.ù��° �̵�����Ʈ ��ġ�� �����ϸ� �ι��� �̵���ġ�� �����Ѵ�
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
    [SerializeField] private float elapsedTIme = 0;//��� ����ð�
    [SerializeField] private float waitTIme = 1.0f;//1�� ��� ����

    [SerializeField] private float jumpTime = 0;
    [SerializeField] private float JumpInterval = 5.0f;
    [SerializeField] private float landTime = 0;//���� �ð� ����
    [SerializeField] private float landInterval = 0.2f;//���� ��� ����

    [SerializeField] private MonsterState monsterState = MonsterState.Patrol;//���� ���� ����

    Vector3 forward = new Vector3(0, 0, 1);//�̵�����

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
            //trPoints ����Ʈ�� 1��° ���� trTarget�� �Ҵ��Ѵ�.
            //������ �ϳ� �����. Ư����Ȳ���� -1�̵Ǵ� ����, ���� �ʱⰪ 1�� ����,
            //�̵� ����Ʈ�� ���������� �ȴٸ� -1�� ����

            currPointNumber += dirNumber;

            //�̵�����Ʈ�� �ִ밪�� ����� ������ �ٲ۴�.
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
        Debug.Log("����");

        /*
         ��ɱ���
        1.�̵�����Ʈ�� �������� ���
        2.�̵�����Ʈ�� �����ߴ��� �Ǻ� ���
        3.�̵��ϴ� ���
        4.�����ϸ� ��� ���·� ��ȯ ���
        */
        trTarget = trPoints[currPointNumber];

        float dis = Vector3.Distance(transform.position, trTarget.position);//���������Ǻ�

        if(dis > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, trTarget.position,
                Time.deltaTime * moveSpeed);
        }
        else
        {
            //����������
            monsterState = MonsterState.Wait;
            elapsedTIme = 0;

            //���ð��� �������� ����
            waitTIme = Random.Range(1.0f, 3.0f);
        }
    }
    private void SetWait()
    {
        /*
         * 1.�����ϸ� 1�� ���� ��� ���(������), elapsedTime����,waitTime����
         * 2.���� �̵�����Ʈ ã�� ���
         * 3.���� ���·� ��ȯ�ϴ� ���
         */
        Debug.Log("���");

        elapsedTIme += Time.deltaTime;//�ð����

        if(elapsedTIme >= waitTIme)
        {
            //������ð��� ���ð��� ���������� ���� �̵�����Ʈ�� ã�´�. 

            //trPoints ����Ʈ�� 1��° ���� trTarget�� �Ҵ��Ѵ�.
            //������ �ϳ� �����. Ư����Ȳ���� -1�̵Ǵ� ����, ���� �ʱⰪ 1�� ����,
            //�̵� ����Ʈ�� ���������� �ȴٸ� -1�� ����

            //�̵��ӵ��� �������� 5~10�� ��
            moveSpeed = Random.Range(5, 10);

            currPointNumber += dirNumber;

            //�̵�����Ʈ�� �ִ밪�� ����� ������ �ٲ۴�.
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

            //�������·� ��ȯ
            monsterState = MonsterState.Patrol;
            //elapsedTIme = 0;
        }
    }
    private void SetChase()
    {
        Debug.Log("�������");
        //��ɱ���
        /*
         �÷��̾ �˾ƾ��Ѵ�. 
        �ö��̴��� ���� �浹�������� �÷��̾� ����
        �÷��̾���� �Ÿ����̸� ���� �÷��̾� ����
        �÷��̾ ���󰡴� �̵�
        ���������϶��� �־����� ���� ���·� ��ȯ
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
         * Ÿ�̸ӱ�� 5��
         * ������� 
         * �������,���� ������
         * ������ �Ǿ����� ���� ���� ��ȯ
         */

        landTime += Time.deltaTime;
        if(landTime >= landInterval)
        {
            //�̶� ��������� �����Ѵ�.
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
        //������������ ����
        jumpTime += Time.deltaTime;
        if(jumpTime >= JumpInterval)
        {
            jumpTime = 0;
            landTime = 0;
            //�������
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
                //��������
                SetPatrol();
                break;

            case MonsterState.Wait:
                //������
                SetWait();
                break;

            case MonsterState.Chase:
                //��������
                SetChase();
                break;

            case MonsterState.Jump:
                //��������
                SetJump();
                break;
        }
    }
}
