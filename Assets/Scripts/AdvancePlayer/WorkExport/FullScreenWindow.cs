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

        slider.onValueChanged.AddListener(SetSliderValue);//슬라이더조절영상구간제어
        Speed_Slider.onValueChanged.AddListener(SetSliderSpeedValue);//스피드슬라이더해당활성영상속도제어
        Volume_Slider.onValueChanged.AddListener(SetVolumeValue);//볼륨슬라이더해당활성영상볼륨제어
    }
    public void NowActiveVideoSetup(int videoIndex)
    {
        nowActiveVideo = videoIndex;
        targetVideoPlayer = videoManager.VideoList_refer[videoIndex].GetComponentInParent<VideoPlayer>();

        videoclip = targetVideoPlayer.clip;
        videoLength = videoclip.length;

        slider.maxValue = (float)targetVideoPlayer.length;//하위자식해당 타깃영상의 총재생길이
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
        videoManager.SetActivateVideo_OnlyPlay(nowActiveVideo);//현재 풀스크린 활성화된 영상을 재생(나머지 영상리스트는 중지)
        //영상플레이시에 타깃videoPlayer가 실행되고,해당 플레이어의 연결된 모두 공용연결된 RenderTexture를 갱신한다
    }

    public void SetPauseEvent()
    {
        videoManager.SetActivateVideo_OnlyPause(nowActiveVideo);//자신을 포함한 모든 비디오 일시정지
    }

    public void SetStopEvent()
    {
        targetVideoPlayer.time = 0;
        targetVideoPlayer.Play();

        videoManager.SetAllVideo_Stop();//자신영상 리셋 및 재생속도 초기화.썸네일다시초기화(자신 포함 나머지요소도 전체 리셋)
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
        targetVideoPlayer.time = _value;//타깃플레이어에 한해서 연결된 슬라이더값 맵핑제어
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
        //영상의 현재 재싱시간,최종적 길이 시간형태로 출력 Util
        string strTime = string.Empty;
        //현재 재생되고 있는 활성화 영상의 시간값 받아온다.
        float time = (float)_time;

        int hour = (int)time / 3600;
        int min = (int)(time % 3600) / 60;
        int sec = (int)(time % 60);

        if (hour > 0)
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
