using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource introSource, loopSource;
    
    void Start()
    {
        introSource.Play();
        //指定的时间播放， AudioSettings.dspTime音频系统的当前时间
        loopSource.PlayScheduled(AudioSettings.dspTime + introSource.clip.length);
    }

    void Update()
    {
        
    }
}
