using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlayerItem : MonoBehaviour
{
    [SerializeField] private int videoIndex;

    public int VideoIndex
    {
        get
        {
            return videoIndex;
        }
    }
    void Start()
    {        
    }

}
