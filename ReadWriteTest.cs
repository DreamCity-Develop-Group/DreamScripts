using Assets.Scripts.UI;
using Assets.Scripts.UI.MenuUI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
#if PLATFORM_ANDROID

#endif

namespace Assets.Scripts
{
    public class ReadWriteTest : UIBase
    {
        private Button agreeButton;
        private Button disagreeButton;
        
        void Awake()
        {
#if PLATFORM_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            }
#endif
        }

        void Start()
        {
            agreeButton = transform.Find("Image/Agree").GetComponent<Button>();
            disagreeButton = transform.Find("Image/DisAgree").GetComponent<Button>();
            setPanelActive(false);
#if PLATFORM_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                // The user denied permission to use the microphone.
                // Display a message explaining why you need it with Yes/No buttons.
                // If the user says yes then present the request again
                // Display a dialog here
                setPanelActive(true);
            }
            else
            {
                setPanelActive(false);
            }
            agreeButton.onClick.AddListener(() =>
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
                setPanelActive(false);
            });
            disagreeButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
#endif
        }

        

    }
}