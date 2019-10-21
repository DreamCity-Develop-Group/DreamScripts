using System;
using Assets.Scripts.Framework;
using Assets.Scripts.Language;
using Assets.Scripts.Net;
using Assets.Scripts.Scenes;
using Assets.Scripts.Scenes.Msg;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

/***
* Title:     
*
* Created:	zp
*
* CreatTime:          2019/09/20 13:33:56
*
* Description:
*
* Version:    0.1
*
*
***/
namespace Assets.Scripts.UI.LoginUI
{
    public class SelectLanguage : UIBase
    {
        Button btnSelectLanguage;
        Button btnEnglish;
        Button btnChinese;
        Button btnKorean;
        //bool isSelect;

        private Transform Language;
        //test
        Button btnConfim;
        InputField testInputIp;
        private Text test;
        private Button btntestButton;
        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                default:
                    break;
            }
        }
        void Awake()
        {
            LanguageService.Instance.Language = new LanguageInfo("Chinese");
            CacheData.Instance().language = "Chinese";
            //LanguageService.Instance.Language = new LanguageInfo("Chinese");
            PlayerPrefs.SetString("language", "Chinese");
            //Debug.Log("slf3");
            //Dispatch(AreaCode.UI, UIEvent.LANGUAGE_VIEW, "Chinese");
        }
        void Start()
        {
            btnSelectLanguage = transform.Find("BtnSelectLanguage").GetComponent<Button>();
            Language = transform.Find("BtnSelectLanguage/Language");
            btnChinese = Language.Find("BtnChinese").GetComponent<Button>();
            btnEnglish = Language.Find("BtnEnglish").GetComponent<Button>();
            btnKorean = Language.Find("BtnKorean").GetComponent<Button>();

            //test***********************************
            //test = transform.Find("Test/Text").GetComponent<Text>();
            //btntestButton = transform.Find("Button").GetComponent<Button>();
            //btntestButton.onClick.AddListener(() =>
            //{
            //    //test
            //    Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_ACTIVE, true);
            //   // Dispatch(AreaCode.NET,ReqEventType.login,null);
            //});
            btnConfim = transform.Find("Test/TestInput/BtnIp").GetComponent<Button>();
            testInputIp = transform.Find("Test/TestInput").GetComponent<InputField>();
            btnConfim.onClick.AddListener(clickIpConfim);
            //**************************************

            btnEnglish.onClick.AddListener(clickEnglish);
            btnChinese.onClick.AddListener(clickChinese);
            btnKorean.onClick.AddListener(clickKorean);
            GameObject textPrefab = (GameObject)Resources.Load("Text");
            //GameObject textObj = (GameObject)Instantiate(textPrefab);
            //textObj.transform.SetParent(this.transform);
            //textObj.transform.localPosition = Vector3.zero;
            Language.gameObject.SetActive(false);
            btnSelectLanguage.onClick.AddListener(clickSelectLanguage);
            if (SceneManager.GetActiveScene().isLoaded)
            {
                PlayerPrefs.SetString("language", "Chinese");
                Debug.Log("slf3");
                Dispatch(AreaCode.UI, UIEvent.LANGUAGE_VIEW, "Chinese");
            }
           
        }
        private void clickIpConfim()
        {
            WebData.address = testInputIp.text;
            Dispatch(AreaCode.NET, ReqEventType.init, null);
        }

        /// <summary>
        /// 核对手机系统语言自动选择默认语言
        /// </summary>
        private void CheckLanaguage()
        {
            string language = Application.systemLanguage.ToString();
       
            bool containChinese = language.IndexOf("Chinese", StringComparison.OrdinalIgnoreCase) >= 0;
            if (containChinese)
            {
                clickChinese();
            }
            bool containEnglish = language.IndexOf("English", StringComparison.OrdinalIgnoreCase) >= 0;
            if (containEnglish)
            {
                clickEnglish();
            }
            bool containKorean = language.IndexOf("Korean", StringComparison.OrdinalIgnoreCase) >= 0;
            if (containKorean)
            {
                clickKorean();
            }
        }


        void clickSelectLanguage()
        {
            Language.gameObject.SetActive(true);
            //if (!isSelect)
            //{
            //    Language.gameObject.SetActive(true);
            //    btnChinese.gameObject.SetActive(true);
            //    btnEnglish.gameObject.SetActive(true);
            //    btnKorean.gameObject.SetActive(true);
            //    isSelect = !isSelect;
            //}
            //else
            //{
            //    btnChinese.gameObject.SetActive(false);
            //    btnEnglish.gameObject.SetActive(false);
            //    btnKorean.gameObject.SetActive(false);
            //    isSelect = !isSelect;
            //}

        }

        void clickEnglish()
        {
            Language.gameObject.SetActive(false);
            LanguageService.Instance.Language = new LanguageInfo("English");
            CacheData.Instance().language = "English";
            PlayerPrefs.SetString("language", "English");
            Dispatch(AreaCode.UI, UIEvent.LANGUAGE_VIEW, "English");
        }
        void clickChinese()
        {
            Language.gameObject.SetActive(false);
            LanguageService.Instance.Language = new LanguageInfo("Chinese");
            CacheData.Instance().language = "Chinese";
            PlayerPrefs.SetString("language", "Chinese");
            Dispatch(AreaCode.UI, UIEvent.LANGUAGE_VIEW, "Chinese");
        }
        void clickKorean()
        {
            Language.gameObject.SetActive(false);
            LanguageService.Instance.Language = new LanguageInfo("Korean");
            CacheData.Instance().language = "Korean";
            PlayerPrefs.SetString("language", "Korean");
            Dispatch(AreaCode.UI, UIEvent.LANGUAGE_VIEW, "Korean");
        }
    }
}
