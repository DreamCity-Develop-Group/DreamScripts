using Assets.Scripts.Framework;
using Assets.Scripts.Language;
using Assets.Scripts.Model;
using Assets.Scripts.Net;
using Assets.Scripts.UI.Msg;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.LoginUI
{
    public class RegistPanel : UIBase
    {
        Button btnAgreement;
        Toggle togAgreement;
        Button btnRegist;
        Button btnIdentify;
        Button btnReturn;
        Button btnLogin;
        InputField inputIdentify;
        InputField inputUserName;
        InputField inputPassWord;
        InputField inputNickName;
        InputField inputInviteCode;
        private Image getCodeImage;
        private Image registImage;
        private Toggle toggle;

        string phone;
        string identify;
        string passWord;
        string nickName;
        string inviteCode;
        //InputField inputRePassWord;
        private void Awake()
        {
            Bind(UIEvent.REG_ACTIVE, UIEvent.REG_PANEL_CODEVIEW,UIEvent.LANGUAGE_VIEW);
       
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.REG_ACTIVE:
                    setPanelActive((bool)message);
                    break;
                case UIEvent.REG_PANEL_CODEVIEW:
                    inputIdentify.text = message.ToString();
                    break;
                case UIEvent.LANGUAGE_VIEW:
                    initSource(message.ToString());
                    break;
                default:
                    break;
            }
        }

        void Start()
        {
            btnAgreement = transform.Find("AgreementButton").GetComponent<Button>();
            togAgreement = transform.Find("AgreeMent").GetComponent<Toggle>();
            togAgreement.isOn = false;
            btnLogin = transform.Find("BtnLogin").GetComponent<Button>();
            btnIdentify = transform.Find("BtnIdentify").GetComponent<Button>();
            btnRegist = transform.Find("BtnRegist").GetComponent<Button>();
            btnReturn = transform.Find("BtnReturn").GetComponent<Button>();
            inputUserName = transform.Find("InputUserName").GetComponent<InputField>();
            inputIdentify = transform.Find("InputIdentify").GetComponent<InputField>(); 
            inputPassWord = transform.Find("InputPassWord").GetComponent<InputField>();
            inputNickName = transform.Find("InputNickName").GetComponent<InputField>();
            inputInviteCode = transform.Find("InputInviteCode").GetComponent<InputField>();

            
            getCodeImage = btnIdentify.GetComponent<Image>();
            registImage = btnRegist.GetComponent<Image>();

            btnLogin.onClick.AddListener(clickLogin);
            btnIdentify.onClick.AddListener(clickIdentify);
            btnRegist.onClick.AddListener(clickRegist);
            btnReturn.onClick.AddListener(clickReturn);
            btnAgreement.onClick.AddListener(clickAgreement);
            btnRegist.enabled = false;
            setPanelActive(false);
        }
        private void initSource(string language)
        {
            //string language = PlayerPrefs.GetString("language");
            //string language = "chinese";
            Debug.Log(language);
            getCodeImage.sprite = Resources.Load<Sprite>("UI/login/" + language + "/" + "huoquyanzhengma@2x");
            registImage.sprite = Resources.Load<Sprite>("UI/login/" + language + "/" + "zhucelv");
        }
        private void clickLogin()
        {
            setPanelActive(false);
            Dispatch(AreaCode.UI,UIEvent.LOG_ACTIVE,true);
            inputPassWord.text = "";
            inputNickName.text = "";
            inputUserName.text = "";
            inputIdentify.text = "";
            inputInviteCode.text = "";
        }
        private void clickIdentify()
        {
            btnRegist.enabled = true;
            phone = inputUserName.text;
            PlayerPrefs.SetString("username",phone);
            Dispatch(AreaCode.NET, ReqEventType.identy, phone);
            Debug.Log("clickIdentify");
        }
        private void clickReturn()
        {
            setPanelActive(false);
            Dispatch(AreaCode.UI,UIEvent.LOG_ACTIVE,true);
            inputPassWord.text = "";
            inputNickName.text = "";
            inputUserName.text = "";
            inputIdentify.text = "";
            inputInviteCode.text = "";
        }
        private void clickRegist()
        {
            phone=inputUserName.text;
            passWord = inputPassWord.text;
            inviteCode = inputInviteCode.text;
            nickName = inputNickName.text;
            identify = inputIdentify.text;
            UserInfo userinfo = new UserInfo(phone,passWord,identify,inviteCode,nickName);
            if(togAgreement.isOn)
            {
                Dispatch(AreaCode.NET, ReqEventType.regist, userinfo);
            }
            else
            {

                HintMsg hintMsg = new HintMsg();
                hintMsg.Change(LanguageService.Instance.GetStringByKey("–≠“È", String.Empty), Color.white);
                Dispatch(AreaCode.UI,UIEvent.HINT_ACTIVE, hintMsg);
            }  
        }
        private void clickAgreement()
        {
            Application.OpenURL("http://156.236.69.200/index.html");
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            btnIdentify.onClick.RemoveAllListeners();
            btnRegist.onClick.RemoveAllListeners();
            btnReturn.onClick.RemoveAllListeners();
        }
    }
}
