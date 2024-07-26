using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{

    [SerializeField] private VideoPlayerItem[] videoList=new VideoPlayerItem[4];
    [SerializeField] private int nowVideoIndex = 0;

    [SerializeField] private bool isFullScreenWindow = false;

    public VideoPlayerItem[] VideoList_refer
    {
        get
        {
            return videoList;
        }
    }
    public int NowVideoIndex
    {
        get
        {
            return nowVideoIndex;
        }
        set
        {
            nowVideoIndex = value;
        }
    }
    public bool IsFullScreenWindow
    {
        get
        {
            return isFullScreenWindow;
        }
        set
        {
            isFullScreenWindow = value;
        }
    }
    void Start()
    {
        var list = FindObjectsOfType<VideoPlayerItem>();
        videoList = new VideoPlayerItem[list.Length];
        for (int e = 0; e < list.Length; e++)
        {
            Debug.Log("VideoManager VideoPlayerList Test> " + e);
            var item = list[e];
            videoList[e] = item;
        }
        Array.Sort(videoList, (item1, item2) => item1.VideoIndex.CompareTo(item2.VideoIndex));
    }
    public void SetActivateVideo_OnlyPlay(int videoIndex)
    {
        Debug.Log("해당 videoIndex 제외한 나머지 비디오는 모두 중지처리 및 랜더텍스처활성화");

        int active_video = 0;
        for(int e=0; e<videoList.Length; e++)
        {
            if(e != videoIndex)
            {
                var VideoPlayer_item = videoList[e].GetComponent<VideoPlayer>();
                Debug.Log($"VideoManager[SetActivateVideo_OnlyPlay]active:{videoIndex} / {e} > activeVideoIndex가 아닌 비디오는 모두 중지 {VideoPlayer_item.clip.name}");

                VideoPlayer_item.playbackSpeed = 1.0f;
                VideoPlayer_item.SetDirectAudioVolume(0, 1.0f);
                VideoPlayer_item.Stop();
                VideoPlayer_item.GetComponentInParent<AdvancedPlayer2>().VideoItem_Screen_SetVisible(false);
            }
            else if (e == videoIndex)
            {
                var VideoPlayer_item = videoList[e].GetComponent<VideoPlayer>();
                active_video = e;
                Debug.Log($"VideoManager[SetActivateVideo_OnlyPlay]active:{videoIndex} / {e} > activeVideoIndex인 비디오만 플레이 {VideoPlayer_item.clip.name}");
                Debug.Log($"VideoManager해당 비디오 플레이 {VideoPlayer_item.clip.name}|{e}");
                VideoPlayer_item.Play();
                nowVideoIndex = active_video;
                VideoPlayer_item.GetComponentInParent<AdvancedPlayer2>().VideoItem_Screen_SetVisible(true);
            }
        }
        //FullScreenWindow.instance.NowActiveVideo = active_video;
    }
    public void SetActivateVideo_OnlyPause(int videoIndex)
    {
        int active_video = 0;
        for (int e = 0; e < videoList.Length; e++)
        {
            if (e != videoIndex)
            {
                var VideoPlayer_item = videoList[e].GetComponent<VideoPlayer>();
                Debug.Log($"VideoManager[SetActivateVideo_OnlyPlay]active:{videoIndex} / {e} > activeVideoIndex가 아닌 비디오는 모두 중지 {VideoPlayer_item.clip.name}");

                VideoPlayer_item.playbackSpeed = 1.0f;
                VideoPlayer_item.SetDirectAudioVolume(0, 1.0f);
                VideoPlayer_item.Stop();
                VideoPlayer_item.GetComponentInParent<AdvancedPlayer2>().VideoItem_Screen_SetVisible(false);
            }
            if (e == videoIndex)
            {
                var VideoPlayer_item = videoList[e].GetComponent<VideoPlayer>();
                active_video = e;
                Debug.Log($"VideoManager[SetActivateVideo_OnlyPlay]active:{videoIndex} / {e} > activeVideoIndex인 비디오만 Pause {VideoPlayer_item.clip.name}");
                Debug.Log($"VideoManager해당 비디오 Pause {VideoPlayer_item.clip.name}|{e}");
                VideoPlayer_item.Play();
                VideoPlayer_item.Pause();
                nowVideoIndex = active_video;
                VideoPlayer_item.GetComponentInParent<AdvancedPlayer2>().VideoItem_Screen_SetVisible(true);
            }
        }
    }
    public void SetAllVideo_Stop()
    {
        for (int e = 0; e < videoList.Length; e++)
        {
            Debug.Log($"VideoManager[SetAllVideo_Stop] {e}");
            videoList[e].GetComponentInParent<VideoPlayer>().playbackSpeed = 1.0f;
            videoList[e].GetComponentInParent<VideoPlayer>().SetDirectAudioVolume(0, 1.0f);
            videoList[e].GetComponentInParent<VideoPlayer>().Stop();
            videoList[e].GetComponentInParent<AdvancedPlayer2>().VideoItem_Screen_SetVisible(false);
        }
    }
}
