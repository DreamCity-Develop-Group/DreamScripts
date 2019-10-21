using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Audio
{

    public class EffectAudio : AudioBase
    {
        private Dictionary<string,AudioClip> audioClips;
        private void Awake()
        {
            Bind(AudioEvent.PLAY_CLICK_AUDIO,AudioEvent.LIKE_CLICK_AUDIO,AudioEvent.COMMERCE_PROMPT_AUDIO,AudioEvent.EXACTABLE_AUDIO);
            audioSource = this.transform.GetComponent<AudioSource>();
        }

        protected internal override void Execute(int eventCode, object message)
        {
            if (!PlayerPrefs.HasKey("GameAudioIsOpen") || PlayerPrefs.GetString("GameAudioIsOpen") == "open")
            {

                switch (eventCode)
                {
                    case AudioEvent.PLAY_CLICK_AUDIO:
                        {
                            playeEffectAudio("ClickVoice");
                            break;
                        }
                    case AudioEvent.LIKE_CLICK_AUDIO:
                        {
                            playeEffectAudio("LikeVoice");
                            break;
                        }
                    case AudioEvent.COMMERCE_PROMPT_AUDIO:
                        {
                            playeEffectAudio("CommercePrompt");
                            break;
                        }
                    case AudioEvent.EXACTABLE_AUDIO:
                        {
                            playeEffectAudio("ExactableVoice");
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 播放音乐的组件
        /// </summary>
        private AudioSource audioSource;


        /// <summary>
        /// 播放音乐
        /// </summary>
        private void playeEffectAudio(string assetName)
        {
            string audioPath = "Sound/" + assetName;
            AudioClip ac = Resources.Load<AudioClip>(audioPath);
           // PlayerPrefs.SetString("AudioPath",audioPath);
            audioSource.clip = ac;
            audioSource.Play();
        }
    }
}
