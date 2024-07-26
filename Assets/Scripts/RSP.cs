using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RSP : MonoBehaviour
{
    [SerializeField] private Button btnPaper;

    //���������� �̹����� �����ϴ� ����
    [SerializeField] private Sprite[] spGroup;

    //�÷��̾�� ��ǻ���� ������ �����ִ� ����
    [SerializeField] private Image imgPlayer;
    [SerializeField] private Image imgCom;

    /*
     * ����0 ����1 ��2
     * */
    [SerializeField] private int userHand = -1;
    [SerializeField] private int comHand = -1;

    //����� ����ϴ� �ؽ�Ʈ ����
    [SerializeField] private TMP_Text textResult;

    private string strResult = "None";

    private void Start()
    {
        textResult.text = strResult;
        btnPaper.onClick.AddListener(PaperEvent);
    }
   
    public void RockEvent()
    {
        userHand = 1; 
        RSPCalculator();
        imgPlayer.sprite = spGroup[userHand];
    }

    public void ScisscorEvent()
    {
        userHand = 0;
        RSPCalculator();
        imgPlayer.sprite = spGroup[userHand];
    }

    public void PaperEvent()
    {
        userHand = 2;
        RSPCalculator();
        imgPlayer.sprite = spGroup[userHand];
    }

    private void RSPCalculator()
    {
        //����ڰ� ���� �´�. ��ǻ�͵� ���� �����Ѵ�.
        //Random.InitState(0);
        comHand = Random.Range(0, 3);

        imgCom.sprite = spGroup[comHand];

        //ProcedureFunc();
        OptimalFunc();

        textResult.text = strResult;
    }
    private void TrickCalc()
    {
        string[] result = { "Win", "Draw", "Lose" };
        int rand = Random.Range(0, 3);
        Debug.Log(result[rand]);
    }

    private void OptimalFunc()
    {
        //���� ������ ���� �� ���� �� 0
        //���� �̱�� ����� ���� ���� ���� �����ÿ� -2 or 1�̴�.

        int calc = userHand - comHand;
        if(calc == 0)
        {
            strResult = "Draw";
        }else if(calc == -2 || calc == 1)
        {
            strResult = "Win";
        }
        else
        {
            strResult = "Lose";
        }
    }
    private void ProcedureFunc()
    {
        switch (userHand)
        {
            case 0:
                if (comHand == 0)
                {
                    strResult = "Draw";
                }
                else if (comHand == 1)
                {
                    strResult = "Lose";
                }
                else if (comHand == 2)
                {
                    strResult = "Win";
                }

                break;

            case 1:
                if (comHand == 0)
                {
                    strResult = "Win";
                }
                else if (comHand == 1)
                {
                    strResult = "Draw";
                }
                else if (comHand == 2)
                {
                    strResult = "Lose";
                }

                break;

            case 2:
                if (comHand == 0)
                {
                    strResult = "Lose";
                }
                else if (comHand == 1)
                {
                    strResult = "Win";
                }
                else if (comHand == 2)
                {
                    strResult = "Draw";
                }

                break;
        }
    }
}
