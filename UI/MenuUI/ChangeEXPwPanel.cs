/***
  * Title:     
  *
  * Created:	zzg
  *
  * CreatTime:  2019/09/20 15:32:00
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/

using System.Collections.Generic;
using Assets.Scripts.Framework;
using Assets.Scripts.Net;
using UnityEngine.UI;
using UnityEngine;
using Assets.Scripts.Audio;

namespace Assets.Scripts.UI.MenuUI
{
    /// <summary>
    /// 修改交易密码
    /// </summary>
    public class ChangeEXPwPanel : UIBase
    {

        private InputField inputFieldMobile;
        private InputField inputFieldVerification;
        private InputField inputFieldCurrentPassword;
        private InputField inputFieldNewPassword;
        private Button btnConfirm;
        private Button btnClose;
        private Button btnGetVerificationCode;

        private string currentpassword;
        private string newpassword;
        private string verificationcode;
        private void Awake()
        {
            Bind(UIEvent.CHANGETRADE_ACTIVE, UIEvent.REG_PANEL_CODEVIEW);
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.CHANGETRADE_ACTIVE:
                    setPanelActive((bool)message);
                    inputFieldVerification.text = "";
                    inputFieldCurrentPassword.text = "";
                    inputFieldNewPassword.text = "";
                    break;
                case UIEvent.REG_PANEL_CODEVIEW:
                    inputFieldVerification.text = message.ToString();
                    break;
                default:
                    break;
            }
        }
        void Start()
        {
            
            inputFieldMobile = transform.Find("BG/InputFieldMobile").GetComponent<InputField>();
            inputFieldVerification = transform.Find("BG/InputFieldVerification").GetComponent<InputField>();
            inputFieldCurrentPassword = transform.Find("BG/InputFieldCurrentPassword").GetComponent<InputField>();
            inputFieldNewPassword = transform.Find("BG/InputFieldNewPassword").GetComponent<InputField>();
            btnConfirm = transform.Find("BG/BtnConfirm").GetComponent<Button>();
            btnClose = transform.Find("BG/BtnClose").GetComponent<Button>();
            btnGetVerificationCode = transform.Find("BG/BtnGetVerificationCode").GetComponent<Button>();
            string language = PlayerPrefs.GetString("language");
            btnGetVerificationCode.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/GetCode");
            btnConfirm.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/ConfirmBig");
            btnGetVerificationCode.onClick.AddListener(clickGetVerificationCode);
            btnConfirm.onClick.AddListener(clickConfirm);
            btnClose.onClick.AddListener(clickClose);
            inputFieldVerification.text = null;
            setPanelActive(false);
        }

        private void clickConfirm()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            currentpassword = inputFieldCurrentPassword.text;
            newpassword = inputFieldNewPassword.text;
            verificationcode = inputFieldVerification.text;
            Dictionary<string, string> msg = new Dictionary<string, string>()
            {
                ["oldpwshop"] = currentpassword,
                ["newpwshop"] = newpassword,
                ["code"] = verificationcode

            };
            Dispatch(AreaCode.NET,ReqEventType.change_expwshop,msg);
           // Dispatch(AreaCode.NET, ReqEventType.change_expwshop, msg);
        }

        private void clickClose()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            setPanelActive(false);
        }

        private void clickGetVerificationCode()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.NET, ReqEventType.identy, null);
        }
        // Update is called once per frame
        void Update()
        {
            if (inputFieldCurrentPassword.text == null || inputFieldNewPassword.text == null ||
                inputFieldVerification.text == null)
            {
                btnConfirm.gameObject.SetActive(false);
            }
            else
            {
                btnConfirm.gameObject.SetActive(true);
            }
        }
    }
}
