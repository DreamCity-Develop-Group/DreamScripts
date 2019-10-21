using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Audio;
using UnityEngine;
/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:         
  *
  * Description:  背景音效
  *
  * Version:    0.1
  *
  *
***/
public class BgAudio : AudioBase
{
    private void Awake()
    {
        Bind(AudioEvent.PLAY_BACKGROUND_AUDIO);
    }

    protected internal override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case AudioEvent.PLAY_BACKGROUND_AUDIO:
                {
                    if ((bool)message)
                    {
                        audioSource.Play();
                    }
                    else
                    {
                        audioSource.Stop(); 
                    }

                    break;
                }
            default:
                break;
        }
    }

    /// <summary>
    /// 播放音乐的组件
    /// </summary>
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey("GameAudioIsOpen") || PlayerPrefs.GetString("GameAudioIsOpen") == "open")
        {
            audioSource.Play(); 
        }
    }

    /// <summary>
    /// 播放音乐
    /// </summary>
    private void playeEffectAudio(string assetName)
    {
        //string audioPath = "Sound/" + assetName;
        //AudioClip ac = Resources.Load<AudioClip>(audioPath);
        //PlayerPrefs.SetString("AudioPath", audioPath);
       // audioSource.clip = ac;
        audioSource.Play();
    }
    /// <summary>
    /// 停止播放音乐
    /// </summary>
    private void stopEffectAudio()
    {
        audioSource.Stop();
    }
}
