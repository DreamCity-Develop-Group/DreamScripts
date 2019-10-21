using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Language;
using Assets.Scripts.Model;
using Assets.Scripts.Net;
using Assets.Scripts.UI.Msg;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    /// <summary>
    /// 好友点赞面板
    /// </summary>
    public class GiveALikePanel : UIBase
    {
        private GameObject GvieLike0;                    //第一类点赞
        private GameObject GvieLike1;                    //第二类点赞
        private GameObject GvieLike2;                    //第三类点赞
        private GameObject GvieLike3;                    //第四类点赞
        private GameObject GvieLike4;                    //第五类点赞
        private GameObject GvieLike5;                    //第六类点赞
        private GameObject GvieLike6;                    //第七类点赞

        private Button likeBtn0;                         //点赞一的按钮
        private Button likeBtn1;                         //点赞一的按钮
        private Button likeBtn2;                         //点赞一的按钮
        private Button likeBtn3;                         //点赞一的按钮
        private Button likeBtn4;                         //点赞一的按钮
        private Button likeBtn5;                         //点赞一的按钮
        private Button likeBtn6;                         //点赞一的按钮
        private bool[] randomArray=new bool[6];
        private GameObject[] GvieLikes;
        /// <summary>
        /// 点赞方id
        /// </summary>
        private string friendId;
        private void Awake()
        {
            Bind(UIEvent.THUMBUP_PANEL_ACTVATE);

        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.THUMBUP_PANEL_ACTVATE:
                    Dispatch(AreaCode.NET,UIEvent.LOADING_ACTIVE,true);
                    setPanelActive(true);
                    InvestList msg = message  as InvestList;
                    friendId = msg.playerId;
                    foreach (var item in msg.investList)
                    {
                        int random = Random.Range(0, 7);
                        if (!randomArray[random])
                        {
                            randomArray[random] = true;
                            GvieLikes[random].SetActive(true);
                        }
                        else
                        {
                            for (int i = randomArray.Length - 1; i >= 0; i--)
                            {
                                if (!randomArray[i])
                                {
                                    GvieLikes[random].SetActive(true);
                                    break;
                                }
                            }
                        }
                    }
                    //置空
                    for (int i = 0; i < randomArray.Length; i++)
                    {
                        randomArray[i] = false;
                    }
                    break;              
                default:
                    break;
            }
        }


        private void Start()
        {
            GvieLike0 = transform.Find("GiveALike0").gameObject;
            GvieLike1 = transform.Find("GiveALike1").gameObject;
            GvieLike2 = transform.Find("GiveALike2").gameObject;
            GvieLike3 = transform.Find("GiveALike3").gameObject;
            GvieLike4 = transform.Find("GiveALike4").gameObject;
            GvieLike5 = transform.Find("GiveALike5").gameObject;
            GvieLike6 = transform.Find("GiveALike6").gameObject;
            likeBtn0 = GvieLike0.GetComponent<Button>();
            likeBtn1 = GvieLike1.GetComponent<Button>();
            likeBtn2 = GvieLike2.GetComponent<Button>();
            likeBtn3 = GvieLike3.GetComponent<Button>();
            likeBtn4 = GvieLike4.GetComponent<Button>();
            likeBtn5 = GvieLike5.GetComponent<Button>();
            likeBtn6 = GvieLike6.GetComponent<Button>();
            GvieLike0.SetActive(false);
            GvieLike1.SetActive(false);
            GvieLike2.SetActive(false);
            GvieLike3.SetActive(false);
            GvieLike4.SetActive(false);
            GvieLike5.SetActive(false);
            GvieLike6.SetActive(false);
            GvieLikes =new GameObject[]
            {
                GvieLike0,GvieLike1,GvieLike2,GvieLike3,GvieLike4,GvieLike5,GvieLike6
            };
            likeBtn0.onClick.AddListener(()=>clickLike0());
            likeBtn1.onClick.AddListener(clickLike1);
            likeBtn2.onClick.AddListener(clickLike2);
            likeBtn3.onClick.AddListener(clickLike3);
            likeBtn4.onClick.AddListener(clickLike4);
            likeBtn5.onClick.AddListener(clickLike5);
            likeBtn6.onClick.AddListener(clickLike6);
            setPanelActive(false);
        }

        HintMsg promptMsg = new HintMsg();
        /// <summary>
        /// 第一类物业点赞
        /// </summary>
        private void clickLike0()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.NET,ReqEventType.GetLike, friendId);
            promptMsg.Change(LanguageService.Instance.GetStringByKey("点赞成功",string.Empty),Color.white);
            GvieLike0.SetActive(false);
        }
        /// <summary>
        /// 第二类物业点赞
        /// </summary>
        private void clickLike1()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.NET, ReqEventType.GetLike, friendId);
            promptMsg.Change(LanguageService.Instance.GetStringByKey("点赞成功", string.Empty), Color.white);
            GvieLike0.SetActive(false);
        }
        /// <summary>
        /// 第三类物业点赞
        /// </summary>
        private void clickLike2()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.NET, ReqEventType.GetLike, friendId);
            promptMsg.Change(LanguageService.Instance.GetStringByKey("点赞成功", string.Empty), Color.white);
            GvieLike0.SetActive(false);
        }
        /// <summary>
        /// 第四类物业点赞
        /// </summary>
        private void clickLike3()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.NET, ReqEventType.GetLike, friendId);
            promptMsg.Change(LanguageService.Instance.GetStringByKey("点赞成功", string.Empty), Color.white);
            GvieLike0.SetActive(false);
        }
        /// <summary>
        /// 第五类物业点赞
        /// </summary>
        private void clickLike4()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.NET, ReqEventType.GetLike, friendId);
            promptMsg.Change(LanguageService.Instance.GetStringByKey("点赞成功", string.Empty), Color.white);
            GvieLike0.SetActive(false);
        }
        /// <summary>
        /// 第六类物业点赞
        /// </summary>
        private void clickLike5()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.NET, ReqEventType.GetLike, friendId);
            promptMsg.Change(LanguageService.Instance.GetStringByKey("点赞成功", string.Empty), Color.white);
            GvieLike0.SetActive(false);
        }
        /// <summary>
        /// 第七类物业点赞
        /// </summary>
        private void clickLike6()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.NET, ReqEventType.GetLike, friendId);
            promptMsg.Change(LanguageService.Instance.GetStringByKey("点赞成功", string.Empty), Color.white);
            GvieLike0.SetActive(false);
        }
    }
}
