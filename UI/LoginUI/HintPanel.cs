using System.Collections;
using Assets.Scripts.UI.Msg;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.LoginUI
{
    public class HintPanel : UIBase
    {

        Text textHint;
        private Image hintImage;
        private CanvasGroup cg;
        [SerializeField]
        [Range(0, 3)]
        private float showTime = 1f;
        private float timer = 0f;

        private void Awake()
        {
            Bind(UIEvent.HINT_ACTIVE);
        }

        protected internal override void Execute(int eventCode,  object message)
        {
            switch (eventCode)
            {
                case UIEvent.HINT_ACTIVE:
                    HintMsg msg = message as HintMsg;
                    hintMessage(msg.Text, msg.Color);
                   
                    break;
              
                default:
                    break;
            }
        }
        void Start()
        {
            hintImage = transform.Find("HintBg").GetComponent<Image>();
            hintImage.SetNativeSize();
            cg = gameObject.GetComponent<CanvasGroup>();
            textHint = transform.Find("HintBg/TextHint").GetComponent<Text>();
            cg.alpha = 0;
        }

        private void hintMessage(string text, Color color)
        {
            textHint.text = text;
            textHint.color = color;
            cg.alpha = 0;
            timer = 0;
            StartCoroutine(hintAnim());
        }


        IEnumerator hintAnim()
        {
            while (cg.alpha < 1f)
            {
                cg.alpha += Time.deltaTime * 3;
                yield return new WaitForEndOfFrame();
            }
            while (timer < showTime)
            {
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            while (cg.alpha > 0)
            {
                cg.alpha -= Time.deltaTime * 2;
                yield return new WaitForEndOfFrame();
            }
        }
 

    }
}
