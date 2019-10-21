/***
  * Title:    JionShamber 
  *
  * Created:	zzg
  *
  * CreatTime:  2019/09/23 18:45:07
  *
  * Description: 加入商会界面
  *
  * Version:    0.1
  *
  *
***/

using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuUI
{
    /// <summary>
    /// 加入商会
    /// </summary>
    public class JionShamber : UIBase
    {
        private Button ColseBtn;                        //关闭按钮
        private Button ConfirmBtn;                      //确定按钮
        private InputField InvitationCodeInput;         //邀请码输入
        private Text PromptInformation;                 //提示信息
        private GameObject JionChamberPanle;            //加入商会面版
        private GameObject IncorrectPrompt;             //邀请码不正确提示面板
        private void Awake()
        {
            Bind(UIEvent.SUCCESSFULSHAMBER_ACTIVE);
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.SUCCESSFULSHAMBER_ACTIVE:
                    setPanelActive((bool)message);
                    break;
                default:
                    break;
            }
        }
        private void Start()
        {
            JionChamberPanle = transform.Find("JionChamber").gameObject;
            IncorrectPrompt = transform.Find("IncorrectPrompt").gameObject;
            ColseBtn = JionChamberPanle.transform.Find("BtnClose").GetComponent<Button>();
            ConfirmBtn = JionChamberPanle.transform.Find("BtnConfirm").GetComponent<Button>();
            InvitationCodeInput = JionChamberPanle.transform.Find("InputField").GetComponent<InputField>();
            PromptInformation = JionChamberPanle.transform.Find("Text").GetComponent<Text>();
            ColseBtn.onClick.AddListener(clickColse);
            ConfirmBtn.onClick.AddListener(clickConfirm);
            setPanelActive(false);
        }
        /// <summary>
        /// 关闭
        /// </summary>
        private void clickColse()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            setPanelActive(false);
        }
        /// <summary>
        /// 确定按钮
        /// </summary>
        private void clickConfirm()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            //如果邀请码正确
            //if(如果邀请码正确)
            //{

            //}
            //else
            //{
            //    IncorrectPrompt.SetActive(true);
            //}
        }
    }
}
