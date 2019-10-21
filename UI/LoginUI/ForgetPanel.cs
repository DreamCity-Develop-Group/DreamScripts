using Assets.Scripts.Framework;
using Assets.Scripts.Model;
using Assets.Scripts.Net;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable InconsistentNaming

/***
* Title:     
*
* Created:	zp
*
* CreatTime:         
* Description:忘记密码
*
* Version:    0.1
*
*
***/
namespace Assets.Scripts.UI.LoginUI
{
    public class ForgetPanel : UIBase
    {
        Button btnCommit;
        Button btnReg;
        Button btnForget;
        Button btnGetIdentity;
        Button btnReturn;
        private Image getCodeImage;
        private Image ensureImage;

        InputField inputUserName;
        InputField inputPassWord;
        InputField inputIdentity;

        Text textIdentityLog;
        string username = "jx";
        string password = "123";
        string identity = "000";

        LoginInfo loginInfo;
        private void Awake()
        {
            Bind(UIEvent.Forget_ACTIVE,UIEvent.LANGUAGE_VIEW);
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.Forget_ACTIVE:
                    setPanelActive((bool)message);
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
            inputUserName = transform.Find("InputUserName").GetComponent<InputField>();
            inputPassWord = transform.Find("InputPassWord").GetComponent<InputField>();
            inputIdentity = transform.Find("InputIdentity").GetComponent<InputField>();

            btnReturn = transform.Find("BtnReturn").GetComponent<Button>();
            btnCommit = transform.Find("BtnCommit").GetComponent<Button>();
            btnReg = transform.Find("BtnReg").GetComponent<Button>();
            btnGetIdentity = transform.Find("BtnGetIdentity").GetComponent<Button>();

            getCodeImage = btnGetIdentity.GetComponent<Image>();
            ensureImage = btnCommit.GetComponent<Image>();

            btnGetIdentity.onClick.AddListener(clickGetIdentity);
            btnCommit.onClick.AddListener(clickCommit);
            btnReg.onClick.AddListener(clickReg);
            btnReturn.onClick.AddListener(clickReturn);
            loginInfo = new LoginInfo();
            setPanelActive(false);
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            btnCommit.onClick.RemoveAllListeners();
            btnGetIdentity.onClick.RemoveAllListeners();
        }
        /// <summary>
        /// /语言版本图片加载
        /// </summary>
        /// <param name="language"></param>
        private void initSource(string language)
        {
            //string language = PlayerPrefs.GetString("language");
            //string language = "chinese";
            Debug.Log(language);
            ensureImage.sprite = Resources.Load<Sprite>("UI/login/" + language + "/" + "queding");
            getCodeImage.sprite = Resources.Load<Sprite>("UI/login/" + language + "/" + "huoquyanzhengma@2x");
        }
        private void clickReturn()
        {
            setPanelActive(false);
            Dispatch(AreaCode.UI,UIEvent.LOG_ACTIVE,true);
            inputUserName.text = "";
            inputPassWord.text = "";
            inputIdentity.text = "";
        }

        private void clickGetIdentity()
        {
            Dispatch(AreaCode.NET, ReqEventType.identy, inputUserName.text);
            Debug.Log("clickGetIdentity");
        }
        private void clickReg()
        {
            setPanelActive(false);
            Dispatch(AreaCode.UI, UIEvent.REG_ACTIVE, null);
            inputUserName.text = "";
            inputPassWord.text = "";
            inputIdentity.text = "";
        }
        private void clickCommit()
        {

            username = inputUserName.text;
            password = inputPassWord.text;
            identity = inputIdentity.text;

            loginInfo.UserName = username;
            loginInfo.Password = password;
            loginInfo.Identity = identity;

            Dispatch(AreaCode.NET, ReqEventType.pwforget, loginInfo);
        }
    }
}
