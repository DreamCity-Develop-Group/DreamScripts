using System;
using System.Collections.Generic;
using Assets.Scripts.Framework;
using Assets.Scripts.Language;
using Assets.Scripts.Model;
using Assets.Scripts.Net;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:          2019/09/25 16:19:32
  *
  * Description: 投资选择界面
  *
  * Version:    0.1
  *
  *
***/
namespace Assets.Scripts.UI
{
    public class SelectInvestPanel : UIBase
    {
        Transform transformStore;
        private ScrollRect m_SR;
        Image storeImage;
        Text storeNameText;


        private Image willUnlockImage;

        /// <summary>
        /// 
        /// </summary>
        Button btnStore;

        /// <summary>
        /// 预约灰色
        /// </summary>
        private Image imgInvested;

        /// <summary>
        /// 预约中
        /// </summary>
        private Image imgInvesting;

        /// <summary>
        /// 经营中
        /// </summary>
        private Image imgInvestManaging;

        /// <summary>
        /// 可提取
        /// </summary>
        private Image imgInvestExtractable;


        private Button btnClose;
       // Transform quest;

        /// <summary>
        /// 滑动框的宽高
        /// </summary>
        private float width;

        private float height;

        private Sprite openImage;
        private Sprite lockImage;

        //TODO
        List<string> storeIdList = new List<string> {"1", "2"};
        List<string> unlockStoreList = new List<string>();
        private string willUnlock = "3";
        List<InvestInfo> investInfoList = new List<InvestInfo>();
        /// <summary>
        /// 投资信息个人与好友的区分表
        /// </summary>
       // Dictionary<bool, int> investMsg = new Dictionary<bool, int>();
        Dictionary<string, GameObject> CacheInvest = new Dictionary<string, GameObject>();
        private void Awake()
        {
            Bind(UIEvent.SELECTINVEST_PANEL_ACTIVE, UIEvent.SELECCTINVEST_PANEL_VIEW);

            Bind(UIEvent.INVEST_REDY_VIEW, UIEvent.INVESTED_REPLY_VIEW, UIEvent.INVESTING_REPLY_VIEW);
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.SELECTINVEST_PANEL_ACTIVE:
                    setPanelActive((bool) message);
                  
                    break;

                case UIEvent.SELECCTINVEST_PANEL_VIEW:
                    Init();
                    //unlockStoreList = message as List<string>;
                    //UnSockStore();
                    break;
                case UIEvent.INVEST_VIEW: //未预约
                    Button btnInvest0 = CacheInvest[message.ToString()].transform.Find("BtnInvest")
                        .GetComponent<Button>();
                    btnInvest0.GetComponentInChildren<Text>().text =
                        LanguageService.Instance.GetStringByKey("700", String.Empty);
                    if (btnInvest0.IsInvoking())
                    {
                        btnInvest0.onClick.RemoveAllListeners();
                    }

                    btnInvest0.onClick.AddListener(() =>
                    {
                        Dispatch(AreaCode.NET, ReqEventType.invest_req, message.ToString());
                        btnInvest0.interactable = false;
                    });
                    break;
                case UIEvent.INVEST_REDY_VIEW: //已预约
                    Button btnInvest1 = CacheInvest[message.ToString()].transform.Find("BtnInvest")
                        .GetComponent<Button>();
                    btnInvest1.interactable = false;
                    btnInvest1.GetComponentInChildren<Text>().text =
                        LanguageService.Instance.GetStringByKey("701", String.Empty);
                    break;
                case UIEvent.INVESTED_REPLY_VIEW: //经营中
                    Button btnInvest2 = CacheInvest[message.ToString()].transform.Find("BtnInvest")
                        .GetComponent<Button>();
                    btnInvest2.interactable = false;
                    btnInvest2.GetComponentInChildren<Text>().text =
                        LanguageService.Instance.GetStringByKey("702", String.Empty);
                    break;
                case UIEvent.INVESTING_REPLY_VIEW: //可提取
                    Button btnInvest3 = CacheInvest[message.ToString()].transform.Find("BtnInvest")
                        .GetComponent<Button>();
                    btnInvest3.interactable = true;
                    if (btnInvest3.IsInvoking())
                    {
                        btnInvest3.onClick.RemoveAllListeners();
                    }

                    btnInvest3.onClick.AddListener(() =>
                    {
                        Dispatch(AreaCode.NET, ReqEventType.Extract, message.ToString());
                        btnInvest3.interactable = false;

                    });
                    btnInvest3.GetComponentInChildren<Text>().text =
                        LanguageService.Instance.GetStringByKey("703", String.Empty);
                    break;
                case UIEvent.WillUNLOCK_VIEW:
                    WillUnLock();
                    break;
                default:
                    break;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            transformStore = transform.Find("ViewPoint/InvestStore");
            //willUnlockImage = transformStore.transform.Find("Quest/Image").GetComponent<Image>();
            // InVestStore = transformStore.GetComponent<RectTransform>();
            m_SR = transform.GetComponent<ScrollRect>();
            //监听值改变事件
            m_SR.onValueChanged.AddListener(ScrollRectChange);
            height = transformStore.GetComponent<RectTransform>().rect.height;
            //quest = transform.Find("ViewPoint/InvestStore/Quest");
            btnClose = transform.Find("BtnClose").GetComponent<Button>();
            btnClose.onClick.AddListener(() =>
            {
                setPanelActive(false);
                ConCamera.IsActivateTouch = true;
            });
            setPanelActive(false);
            openImage = Resources.Load<Sprite>("UI/investImg/open");
            lockImage = Resources.Load<Sprite>("UI/investImg/Lock");
        }

        public void Test()
        {
            unlockStoreList = new List<string> {"3", "4"};
            //UnSockStore();
        }

        void Init()
        {
            foreach (var item in CacheData.Instance().InvestData)
            {
                GameObject slectUIstore=null;
                Image imgBg;
                if (!CacheInvest.ContainsKey(item.Value.inType.ToString()))
                {
                     slectUIstore =
                        Instantiate(Resources.Load<GameObject>("Prefabs/SelectStorePrefab"), transformStore);
                    CacheInvest.Add(item.Value.investId.ToString(), slectUIstore);
                }
              
                Button btnInvest;
                GameObject wilOpen;
                if (slectUIstore != null)
                {
                    imgBg = slectUIstore.GetComponent<Image>();
                    if (transformStore.childCount > 3)
                    {
                        width = transformStore.GetComponent<RectTransform>().rect.width;
                        transformStore.GetComponent<RectTransform>().sizeDelta = new Vector2(width + 365, height);
                    }
                    //transformStore.GetComponent<RectTransform>().offsetMax.Set(width + 365,height);

                    storeImage = slectUIstore.transform.Find("Image").GetComponent<Image>();
                    storeNameText = slectUIstore.transform.Find("Text").GetComponent<Text>();
                    btnInvest = slectUIstore.transform.Find("BtnInvest").GetComponent<Button>();
                    btnStore = storeImage.GetComponent<Button>();
                    storeImage.sprite =
                        Resources.Load("UI/investImg/" + item.Value.inType + "小@2x", typeof(Sprite)) as Sprite;
                    storeImage.SetNativeSize();
                    if(!CacheInvest.ContainsKey(item.Key.ToString()))
                        CacheInvest.Add(item.Key.ToString(), slectUIstore);
                    wilOpen = slectUIstore.transform.Find("WillOpen").gameObject;
                    storeNameText.text = CacheData.Instance().GetStoreInfo()[item.Key.ToString()].投资名称;
                    imgBg.sprite = lockImage;

                    //解锁状态
                    if (item.Value.openState == "Y")
                    {
                        wilOpen.SetActive(false);
                        btnInvest.gameObject.SetActive(true);
                        UpdataState(item.Value,btnInvest,item.Value.state,item.Value.investId.ToString());
                        imgBg.sprite  = openImage;
                    }
                    else
                    {
                        wilOpen.SetActive(true);
                        wilOpen.GetComponent<Text>().text = LanguageService.Instance.GetStringByKey("敬请期待", string.Empty);
                        btnInvest.gameObject.SetActive(false);
                    }
                    btnStore.onClick.AddListener(() =>
                    {
                        setPanelActive(false);
                        //Dispatch(AreaCode.NET,);
                        Dispatch(AreaCode.UI, UIEvent.IVEST_PANEL_ACTIVE, item.Value.inType);
                    });
                    imgBg.SetNativeSize();
                    //TODO将解锁
                    // willUnlock = (System.Convert.ToInt16(item.Value.investId) + 1).ToString();
                }
              //  quest.transform.SetAsLastSibling();
                // }

                // m_SR.content.localPosition = Vector2.zero;
            }
        }

        void WillUnLock()
        {
            foreach (var item in CacheData.Instance().GetStoreInfo())
            {
                //if (item.id.Equals(willUnlock))
                //{
                //    willUnlockImage.sprite =
                //        Resources.Load("UI/investImg/" + item.id + "小@2x", typeof(Sprite)) as Sprite;
                //    return;
                //}
            }
        }
        /// <summary>
        ///投资按钮状态
        /// </summary>
        /// <param name="item"></param>
        /// <param name="btnInvest"></param>
        /// <param name="state"></param>
        void UpdataState(InvestInfo item,Button btnInvest, int state,string investId)
        {
            switch (state)
            {
                case 700:
                    btnInvest.transform.Find("Text").GetComponent<Text>().text =
                        LanguageService.Instance.GetStringByKey("700", String.Empty);
                    btnInvest.interactable = true;
                    if (btnInvest.IsInvoking())
                    {
                        btnInvest.onClick.RemoveAllListeners();
                    }

                    btnInvest.onClick.AddListener(() =>
                    {
                        Dispatch(AreaCode.NET, ReqEventType.invest_req, investId);
                        btnInvest.interactable = false;
                    });
                    break;
                case 701:
                    btnInvest.transform.Find("Text").GetComponent<Text>().text =
                        LanguageService.Instance.GetStringByKey("701", String.Empty);
                    btnInvest.interactable = false;
                    break;
                case 702:
                    btnInvest.transform.Find("Text").GetComponent<Text>().text =
                        LanguageService.Instance.GetStringByKey("702", String.Empty);
                    btnInvest.interactable = false;
                    break;
                case 703:
                    btnInvest.transform.Find("Text").GetComponent<Text>().text =
                        LanguageService.Instance.GetStringByKey("703", String.Empty);
                    btnInvest.interactable = true;
                    if (btnInvest.IsInvoking())
                    {
                        btnInvest.onClick.RemoveAllListeners();
                    }

                    btnInvest.onClick.AddListener(() =>
                    {
                        Dispatch(AreaCode.NET, ReqEventType.Extract, investId);
                        btnInvest.interactable = false;

                    });
                    break;
            }
        }

        private void ScrollRectChange(Vector2 T)
        {
            m_SR.horizontalScrollbar.value = T.x;
        }

    }
}
