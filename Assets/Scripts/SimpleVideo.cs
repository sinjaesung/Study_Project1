using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SimpleVideo : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private List<Button> btnGroup = new List<Button>();

    [Header("Information")]
    [SerializeField] private float playSpeed = 1.0f;

    void Awake()
    {
        //btnGroup.Add(GetComponentInChildren<Button>());

        btnGroup.AddRange(GetComponentsInChildren<Button>());

        videoPlayer = FindObjectOfType<VideoPlayer>();
    }

    private void Start()
    {
        btnGroup[0].onClick.AddListener(SetPlay);
        btnGroup[1].onClick.AddListener(SetPause);
        btnGroup[2].onClick.AddListener(SetStop);
        btnGroup[3].onClick.AddListener(SetSpeed);

    }
    private void SetPlay()
    {
        Debug.Log("�÷���");
        videoPlayer.Play();
    }
    private void SetPause()
    {
        Debug.Log("�Ͻ�����");
        videoPlayer.Pause();
    }
    private void SetStop()
    {
        Debug.Log("����");
        videoPlayer.Stop();

        playSpeed = 1.0f;
        videoPlayer.playbackSpeed = playSpeed;
    }
    private void SetSpeed()
    {
        Debug.Log("����ӵ�");
        videoPlayer.playbackSpeed = playSpeed;
    }
}
