using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Model;
using Assets.Scripts.Net;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class EarningsPanel : UIBase
    {
        private Button[] EarningsBtn = new Button[7];
        private GameObject[] Earning = new GameObject[7];
        private List<InvestInfo> investList = new List<InvestInfo>();
        private void Awake()
        {
            Bind(UIEvent.EARNINGS_PANEL_ACTVTE);

        }
        //int[] investList=new int[];
        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.EARNINGS_PANEL_ACTVTE:
                    setPanelActive(true);
                    investList = message as List<InvestInfo>;
                    if (investList != null)
                        for (int i = 0; i < investList.Count; i++)
                        {
                            if (investList[i].state == 703)
                            {
                                int inType = investList[i].investId;
                                Earning[i].SetActive(true);
                                Earning[i].GetComponent<Button>().onClick.AddListener(() =>
                                {
                                    Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
                                    EarningsBtn[0].gameObject.SetActive(false);
                                    Dispatch(AreaCode.UI, UIEvent.IVEST_PANEL_ACTIVE, inType);
                                });
                            }
                        }

                    break;
                case UIEvent.Updata_Earnings_Panel:
                    //setPanelActive(true);
                   
                    //Earning[].SetActive(true);
                    //Earning[i].GetComponent<Button>().onClick.AddListener(() =>
                    //{
                    //    Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
                    //    EarningsBtn[0].gameObject.SetActive(false);
                    //    Dispatch(AreaCode.UI, UIEvent.IVEST_PANEL_ACTIVE, inType);
                    //});
                    break;
                default:
                    break;
            }
        }
        private void Start()
        {
            for (int i = 0; i < Earning.Length; i++)
            {
                Earning[i] = transform.Find("Earnings" + i).gameObject;
                EarningsBtn[i] = Earning[i].GetComponent<Button>();
                Earning[i].SetActive(false);
            }
            setPanelActive(false);
        }
    }
}
