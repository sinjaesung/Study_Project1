using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RSP : MonoBehaviour
{
    [SerializeField] private Button btnPaper;

    //가위바위보 이미지를 저장하는 공간
    [SerializeField] private Sprite[] spGroup;

    //플레이어와 컴퓨터의 정보를 보여주는 변수
    [SerializeField] private Image imgPlayer;
    [SerializeField] private Image imgCom;

    /*
     * 가위0 바위1 보2
     * */
    [SerializeField] private int userHand = -1;
    [SerializeField] private int comHand = -1;

    //결과를 출력하는 텍스트 변수
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
        //사용자가 손을 냈다. 컴퓨터도 손을 내야한다.
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
        //서로 같은값 내면 두 값의 차 0
        //내가 이기는 경우의 수는 서로 값을 뺐을시에 -2 or 1이다.

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
