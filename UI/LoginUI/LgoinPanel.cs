using Assets.Scripts.Framework;
using Assets.Scripts.Model;
using Assets.Scripts.Net;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable InconsistentNaming

namespace Assets.Scripts.UI.LoginUI
{
    public class LgoinPanel : UIBase
    {
        Button btnLogin;
        Button btnReg;
        Button btnIdentityLog;
        private Button btnSelectPassWordLog;
        Button btnForget;
        InputField inputUserName;
        InputField inputPassWord;
        InputField inputIdentity;
        Button btnGetIdentity;

        private Image headImage;
        private Image titleImage;
        private Image loginImage;
        private Image getIndentityImage;


        Text textIdentityLog;
        string username ;
        string password ;
        string identity ;
        private string code;

        bool isLogIdentity = false;
        LoginInfo loginInfo;
        private void Awake()
        {
            Bind(UIEvent.LOG_ACTIVE,UIEvent.LANGUAGE_VIEW, UIEvent.REG_PANEL_CODEVIEW);
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.LOG_ACTIVE:
                    setPanelActive((bool)message);                
                    break;
                case UIEvent.LANGUAGE_VIEW:
                    initSource(message.ToString());
                    break;
                case UIEvent.REG_PANEL_CODEVIEW:
                    inputIdentity.text = message.ToString();
                    break;
                //case 
                default:
                    break;
            }
        }
        void Start()
        {
            inputUserName = transform.Find("InputUserName").GetComponent<InputField>();
            inputPassWord = transform.Find("InputPassWord").GetComponent<InputField>();
            inputIdentity = transform.Find("InputIdentity").GetComponent<InputField>();

            btnSelectPassWordLog = transform.Find("BtnPassWordLogin").GetComponent<Button>();

            btnForget = transform.Find("BtnForget").GetComponent<Button>();
            btnLogin = transform.Find("BtnLogin").GetComponent<Button>();
            btnReg = transform.Find("BtnReg").GetComponent<Button>();
            btnIdentityLog = transform.Find("BtnIdentityLog").GetComponent<Button>();
            btnGetIdentity = transform.Find("BtnGetIdentity").GetComponent<Button>();
            textIdentityLog = btnIdentityLog.GetComponent<Text>();

            btnIdentityLog.onClick.AddListener(clickIdentityLog);
            btnGetIdentity.onClick.AddListener(clickGetIdentity);
            btnLogin.onClick.AddListener(clickLogin);
            btnReg.onClick.AddListener(clickReg);
            btnSelectPassWordLog.onClick.AddListener(clickSelectPassWordLogin);
            btnForget.onClick.AddListener(clickForget);

            getIndentityImage = btnGetIdentity.GetComponent<Image>();;
            headImage = transform.Find("HeadImage").GetComponent<Image>();
            loginImage = btnLogin.GetComponent<Image>();
            //inputUserName.GetComponent<LanguageText>().Key =LanguageService.Instance.GetStringByKey("UILogin.InputUserName", string.Empty);

            btnSelectPassWordLog.gameObject.SetActive(false);
            btnGetIdentity.gameObject.SetActive(false);
            inputIdentity.gameObject.SetActive(false);
            loginInfo = new LoginInfo();
            setPanelActive(false);
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            btnLogin.onClick.RemoveAllListeners();
            btnGetIdentity.onClick.RemoveAllListeners();
            btnIdentityLog.onClick.RemoveAllListeners();
        }
        /// <summary>
        /// /语言版本图片加载
        /// </summary/>
        /// <param name="language"></param>
        private void initSource(string language)
        {
            //string language = PlayerPrefs.GetString("language");
            //string language = "chinese";
            Debug.Log(language);
            headImage.sprite = Resources.Load<Sprite>("UI/login/" + language + "/" + "HeadTitle");
            loginImage.sprite = Resources.Load<Sprite>("UI/login/" + language + "/" + "dengluhuang");
            getIndentityImage.sprite = Resources.Load<Sprite>("UI/login/" + language + "/" + "huoquyanzhengma@2x");
        }
        private void clickForget()
        {
            Dispatch(AreaCode.UI, UIEvent.Forget_ACTIVE, true);
            Dispatch(AreaCode.UI, UIEvent.LOG_ACTIVE, false);
            Debug.Log("clickGetIdentity");
        }
        private void clickGetIdentity()
        {
            username = inputUserName.text;
            Dispatch(AreaCode.NET, ReqEventType.identy,username);
            Debug.Log("clickGetIdentity");
        }

        private void clickSelectPassWordLogin()
        {
            if(isLogIdentity)
            {
                btnIdentityLog.gameObject.SetActive(true);
                btnSelectPassWordLog.gameObject.SetActive(false);
                btnForget.gameObject.SetActive(true);
                inputPassWord.gameObject.SetActive(true);
                inputIdentity.gameObject.SetActive(false);
                btnGetIdentity.gameObject.SetActive(false);
                isLogIdentity = !isLogIdentity;
                inputIdentity.text = "";
            }
        }
        private void clickIdentityLog()
        {
            if (!isLogIdentity)
            {
                btnIdentityLog.gameObject.SetActive(false);
                btnSelectPassWordLog.gameObject.SetActive(true);
                btnForget.gameObject.SetActive(false);
                inputPassWord.gameObject.SetActive(false);
                inputIdentity.gameObject.SetActive(true);
                btnGetIdentity.gameObject.SetActive(true);
                isLogIdentity = !isLogIdentity;
                inputPassWord.text = "";
            }
          

        }
        private void clickReg()
        {
            setPanelActive(false);
            Dispatch(AreaCode.UI, UIEvent.REG_ACTIVE, true);
            inputUserName.text = "";
            inputPassWord.text = "";
            inputIdentity.text = "";
        }
        private void clickLogin()
        {
            //testTODO调试用
            // Dispatch(AreaCode.SCENE, SceneEvent.MENU_PLAY_SCENE, new SceneMsg("menu", null));
            username = inputUserName.text;
            loginInfo.UserName = username;

            if (isLogIdentity)
            {
                identity = inputIdentity.text;
                loginInfo.Password = identity;
                Dispatch(AreaCode.NET, ReqEventType.idlogin, loginInfo);
            }
            else
            {
                password = inputPassWord.text;
                loginInfo.Password = password;
                Dispatch(AreaCode.NET, ReqEventType.pwlogin, loginInfo);
            }
        }
    }
}
