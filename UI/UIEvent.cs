// ReSharper disable InconsistentNaming

using System.CodeDom;

namespace Assets.Scripts.UI
{
    // ReSharper disable once InconsistentNaming
    public class UIEvent 
    {
        
        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// 请求屏蔽操作
        /// </summary>
        public const int LOAD_PANEL_HINDED = int.MinValue;
        /// <summary>
        /// 他人界面
        /// </summary>
        public const int PlayerMenu_Panel = -10;
        /// <summary>
        /// 更新主場景
        /// </summary>
        public const int Updata_Earnings_Panel=-9;
        /// <summary>
        /// 交易明细
        /// </summary>
        public const int TRADERECORD_VIEW = -8;
        /// <summary>
        /// 主界面更新数据
        /// </summary>
        public const int MENU_UPDATE_VIEW = -7;
        /// <summary>
        ///加载中到他人界面
        /// </summary>
        public const int LOADING_ACTIVE = -6;

        /// <summary>
        /// 提示数据
        /// </summary>
        public const int HINT_VIEW = -5;
        /// <summary>
        /// 加载
        /// </summary>
        public const int LOAD_PANEL_ACTIVE = -4;
        /// <summary>
        /// 验证码
        /// </summary>
        public const int REG_PANEL_CODEVIEW = -2;
        /// <summary>
        /// 登入注册选择界面
        /// </summary>
        public const int LOGINSELECT_PANEL_ACTIVE = -1;

        /// <summary>
        /// 登入界面
        /// </summary>
        public const int LOG_ACTIVE = 0; 
        /// <summary>
        ///注册界面
        /// </summary>
        public const int REG_ACTIVE =1;
        /// <summary>
        ///忘记密码界面
        /// </summary>
        public const int Forget_ACTIVE = 2;
        /// <summary>
        ///设置界面
        /// </summary>
        public const int SET_PANEL_ACTIVE = 3;
        /// <summary>
        /// 资产界面
        /// </summary>
        public const int CHARGE_PANEL_ACTIVE = 4;
        /// <summary>
        /// 
        /// </summary>
        public const int GAMEVOICE =5 ;
        /// <summary>
        /// 
        /// </summary>
        public const int GAMEBG = 6;
        /// <summary>
        /// 好友列表界面
        /// </summary>
        public const int FRIENDMENU_PANEL_ACTIVE = 7;
        /// <summary>
        /// 验证码界面
        /// </summary>
        public const int QRECODE_PANEL_ACTIVE = 8;
        /// <summary>
        /// 邮件界面
        /// </summary>
        public const int MSG_PANEL_ACTIVE = 9;
        /// <summary>
        /// 音效界面
        /// </summary>
        public const int VOICE_PANEL_ACTIVE = 10;
        /// <summary>
        /// 安全界面
        /// </summary>
        public const int SECURITY_PANEL_ACTIVE = 11;
        /// <summary>
        /// 好友界面
        /// </summary>
        public const int FRIEND_LIST_PANEL_ACTIVE = 12;
        /// <summary>
        /// 好友列表获取
        /// </summary>
        public const int FRIEND_LIST_PANEL_VIEW = 13;
        /// <summary>
        /// 广场界面
        /// </summary>
        public const int SQUARE_LIST_PANEL_ACTIVE = 14;
        /// <summary>
        /// 广场用户列表获取
        /// </summary>
        public const int SQUARE_LIST_PANEL_VIEW = 15;
        /// <summary>
        /// 自建商会面板
        /// </summary>
        public const int COMMERCE_PANEL_ACTIVE = 16;
        /// <summary>
        /// 没加入商会面板
        /// </summary>
        public const int COMMERCE_NOJIONPANEL_ACTIVE = 17;
        /// <summary>
        /// 语言
        /// </summary>
        public const int LANGUAGE_VIEW = 18;
        /// <summary>
        /// 设置交易密码
        /// </summary>
        public const int SETTRANSACT_ACTIVE = 19;
        /// <summary>
        /// 修改交易密码
        /// </summary>
        public const int CHANGETRADE_ACTIVE = 20;
        /// <summary>
        /// 修改登陆密码
        /// </summary>
        public const int CHANGELONG_ACTIVE = 21;


        /// <summary>
        /// 场景投资界面数据
        /// </summary>
        public const int SENCE_INVEST_VIEW = 22;
        /// <summary>
        /// 主界面数据
        /// </summary>
        public const int MENU_PANEL_VIEW = 23;
        /// <summary>
        /// 申请好友数据
        /// </summary>
        public const int APPLY_PANEL_VIEW = 24;

        /// <summary>
        /// 转账
        /// </summary>
        public const int TRANSFERACCOUNTS_ACTIVE = 25;
        /// <summary>
        /// 充值
        /// </summary>
        public const int TOPUP_ACTIVE = 26;
        /// <summary>
        /// 输入交易码
        /// </summary>
        public const int TRANSACTIONCODE_ACTIVE = 27;
        /// <summary>
        /// 好友申请界面
        /// </summary>
        public const int APPLYFOR_ACTIVE = 28;
        /// <summary>
        /// 获取申请好友列表
        /// </summary>
        public const int APPLYFOR_VIEW = 29;
        /// <summary>
        /// 成功加入商会
        /// </summary>
        public const int SUCCESSFULSHAMBER_ACTIVE = 30;
        /// <summary>
        /// 投资商家界面
        /// </summary>
        public const int IVEST_PANEL_ACTIVE = 31;
        /// <summary>
        /// 选择投资商界面
        /// </summary>
        public const int SELECTINVEST_PANEL_ACTIVE = 32;
        /// <summary>
        /// 选择投资商版本数据
        /// </summary>
        public const int SELECCTINVEST_PANEL_VIEW = 33;
        /// <summary>
        /// 经营中
        /// </summary>
        public const int INVESTED_REPLY_VIEW = 34;
        /// <summary>
        ///可提取
        /// </summary>
        public const int INVESTING_REPLY_VIEW = 35;
        /// <summary>
        /// 邮箱数据
        /// </summary>
        public const int MESSAGE_PANEL_VIEW = 36;
        /// <summary>
        /// 商会成员信息
        /// </summary>
        public const int COMMERCE_PANEL_VIEW = 37;
        /// <summary>
        /// 商会邀请码弹框
        /// </summary>
        public const int Commerce_IsIn_VIEW =38;
        /// <summary>
        /// 加入企业商会弹框
        /// </summary>
        public const int JOIN_Chamber_Commerce_VIEW = 39;
        /// <summary>
        /// 
        /// </summary>
        public const int BusinessLicense_VIEW = 40;
        /// <summary>
        /// 商会升级通知
        /// </summary>
        public const int BusinessPrompt_NOTIVE_VIEW = 41;
        /// <summary>
        /// 分享海报界面
        /// </summary>
        public const int SHARKEPOST_PANEL_VIEW = 42;
        /// <summary>
        /// 商会邀请码正确
        /// </summary>
        public const int CHAMBERCODECRRECT = 43;
        /// <summary>
        /// 商会设置交易码
        /// </summary>
        public const int CHAMBERTRANSACTION = 44;
        /// <summary>
        /// 加入商会介绍
        /// </summary>
        public const int CHAMBEROFCOMMERRULES = 45;
        /// <summary>
        /// 加入商会成功
        /// </summary>
        public const int JOINCHAMBERSUCCESSFUL = 46;

     
       
         /// <summary>
         /// 兑换中心数据
         /// </summary>
        public const int EXCHANGECENTER_STATE_VIEW = 47;
        /// <summary>
        /// 投资数据
        /// </summary>
         public const int INVEST_PANEL_VIEW = 48;
        /// <summary>
        /// 將解鎖的項目
        /// </summary>
        public const int WillUNLOCK_VIEW = 49;



        public const int ConfirmationPaymentPanel_Active = 50;
        /// <summary>
        /// 兑换弹框
        /// </summary>
        public const int EXECHANGE_PANEL_ACTIVE = 51;

        /// <summary>
        /// 兑换中心
        /// </summary>
        public const int EXECHANGECENTER_PANEL_ACTIVE = 52;
        /// <summary>
        /// 好友点赞激活
        /// </summary>
        public const int THUMBUP_PANEL_ACTVATE = 53;
        /// <summary>
        /// 收益面板激活
        /// </summary>
        public const int EARNINGS_PANEL_ACTVTE = 54; 
		
		 /// <summary>
        /// 核对交密码
        /// </summary>
        public const int EnterTradeCode_Panel_Active = 55;

        /// <summary>
        /// 资金不足
        /// </summary>
        public const int BusinessesAreUnderfunded_Panle_Active = 56;
      
        /// <summary>
        /// 已预约
        /// </summary>
        public const int INVEST_REDY_VIEW = 57;
        /// <summary>
        /// 初始预约
        /// </summary>
        public const int INVEST_VIEW = 58;
        /// <summary>
        /// 提示界面
        /// </summary>
        public const int HINT_ACTIVE = int.MaxValue;
    }
}
