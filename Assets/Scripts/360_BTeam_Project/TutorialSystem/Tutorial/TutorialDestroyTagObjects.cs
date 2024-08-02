using UnityEngine;

public class TutorialDestroyTagObjects : TutorialBase
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private GameObject[] objectList;
    [SerializeField]
    private string tagName;

    public override void Enter()
    {
        Debug.Log("TutorialDestroyTagObjects Enter>>");
        // �÷��̾��� �̵�, ������ �����ϵ��� ����
        playerController.IsMoved = true;
        playerController.IsAttacked = true;

        // �ı��ؾ��� ������Ʈ���� Ȱ��ȭ
        for (int i = 0; i < objectList.Length; ++i)
        {
            objectList[i].SetActive(true);
        }
    }

    public override void Execute(TutorialController controller)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tagName);

        if (objects.Length == 0)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        Debug.Log("TutorialDestroyTagObjects Exit>>");
        playerController.IsMoved = false;
        playerController.IsAttacked = false;
    }
}