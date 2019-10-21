
/***
  * Title:    ChargePanel 
  *
  * Created:	zp
  *
  * CreatTime:  2019/09/10 11:58:34
  *
  * Description: 资产界面
  *
  * Version:    0.1
  *
  *
***/

using System.Collections.Generic;
using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using Assets.Scripts.Language;
using Assets.Scripts.Model;
using Assets.Scripts.Net;
using Assets.Scripts.Tools;
using Assets.Scripts.UI.Msg;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuUI
{
    public class ChargePanel : UIBase
    {

        private Button transferAccounts;                  //转账按钮
        private Button topUp;                             //充值按钮
        private Button InviteFriends;                     //邀请好友
        private Button transactionRecord;                 //交易记录按钮
        private Button CopyInvitationCode;                //复制邀请码
        private GameObject AssetPanel;                    //资产面板
        private GameObject PanleInviterFrindsBG;          //邀请好友面板
        private GameObject TransactionRecord;             //交易记录面板  
        private GameObject ToHold;                        //保存到相册
        private Button ToHoldBtn;                         //关闭保存提示
        private Button TransctionClose;                   //交易面板关闭
        private Transform TransactionListPraent;          //交易列表父物体
        private Button ShareCloseBtn;
        private List<TradeRecord> List_Transaction = new List<TradeRecord>();  //存储交易记录的所有记录
        private Button SavePicturesToAlbum;                //保存图片到相册
        private Text SaveText;                             //保存相册提示
        //交易记录
        private Text TransactionRecordTitle;               //标题
        private Text TransactionRecordTime;                //时间
        private Text TransactionRecordCategory;            //类别
        private Text TransactionRecordIncomeExpenses;      //收支
        private Text TransactionRecordState;               //状态
        private Text TransactionRecordBalance;             //余额
        private Button TransactionRecordClose;             //关闭

        //动态加载图片
        private Image transferBtn;                    //转账按钮
        private Image TopUpBtn;                       //充值按钮
        private Image InviteFriendsBtn;               //邀请好友按钮

        private Text Title;                            //标题
        private Text UserName;                       //玩家昵称
        private Text AccumulatedEarnings;            //累计收益
        private Text AccumulatedEarningsNum;         //累计收益数  
        private Text TotalAssets;                    //持仓总资产
        private Text TotalAssetsNum;                 //持仓总资产数
        private Text PositionUSDT;                   //持仓USDT
        private Text PositionUSDTNum;                //持仓USDT数
        private Text AvailableUSDT;                  //可用USDT
        private Text AvailableUSDTNum;               //可用USDT数
        private Text FreezeUSDT;                     //冻结USDT
        private Text FreezeUSDTNum;                  //冻结USDT数
        private Text PositionMT;                     //持仓MT;
        private Text positionMTNum;                  //持仓MT数
        private Text AvailableMT;                    //可用MT
        private Text AvailableNum;                   //可用MT数
        private Text FreezeMT;                       //冻结MT
        private Text FreezeMTNum;                    //冻结MT数
        private Text ChamberOfCommerceLV;            //商会等级
        private Text ChamberLV;                      //商会几星等级(设置玩家的商会等级的文本)
        private Text ChamberOfCommerceMembers;       //商会成员
        private Text ChamberNum;                     //商会成员数 
        private Text TxtInviteCode;                  //文字邀请码
        private Text InviteCode;                     //邀请码
        private Text TxtTransactionRecords;          //交易记录 

        private PropertyInfo propertyInfo;

        /// <summary>
        /// 多语言
        /// </summary>
        private void Multilingual()
        {
            string language = PlayerPrefs.GetString("language");
            //多语系动态加载图片
            transferBtn.sprite = Resources.Load<Sprite>("UI/menu/" + language + "/TransferAccounts");
            TopUpBtn.sprite = Resources.Load<Sprite>("UI/menu/" + language + "/TopUp");
            InviteFriendsBtn.sprite = Resources.Load<Sprite>("UI/menu/" + language + "/InviteFriends");
            ToHold.GetComponent<Image>().sprite= Resources.Load<Sprite>("UI/menu/" + language + "/SavePhoto");
            //Title.text = "";
            //AccumulatedEarnings.text = "";
            //TotalAssets.text = "";
            //PositionUSDT.text = "";
            //FreezeUSDT.text = "";
            //PositionMT.text = "";
            //AvailableMT.text = "";
            //FreezeMT.text = "";
            //ChamberOfCommerceLV.text = "";
            //ChamberOfCommerceMembers.text = "";
            //TxtInviteCode.text = "";
            //TxtTransactionRecords.text = "";
            //SaveText.text = "";
            //TransactionRecordTitle.text = "";
            //TransactionRecordTime.text = "";
            //TransactionRecordCategory.text = "";
            //TransactionRecordIncomeExpenses.text = "";
            //TransactionRecordState.text = "";
            //TransactionRecordBalance.text = "";
        }

    private void Awake()
    {
        Bind(UIEvent.CHARGE_PANEL_ACTIVE,UIEvent.TRADERECORD_VIEW);
    }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.CHARGE_PANEL_ACTIVE:
                    propertyInfo = message as PropertyInfo;
                    Init();
                    setPanelActive(true);
                    break;
                case UIEvent.TRADERECORD_VIEW:
                    TransactionRecord.SetActive(true);
                    List<TradeRecord> msg=new List<TradeRecord>();
                    if (message is TradeRecordList tradeRecords)
                    {
                        for (int i = List_Transaction.Count; i < tradeRecords.tradeRecordList.Count; i++)
                        {
                            msg.Add(tradeRecords.tradeRecordList[i]);
                            List_Transaction.Add(tradeRecords.tradeRecordList[i]);
                        }
                        UpdateTradeRecord(msg);
                        msg = null;
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 资产数据
        /// </summary>
        private void Init()
        {
            UserName.text = CacheData.Instance().nick;
            AccumulatedEarningsNum.text = propertyInfo.accumulated_total_income.ToString();
            TotalAssetsNum.text = propertyInfo.total_property;
            PositionUSDTNum.text = propertyInfo.total_usdt;
            AvailableUSDTNum.text = propertyInfo.available_usdt;
            FreezeUSDTNum.text = propertyInfo.frozen_usdt;
            positionMTNum.text = propertyInfo.total_mt;
            AvailableNum.text = propertyInfo.available_mt;
            FreezeMTNum.text = propertyInfo.frozen_mt;
            ChamberLV.text = propertyInfo.commerce_lv;
            ChamberNum.text = propertyInfo.commerce_member;
            if (CacheData.Instance().CommerceState == 2)
                InviteCode.text = propertyInfo.invite;
            else
                InviteCode.text = "";

        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            btnClose.onClick.RemoveAllListeners();
        }

        Button btnClose;

        private void Start()
        {
            promptMsg = new HintMsg();
            AssetPanel = transform.Find("bg").gameObject;
            transferBtn = transform.Find("bg/BtnTransfer").GetComponent<Image>();
            TopUpBtn = transform.Find("bg/BtnRecharge").GetComponent<Image>();
            InviteFriendsBtn = transform.Find("bg/BtnApplyFreind").GetComponent<Image>();
            ToHold = transform.Find("ToHold").gameObject;
            ToHoldBtn = ToHold.GetComponent<Button>();
            PanleInviterFrindsBG = transform.Find("PanleInviterFrindsBG").gameObject;
            TransactionRecord = transform.Find("TransactionRecord").gameObject;
            transferAccounts = transform.Find("bg/BtnTransfer").GetComponent<Button>();
            topUp = transform.Find("bg/BtnRecharge").GetComponent<Button>();
            InviteFriends = transform.Find("bg/BtnApplyFreind").GetComponent<Button>();
            transactionRecord = transform.Find("bg/BtnChargeLog").GetComponent<Button>();
            CopyInvitationCode = transform.Find("bg/BtnCopy").GetComponent<Button>();
            btnClose = transform.Find("bg/BtnClose").GetComponent<Button>();
            TransctionClose = TransactionRecord.transform.Find("Frame/BtnColse").GetComponent<Button>();
            TransactionListPraent = TransactionRecord.transform.Find("Frame/RecordList/Viewport/Content");
            ShareCloseBtn = PanleInviterFrindsBG.transform.Find("PanleInviteFriends/CloseBtn").GetComponent<Button>();
            ShareCloseBtn.onClick.AddListener(clickShareClose);

            UserName = transform.Find("bg/NickName").GetComponent<Text>();
            AccumulatedEarnings = transform.Find("bg/NameEarning").GetComponent<Text>();
            AccumulatedEarningsNum = transform.Find("bg/TextAllEarning").GetComponent<Text>();
            TotalAssets = transform.Find("bg/NameHold").GetComponent<Text>();
            TotalAssetsNum = transform.Find("bg/TextAllHold").GetComponent<Text>();
            TxtTransactionRecords = transform.Find("bg/BtnChargeLog/Text").GetComponent<Text>();
            Title = transform.Find("bg/Title").GetComponent<Text>();
            PositionUSDT = transform.Find("bg/HoldUSDT").GetComponent<Text>();
            PositionUSDTNum = transform.Find("bg/HoldNum").GetComponent<Text>();
            AvailableUSDT = transform.Find("bg/AvailableUSDT").GetComponent<Text>();
            AvailableUSDTNum = transform.Find("bg/AvailableNum").GetComponent<Text>();
            FreezeUSDT = transform.Find("bg/FreezeUSDT").GetComponent<Text>();
            FreezeUSDTNum = transform.Find("bg/FreezeNum").GetComponent<Text>();
            PositionMT = transform.Find("bg/HoldMT").GetComponent<Text>();
            positionMTNum = transform.Find("bg/HoldNumMT").GetComponent<Text>();
            AvailableMT = transform.Find("bg/AvailableMT").GetComponent<Text>();
            AvailableNum = transform.Find("bg/AvailableNumMT").GetComponent<Text>();
            FreezeMT = transform.Find("bg/FreezeMT").GetComponent<Text>();
            FreezeMTNum = transform.Find("bg/FreezeNumMT").GetComponent<Text>();
            ChamberOfCommerceLV = transform.Find("bg/ChamberLV").GetComponent<Text>();
            ChamberOfCommerceMembers = transform.Find("bg/ChamberMembers").GetComponent<Text>();
            ChamberLV = ChamberOfCommerceLV.transform.Find("LV").GetComponent<Text>();
            ChamberNum = ChamberOfCommerceMembers.transform.Find("Num").GetComponent<Text>();
            TxtInviteCode = transform.Find("bg/InviteCode").GetComponent<Text>();
            InviteCode = TxtInviteCode.transform.Find("Code").GetComponent<Text>();
            SavePicturesToAlbum = transform.Find("PanleInviterFrindsBG/PanleInviteFriends").GetComponent<Button>();
            TransactionRecordTitle = TransactionRecord.transform.Find("Frame/Title").GetComponent<Text>();
            TransactionRecordTime = TransactionRecord.transform.Find("Frame/Classify/Time").GetComponent<Text>();
            TransactionRecordCategory = TransactionRecord.transform.Find("Frame/Classify/Category").GetComponent<Text>();
            TransactionRecordIncomeExpenses = TransactionRecord.transform.Find("Frame/Classify/IncomeExpenses").GetComponent<Text>();
            TransactionRecordState = TransactionRecord.transform.Find("Frame/Classify/State").GetComponent<Text>();
            TransactionRecordBalance = TransactionRecord.transform.Find("Frame/Classify/Balance").GetComponent<Text>();
            TransactionRecordClose= TransactionRecord.transform.Find("Frame/BtnColse").GetComponent<Button>();
            SaveText = PanleInviterFrindsBG.transform.Find("PanleInviteFriends/Text").GetComponent<Text>();

            transferAccounts.onClick.AddListener(clickTransferAccounts);
            topUp.onClick.AddListener(clickTopUp);
            InviteFriends.onClick.AddListener(clickInviteFriends);
            transactionRecord.onClick.AddListener(clicktransactionRecord);
            CopyInvitationCode.onClick.AddListener(clickCopyInvitationCode);
            btnClose.onClick.AddListener(clickClose);
            TransctionClose.onClick.AddListener(clickTransactionCosle);
            SavePicturesToAlbum.onClick.AddListener(SavePhoto);
            ToHoldBtn.onClick.AddListener(CloseSavePhoto);
            TransactionRecordClose.onClick.AddListener(CloseTrancation);
            setPanelActive(false);
            PanleInviterFrindsBG.SetActive(false);
            ToHold.SetActive(false);
            TransactionRecord.SetActive(false);
           
            Multilingual();
        }
        /// <summary>
        /// 交易明细
        /// </summary>
        private void UpdateTradeRecord(List<TradeRecord> msg)
        {
            if (msg.Count > 0)
            {
                for (int i = 0; i < msg.Count; i++)
                {
                    GameObject obj = null;
                    if (i % 2 == 0)
                    {
                        obj = Resources.Load("PerFab/RecordItme0") as GameObject;
                        obj = CreatePreObj(obj, TransactionListPraent);
                    }
                    else
                    {
                        obj = Resources.Load("PerFab/RecordItme1") as GameObject;
                        obj = CreatePreObj(obj, TransactionListPraent);
                    }
                    //找到Test，赋值信息
                    obj.transform.Find("Time").GetComponent<Text>().text = msg[i].createTime;
                    obj.transform.Find("Classes").GetComponent<Text>().text = msg[i].tradeDetailType;
                    obj.transform.Find("IncomeAndExpense").GetComponent<Text>().text = msg[i].tradeAmount.ToString("#0.00");
                    obj.transform.Find("State").GetComponent<Text>().text = msg[i].tradeStatus;
                    obj.transform.Find("Balance").GetComponent<Text>().text = msg[i].usdtSurplus.ToString("#0.00");
                }
            }
        }


        private HintMsg promptMsg;
        /// <summary>
        /// 关闭资产面板
        /// </summary>
        private void clickClose()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            setPanelActive(false);
            ConCamera.IsActivateTouch = true;
        }
        /// <summary>
        /// 转账
        /// </summary>
        private void clickTransferAccounts()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.UI, UIEvent.TRANSFERACCOUNTS_ACTIVE, true);
        }
        /// <summary>
        /// 充值
        /// </summary>
        private void clickTopUp()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.UI, UIEvent.QRECODE_PANEL_ACTIVE, MsgTool.CreatQRcode("0x8dbd8843d9e9de809c19ed53e0403475c987ab15"));
        }
        /// <summary>
        /// 邀请好友
        /// </summary>
        private void clickInviteFriends()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.UI, UIEvent.SHARKEPOST_PANEL_VIEW, true);
            //PanleInviterFrindsBG.SetActive(true);
        }
        /// <summary>
        /// 交易记录
        /// </summary>
        private void clicktransactionRecord()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.NET,ReqEventType.GetTradeCord,null);
            
        }
        /// <summary>
        /// 关闭交易面板
        /// </summary>
        private void clickTransactionCosle()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            TransactionRecord.SetActive(false);
        }

        /// <summary>
        /// 复制邀请码
        /// </summary>
        private void clickCopyInvitationCode()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
#if UNITY_ANDROID
            if (!string.IsNullOrEmpty(InviteCode.text))
            {
                //"com.inode.dreamcity.SaveImageActivity"
                using (var test = new AndroidJavaObject("com.inode.dreamcity.SaveImageActivity"))
                {
                    if (test.Call<bool>("CopyText", InviteCode.text))
                    {
                        promptMsg.Change(LanguageService.Instance.GetStringByKey("复制成功", string.Empty), Color.white);
                        Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                    }
                }
            }
#endif

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

            Transform trans = null;
            trans = GameObject.Instantiate(Prefab, m_transPerfab).transform;
            trans.localPosition = Vector3.zero;
            trans.localRotation = Quaternion.identity;
            trans.localScale = Vector3.one;
            obj = trans.gameObject;
            obj.SetActive(true);

            return obj;
        }
        /// <summary>
        /// 保存相册
        /// </summary>
        private void SavePhoto()
        {
           // PanleInviterFrindsBG.SetActive(false);
            AssetPanel.SetActive(false);
        }
        /// <summary>
        /// 关闭保存
        /// </summary>
        private void CloseSavePhoto()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            ToHold.SetActive(false);
        }
        /// <summary>
        /// 关闭交易记录
        /// </summary>
        private void CloseTrancation()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            TransactionRecord.SetActive(false);
        }
        private void clickShareClose()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            PanleInviterFrindsBG.SetActive(false);
        }
    }
}
