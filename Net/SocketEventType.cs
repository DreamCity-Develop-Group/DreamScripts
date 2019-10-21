

/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:          2019/10/02 12:26:39
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/
using System.CodeDom;
namespace Assets.Scripts.Net
{
    public class SocketEventType
    {
        public const string Reply = "token";
        public const string TokenLogin = "tokenCheck";
        /// <summary>
        /// 初始连接
        /// </summary>
        public const string InitConnect = "init";
        /// <summary>
        /// 主页信息
        /// </summary>
        public const string Default= "default";
        /// <summary>
        /// 用戶注冊
        /// </summary>
        public const string Regist = "reg";

        /// <summary>
        /// 密码登入
        /// </summary>
        public const string PassWordLogin = "login";
        /// <summary>
        /// 验证码登入
        /// </summary>
        public const string CodeLogin = "codeLogin";
        /// <summary>
        /// 忘记密码
        /// </summary>
        public const string ForgerPassWord = "pwforget";
        /// <summary>
        /// 
        /// </summary>
        public const string Exit = "exit";
        /// <summary>
        /// 获取验证码
        /// </summary>
        public const string GetCode = "getCode";
        /// <summary>
        /// 设置交易密码
        /// </summary>
        public const string SetExchangePassWord = "expwshop";
        /// <summary>
        /// 好友点赞
        /// </summary>
        public const string LikeFriend = "likefriend";
        /// <summary>
        /// 搜索好友(换一批)
        /// </summary>
        public const string SearchFriend = "searchFriend";
        /// <summary>
        /// 申请列表
        /// </summary>
        public const string ApplyList = "applyFriend";
        /// <summary>
        /// 添加好友
        /// </summary>
        public const string AddFriend = "addFriend";
        /// <summary>
        /// 广场列表
        /// </summary>
        public const string SquareFriends = "squareFriends";
        /// <summary>
        /// 好友列表
        /// </summary>
        public const string FriendList = "friendList";
        /// <summary>
        /// 
        /// </summary>
        public const string NextGround = "";
        /// <summary>
        /// 修改密码
        /// </summary>
        public const string ChangPassWord = "expw";
        /// <summary>
        /// 转账
        /// </summary>
        public const string TransferAccount = "trade/transfer";
        /// <summary>
        /// 充值
        /// </summary>
        public const string Recharge = "recharge";
        /// <summary>
        /// 资产信息
        /// </summary>
        public const string PropertyInfo = "getPlayerAccount";
        /// <summary>
        /// 投资
        /// </summary>
        public const string Invest = "playerInvest";
        /// <summary>
        /// 投资信息
        /// </summary>
        public const string InvestInfo = "getInvestList";
        /// <summary>
        /// 商会获取成员信息
        /// </summary>
        public const string Commerce = "getMembers";
        /// <summary>
        /// 会长发货
        /// </summary>
        public const string SendMt = "sendmt";
        /// <summary>
        /// 购买MT
        /// </summary>
        public const string BuyMt = "buy";
        /// <summary>
        /// 商会添加经营许可
        /// </summary>
        public const string JoinCommerce = "join";
        /// <summary>
        /// 邮件
        /// </summary>
        public const string Message = "getMessageList";
        /// <summary>
        /// 商会升级
        /// </summary>
        public const string CommercePrompt = "push";
        /// <summary>
        /// 上传头像
        /// </summary>
        public const string UpLoadPlayerImg = "uploadPlayerImg";
        /// <summary>
        /// 同意申请
        /// </summary>
        public const string ArgreeApply = "agreeApply";
        /// <summary>
        /// 好友主页
        /// </summary>
        public const string FriendHomePage = "friendHomePage";
        /// <summary>
        /// 确认密码
        /// </summary>
        public const string ConfirmPass = "check/orderPass";
        /// <summary>
        /// 
        /// </summary>
        public const string CheckMoney = "createOrder";
        /// <summary>
        /// 
        /// </summary>
        public const string ExchangeCenter = "get/getSalesOrder";
        /// <summary>
        /// 读取信息
        /// </summary>
        public const string HasReadMessage = "readMessage";
        /// 提现
        /// </summary>
        public const string Withdraw = "withdraw";
        /// <summary>
        /// 修改交易密码/设置交易密码
        /// </summary>
        public const string ChangeShopPass = "expwshop";
        /// <summary>
        /// 加入商会
        /// </summary>
        public const string AddTree = "add";
        /// <summary>
        /// 投资请求
        /// </summary>
        public const string PlayerInvest = "playerInvest";
        /// <summary>
        /// 提取
        /// </summary>
        public const string Extract = "extract";
        /// <summary>
        /// 交易明细
        /// </summary>
        public const string GetTradeList = "trade/getTradeDetailList";
    }
}
