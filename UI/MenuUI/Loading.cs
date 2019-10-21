using System.Collections;
using Assets.Scripts.Framework;
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
                    StartCoroutine(LoadingHome());
                    break;
            }
        }
        // Start is called before the first frame update
        private void Start()
        {
            sliderLoading = transform.Find("SliderLoading").GetComponent<Slider>();
            setPanelActive(false);
        }

        IEnumerator LoadingHome()
        {
            while (sliderLoading.GetComponent<Slider>().value <1)
            {
                sliderLoading.GetComponent<Slider>().value += Time.deltaTime;
                
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
            //string language = PlayerPrefs.GetString("language");
            //string language = "chinese";
            Debug.Log(language);
            //headImage.sprite = Resources.Load<Sprite>("UI/login/" + language + "/" + "HeadTitle");
        }
    }
}
