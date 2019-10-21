using System;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Language
{
	[RequireComponent(typeof (Text))]
	[AddComponentMenu("Language/LanguageText")]

	public class LanguageText : UIBase {
		 public string Language;
		 public string File;
		 public string Key;
		 public string Value;
		public LanguageService Localization;
        // Use this for initialization

        private void Awake()
        {
            Bind(UIEvent.LANGUAGE_VIEW);
        }

        private void Start()
        {
            
        }
        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.LANGUAGE_VIEW:
                     Language = message.ToString();
                     SetTextLanguage(Language);
                    break;
                default:
                    break;
            }
        }



        private void SetTextLanguage(string language)
        {
            Localization = LanguageService.Instance;
            Text labelText = GetComponent<Text>();
            GetComponent<Text>().text = Localization.GetFromFile(File, Key, labelText.text);
            string languagePath = "Localization/" + Language;
            Font t = Resources.Load<Font>(languagePath);

            GetComponent<Text>().font = t;
          //  Language = language;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

  //      void Start()
		//{//LightmapsModeLegacy 
		//	//Localization = LanguageService.Instance;
		//	//var label = GetComponent<Text>();
		//	//GetComponent<Text>().text = Localization.GetFromFile(File, Key, label.text);
		//}

	}
}

