using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class AdvancedPlayer2 : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Header("Components")]
    [SerializeField] public VideoPlayer videoPlayer;//�ڽ�����videoPlayer

    [SerializeField] public RectTransform rtWindow;//�ڱ��ڽ�SelfGameObject RectTransform

    [SerializeField] public Image imgLock;//������̹���

    [Header("Information")]
    [SerializeField] private Vector2 prevWindowSize = new Vector2(0, 0);//�ʱⰪ����ũ��ǥ������
    [SerializeField] private Vector2 prevWindowSize2 = Vector2.zero;

    [SerializeField] private Image imgUIFrame;//VideoControlUnit

    [SerializeField] private Slider slider;
    [SerializeField] private Slider speed_Slider;
    [SerializeField] private Slider volume_Slider;
    [SerializeField] private TMP_Text textTime;

    [SerializeField] private int videoIndex = 0;

    [SerializeField] VideoManager videoManager;
    [SerializeField] VideoClip videoclip;
    [SerializeField] double videoLength;

    [SerializeField] GameObject fullscreenwindow;

    [SerializeField] RawImage rawImagePlayScreen;
    private void Awake()
    {
        videoPlayer = GetComponentInChildren<VideoPlayer>();
        rtWindow = transform.GetComponent<RectTransform>();
        imgLock = transform.GetComponentInChildren<PlayerThumbnail>().GetComponent<Image>();
        imgUIFrame = transform.GetComponentInChildren<VideoItem_UIFrameControl>().GetComponent<Image>();
        slider = imgUIFrame.GetComponentInChildren<Slider>();
        speed_Slider = GetComponentInChildren<SpeedSlider>().GetComponent<Slider>();
        volume_Slider = GetComponentInChildren<VolumeSlider>().GetComponent<Slider>();
        textTime = slider.GetComponentInChildren<TMP_Text>();

        videoManager = FindObjectOfType<VideoManager>();

        videoclip = videoPlayer.clip;
        videoLength = videoclip.length;

        rawImagePlayScreen = GetComponentInChildren<RawImage>();
    }
    public void VideoItem_Screen_SetVisible(bool visible)
    {
        rawImagePlayScreen.gameObject.SetActive(visible);
        imgLock.gameObject.SetActive(!visible);//ȭ�������ũ��true,�����false | ȭ�������ũ��false,�����true
    }
    private void OnEnable()
    {
        
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(SetSliderValue);//�����̴��������󱸰�����
        speed_Slider.onValueChanged.AddListener(SetSliderSpeedValue);
        volume_Slider.onValueChanged.AddListener(SetVolumeValue);

        //�ʱ�ȭ �۾��� ���ش�.
        prevWindowSize = rtWindow.sizeDelta;//640,360�ʱ�ũ���Ҵ�

        //����ð� Ȯ���ϱ� ���� �ʿ��� �͵��� �ִ�.
        //������ �ѽð�(max), ����ǰ��ִ� ���� �ð�(cur)
        slider.maxValue = (float)videoPlayer.length;//�����ڽ�(�ش缶�����ش��ϴ¿���)�Ǳ���
    }
    
    public void WindowModeEvent()
    {
        Debug.Log("â���");

        //rtWindow.anchorMin = new Vector2(0, 1);
        //rtWindow.anchorMax = new Vector2(0.5f, 0.5f);

        //rtWindow.sizeDelta = new Vector2(1280f, 720f);
        rtWindow.sizeDelta = prevWindowSize;
        //�ڽ��� ũ�⸦ ���� ������ ũ���� ǥ��
    }

    public void FullModeEvent()
    {
        Debug.Log("��üȭ��"+FullScreenWindow.instance.name);
        fullscreenwindow = FullScreenWindow.instance.FullScreenWindow_self_refer;

        //=======================
        //1.anchor���� 0,0,1,1 ���·� �Ҵ�
        //2.offsetSize�� 0,0,0,0 ���� �ʱ�ȭ

        // rtWindow.anchorMin = new Vector2(0, 0);
        //rtWindow.anchorMax = new Vector2(1, 1);

        //rtWindow.offsetMin = new Vector2(0, 0);
        //rtWindow.offsetMax = new Vector2(0, 0);
        //rtWindow.sizeDelta = new Vector2(1920, 1080);

        //�ڽ��� ũ�⸦ �ڽ� �����ϰ��ִ� ĵ������� ��ü ������ ũ���� ǥ��
        //fullscreenwindow.SetActive(true);
        FullScreenWindow.instance.SetVisible(true);
        //FullScreenWindow.instance.NowActiveVideo = videoIndex;
        FullScreenWindow.instance.NowActiveVideoSetup(videoIndex);
    }

    public void SetPlayEvent()
    {
        Debug.Log("���");
        //videoPlayer.Play();
        imgLock.gameObject.SetActive(false);

        //�ڽſ����� ���� ����.�ڽſ� �ش��ϴ� RenderTexture�Ҵ��Ͽ� ����.
        videoManager.NowVideoIndex = videoIndex;
        videoManager.SetActivateVideo_OnlyPlay(videoIndex);
    }

    public void SetPauseEvent()
    {
        Debug.Log("�Ͻ�����");
        //videoPlayer.Pause();

        //�ڽ��� ������ ��� ���� �Ͻ�����
        videoManager.SetActivateVideo_OnlyPause(videoIndex);
    }

    public void SetStopEvent()
    {
        Debug.Log("����");
        //videoPlayer.Stop();
        imgLock.gameObject.SetActive(true);

        //�ڽſ��� ���� �� ����ӵ� �ʱ�ȭ.����ϴٽ��ʱ�ȭ(�ڽ� ���� ��������ҵ� ��ü ����)
        videoManager.SetAllVideo_Stop();
    }

    #region �������̽��� ����ߴ�.
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("ȣ��");
        imgUIFrame.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("��ȣ��");
        imgUIFrame.gameObject.SetActive(false);
    }
    #endregion

    private void SetSliderValue(float _value)
    {
      //  Debug.Log("SetSliderValue>:" + _value);
        videoPlayer.time = _value;
        //���� ������� ����
    }
    private void SetSliderSpeedValue(float _value)
    {
        Debug.Log("AdvancedPlayer2 SetSliderSpeedValue>" + _value);
        videoPlayer.playbackSpeed = _value;
    }
    private void SetVolumeValue(float _value)
    {
        Debug.Log("AdvancedPlayer2 SetVolumeValue>" + _value);
        videoPlayer.SetDirectAudioVolume(0, _value);//0~1 volume
    }
    private string CalcTime(double _time)
    {
        //������ ���� ����ð�,������ ���� �ð����·� ��� Util
        string strTime = string.Empty;
        //���� ����ǰ� �ִ� ������ �ð����� �޾ƿ´�.
        float time = (float)_time;

        int hour = (int)time / 3600;
        int min = (int)(time % 3600) / 60;
        int sec = (int)(time % 60);

        if(hour > 0)
        {
            //������ ����ð��� 1�ð� �̻��̸� �ð��� ǥ��
            strTime = hour.ToString("D2") + ":" + min.ToString("D2") + ":" + sec.ToString("D2");
        }
        else
        {
            strTime = min.ToString("D2") + ":" + sec.ToString("D2");
        }

        return strTime;
    }

    void Update()
    {
       // Debug.Log("now playtime:" + videoPlayer.time + "/" + videoPlayer.length);

        slider.value = (float)videoPlayer.time;
        speed_Slider.value = (float)videoPlayer.playbackSpeed;
        volume_Slider.value = (float)videoPlayer.GetDirectAudioVolume(0);

        //textTime.text = string.Format("{0} / {1}", CalcTime(videoPlayer.time), CalcTime(videoPlayer.length));
        textTime.text = CalcTime(videoPlayer.time) + " / " + CalcTime(videoPlayer.length);
    }
}
