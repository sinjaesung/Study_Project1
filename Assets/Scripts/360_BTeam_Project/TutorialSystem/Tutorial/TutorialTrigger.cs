using UnityEngine;

public class TutorialTrigger : TutorialBase
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private Transform triggerObject;

    public bool isTrigger { set; get; } = false;

    public override void Enter()
    {
        Debug.Log("TutorialTrigger Enter>>");
        // �÷��̾� �̵� ����
        playerController.IsMoved = true;
        // Trigger ������Ʈ Ȱ��ȭ
        triggerObject.gameObject.SetActive(true);
    }

    public override void Execute(TutorialController controller)
    {
        /*
		/// �Ÿ� ����
		if ( (triggerObject.position - playerController.transform.position).sqrMagnitude < 0.1f )
		{
			controller.SetNextTutorial();
		}*/

        /// �浹 ����
        // TutorialTrigger ������Ʈ�� ��ġ�� �÷��̾�� �����ϰ� ���� (Trigger ������Ʈ�� �浹�� �� �ֵ���)
        transform.position = playerController.transform.position;

        if (isTrigger == true)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        Debug.Log("TutorialTrigger Exit>>");

        // �÷��̾� �̵� �Ұ���
        playerController.IsMoved = false;
        // Trigger ������Ʈ ��Ȱ��ȭ
        triggerObject.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.Equals(triggerObject))
        {
            isTrigger = true;

            collision.gameObject.SetActive(false);
        }
    }
}

