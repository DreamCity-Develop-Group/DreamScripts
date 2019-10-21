/***
  * Title:     
  *
  * Created:	zzg
  *
  * CreatTime:  2019/09/20 11:16:17
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using Assets.Scripts.Language;
using Assets.Scripts.Model;
using Assets.Scripts.Net;
using Assets.Scripts.UI.Msg;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

namespace Assets.Scripts.UI.MenuUI
{

    /// <summary>
    /// 商会面板
    /// </summary>
    public class ChamberPanel : UIBase
    {
        string language;                                                //语系切换

        private GameObject Chamber;                                      //商会主面板
        private GameObject ChamberRule;                                  //商会规则
        private GameObject BusinessesAreUnderfunded;                     //企业商会兑换资金不足
        private GameObject EnterTradeCode;                               //输入交易码
        private GameObject TopUpRequest;                                 //提交请求
        private GameObject ExchangeCenterBG;                             //兑换中心
        private GameObject AutomaticDeliveryPanel;                       //自动发货
        private GameObject InsufficientFund;                             //自动发货资金不足
        private GameObject SuccessfullySet;                              //设置自动发货成功 
        //商会面板
        private Text chamberTitle;                        //商会标题             
        private Button ClosePanel;                        //关闭商会面板
        private Button Enterprise;                        //企业商会按钮
        private Button MyEnterprise;                      //我的商会按钮
        private Button ExchangeCenter;                    //兑换中心
        private GameObject EnterpriseClick;               //企业激活按钮
        private GameObject MyEnterpriseClick;             //我的商会激活按钮
        private Image CoreMember;                         //核心成员
        private Text CommonMemberNum;                     //普通成员人数
        private GameObject EnterpriseInfo;                //企业商会信息
        private GameObject MyEnterpriseInfo;              //我的商会信息
        private Button ExchangeCenterBtn;                 //兑换中心按钮 

        private GameObject ExchangeCenterClick;           //兑换中心激活按钮

        //商会规则
        private Button CloseRule;                        //关闭规则    
        //资金不足
        private Button GoPrepaidBtn;                     //去充值
        private Button CloseBtn;                         //关闭
        //输入交易码
        private Button TradeClose;                       //关闭交易面板
        private Button TradeConfindBtn;                  //确定
        private Button TradeCancel;                      //取消
        private InputField InputTradeCode;               //交易码输入  
        //兑换中心
        private Text CenterTitle;                         //标题
        private Button CenterCloseBtn;                    //关闭兑换中心
        private Button CenterEnterpriseBtn;              //企业商会
        private Button CenterMyEnter;                     //我的商会
        private Button AutomaticDelivery;                 //自动发货
        private Button AKeyDelivery;                      //一键发货 
        //自动发货
        private Text AutomaticDeliveryTitle;             //自动发货标题
        private Text Hint;                               //自动发货提示
        private Text Reminder;                           //备货数量提示
        //private InputField InputReminderNum;             //输入的备用数量
        private Text DeliveryConversionRate;            //USDT兑换率
        private Button OpenOrClose;                     //自动发货开关
        private Button DeliveryClose;                   //关闭
        private Button DeliveryEnsure;                  //确定 
        private Sprite[] Spr_OpenOrClose = new Sprite[2];//自动开关按钮图
        public int IsAutomaticDelivery = 0;            //是否自动发货 (0为关。1为开）
        //自动发货资金不足
        private Button BtnClose;                        //关闭提示
        private Button BtnGoRecharge;                   //去充值  
        //设置自动发货成功
        private Button GoodBtn;                        //好的
        //企业商会面板
        private Text ChanberLV;                          //企业商会等级
        private Text LVNum;                              //企业商会等级数
        private InputField inputConversion;              //输入兑换金额
        private Text ConversionRate;                     //兑换率
        private Button BtnRule;                          //规则按钮
        private Button BtnComfing;                       //确定 
        //我的商会信息

        //提交请求
        private Button RequestClose;                     //关闭请求
        //数据信息
        private List<MermberInfo> listMyMember = new List<MermberInfo>();   //我的核心成员列表
        private int count;//普通成员数量
      //  private List<ExchangeInfo> listConversion = new List<ExchangeInfo>(); //我的兑换列表
        private GameObject MyMemberInfoPerfab0;              //我的核心成员信息预制体 
        private GameObject MyMemberInfoPerfab1;              //我的核心成员信息预制体 
        private Transform TranConnet;                        //信息的父物体Transform

        private GameObject ExchangeCenterInfoPerfab0;        //我的核心成员信息预制体 
        private GameObject ExchangeCenterInfoPerfab1;       //我的核心成员信息预制体 
        private Transform TranExchangeCenterConnet;         //信息的父物体Transform

       // private Dictionary<string,GameObject> listNoExchangeRequestForshipment = new Dictionary<string,GameObject>();
        /// <summary>
        /// 商会成员信息
        /// </summary>
        private CommerceInfo commerceInfo = new CommerceInfo();
        private float PlayerUSDT = 0;            //玩家USDT数
        /// <summary>
        /// 待发货的兑换请求
        /// </summary>
        private Dictionary<string, GameObject> exchangeCenterDic = new Dictionary<string, GameObject>();
        private List<ExchangeInfo> _exchangeInfo = new List<ExchangeInfo>();
        private HintMsg promptMsg;

        private string ConversionRateText;
        private ScrollRect _exchangeRect;
        private Transform _exchangeContent;
        private void Awake()
        {
            Bind(UIEvent.COMMERCE_PANEL_ACTIVE);
            Bind(UIEvent.COMMERCE_PANEL_VIEW,UIEvent.EXCHANGECENTER_STATE_VIEW,UIEvent.EnterTradeCode_Panel_Active,UIEvent.BusinessesAreUnderfunded_Panle_Active,UIEvent.EXECHANGECENTER_PANEL_ACTIVE);
        }
        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                ////如果没有加入商会
                //case UIEvent.COMMERCE_NOJIONPANEL_ACTIVE:
                //    setPanelActive((bool)message);
                //    break;
                //加入商会
                case UIEvent.COMMERCE_PANEL_ACTIVE:
                    setPanelActive((bool)message);
                        LVNum.text = CacheData.Instance().CommerceLevel.ToString();
                    ConversionRate.text =
                        ConversionRate.text.Replace("rate", CacheData.Instance().ExchangeRate.ToString());
                    ConversionRateText = ConversionRate.text;
                        Chamber.gameObject.SetActive((bool)message);
                    inputConversion.text = "";
                    break;
                case UIEvent.COMMERCE_PANEL_VIEW:
                    CommerceInfo memberdata =message as CommerceInfo;
                    if (memberdata == null) throw new ArgumentNullException(nameof(memberdata));
                    for(int i= listMyMember.Count;i< memberdata.members.Count;i++)
                    {
                        listMyMember.Add(memberdata.members[i]);
                    }
                    listMyMember = memberdata.members;
                    count = memberdata.num;
                    memberdata = null;
                    UpdataMembers();
                    break;
                case UIEvent.EXCHANGECENTER_STATE_VIEW:
                    //深复制缓存处理
                    //listConversion.ForEach(i => CacheData.Instance().CommerceExchangeMembers.Add(i));
                   // UpdateBtnState();
                    //listConversion = message as List<ExchangeInfo>;
                    UpdateExchangeList(message as List<ExchangeInfo>);
                    break;
                case UIEvent.EXECHANGECENTER_PANEL_ACTIVE:
                    setPanelActive(true);
                    _exchangeInfo = message as List<ExchangeInfo>;
                    foreach (var item in _exchangeInfo)
                    {
                        if ((OrderState)item.status   == OrderState.TOBESHIPPED)
                        {
                           // UpdateBtnState(item.orderId);
                        }
                    }
                    ExchangeCenterBG.SetActive(true);
                    break;
                case UIEvent.EnterTradeCode_Panel_Active:
                    setPanelActive(true);
                    if ((bool)message)
                    {
                        EnterTradeCode.SetActive(true);
                    }
                    else
                    {
                        //Chamber.SetActive(false);
                        EnterTradeCode.SetActive(false);
                        //TopUpRequest.SetActive(true);
                        promptMsg.Change(LanguageService.Instance.GetStringByKey("ExchangeAction", String.Empty), Color.white);
                        Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);

                    }
                    break;
                case UIEvent.BusinessesAreUnderfunded_Panle_Active:
                    BusinessesAreUnderfunded.SetActive(true);
                    break;
                default:
                    break;
            }
        }
   
        void Start()
        {
          
            _exchangeRect = transform.Find("ExchangeCenterBG/Scroll View")
                .GetComponent<ScrollRect>();
            _exchangeContent = _exchangeRect.transform.Find("Viewport/Content").transform;
            //监听值改变事件
            _exchangeRect.onValueChanged.AddListener(ScrollRectChange);
            promptMsg = new HintMsg();
            MyMemberInfoPerfab0 = Resources.Load<GameObject>("PerFab/MemberInformation0");
            MyMemberInfoPerfab1 = Resources.Load<GameObject>("PerFab/MemberInformation1");
            ExchangeCenterInfoPerfab0 = Resources.Load<GameObject>("PerFab/ForRecord0");
            ExchangeCenterInfoPerfab1 = Resources.Load<GameObject>("PerFab/ForRecord1");
            GetGameObj();
            ClosePanel.onClick.AddListener(OnclikPanel);
            Enterprise.onClick.AddListener(OnClickEnterprise);
            MyEnterprise.onClick.AddListener(OnClickMyEnterprise);
            ExchangeCenter.onClick.AddListener(OnClickExchangeCenter);
            CenterCloseBtn.onClick.AddListener(OnClickCloseCenter);
            CenterEnterpriseBtn.onClick.AddListener(OnClickCanterEnterprise);
            CenterMyEnter.onClick.AddListener(OnClickCanterMyEnterprise);
            ExchangeCenterBtn.onClick.AddListener(OnClickExchangeCenterBtn);
            BtnRule.onClick.AddListener(OnClickBtnRule);
            CloseRule.onClick.AddListener(OnClickCloseRule);
            BtnComfing.onClick.AddListener(OnClickEnsure);
            TradeClose.onClick.AddListener(OnClickTrande);
            TradeCancel.onClick.AddListener(OnClickTrande);
            TradeConfindBtn.onClick.AddListener(OnClickTrandeConfindBtn);
            RequestClose.onClick.AddListener(OnClickRequestClose);
            AutomaticDelivery.onClick.AddListener(OnClickAutomaticDelivery);
            DeliveryClose.onClick.AddListener(OnClickCloseAutomaticDelivery);
            OpenOrClose.onClick.AddListener(SettingAutomaticDelivery);
            //DeliveryEnsure.onClick.AddListener(OnClickAutomaticDeliveryEnsure);
            AKeyDelivery.onClick.AddListener(OnClickAKeyDelivery);
            //inputConversion.GetComponent<InputField>().onValueChanged.AddListener((x) =>
            //{
            //    double number = System.Convert.ToDouble(x);
            //    double money = number * CacheData.Instance().ExchangeRate;
            //    ConversionRate.text = ConversionRateText.Insert(1, money.ToString());
            //});
            inputConversion.GetComponent<InputField>().onEndEdit.AddListener((x) =>
            {
                double number = System.Convert.ToDouble(x);
                double money = number * CacheData.Instance().ExchangeRate;
                ConversionRate.text = ConversionRateText.Insert(1, money.ToString());
            });
            if (PlayerPrefs.GetInt("IsAutomaticDelivery") == 1)
            {
                OpenOrClose.GetComponent<Image>().sprite = Spr_OpenOrClose[1];
            }
            else
            {
                OpenOrClose.GetComponent<Image>().sprite = Spr_OpenOrClose[0];
            }
            ChamberRule.SetActive(false);
            MyEnterpriseInfo.SetActive(false);
            MyEnterpriseClick.SetActive(false);
            BusinessesAreUnderfunded.SetActive(false);
            EnterTradeCode.SetActive(false);
            TopUpRequest.SetActive(false);
            ExchangeCenterBG.SetActive(false);
            AutomaticDeliveryPanel.SetActive(false);
            InsufficientFund.SetActive(false);
            SuccessfullySet.SetActive(false);
            setPanelActive(false);
            ManyLanguages();
        }
        /// <summary>
        /// 查找游戏物体
        /// </summary>
        private void GetGameObj()
        {
            Chamber = transform.Find("Chamber").gameObject;
            ChamberRule = transform.Find("ChamberRuleBg").gameObject;
            BusinessesAreUnderfunded = transform.Find("BusinessesAreUnderfunded").gameObject;
            EnterTradeCode = transform.Find("EnterTradeCode").gameObject;
            TopUpRequest = transform.Find("TopUpRequest").gameObject;
            ExchangeCenterBG = transform.Find("ExchangeCenterBG").gameObject;
            AutomaticDeliveryPanel = transform.Find("AutomaticDeliveryPanel").gameObject;
            InsufficientFund = transform.Find("InsufficientFund").gameObject;
            SuccessfullySet = transform.Find("SuccessfullySet").gameObject;
            //商会主页面
            chamberTitle = Chamber.transform.Find("Title").GetComponent<Text>();
            ClosePanel = Chamber.transform.Find("BtnClose").GetComponent<Button>();
            Enterprise = Chamber.transform.Find("Enterprise").GetComponent<Button>();
            MyEnterprise = Chamber.transform.Find("MyEnterprise").GetComponent<Button>();
            ExchangeCenter = Chamber.transform.Find("ExchangeCenter").GetComponent<Button>();
            EnterpriseClick = Chamber.transform.Find("EnterpriseClick").gameObject;
            MyEnterpriseClick = Chamber.transform.Find("MyEnterpriseClick").gameObject;           
            EnterpriseInfo = Chamber.transform.Find("EnterpriseInfo").gameObject;
            MyEnterpriseInfo = Chamber.transform.Find("MyEnterpriseInfo").gameObject;
            ExchangeCenterBtn = MyEnterpriseClick.transform.Find("ExchangeCenter").GetComponent<Button>();
            CoreMember = MyEnterpriseClick.transform.Find("CoreMember").GetComponent<Image>();
            CommonMemberNum = MyEnterpriseClick.transform.Find("OrdinaryMembers/Text").GetComponent<Text>();
            //规则
            CloseRule = ChamberRule.transform.Find("CloseBtn").GetComponent<Button>();
            //资金不足
            GoPrepaidBtn = BusinessesAreUnderfunded.transform.Find("GoPrepaidBtn").GetComponent<Button>();
            CloseBtn= BusinessesAreUnderfunded.transform.Find("CloseBtn").GetComponent<Button>();
            //交易码
            TradeClose = EnterTradeCode.transform.Find("CloseBtn").GetComponent<Button>();
            TradeConfindBtn = EnterTradeCode.transform.Find("Determine").GetComponent<Button>();
            TradeCancel = EnterTradeCode.transform.Find("Cancel").GetComponent<Button>();
            InputTradeCode = EnterTradeCode.transform.Find("InputField").GetComponent<InputField>();
            //兑换中心
            CenterTitle = ExchangeCenterBG.transform.Find("Title").GetComponent<Text>();
            CenterCloseBtn = ExchangeCenterBG.transform.Find("CloseBtn").GetComponent<Button>();
            CenterEnterpriseBtn = ExchangeCenterBG.transform.Find("Enterprise").GetComponent<Button>();
            CenterMyEnter = ExchangeCenterBG.transform.Find("MyEnterprise").GetComponent<Button>();
            ExchangeCenterClick = ExchangeCenterBG.transform.Find("ExchangeCenterClick").gameObject;
            AutomaticDelivery = ExchangeCenterBG.transform.Find("AutomaticDeliveryBtn").GetComponent<Button>();
            AKeyDelivery = ExchangeCenterBG.transform.Find("AKeyDeliveryBigBtn").GetComponent<Button>();
            //自动发货
            AutomaticDeliveryTitle = AutomaticDeliveryPanel.transform.Find("Title").GetComponent<Text>();
            Hint = AutomaticDeliveryPanel.transform.Find("Automatic").GetComponent<Text>();
            Reminder = AutomaticDeliveryPanel.transform.Find("Reminder").GetComponent<Text>();
            //InputReminderNum = AutomaticDeliveryPanel.transform.Find("InputField").GetComponent<InputField>();
            DeliveryConversionRate = AutomaticDeliveryPanel.transform.Find("ConversionRate").GetComponent<Text>();
            OpenOrClose = AutomaticDeliveryPanel.transform.Find("OnOff").GetComponent<Button>();
            DeliveryClose = AutomaticDeliveryPanel.transform.Find("DeliveryClose").GetComponent<Button>();
            DeliveryEnsure = AutomaticDeliveryPanel.transform.Find("DetermineBtn").GetComponent<Button>();
            //自动发货资金不足
            BtnClose = InsufficientFund.transform.Find("BtnClose").GetComponent<Button>();
            BtnGoRecharge= InsufficientFund.transform.Find("BtnGoRecharge").GetComponent<Button>();
            //设置成功
            GoodBtn = SuccessfullySet.transform.Find("OK").GetComponent<Button>();
            //企业商会
            ChanberLV = EnterpriseInfo.transform.Find("Image/LV").GetComponent<Text>();
            LVNum= EnterpriseInfo.transform.Find("Image/LVNum").GetComponent<Text>();
            inputConversion = EnterpriseInfo.transform.Find("InputField").GetComponent<InputField>();
            ConversionRate = EnterpriseInfo.transform.Find("ExchangeRate").GetComponent<Text>();
            BtnRule = EnterpriseInfo.transform.Find("BtnRule").GetComponent<Button>();
            BtnComfing = EnterpriseInfo.transform.Find("BtnComfing").GetComponent<Button>();
            //提交请求
            RequestClose = TopUpRequest.GetComponent<Button>();

            
            TranConnet = MyEnterpriseInfo.transform.Find("Scroll View/Viewport/Content");
            TranExchangeCenterConnet = ExchangeCenterBG.transform.Find("Scroll View/Viewport/Content");        

           

        }
        /// <summary>
        /// 多语言切换图
        /// </summary>
        private void ManyLanguages()
        {
            language = PlayerPrefs.GetString("language");

            Enterprise.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/EnterpriseChamberOfCommerce");
            MyEnterprise.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/MyChamber");
            ExchangeCenter.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/ExchangeCenter");
            EnterpriseClick.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/EnterpriseChamberOfCommerceBig");
            MyEnterpriseClick.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/MyChamberBig");
            CoreMember.sprite = Resources.Load<Sprite>("UI/menu/" + language + "/CoreMember");
            ExchangeCenterClick.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/ExchangeCenterBig");
            ExchangeCenterBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/ExchangeCenter");
            CenterEnterpriseBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/EnterpriseChamberOfCommerce");
            CenterMyEnter.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/MyChamber");
            AutomaticDelivery.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/AutomaticDelivery");
            AKeyDelivery.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/AKeyDeliveryBig");
            ChamberRule.transform.Find("ChamberRule").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/ChamberRules");
            BtnComfing.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/ConfirmBig");
            TradeConfindBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/ConfirmBig");
            TradeCancel.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/CancelBig");
            DeliveryEnsure.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/ConfirmBig");
            GoodBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/OK");
            BtnGoRecharge.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/GoToPrepaid");
            TopUpRequest.GetComponent<Image>().sprite= Resources.Load<Sprite>("UI/menu/" + language + "/Submitted");
            GoPrepaidBtn.GetComponent<Image>().sprite= Resources.Load<Sprite>("UI/menu/" + language + "/GoToPrepaid");
            for (int i = 0; i < Spr_OpenOrClose.Length; i++)
            {
                Spr_OpenOrClose[i]= Resources.Load<Sprite>("UI/menu/" + language + "/Switch"+i);
            }
        }
        /// <summary>
        /// 关闭商会面板
        /// </summary>
        private void OnclikPanel()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.UI, UIEvent.COMMERCE_PANEL_ACTIVE, false);
            setPanelActive(false);
            ConCamera.IsActivateTouch = true;
        }
        /// <summary>
        /// 点击企业商会按钮
        /// </summary>
        private void OnClickEnterprise()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            EnterpriseClick.SetActive(true);
            EnterpriseInfo.SetActive(true);
            MyEnterpriseInfo.SetActive(false);
            MyEnterpriseClick.SetActive(false);
            ExchangeCenter.gameObject.SetActive(true);
        }
        /// <summary>
        /// 点击我的商会按钮
        /// </summary>
        private void OnClickMyEnterprise()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.NET,ReqEventType.commerce_member,null);
            EnterpriseClick.SetActive(false);
            MyEnterpriseInfo.SetActive(true);
            EnterpriseInfo.SetActive(false);
            MyEnterpriseClick.SetActive(true);
            ExchangeCenter.gameObject.SetActive(false);
        }
        private void UpdataMembers()
        {
            //CommonMemberNum.text = count.ToString();
            CommonMemberNum.text = LanguageService.Instance.GetStringByKey("CommonNumber", String.Empty).Replace("number",count.ToString());
              
            //初步在此生成成员信息
            if(listMyMember.Count>0)
            {
                for (int i = 0; i < listMyMember.Count; i++)
                {
                    GameObject obj = null;
                    if (i%2==0)
                    {
                        obj =  CreatePreObj(MyMemberInfoPerfab0, TranConnet);            
                    }
                    else
                    {
                        obj= CreatePreObj(MyMemberInfoPerfab1, TranConnet);                      
                    }
                    obj.transform.Find("Number").GetComponent<Text>().text = listMyMember[i].playerName;
                    obj.transform.Find("Time").GetComponent<Text>().text = listMyMember[i].createTime;
                    obj.SetActive(true);
                }
            }
        }
        /// <summary>
        /// 点击兑换中心
        /// </summary>
        private void OnClickExchangeCenter()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.NET,ReqEventType.ExchangeCenter,null);

            ExchangeCenterBG.SetActive(true);
        }

        void UpdateExchangeList(List<ExchangeInfo> listConversion)
        {
            //初步在此生成兑换信息
            if (listConversion.Count > 0)
            {
                for (int i = 0; i < listConversion.Count; i++)
                {
                    GameObject obj = null;
                    if (i % 2 == 0)
                    {
                        obj = CreatePreObj(ExchangeCenterInfoPerfab0, TranExchangeCenterConnet);             
                    }
                    else
                    {
                        obj = CreatePreObj(ExchangeCenterInfoPerfab1, TranExchangeCenterConnet);
                    }

                    if (_exchangeContent.childCount >= 5)
                    {
                        float width = _exchangeContent.GetComponent<RectTransform>().rect.width;
                        float height = _exchangeContent.GetComponent<RectTransform>().rect.height;
                        float objHeight = obj.GetComponent<RectTransform>().rect.height;
                        _exchangeContent.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height+objHeight);
                    }
                    obj.transform.Find("Time").GetComponent<Text>().text = listConversion[i].date;
                    obj.transform.Find("Nummber").GetComponent<Text>().text = listConversion[i].player;
                    obj.transform.Find("MTNum").GetComponent<Text>().text = listConversion[i].amount.ToString("#0.00");
                    obj.transform.Find("MT").GetComponent<Text>().text = "兑换(MT)";
                    obj.transform.Find("USDT").GetComponent<Text>().text = "支付(USDT)";
                    obj.transform.Find("USDTNum").GetComponent<Text>().text = listConversion[i].pay.ToString("#0.00");
                    if ((OrderState)listConversion[i].status == OrderState.TOBESHIPPED)
                    {
                        obj.transform.Find("TheDeliveryBtn").GetComponent<Button>().onClick.AddListener(() =>
                        {
                            Dispatch(AreaCode.NET, ReqEventType.commerce_sendmt, listConversion[i].status);
                        });
                        if (exchangeCenterDic != null && !exchangeCenterDic.ContainsKey(listConversion[i].orderId))
                            exchangeCenterDic.Add(listConversion[i].orderId, obj);
                    }
                    obj.SetActive(true);
                }
            }
            ExchangeCenterBG.SetActive(true);
        }
        /// <summary>
        /// 发货按钮状态
        /// </summary>
        private void UpdateBtnState(OrderState state)
        {
            if (exchangeCenterDic.Count <1)
            {
                return;
            }
            foreach (var item in exchangeCenterDic)
            {
                item.Value.transform.Find("TheDeliveryBtn").gameObject.SetActive(false);
                item.Value.transform.Find("HandlingMarks").gameObject.SetActive(true);
                switch (state)
                {
                    case OrderState.TOBESHIPPED:
                        //待发货
                        item.Value.transform.Find("TheDeliveryBtn").gameObject.SetActive(true);
                        item.Value.transform.Find("RefusedToBtn").gameObject.SetActive(false);
                        item.Value.transform.Find("HandlingMarks").gameObject.SetActive(false);
                        break;
                    case OrderState.EXPIRED:
                        //过期
                        item.Value.transform.Find("RefusedToBtn").gameObject.SetActive(false);
                        item.Value.transform.Find("TheDeliveryBtn").gameObject.SetActive(false);
                        item.Value.transform.Find("HandlingMarks").GetComponent<Image>().sprite= Resources.Load<Sprite>("UI/menu/" + language + "/HaveExpired");
                        item.Value.transform.Find("HandlingMarks").gameObject.SetActive(true);
                        exchangeCenterDic.Remove(item.Key);
                        break;
                    case OrderState.FINISHED:
                        //完成
                        item.Value.transform.Find("RefusedToBtn").gameObject.SetActive(false);
                        item.Value.transform.Find("TheDeliveryBtn").gameObject.SetActive(false);
                        item.Value.transform.Find("HandlingMarks").gameObject.SetActive(false);
                        exchangeCenterDic.Remove(item.Key);
                        break;
                    case OrderState.REFUSE:
                        //拒绝
                        item.Value.transform.Find("RefusedToBtn").gameObject.SetActive(false);
                        item.Value.transform.Find("TheDeliveryBtn").gameObject.SetActive(false);
                        item.Value.transform.Find("HandlingMarks").GetComponent<Image>().sprite= Resources.Load<Sprite>("UI/menu/" + language + "/HasRefusedTo");
                        item.Value.transform.Find("HandlingMarks").gameObject.SetActive(true);
                        exchangeCenterDic.Remove(item.Key);
                        break;
                    default:
                        break;
                }
            }
        }
     
        /// <summary>
        /// 关闭兑换中心
        /// </summary>
        private void OnClickCloseCenter()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            ExchangeCenterBG.SetActive(false);
        }
        /// <summary>
        /// 兑换中心的企业
        /// </summary>
        private void OnClickCanterEnterprise()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            ExchangeCenterBG.SetActive(false);
            EnterpriseClick.SetActive(true);
            EnterpriseInfo.SetActive(true);
            MyEnterpriseInfo.SetActive(false);
            MyEnterpriseClick.SetActive(false);
            ExchangeCenter.gameObject.SetActive(true);
        }
        /// <summary>
        /// 兑换中心的我的商会
        /// </summary>
        private void OnClickCanterMyEnterprise()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.NET,ReqEventType.commerce_member,null);
            ExchangeCenterBG.SetActive(false);
            EnterpriseClick.SetActive(false);
            MyEnterpriseInfo.SetActive(true);
            EnterpriseInfo.SetActive(false);
            MyEnterpriseClick.SetActive(true);
            ExchangeCenter.gameObject.SetActive(false);
            Chamber.SetActive(true);
        }
        /// <summary>
        /// 我的商会的兑换中心的按钮
        /// </summary>
        private void OnClickExchangeCenterBtn()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            ExchangeCenterBG.SetActive(true);
        }
        /// <summary>
        /// 点击问号
        /// </summary>
        private void OnClickBtnRule()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            ChamberRule.SetActive(true);
        }
        /// <summary>
        /// 关闭规则
        /// </summary>
        private void OnClickCloseRule()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            ChamberRule.SetActive(false);
        }
        /// <summary>
        /// 企业商会兑换确定按钮
        /// </summary>
        private void OnClickEnsure()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            string USDT = inputConversion.text;
           // double USDTNum = CacheData.Instance().Usdt;
            if (USDT != null || USDT.Equals(""))
            {
                    Dispatch(AreaCode.NET,ReqEventType.checkMoney,USDT);

                    //EnterTradeCode.SetActive(true);
            }
            else
            {
                //提示输入错误
            }

        }
        /// <summary>
        /// 关闭输入交易码
        /// </summary>
        private void OnClickTrande()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            EnterTradeCode.SetActive(false);
            InputTradeCode.text = "";
        }
        /// <summary>
        /// 确定交易码
        /// </summary>
        private void OnClickTrandeConfindBtn()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            string confirmPass = InputTradeCode.text;
            if (confirmPass.IsNullOrEmpty())
            {
                //TODO提示
            }
            else
            {
                Dispatch(AreaCode.NET, ReqEventType.confirmPass,confirmPass);
            }
           
        }
       /// <summary>
       /// 关闭发送兑换请求
       /// </summary>
        private void OnClickRequestClose()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            TopUpRequest.SetActive(false);
            setPanelActive(false);
            ConCamera.IsActivateTouch = true;
        }
        /// <summary>
        /// 点击自动发货按钮
        /// </summary>
        private void OnClickAutomaticDelivery()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            if (CacheData.Instance().SET_AutoState)
            {
                OpenOrClose.GetComponent<Image>().sprite = Spr_OpenOrClose[1];
            }
            else
            {
                OpenOrClose.GetComponent<Image>().sprite = Spr_OpenOrClose[0];
            }
            // Dispatch(AreaCode.NET,ReqEventType.auto_send,"true");
            AutomaticDeliveryPanel.SetActive(true);
            ExchangeCenterBG.SetActive(false);
            Chamber.SetActive(false);
        }
        /// <summary>
        /// 关闭自动发货
        /// </summary>
        private void OnClickCloseAutomaticDelivery()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.NET, ReqEventType.auto_send, "false");
            AutomaticDeliveryPanel.SetActive(false);
            CacheData.Instance().SET_AutoState = false;
            setPanelActive(false);
            ConCamera.IsActivateTouch = true;
        }
        /// <summary>
        /// 设置是否自动发货
        /// </summary>
        private void SettingAutomaticDelivery()
        {
            if (CacheData.Instance().SET_AutoState)
            {
                OpenOrClose.GetComponent<Image>().sprite = Spr_OpenOrClose[0];
                Dispatch(AreaCode.NET, ReqEventType.auto_send, "false");
            }
            else
            {
                if(CacheData.CommerceRestoreMT > CacheData.Instance().Mt)
                {
                    OpenOrClose.enabled = false;
                    OpenOrClose.GetComponent<Image>().sprite = Spr_OpenOrClose[0];
                    return;
                }
                else
                {
                    OpenOrClose.enabled = true;
                    Dispatch(AreaCode.NET, ReqEventType.auto_send, "true");
                    OpenOrClose.GetComponent<Image>().sprite = Spr_OpenOrClose[1];
                }
              
            }
            
        }
        /// <summary>
        /// 自动发货确定
        /// </summary>
        private void OnClickAutomaticDeliveryEnsure()
        {
            //判断MT是否足够
            if (CacheData.CommerceRestoreMT > CacheData.Instance().Mt)
            {
                OpenOrClose.GetComponent<Image>().sprite = Spr_OpenOrClose[0];
                AutomaticDeliveryPanel.SetActive(false);
                InsufficientFund.SetActive(true);
            }
            else
            {
                Dispatch(AreaCode.NET, ReqEventType.auto_send, "true");
                 AutomaticDeliveryPanel.SetActive(false);
                 SuccessfullySet.SetActive(true);
                    StartCoroutine(CloseScc());
            }
        }
        /// <summary>
        /// 一键发货
        /// </summary>
        private void OnClickAKeyDelivery()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            //for (int i = 0; i < listNoExchangeRequestForshipment.Count; i++)
            //{
            //    //listNoExchangeRequestForshipment[i].transform.Find("ShippingStatus").GetComponent<Image>().sprite = "已发货";
            //}

            Dispatch(AreaCode.NET,ReqEventType.commerceSendMT, exchangeCenterDic.Keys.ToList());
        }
        /// <summary>
        /// 去兑换
        /// </summary>
        private void OnClickToChange()
        {
            InsufficientFund.SetActive(false);
            Chamber.SetActive(true);
        }
        /// <summary>
        /// 关闭去兑换
        /// </summary>
        private void OnClickCloseChange()
        {
            InsufficientFund.SetActive(false);
        }
 
        private IEnumerator CloseScc()
        {
            yield return new WaitForSeconds(1f);
            SuccessfullySet.SetActive(false);
        }
        private Queue<GameObject> m_queue_gPreObj = new Queue<GameObject>();          //对象池
        private Transform TempTrans;
        /// <summary>
        /// 创建预制体
        /// </summary>
        /// <param name="Prefab">预制体</param>
        /// <param name="m_transPerfab">预制体父物体的transform</param>
        /// <returns></returns>
        public GameObject CreatePreObj(GameObject Prefab, Transform m_transPerfab)
        {
            GameObject obj = null;
            if (m_queue_gPreObj.Count > 0)
            {
                obj = m_queue_gPreObj.Dequeue();
            }
            else
            {
                Transform trans = null;
                trans = GameObject.Instantiate(Prefab, m_transPerfab).transform;
                trans.localPosition = Vector3.zero;
                trans.localRotation = Quaternion.identity;
                trans.localScale = Vector3.one;
                obj = trans.gameObject;
                obj.SetActive(false);
            }
            return obj;
        }
        /// <summary>
        /// 预制体回收
        /// </summary>
        /// <param name="obj">回收的预制体</param>
        private void RePreObj(GameObject obj)
        {
            if (obj != null)
            {
                obj.SetActive(false);
                obj.transform.SetParent(TempTrans);
                m_queue_gPreObj.Enqueue(obj);
            }
        }

        private void GetSendState()
        {
            foreach (var item in CacheData.Instance().CommerceExchangeMembers)
            {
               
                 UpdateBtnState((OrderState)item.status);
                
            }
        }
        private void ScrollRectChange(Vector2 T)
        {
            _exchangeRect.verticalScrollbar.value = T.y;
            if (T.y >1)
            {
                Dispatch(AreaCode.NET,ReqEventType.ExchangeCenter,CacheData.Instance().ExchangePage+1);
            }
        }

    }
}
