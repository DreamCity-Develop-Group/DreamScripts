
/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:  2019/09/11 11:01:10
  *
  * Description: QRcode
  *
  * Version:    0.1
  *
  *
***/

using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using System.Collections;
using System.IO;
using Assets.Scripts.Language;
using Assets.Scripts.UI.Msg;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuUI
{
    public class QRecodePanel : UIBase
    {

        private Image BG;              //充值框
        Button btnClose;
        RawImage imageQRecode;
        Texture2D image;
        private Button CopyBtn;        //复制地址按钮
        private Button DetermineBtn;   //充值确定按钮
        private Text siteText;
        private HintMsg promptMsg;
        private void Awake()
        {
            Bind(UIEvent.QRECODE_PANEL_ACTIVE);
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.QRECODE_PANEL_ACTIVE:
                    image = message as Texture2D;
                    imageQRecode.texture = image;
                    setPanelActive(true);
                    break;
                default:
                    break;
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            btnClose.onClick.RemoveAllListeners();
        }
        void Start()
        {
            promptMsg = new HintMsg();
            btnClose = transform.Find("bg/BtnClose").GetComponent<Button>();
            imageQRecode = transform.Find("bg/ImageQRecode").GetComponent<RawImage>();
            BG = transform.Find("bg").GetComponent<Image>();
            DetermineBtn = transform.Find("bg/ConfrimButton").GetComponent<Button>();
            CopyBtn= transform.Find("bg/CopyButton").GetComponent<Button>();
            siteText = transform.Find("bg/Site").GetComponent<Text>();
            CopyBtn.onClick.AddListener(() =>
            {
                if (!PermissionsRationaleDialog.IsPermitted(AndroidPermission.WRITE_EXTERNAL_STORAGE))
                {
                    PermissionsRationaleDialog.RequestPermission(AndroidPermission.WRITE_EXTERNAL_STORAGE);
                    Debug.Log("No WRITE_EXTERNAL_STORAGE Permission");
                }
#if UNITY_ANDROID
                if (!string.IsNullOrEmpty(siteText.text))
                {
                    //"com.inode.dreamcity.SaveImageActivity"
                    using (var test = new AndroidJavaObject("com.inode.dreamcity.SaveImageActivity"))
                    {
                        if (test.Call<bool>("CopyText", siteText.text))
                        {
                            promptMsg.Change(LanguageService.Instance.GetStringByKey("复制成功",string.Empty),Color.white);
                            Dispatch(AreaCode.UI,UIEvent.HINT_ACTIVE,promptMsg);
                        }
                    }
                }
#endif
            });
            btnClose.onClick.AddListener(clickClose);
            setPanelActive(false);
            Multilingual();
        }
        private void Multilingual()
        {
            string language = PlayerPrefs.GetString("language");
            //language = "chinese";
            BG.sprite = Resources.Load<Sprite>("UI/menu/" + language + "/TopUpFrame");
        }
        private void clickClose()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            setPanelActive(false);
        }
        private void clickSave()
        {
            StartCoroutine(SaveImages(image));
        }
        byte[] byteImage;
        //    /// <summary>
        //    /// 保存Png图片
        //    /// </summary>
        //    /// <param name="texture"></param>
        //    /// <returns></returns>
        IEnumerator SaveImages(Texture2D texture)
        {
            string path = Application.persistentDataPath;
            //#if UNITY_ANDROID
            //        path = "/storage/emulated/0/DCIM/DreamCity"; //设置图片保存到设备的目.
            //#endif
            //        if (!Directory.Exists(path)) 
            //            Directory.CreateDirectory(path);


            byteImage = texture.EncodeToPNG();
            string savePath = string.Format("{0}/{1}.png", path, "dreamCode");
            File.WriteAllBytes(savePath, byteImage);
            savePngAndUpdate(path);
            yield return new WaitForEndOfFrame();
        }
        public void savePngAndUpdate(string path)
        {
#if UNITY_IOS

#elif UNITY_ANDROID
            //GetAndroidJavaObject().Call("saveImage", fileName, byteImage);
            GetAndroidJavaObject().Call("testCallAndroid");
            GetAndroidJavaObject().Call("requestExternalStorage");
            GetAndroidJavaObject().Call("saveImageToGallery", path, "保存成功");
       
            Debug.Log(path);
#endif
        }
#if UNITY_ANDROID
        public AndroidJavaObject GetAndroidJavaObject()
        {
            return new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        }
#endif

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="textForEncoding"></param>
        /// <param name="logo"></param>
        /// <returns></returns>

        ///// <summary>
        ///// 二维码图片
        ///// </summary>
        ///// <param name="textForEncoding"></param>
        ///// <returns></returns>
        //public Sprite GetSprite(string textForEncoding)
        //{
        //    Texture2D logo = Resources.Load("") as Texture2D;
        //    Texture2D image = CreatQRcode(textForEncoding,logo);
        //    if(image = null)
        //    {
        //        return null;
        //    }
        //    Sprite  tempSprite = Sprite.Create(image,new Rect(0,0,image.width,image.height),new Vector2(0,0));
        //    Resources.UnloadAsset(logo);
        //    //Destroy(image);
        //    //image =null;
        //    return tempSprite;
        //}
    }
}
