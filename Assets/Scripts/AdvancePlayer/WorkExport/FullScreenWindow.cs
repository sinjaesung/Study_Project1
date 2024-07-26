using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class FullScreenWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform CloseBtn;

    public static FullScreenWindow instance = null;

    [SerializeField] private GameObject FullScreenWindow_self;

    [Header("Components")]
    [SerializeField] public VideoPlayer targetVideoPlayer;

    [Header("ScreenInfo")]
    [SerializeField] private int nowActiveVideo = 0;

    [SerializeField] private Image imgUIFrame;//VideoControlUnit
    [SerializeField] private Slider slider;
    [SerializeField] private Slider Speed_Slider;
    [SerializeField] private Slider Volume_Slider;
    [SerializeField] private TMP_Text textTime;

    [SerializeField] VideoClip videoclip;
    [SerializeField] double videoLength;
    [SerializeField] VideoManager videoManager;
    public GameObject FullScreenWindow_self_refer
    {
        get
        {
            return FullScreenWindow_self;
        }
        set
        {
            FullScreenWindow_self = value;
        }
    }
    public int NowActiveVideo
    {
        get
        {
            return nowActiveVideo;
        }
        set
        {
            nowActiveVideo = value;
        }
    }
    private void Awake()
    {
        FullScreenWindow_self = transform.gameObject;

        SetVisible(false);

        imgUIFrame = transform.GetComponentInChildren<VideoItem_UIFrameControl>().GetComponent<Image>();
        slider = imgUIFrame.GetComponentInChildren<Slider>();
        Speed_Slider = imgUIFrame.GetComponentInChildren<SpeedSlider>().GetComponent<Slider>();
        Volume_Slider = imgUIFrame.GetComponentInChildren<VolumeSlider>().GetComponent<Slider>();
        textTime = slider.GetComponentInChildren<TMP_Text>();

        videoManager = FindObjectOfType<VideoManager>();
    }
    void Start()
    {
        instance = this;

        CloseBtn = transform.Find("CloseBtn");

        slider.onValueChanged.AddListener(SetSliderValue);//�����̴��������󱸰�����
        Speed_Slider.onValueChanged.AddListener(SetSliderSpeedValue);//���ǵ彽���̴��ش�Ȱ������ӵ�����
        Volume_Slider.onValueChanged.AddListener(SetVolumeValue);//���������̴��ش�Ȱ�����󺼷�����
    }
    public void NowActiveVideoSetup(int videoIndex)
    {
        nowActiveVideo = videoIndex;
        targetVideoPlayer = videoManager.VideoList_refer[videoIndex].GetComponentInParent<VideoPlayer>();

        videoclip = targetVideoPlayer.clip;
        videoLength = videoclip.length;

        slider.maxValue = (float)targetVideoPlayer.length;//�����ڽ��ش� Ÿ�꿵���� ���������
    }

    public void SetVisible(bool isvisible=true)
    {
        //gameObject.SetActive(isvisible);
        if (isvisible)
        {
            Debug.Log("FullScreenWindow>isvisible>" + isvisible);
            // transform.GetComponent<RectTransform>().anchoredPosition.Set(0, 0);
            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
        else
        {
            Debug.Log("FullScreenWindow>isvisible>" + isvisible);
            //transform.GetComponent<RectTransform>().anchoredPosition.Set(0, -2000);
            transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -2000);
        }
    }

    public void SetPlayEvent()
    {
        videoManager.NowVideoIndex = nowActiveVideo;
        videoManager.SetActivateVideo_OnlyPlay(nowActiveVideo);//���� Ǯ��ũ�� Ȱ��ȭ�� ������ ���(������ ���󸮽�Ʈ�� ����)
        //�����÷��̽ÿ� Ÿ��videoPlayer�� ����ǰ�,�ش� �÷��̾��� ����� ��� ���뿬��� RenderTexture�� �����Ѵ�
    }

    public void SetPauseEvent()
    {
        videoManager.SetActivateVideo_OnlyPause(nowActiveVideo);//�ڽ��� ������ ��� ���� �Ͻ�����
    }

    public void SetStopEvent()
    {
        targetVideoPlayer.time = 0;
        targetVideoPlayer.Play();

        videoManager.SetAllVideo_Stop();//�ڽſ��� ���� �� ����ӵ� �ʱ�ȭ.����ϴٽ��ʱ�ȭ(�ڽ� ���� ��������ҵ� ��ü ����)
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
        targetVideoPlayer.time = _value;//Ÿ���÷��̾ ���ؼ� ����� �����̴��� ��������
    }
    private void SetSliderSpeedValue(float _value)
    {
        Debug.Log("FullScreenWindow SetSliderSpeedValue>" + _value);
        targetVideoPlayer.playbackSpeed = _value;
    }
    private void SetVolumeValue(float _value)
    {
        Debug.Log("FullScreenWindow SetVolumeValue>" + _value);
        targetVideoPlayer.SetDirectAudioVolume(0, _value);//0~1 volume
    }
    private string CalcTime(double _time)
    {
        //������ ���� ��̽ð�,������ ���� �ð����·� ��� Util
        string strTime = string.Empty;
        //���� ����ǰ� �ִ� Ȱ��ȭ ������ �ð��� �޾ƿ´�.
        float time = (float)_time;

        int hour = (int)time / 3600;
        int min = (int)(time % 3600) / 60;
        int sec = (int)(time % 60);

        if (hour > 0)
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

        if(targetVideoPlayer != null)
        {
            slider.value = (float)targetVideoPlayer.time;
            Speed_Slider.value = (float)targetVideoPlayer.playbackSpeed;
            Volume_Slider.value = (float)targetVideoPlayer.GetDirectAudioVolume(0);

            //textTime.text = string.Format("{0} / {1}", CalcTime(videoPlayer.time), CalcTime(videoPlayer.length));
            textTime.text = CalcTime(targetVideoPlayer.time) + " / " + CalcTime(targetVideoPlayer.length);
        }  
    }
}
