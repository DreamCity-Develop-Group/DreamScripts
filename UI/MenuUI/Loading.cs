using System.Collections;
using Assets.Scripts.Framework;
using Assets.Scripts.Language;
using Assets.Scripts.Model;
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
  * CreatTime:          2019/010/10 16:42:08
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/
namespace Assets.Scripts.UI.LoginUI
{
    public class Loading : UIBase
    {
   
        Slider sliderLoading;
        private Text bottomText;
        private void Awake()
        {
            Bind(UIEvent.LOADING_ACTIVE);

        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.LOADING_ACTIVE:
                    setPanelActive(true);
                    UserInfos others = message as UserInfos;
                    bottomText.text = LanguageService.Instance.GetStringByKey(others == null ? "返回您的城市" : "前往临近城市", string.Empty);
                    StartCoroutine(LoadingHome(others));
                    break;
            }
        }
        // Start is called before the first frame update
        private void Start()
        {
            bottomText = transform.Find("Panel/bottomText").GetComponent<Text>();
            sliderLoading = transform.Find("SliderLoading").GetComponent<Slider>();
            setPanelActive(false);
        }

        IEnumerator LoadingHome(UserInfos user)
        {
            while (sliderLoading.GetComponent<Slider>().value <1)
            {
                sliderLoading.GetComponent<Slider>().value += Time.deltaTime*0.5f;
                
                yield return new WaitForEndOfFrame();
            }
            setPanelActive(false);
            sliderLoading.GetComponent<Slider>().value = 0;
            Dispatch(AreaCode.UI, UIEvent.PlayerMenu_Panel,user);
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
            //headImage.sprite = Resources.Load<Sprite>("UI/login/" + language + "/" + "HeadTitle");
        }
    }
}
