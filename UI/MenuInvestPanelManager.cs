using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using Assets.Scripts.Language;
using Assets.Scripts.Model;
using Assets.Scripts.Net;
using LitJson;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

/***
* Title:     
*
* Created:	zp
*
* CreatTime:          2019/09/24 20:28:00
*
* Description:投资界面
*
* Version:    0.1
*
*
***/
namespace Assets.Scripts.UI
{
    public class MenuInvestPanelManager : UIBase
    {
        Button btnExtract;
        Text storeName;
        Text proIncome;
        Text investUSDT;

        private GameObject exactGameObject;
        private GameObject personGameObject;
        private GameObject componeyGameObject;
        /// <summary>
        /// 定额税
        /// </summary>
        private Text exactTax;
        /// <summary>
        /// 个人税
        /// </summary>
        private Text personTax;
        /// <summary>
        /// 公司税
        /// </summary>
        private Text componeyTax;
        /// <summary>
        /// 收益余额
        /// </summary>
        private Text amountIncome; 
        /// <summary>
        /// 可提取
        /// </summary>
        private Text extractable;

        private Text expectGetInterestTime;
        Button btnClose;
        private Button btnShare;

        Image imageStoreMenu;
        /// <summary>
        /// 初始化数据
        /// </summary>
        private Dictionary<string,StoreInfo> dicStores;

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<bool, int> investMsg = new Dictionary<bool, int>();
        //Dictionary<string, StoreInfo> dicStores =new Dictionary<string, StoreInfo>();
        Queue<GameObject> CacheQueueStores = new Queue<GameObject>();
        Queue<GameObject> QueueStores = new Queue<GameObject>();
        StoreInfo storeInfo;

        private void Awake()
        {
            Bind(UIEvent.IVEST_PANEL_ACTIVE);
            Init();
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.IVEST_PANEL_ACTIVE:
                    SceneInvestOnclick(message.ToString());
                    //InitStore(null);
                    setPanelActive(true);
                    break;
                //case UIEvent.INVEST_PANEL_VIEW:
                //    CacheData.Instance().InvestData = message as Dictionary<string,InvestInfo>;
                //    //InitStore(null);
                //    setPanelActive(true);
                //    break;
                default:
                    break;
            }
        }
        private void Start()
        {
            exactGameObject = transform.Find("StorePrefab/StoreInfoPanel/ExactTax").gameObject;
            personGameObject = transform.Find("StorePrefab/StoreInfoPanel/PersonTax").gameObject;
            componeyGameObject = transform.Find("StorePrefab/StoreInfoPanel/ComponeyTax").gameObject;

            btnExtract = transform.Find("StorePrefab/BtnExtract").GetComponent<Button>();
            storeName = transform.Find("StorePrefab/StoreInfoPanel/StoreName").GetComponent<Text>();
            proIncome = transform.Find("StorePrefab/StoreInfoPanel/HeadProIncome/ProIncome").GetComponent<Text>(); 
            investUSDT = transform.Find("StorePrefab/StoreInfoPanel/HeadInvestUSDT/InvestUSDT").GetComponent<Text>();
            amountIncome = transform.Find("StorePrefab/StoreInfoPanel/HeadAmountIncome/AmountIncome").GetComponent<Text>();
            extractable = transform.Find("StorePrefab/StoreInfoPanel/HeadExtractable/Extractable").GetComponent<Text>();
            exactTax = transform.Find("StorePrefab/StoreInfoPanel/ExactTax/ExactTax").GetComponent<Text>();
            personTax = transform.Find("StorePrefab/StoreInfoPanel/PersonTax/PersonQuota").GetComponent<Text>();
            componeyTax = transform.Find("StorePrefab/StoreInfoPanel/ComponeyTax/ComponeyQuota").GetComponent<Text>();
            imageStoreMenu = transform.Find("StorePrefab/StoreImage").GetComponent<Image>();
            btnClose = transform.Find("StorePrefab/BtnClose").GetComponent<Button>();
            btnShare = storeName.transform.Find("Button").GetComponent<Button>();
            expectGetInterestTime =
                transform.Find("StorePrefab/StoreImage/TextExpectGetInterestTime").GetComponent<Text>();
            Debug.Log(btnClose);
            btnClose.onClick.AddListener(clickClose);
            btnShare.onClick.AddListener(clickShare);
            setPanelActive(false);
        }

      
        /// <summary>
        /// 初始化
        /// </summary>

        private void Init()
        {

            dicStores = CacheData.Instance().GetStoreInfo();

            GameObject UIstore = Resources.Load("Prefabs/StorePrefab") as GameObject;

            if (dicStores == null)
            {
                return;
            }
            //可优化部分
            //foreach (var item in listStores)
            //{
            //    dicStores.Add(item.id, item);
            //}
        }
        /// <summary>
        /// 产生store
        /// </summary>
        private  void initStore()
        {
            GameObject UIstore = Instantiate(Resources.Load("Prefabs/StorePrefab")) as GameObject;
            //Instantiate(UIstore);
            UIstore.transform.SetParent(this.transform);
            UIstore.SetActive(true);
            CacheQueueStores.Enqueue(UIstore);
        }
        GameObject gameobject;
        /// <summary>
        /// 场景点击投资弹框Todo：增加信息
        /// </summary>
        private void SceneInvestOnclick(string inType)
        {
            if (inType == "11")
            {
                personGameObject.SetActive(false);
                componeyGameObject.SetActive(false);
                exactGameObject.SetActive(true); 
                exactTax.text = CacheData.Instance().InvestData[inType].quotaTax.ToString();
            }
            else
            {
                personGameObject.SetActive(true);
                componeyGameObject.SetActive(true);
                exactGameObject.SetActive(false);
                personTax.text = CacheData.Instance().InvestData[inType].personTax.ToString("0.##%");
                componeyTax.text = CacheData.Instance().InvestData[inType].enterpriseTax.ToString("0.##%");
            }
            imageStoreMenu.sprite = Resources.Load("UI/investImg/" + inType + "大@2x",typeof(Sprite)) as Sprite;
            imageStoreMenu.SetNativeSize();
            storeInfo = dicStores[inType];
            storeName.text = storeInfo.投资名称;
            expectGetInterestTime.text = LanguageService.Instance.GetStringByKey("resultTime", string.Empty)
                .Replace("resultTime", CacheData.Instance().InvestData[inType].resultTime);
            investUSDT.text = CacheData.Instance().InvestData[inType].investMoney.ToString("#0.00");
            proIncome.text = CacheData.Instance().InvestData[inType].expectIncome.ToString("#0.00");
            if (CacheData.Instance().InvestData[inType].openState == "Y")
            {
                btnExtract.gameObject.SetActive(true);
                UpdataState(btnExtract,CacheData.Instance().InvestData[inType].state, CacheData.Instance().InvestData[inType].investId.ToString());
                if (CacheData.Instance().InvestData[inType].state == 703)
                {
                    amountIncome.gameObject.transform.parent.gameObject.SetActive(true);
                    extractable.gameObject.transform.parent.gameObject.SetActive(true);
                    extractable.text = CacheData.Instance().InvestData[inType].extractable.ToString();
                    amountIncome.text = CacheData.Instance().InvestData[inType].incomeLeft.ToString();

                }
                else
                {
                    amountIncome.gameObject.transform.parent.gameObject.SetActive(false);
                    extractable.gameObject.transform.parent.gameObject.SetActive(false);
                }

            }
            else
            {
                btnExtract.gameObject.SetActive(false);
                amountIncome.gameObject.transform.parent.gameObject.SetActive(false);
                extractable.gameObject.transform.parent.gameObject.SetActive(false);
            }
           
        }
        private void clickShare()
        {
            Dispatch(AreaCode.UI,UIEvent.SHARKEPOST_PANEL_VIEW,true);
        }
        private void clickClose()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            setPanelActive(false);
            Dispatch(AreaCode.UI, UIEvent.SELECTINVEST_PANEL_ACTIVE,true);
           // ConCamera.IsActivateTouch = true;
            //CacheQueueStores.Enqueue(gameobject);
        }
     
        void UpdataState(Button btnInvest,int state,string investId)
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


    }
}
