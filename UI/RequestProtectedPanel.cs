using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:          2019/10/02 09:28:58
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/
namespace Assets.Scripts.UI
{
    public class RequestProtectedPanel : UIBase
    {
        Animation t;

        private void Awake()
        {
            Bind(UIEvent.LOAD_PANEL_HINDED);
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.LOAD_PANEL_HINDED:
                    if ((bool) message)
                    {
                        setPanelActive(true);
                        StartCoroutine(AnimatePlay());
                    }
                    else
                    {
                        StopCoroutine(AnimatePlay());
                        t?.Stop();
                        setPanelActive(false);
                    }

                    break;

                default:
                    break;
            }
        }

        void Start()
        {
            t = transform.Find("Image").GetComponent<Animation>();
            setPanelActive(false);
        }

        IEnumerator AnimatePlay()
        {
                t?.Play();
           yield return  new WaitForSeconds(2);
                t?.Stop();
                setPanelActive(false);
        }
    }

}
