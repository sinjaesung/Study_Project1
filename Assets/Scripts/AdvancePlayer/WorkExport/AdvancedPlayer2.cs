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
    [SerializeField] public VideoPlayer videoPlayer;//자식하위videoPlayer

    [SerializeField] public RectTransform rtWindow;//자기자신SelfGameObject RectTransform

    [SerializeField] public Image imgLock;//썸네일이미지

    [Header("Information")]
    [SerializeField] private Vector2 prevWindowSize = new Vector2(0, 0);//초기값벡터크기표현관련
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
        imgLock.gameObject.SetActive(!visible);//화면재생스크린true,썸네일false | 화면재생스크린false,썸네일true
    }
    private void OnEnable()
    {
        
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(SetSliderValue);//슬라이더조절영상구간제어
        speed_Slider.onValueChanged.AddListener(SetSliderSpeedValue);
        volume_Slider.onValueChanged.AddListener(SetVolumeValue);

        //초기화 작업을 해준다.
        prevWindowSize = rtWindow.sizeDelta;//640,360초기크기할당

        //재생시간 확인하기 위해 필요한 것들이 있다.
        //영상의 총시간(max), 재생되고있는 현재 시간(cur)
        slider.maxValue = (float)videoPlayer.length;//하위자식(해당섬네일해당하는영상)의길이
    }
    
    public void WindowModeEvent()
    {
        Debug.Log("창모드");

        //rtWindow.anchorMin = new Vector2(0, 1);
        //rtWindow.anchorMax = new Vector2(0.5f, 0.5f);

        //rtWindow.sizeDelta = new Vector2(1280f, 720f);
        rtWindow.sizeDelta = prevWindowSize;
        //자신의 크기를 원래 본래의 크기대로 표현
    }

    public void FullModeEvent()
    {
        Debug.Log("전체화면"+FullScreenWindow.instance.name);
        fullscreenwindow = FullScreenWindow.instance.FullScreenWindow_self_refer;

        //=======================
        //1.anchor값을 0,0,1,1 형태로 할당
        //2.offsetSize를 0,0,0,0 으로 초기화

        // rtWindow.anchorMin = new Vector2(0, 0);
        //rtWindow.anchorMax = new Vector2(1, 1);

        //rtWindow.offsetMin = new Vector2(0, 0);
        //rtWindow.offsetMax = new Vector2(0, 0);
        //rtWindow.sizeDelta = new Vector2(1920, 1080);

        //자신의 크기를 자신 포함하고있는 캔버스요소 전체 꽉차는 크기대로 표현
        //fullscreenwindow.SetActive(true);
        FullScreenWindow.instance.SetVisible(true);
        //FullScreenWindow.instance.NowActiveVideo = videoIndex;
        FullScreenWindow.instance.NowActiveVideoSetup(videoIndex);
    }

    public void SetPlayEvent()
    {
        Debug.Log("재생");
        //videoPlayer.Play();
        imgLock.gameObject.SetActive(false);

        //자신영상을 그저 실행.자신에 해당하는 RenderTexture할당하여 실행.
        videoManager.NowVideoIndex = videoIndex;
        videoManager.SetActivateVideo_OnlyPlay(videoIndex);
    }

    public void SetPauseEvent()
    {
        Debug.Log("일시정지");
        //videoPlayer.Pause();

        //자신을 포함한 모든 비디오 일시정지
        videoManager.SetActivateVideo_OnlyPause(videoIndex);
    }

    public void SetStopEvent()
    {
        Debug.Log("리셋");
        //videoPlayer.Stop();
        imgLock.gameObject.SetActive(true);

        //자신영상 리셋 및 재생속도 초기화.썸네일다시초기화(자신 포함 나머지요소도 전체 리셋)
        videoManager.SetAllVideo_Stop();
    }

    #region 인터페이스를 사용했다.
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("호버");
        imgUIFrame.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("언호버");
        imgUIFrame.gameObject.SetActive(false);
    }
    #endregion

    private void SetSliderValue(float _value)
    {
      //  Debug.Log("SetSliderValue>:" + _value);
        videoPlayer.time = _value;
        //영상 재생구간 제어
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
        //영상의 현재 재생시간,최종적 길이 시간형태로 출력 Util
        string strTime = string.Empty;
        //현재 재생되고 있는 영상의 시간값을 받아온다.
        float time = (float)_time;

        int hour = (int)time / 3600;
        int min = (int)(time % 3600) / 60;
        int sec = (int)(time % 60);

        if(hour > 0)
        {
            //영상의 재생시간이 1시간 이상이면 시간을 표기
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
