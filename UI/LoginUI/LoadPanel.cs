using System.Collections;
using Assets.Scripts.Framework;
using Assets.Scripts.Net;
using Assets.Scripts.Scenes;
using Assets.Scripts.Scenes.Msg;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// ReSharper disable InconsistentNaming

/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:          2019/09/20 18:42:08
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/
namespace Assets.Scripts.UI.LoginUI
{
    public class LoadPanel : UIBase
    {
   
        Slider sliderLoading;
        GameObject gameObjectLoginSelectPanel;
        Button btnRegist;
        Button btnLogin;

        private Image headImage;
       // private Image titleImage;
        private Image loginImage;
        private Image regiestImage;
        private void Awake()
        {
            Bind(UIEvent.LOAD_PANEL_ACTIVE, UIEvent.LOGINSELECT_PANEL_ACTIVE,UIEvent.LANGUAGE_VIEW);

        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.LOAD_PANEL_ACTIVE:
                    setPanelActive((bool)message);
                    if ((bool) message)
                    {
                        //test hide TODO
                        gameObjectLoginSelectPanel.SetActive(false);
                        sliderLoading.gameObject.SetActive(true);
                        StartCoroutine(Loading());
                    }
                    break;
                case UIEvent.LOGINSELECT_PANEL_ACTIVE:
                    setPanelActive(true);
                    gameObjectLoginSelectPanel.SetActive(true);
                    break;
                case UIEvent.LANGUAGE_VIEW:
                    initSource(message.ToString());
                    break;
            }
        }
        // Start is called before the first frame update
        private void Start()
        {
            gameObjectLoginSelectPanel = transform.Find("LoginSelectPanel").gameObject;
            btnLogin = transform.Find("LoginSelectPanel/BtnLogin").GetComponent<Button>();
            btnRegist = transform.Find("LoginSelectPanel/BtnRegist").GetComponent<Button>();

            headImage = transform.Find("HeadImage").GetComponent<Image>();
            loginImage = btnLogin.GetComponent<Image>();
            regiestImage = btnRegist.GetComponent<Image>();

            btnLogin.onClick.AddListener(clickLogin);
            btnRegist.onClick.AddListener(clickRegist);
            //系统字体
            //string language = Application.systemLanguage.ToString();
            //Debug.Log(language);
            PlayerPrefs.SetString("language","Chinese");

            sliderLoading = transform.Find("SliderLoading").GetComponent<Slider>();

            sliderLoading.gameObject.SetActive(false);
            //setPanelActive(false);
           
        }

        IEnumerator Loading()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("menu");
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress <0.8)
                {
                    sliderLoading.GetComponent<Slider>().value = asyncOperation.progress;
                }
                else
                {
                    sliderLoading.GetComponent<Slider>().value += Time.deltaTime * 0.5f;
                    asyncOperation.allowSceneActivation = true;
                }
                yield return new WaitForEndOfFrame();
            }
           
            setPanelActive(false);
        }
        /// <summary>
        /// /语言版本图片加载
        /// </summary/>
        /// <param name="language"></param>
        private void initSource(string language)
        {
            headImage.sprite = Resources.Load<Sprite>("UI/login/" + language + "/" + "HeadTitle");
            loginImage.sprite = Resources.Load<Sprite>("UI/login/" + language + "/" + "dengluhuang");
            regiestImage.sprite = Resources.Load<Sprite>("UI/login/" + language + "/" + "zhucelan");
        }
        private void clickLogin()
        {
            setPanelActive(false);
            Dispatch(AreaCode.UI, UIEvent.LOG_ACTIVE, true);
            Dispatch(AreaCode.NET, ReqEventType.init, null);
        }

        private void clickRegist()
        {
            setPanelActive(false);
            Dispatch(AreaCode.UI, UIEvent.REG_ACTIVE, true);
            Dispatch(AreaCode.NET, ReqEventType.init, null);
        }

    }
}
