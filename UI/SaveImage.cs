// /************************************************************
// *                                                           *
// *        zhupeng                             *
// *                                                           *
// *   Created 2019 by inode Games                         *
// *                                                           *
// *   bitbendergames@gmail.com                                *
// *                                                           *
// ************************************************************/

using System.Collections;
using System.IO;
using Assets.Scripts.Framework;
using Assets.Scripts.Language;
using Assets.Scripts.UI.Msg;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/***
* Title:     
*
* Created:	zp
*
* CreatTime:          2019/10/02 10:13:39
*
* Description:
*
* Version:    0.1
*
*
***/
namespace Assets.Scripts.UI
{
    public class SaveImage : UIBase, IPointerClickHandler, IPointerExitHandler, IPointerDownHandler
    {
        public UnityEvent onLongPress = new UnityEvent();
        private float holdTime = 1f;
        private HintMsg promptMsg = new HintMsg();

        public void OnPointerClick(PointerEventData eventData)
        {
            //print("I was clicked:" + eventData.pointerCurrentRaycast.gameObject.name);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CancelInvoke("OnLongPress");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // this.position = eventData.position;
            // menu.pivot = new Vector2(eventData.position.x/Screen.width, eventData.position.y/Screen.height);
            // menu.gameObject.SetActive(false);
            Invoke("OnLongPress", holdTime);
        }

        private void OnLongPress()
        {
            Texture2D posTexture2D = GetComponent<RawImage>().texture as Texture2D;
            if (!PermissionsRationaleDialog.IsPermitted(AndroidPermission.WRITE_EXTERNAL_STORAGE))
            {
                PermissionsRationaleDialog.RequestPermission(AndroidPermission.WRITE_EXTERNAL_STORAGE);
                Debug.Log("No WRITE_EXTERNAL_STORAGE Permission");
            }
            if (gameObject.name.Equals("ImageQRecode"))
            {
                StartCoroutine(SaveImages(posTexture2D,"chargeQR"));
                return;
            }
            int width = (int)GetComponent<RectTransform>().rect.width;
            int height = (int)GetComponent<RectTransform>().rect.height;
            StartCoroutine(ScreenShotPNG( width,  height));
        }

        IEnumerator ScreenShotPNG(int width,int height)
        {
            yield return new WaitForEndOfFrame();
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect((Screen.width/2-width/2), 0, Screen.width, Screen.height), 0, 0);
            tex.Apply();
            byte[] bytex = tex.EncodeToPNG();
            StartCoroutine(SaveImages(tex,"shareQr"));
            Destroy(tex);
          //  promptMsg.Change(LanguageService.Instance.GetStringByKey("保存成功", string.Empty), Color.white);
            //Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
            gameObject.SetActive(false);
            transform.parent.gameObject.SetActive(false);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            CancelInvoke("OnLongPress");
        }

        byte[] byteImage;

        //    /// <summary>
        //    /// 保存Png图片
        //    /// </summary>
        //    /// <param name="texture"></param>
        //    /// <returns></returns>
        IEnumerator SaveImages(Texture2D texture,string name)
        {
            string path = Application.persistentDataPath;
            Debug.Log(path);
#if UNITY_ANDROID
            path ="/storage/emulated/0/DCIM/DreamCity"; //设置图片保存到设备的目.
#endif
            Debug.Log(path);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string savePath = $"{path}/{name}.png";
            byte[] bytex = texture.EncodeToPNG();
            File.WriteAllBytes(savePath, bytex);
            if (savePngAndUpdate(path))
            {
                promptMsg.Change(LanguageService.Instance.GetStringByKey("保存成功", string.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
            }
            yield return new WaitForEndOfFrame();
        }

        private bool savePngAndUpdate(string path)
        {

#if UNITY_IOS

#elif UNITY_ANDROID
            //GetAndroidJavaObject().Call("saveImage", fileName, byteImage);
            //GetAndroidJavaObject().Call("testCallAndroid");
            //GetAndroidJavaObject().Call("requestExternalStorage");
            using (var test = new AndroidJavaObject("com.inode.dreamcity.SaveImageActivity"))
            {
                return test.Call<bool>("scanFile",path, "保存成功");
            }
#endif
        }
//#if UNITY_ANDROID
//        public AndroidJavaObject GetAndroidJavaObject()
//        {
           
           
//        }
//#endif

    }
}
