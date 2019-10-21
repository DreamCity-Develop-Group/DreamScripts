using System.Collections.Generic;
using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using Assets.Scripts.Net;
using Assets.Scripts.Tools;
using UnityEngine;
using UnityEngine.UI;

/***
* Title:     
*
* Created:	zp
*
* CreatTime:  2019/09/16 17:28:17
*
* Description: 改密码面板
*
* Version:    0.1
*
*
***/

namespace Assets.Scripts.UI.MenuUI
{
    public class ChangePwPanel : UIBase
    {
        private InputField _inputFieldMobile;
        private InputField _inputFieldVerification;
        private InputField _inputFieldCurrentPassword;
        private InputField _inputFieldNewPassword;
        private Button _btnConfirm;
        private Button _btnClose;
        private Button _btnGetVerificationCode;

        private string _currentpassword;
        private string _newpassword;
        private string _verificationcode;
        private void Awake()
        {
            Bind(UIEvent.CHANGELONG_ACTIVE,UIEvent.REG_PANEL_CODEVIEW);
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.CHANGELONG_ACTIVE:
                    setPanelActive((bool)message);
                    ClearInputRecord();
                    break;
                case UIEvent.REG_PANEL_CODEVIEW:
                    _inputFieldVerification.text = message.ToString();
                    break;
                default:
                    break;
            }
        }
        void Start()
        {
           // inputFieldMobile = transform.Find("BG/InputFieldMobile").GetComponent<InputField>();
            _inputFieldVerification= transform.Find("BG/InputFieldVerification").GetComponent<InputField>();
            _inputFieldCurrentPassword = transform.Find("BG/InputFieldCurrentPassword").GetComponent<InputField>();
            _inputFieldNewPassword = transform.Find("BG/InputFieldNewPassword").GetComponent<InputField>();
            _btnConfirm = transform.Find("BG/BtnConfirm").GetComponent<Button>();
            _btnClose = transform.Find("BG/BtnClose").GetComponent<Button>();
            _btnGetVerificationCode = transform.Find("BG/BtnGetVerificationCode").GetComponent<Button>();

            _btnGetVerificationCode.onClick.AddListener(ClickGetVerificationCode);
            _btnConfirm.onClick.AddListener(ClickConfirm);
            _btnClose.onClick.AddListener(ClickClose);
            string language = PlayerPrefs.GetString("language");
            _btnGetVerificationCode.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/GetCode");
            _btnConfirm.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/ConfirmBig");
            setPanelActive(false);
        }

        private void ClickConfirm()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            _currentpassword = _inputFieldCurrentPassword.text;
             _newpassword = _inputFieldNewPassword.text;
             _verificationcode = _inputFieldVerification.text;
            Dictionary<string,string> msg = new Dictionary<string, string>()
            {
                ["oldpw"]= _currentpassword,
                ["newpw"] = _newpassword,
                ["code"]=_verificationcode,
            };
            Dispatch(AreaCode.NET, ReqEventType.expw, msg); 
        }

        private void ClickClose()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            setPanelActive(false);
        }

        private void ClickGetVerificationCode()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.NET,ReqEventType.identy,null);
        }
        // Update is called once per frame
        void Update()
        {
            //if (_inputFieldCurrentPassword.text == null || _inputFieldNewPassword.text == null ||
            //    _inputFieldVerification.text == null)
            //{
            //    _btnConfirm.gameObject.SetActive(false);
            //}
            //else
            //{
            //    _btnConfirm.gameObject.SetActive(true);
            //}
        }
        private void ClearInputRecord()
        {
            _inputFieldVerification.text = "";
            _inputFieldCurrentPassword.text = "";
            _inputFieldNewPassword.text = "";
        }
    }
}
