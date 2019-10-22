/***
  * Title:    NoHaveChamber 
  *
  * Created:	zzg
  *
  * CreatTime:  2019/09/23 17:25:07
  *
  * Description: 加入商会界面
  *
  * Version:    0.1
  *
  *
***/

using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using Assets.Scripts.Model;
using Assets.Scripts.Net;
using Assets.Scripts.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuUI
{
    /// <summary>
    /// 未创建个人商会面板
    /// </summary>
    public class NoHaveChamber : UIBase
    {
        //面板
        private GameObject JionChamber;                           //加入商会面板
        private GameObject IncorrectPrompt;                       //邀请码错误提示
        private GameObject JIonScee;                              //邀请码正确
        private GameObject SettingtradingCode;                    //设置交易码
        private GameObject ChamberOfCommerceRules;                //商会规则
        private GameObject ConfirmationPaymentPanel;              //确认支付面板
        private GameObject TransactionPassword;                   //交易密码面板
        private GameObject InsufficientFunds;                     //资金不足提示
        private GameObject SuccessfulJoin;                        //成功加入商会   
        //加入商会  
        private Button DetermineBtn;                              //确定按钮
        private Button CloseBtn;                                  //关闭按钮
        private InputField InputChamberCode;                      //输入商会邀请码
        private Text txt_Text;                                    //描述
        private Button PromptOKBtn;                               //好的按钮
        private Button PromptClose;                               //错误提示关闭按钮
        private Text promptTxt;                                   //描述
        //成功加入商会
        private Text succTile;                                     //标题
        private Text succInstructions;                             //描述
        private Button ConfindBtn;                                 //确定按钮
        //设置交易码
        private Text tradingTitle;                                 //标题
        private GameObject ErrorMessage;                           //错误提示 
        private Button CloseError;                                 //关闭错误提示
        private Button ErrorGood;                                  //提示关闭
        private InputField InputField;                             //输入交易码
        private Button tradingDeDetermineBtn;                      //确定按钮
        private Button tradingCancel;                              //取消     
        //商会规则
        private Text ChamberRulesTitle;                            //商会规则的标题
        private Image MembershipStatement;                         //规则图片
        private Button PayBtn;                                     //支付按钮
        //确定支付
        private Text description;                                  //描述
        private Button payClose;                                   //关闭
        private Button payDetermine;                               //确定
        private Button payCancle;                                  //取消
        //输入交易密码
        private Button TranCloseBtn;                               //关闭交易密码
        private Button TranDetermine;                              //交易密码确定按钮
        private Button TranCanle;                                  //交易密码取消按钮
        private InputField TranInput;                              //交易密码输入
        //资金不足
        private Button fundsColse;                                 //关闭
        private Button GoPayBtn;                                   //去充值
        private Text FundsTxt;  //描述 
        private string commerceCode;        //商会邀请码
        CommerceInfo commerceInfo = new CommerceInfo();
        private void Awake()
        {
            Bind(UIEvent.COMMERCE_NOJIONPANEL_ACTIVE,UIEvent.BusinessPrompt_NOTIVE_VIEW,UIEvent.HINT_ACTIVE,UIEvent.CHAMBERTRANSACTION,UIEvent.CHAMBERCODECRRECT,UIEvent.ConfirmationPaymentPanel_Active);
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.COMMERCE_NOJIONPANEL_ACTIVE:
                     setPanelActive((bool)message);
                     if (CacheData.Instance().CommerceState == 0)
                     {
                         JionChamber.SetActive(true);
                         InputChamberCode.text = "";
                     }
                     break;
                case UIEvent.ConfirmationPaymentPanel_Active:
                    ConfirmationPaymentPanel.SetActive(false);
                    break;
                case UIEvent.CHAMBERCODECRRECT:
                     setPanelActive((bool)message);
                    JIonScee.SetActive((bool)message);                     //邀请码正确弹出框
                    //if ((bool)message == true)
                    //    JionChamber.SetActive(false);
                    break;
                case UIEvent.CHAMBEROFCOMMERRULES:
                    ChamberOfCommerceRules.SetActive((bool)message);       //入会介绍弹出框
                    if ((bool)message == true)
                        SettingtradingCode.SetActive(false);
                    break;
                case UIEvent.CREATCOMMERCE_CHEXCKPASS:
                    TransactionPassword.SetActive(true);
                    break;
                case UIEvent.JOINCHAMBERSUCCESSFUL:
                    //JIonScee.SetActive((bool)message);
                    SuccessfulJoin.SetActive((bool)message);              //成功加入 
                    break;
                default:
                    break;
            }
        }
        void Start()
        {
            JionChamber = transform.Find("JionChamber").gameObject;
            IncorrectPrompt = transform.Find("IncorrectPrompt").gameObject;
            JIonScee = transform.Find("JoinScee").gameObject;
            SettingtradingCode = transform.Find("SettingtradingCode").gameObject;
            ChamberOfCommerceRules = transform.Find("ChamberOfCommerceRules").gameObject;
            ConfirmationPaymentPanel = transform.Find("ConfirmationPaymentPanel").gameObject;
            InsufficientFunds = transform.Find("InsufficientFunds").gameObject;
            TransactionPassword = transform.Find("TransactionPassword").gameObject;
            SuccessfulJoin = transform.Find("Successful").gameObject;
            DetermineBtn = transform.Find("JionChamber/BtnConfirm").GetComponent<Button>();
            CloseBtn = transform.Find("JionChamber/BtnClose").GetComponent<Button>();
            InputChamberCode = transform.Find("JionChamber/InputField").GetComponent<InputField>();
            txt_Text = transform.Find("JionChamber/Text").GetComponent<Text>();
            succTile = JIonScee.transform.Find("MembershipOn/Title").GetComponent<Text>();
            succInstructions = JIonScee.transform.Find("MembershipOn/PromptInformation").GetComponent<Text>();
            ConfindBtn = JIonScee.transform.Find("MembershipOn/GoodBtn").GetComponent<Button>();
            tradingTitle = SettingtradingCode.transform.Find("bg/Title").GetComponent<Text>();
            ErrorMessage = transform.Find("TransactionCodeError").gameObject;
            CloseError = ErrorMessage.transform.Find("Close").GetComponent<Button>();
            ErrorGood = ErrorMessage.transform.Find("OK").GetComponent<Button>();
            InputField = SettingtradingCode.transform.Find("bg/InputExPw").GetComponent<InputField>();
            tradingDeDetermineBtn = SettingtradingCode.transform.Find("bg/BtnCommit").GetComponent<Button>();
            tradingCancel = SettingtradingCode.transform.Find("bg/BtnCancle").GetComponent<Button>();
            ChamberRulesTitle = ChamberOfCommerceRules.transform.Find("Title").GetComponent<Text>();
            MembershipStatement = ChamberOfCommerceRules.transform.Find("RulesFrame").GetComponent<Image>();
            PayBtn = ChamberOfCommerceRules.transform.Find("RulesFrame/PayBtn").GetComponent<Button>();
            description = ConfirmationPaymentPanel.transform.Find("Text").GetComponent<Text>();
            payClose = ConfirmationPaymentPanel.transform.Find("CloseBtn").GetComponent<Button>();
            payDetermine = ConfirmationPaymentPanel.transform.Find("Confirm").GetComponent<Button>();
            payCancle = ConfirmationPaymentPanel.transform.Find("Canle").GetComponent<Button>();
            fundsColse = InsufficientFunds.transform.Find("CloseBtn").GetComponent<Button>();
            GoPayBtn = InsufficientFunds.transform.Find("GoTOPUP").GetComponent<Button>();
            FundsTxt = InsufficientFunds.transform.Find("Text").GetComponent<Text>();
            CloseBtn.onClick.AddListener(clickClose);
            PromptOKBtn = IncorrectPrompt.transform.Find("OK").GetComponent<Button>();
            PromptClose = IncorrectPrompt.transform.Find("Close").GetComponent<Button>();
            TranCloseBtn = TransactionPassword.transform.Find("CloseBtn").GetComponent<Button>();
            TranDetermine= TransactionPassword.transform.Find("ConfirmBtn").GetComponent<Button>();
            TranCanle = TransactionPassword.transform.Find("CancelBtn").GetComponent<Button>();
            TranInput = TransactionPassword.transform.Find("InputField").GetComponent<InputField>();
            TranCloseBtn.onClick.AddListener(clickTranClose);
            TranCanle.onClick.AddListener(clickTranClose);
            TranDetermine.onClick.AddListener(clickTranConfirmation);
            DetermineBtn.onClick.AddListener(clickDetermine);
            PromptClose.onClick.AddListener(clickPromptClose);
            PromptOKBtn.onClick.AddListener(clickPromptClose);
            ConfindBtn.onClick.AddListener(clickOK);
            tradingDeDetermineBtn.onClick.AddListener(SettingTrandingOK);
            tradingCancel.onClick.AddListener(CloseSettingTranding);
            PayBtn.onClick.AddListener(cilckPay);
            payClose.onClick.AddListener(CloseConfirmationPaymentPanel);
            payCancle.onClick.AddListener(CloseConfirmationPaymentPanel);
            GoPayBtn.onClick.AddListener(clickGoPay);
            fundsColse.onClick.AddListener(clickClosePay);
            payDetermine.onClick.AddListener(clickConfirmationPayment);
            CloseError.onClick.AddListener(CloseErrorTrand);
            ErrorGood.onClick.AddListener(CloseErrorTrand);
            ManyLanguages();
            ErrorMessage.SetActive(false);
            IncorrectPrompt.SetActive(false);
            JIonScee.SetActive(false);
            SettingtradingCode.SetActive(false);
            ChamberOfCommerceRules.SetActive(false);
            ConfirmationPaymentPanel.SetActive(false);
            InsufficientFunds.SetActive(false);
            SuccessfulJoin.SetActive(false);
            TransactionPassword.SetActive(false);
            JionChamber.SetActive(false);
            setPanelActive(false);
        }

        /// <summary>
        /// 多语言换图
        /// </summary>
        private void ManyLanguages()
        {
            string language = PlayerPrefs.GetString("language");
            DetermineBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/"+ language+ "/ConfirmBig");
            PromptOKBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/"+ language + "/OK");
            ConfindBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/OK");
            ErrorGood.GetComponent<Image>().sprite= Resources.Load<Sprite>("UI/menu/" + language + "/OK");
            tradingDeDetermineBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/ConfirmBig");
            tradingCancel.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/CancelBig");
            MembershipStatement.sprite = Resources.Load<Sprite>("UI/menu/" + language + "/MembershipRules");
            PayBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/Pay");
            payDetermine.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/ConfirmBig");
            payCancle.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/CancelBig");
            GoPayBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/GoToPrepaid");
            TranCanle.GetComponent<Image>().sprite= Resources.Load<Sprite>("UI/menu/" + language + "/CancelBig");
            TranDetermine.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/ConfirmBig");
        }
    /// <summary>
    /// 关闭
    /// </summary>
        private void clickClose()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            setPanelActive(false);
            ConCamera.IsActivateTouch = true;
        }
        /// <summary>
        /// 确定加入商会
        /// </summary>
        private void clickDetermine()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            string commerceCode = InputChamberCode.text;
            if (commerceCode == null || commerceCode.Equals(""))
            {
                //TODO输入提示
                IncorrectPrompt.SetActive(true);
            }

            Dispatch(AreaCode.NET, ReqEventType.commerce_in, InputChamberCode.text.ToString());
     
        }

        /// <summary>
        /// 关闭邀请码错误提示
        /// </summary>
        private void clickPromptClose()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            IncorrectPrompt.SetActive(false);
        }
        /// <summary>
        /// 好的按钮
        /// </summary>
        private void clickOK()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            if(CacheData.Instance().isHasTradePassword)
            {
                ChamberOfCommerceRules.SetActive(true);
            }
            //else
            //{
            //    SettingtradingCode.SetActive(true);
            //}
           
            JIonScee.SetActive(false);
           
        }
        /// <summary>
        /// 设置交易码
        /// </summary>
        private void SettingTrandingOK()
        {
            string tradingCode = InputField.text;
            if (tradingCode == null || tradingCode.Equals(""))
            {
                //TODO输入提示
                ErrorMessage.SetActive(true);
            }
            else
            {
                Dispatch(AreaCode.NET,ReqEventType.expwshop,tradingCode);
                SettingtradingCode.SetActive(false);
                InputField.text = "";
               // ChamberOfCommerceRules.SetActive(true);
            }           
            // Dispatch(AreaCode.NET, ReqEventType.permission_commerce, InputField.text.ToString());  
        }
        /// <summary>
        /// 取消、关闭设置交易码
        /// </summary>
        private void CloseSettingTranding()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            SettingtradingCode.SetActive(false);
            setPanelActive(false);
            InputField.text = "";
        }
        /// <summary>
        /// 关闭交易码错误提示
        /// </summary>
        private void CloseErrorTrand()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            ErrorMessage.SetActive(false);
        }
        /// <summary>
        /// 去支付
        /// </summary>
        private void cilckPay()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            ChamberOfCommerceRules.SetActive(false);
            ConfirmationPaymentPanel.SetActive(true);
        }
        /// <summary>
        /// 关闭确定支付面板
        /// </summary>
        private void CloseConfirmationPaymentPanel()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            ConfirmationPaymentPanel.SetActive(false);
        }
        /// <summary>
        /// 确认支付
        /// </summary>
        private void clickConfirmationPayment()
        {
            //判断USDT是否足够
            if (CacheData.Instance().Usdt<10)
            {
                InsufficientFunds.SetActive(true);
            }
            else
            {
                //TODO
                // Dispatch(AreaCode.NET, ReqEventType.recharge, address);
              
                    
             
               Dispatch(AreaCode.NET,ReqEventType.permission_commerce, null);

            }

        }
        /// <summary>
        /// 交易密码确定按钮
        /// </summary>
        private void clickTranConfirmation()
        {
            string tradingCode = TranInput.text;
            if (tradingCode == null || tradingCode.Equals(""))
            {
                //TODO输入提示
                ErrorMessage.SetActive(true);
            }
            else
            {
                Dispatch(AreaCode.NET, ReqEventType.confirmPass, TranInput.text.ToString());
            }
        }
        /// <summary>
        /// 取消/关闭交易码
        /// </summary>
        private void clickTranClose()
        {
            TransactionPassword.SetActive(false);
            TranInput.text = "";
        }
       /// <summary>
       /// 去充值
       /// </summary>
        private void clickGoPay()
        {
            Dispatch(AreaCode.UI, UIEvent.QRECODE_PANEL_ACTIVE, MsgTool.CreatQRcode(CacheData.Instance().Address));
        }
        /// <summary>
        /// 关闭去充值面板
        /// </summary>
        private void clickClosePay()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            InsufficientFunds.SetActive(false);
        }
    }
}
