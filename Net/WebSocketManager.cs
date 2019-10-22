using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using Assets.Scripts.Framework;
using Assets.Scripts.Model;
using Assets.Scripts.Net.Code;
using Assets.Scripts.Net.Handler;
using Assets.Scripts.Net.Request;
using Assets.Scripts.Audio;
using Assets.Scripts.Language;
using Assets.Scripts.Scenes;
using Assets.Scripts.Scenes.Msg;
using Assets.Scripts.Tools;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Msg;
using LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Net
{
    public class WebSocketManager : ManagerBase
    {
        public static WebSocketManager Instance = null;

        private void Awake()
        {
            Instance = this;
            Add(0, this);
        }
        #region 处理发送服务器的请求

        private AccountRequestMsg accountRequestMsg = new AccountRequestMsg();
        private FriendRequestMsg friendRequestMsg = new FriendRequestMsg();
        private SetRequestMsg setRequestMsg = new SetRequestMsg();
        private CommerceRequsetMsg commerceRequsetMsg = new CommerceRequsetMsg();
        private InvestRequestMsg investRequestMsg = new InvestRequestMsg();
        private SocketMsg<Dictionary<string, string>> socketMsg;
        private SocketMsg<AccountInfo> accountSocketMsg = new SocketMsg<AccountInfo>();
        private SocketMsg<SquareUser> squareMsg;
        private SocketMsg<ReqCommerceInfo> reqCommerceSocketMsg = new SocketMsg<ReqCommerceInfo>();
        private SocketMsg<TransferInfo> reqTrasferSocketMsg = new SocketMsg<TransferInfo>();
        protected internal override void Execute(int eventCode, object message)
        {
            //发一次请求触发一次点击音效,（排除点赞，可提取，商会升级）

            if (PlayerPrefs.GetString("GameAudioIsOpen") == "open")
            {
                if (eventCode == ReqEventType.likefriend)
                {
                    Dispatch(AreaCode.AUDIO, AudioEvent.LIKE_CLICK_AUDIO, "LikeVoice");
                }
                else if (eventCode == ReqEventType.cach_withdrawal)
                {
                    Dispatch(AreaCode.AUDIO, AudioEvent.EXACTABLE_AUDIO, "ExactableVoice");
                }
                else
                {
                    Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
                }
            }


            // Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, true);
            //初始化联接操作
            if (_wabData.WebSocket == null || eventCode == ReqEventType.init)
            {
                if (PlayerPrefs.HasKey("username"))
                {
                    CacheData.Instance().Username = PlayerPrefs.GetString("username");
                }
                Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, true);
                _wabData.OpenWebSocket();
                //登入断线重连
                if (PlayerPrefs.HasKey("token") && _wabData.WebSocket.IsAlive)
                {
                    _wabData.WebSocket.Send("token&&"+ CacheData.Instance().Username);
                }
                //        // ["token"] = CacheData.Instance().Token
                //        ["token"] = PlayerPrefs.GetString("token")
                //    };
                //    _wabData.SendMsg(logMsg);
                //}
                return;
            }

            if (_wabData.WebSocket != null && _wabData.WebSocket.IsAlive)
            {

                switch (eventCode)
                {
                    case ReqEventType.pwlogin:
                        //密码登入操作
                        socketMsg = accountRequestMsg.ReqPWLoginMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, true);
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.idlogin:
                        //验证码登入
                        socketMsg = accountRequestMsg.ReqIDLoginMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, true);
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.regist:
                        //注册操作
                        socketMsg = accountRequestMsg.ReqRegMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, true);
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.pwforget:
                        //忘记密码
                        socketMsg = accountRequestMsg.ReqForgetMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, true);
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.addfriend:
                        //添加好友
                        socketMsg = friendRequestMsg.ReqAddFriendMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, true);
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.identy:
                        //获取验证码
                        socketMsg = accountRequestMsg.ReqGetIdentityMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, true);
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.expw:
                        //修改密码
                        socketMsg = setRequestMsg.ReqPWChangeMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, true);
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.expwshop:
                        //设置交易密码
                         socketMsg = setRequestMsg.ReqExPwShopMsg(message);
                        //socketMsg = setRequestMsg.ReqPWChangeMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, true);
                        _wabData.SendMsg(socketMsg);
                        break;
                
                    //case ReqEventType.voiceset:
                    //    //音效设置
                    //    socketMsg = setRequestMsg.ReqVoiceSetMsg(message);
                    //    _wabData.SendMsg(socketMsg);
                    //    break;
                    case ReqEventType.searchfriend:
                        //搜索用户
                        socketMsg = friendRequestMsg.ReqSearchUserMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, true);
                        _wabData.SendMsg(socketMsg);

                        break;
                    case ReqEventType.likefriend:
                        //好友点赞
                        socketMsg = friendRequestMsg.ReqLikeFriendMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, true);
                        _wabData.SendMsg(socketMsg);

                        break;
                    case ReqEventType.applytofriend:
                        //申请通过/拒绝

                        socketMsg = friendRequestMsg.ReqAgreeFriendMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, true);
                        _wabData.SendMsg(socketMsg);

                        break;
                    case ReqEventType.property:
                        //测试资产请求
                        socketMsg = accountRequestMsg.ReqPropertyTestMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, true);
                        _wabData.SendMsg(socketMsg);

                        break;
                    case ReqEventType.nextgrouds:
                        //换一批
                        socketMsg = friendRequestMsg.ReqNextUserList(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, true);
                        _wabData.SendMsg(socketMsg);

                        break;
                    case ReqEventType.commerce_member:
                        //商会成员信息请求
                        reqCommerceSocketMsg = commerceRequsetMsg.ReqCommerceMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(reqCommerceSocketMsg);
                        Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, true);
                        break;
                    case ReqEventType.commerce_in:
                        //商会加入请求
                        reqCommerceSocketMsg = commerceRequsetMsg.ReqComeCommerceMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(reqCommerceSocketMsg);
                        break;
                    case ReqEventType.transfer:
                        reqTrasferSocketMsg = accountRequestMsg.ReqTransferMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(reqTrasferSocketMsg);
                        break;
                    case ReqEventType.recharge:
                        socketMsg = accountRequestMsg.ReqRechargeMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.commerce_sendmt:
                        reqCommerceSocketMsg = commerceRequsetMsg.ReqSendMTMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(reqCommerceSocketMsg);
                        break;
                    case ReqEventType.invest_req:
                        socketMsg = investRequestMsg.ReqInvestMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.squarefriend:
                        socketMsg = friendRequestMsg.ReqSquareMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.menu_req:
                        socketMsg = accountRequestMsg.ReqMenuMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.change_expwshop:
                        socketMsg = setRequestMsg.ReqPWShopChangeMsg(message);
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.applyfriend:
                        socketMsg = friendRequestMsg.ReqApplyFriendList(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.permission_commerce:
                        reqCommerceSocketMsg = commerceRequsetMsg.ReqPermissionCommerceMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(reqCommerceSocketMsg);
                        break;
                    case ReqEventType.buyMt:
                        reqCommerceSocketMsg = commerceRequsetMsg.ReqBuyMTMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(reqCommerceSocketMsg);
                        break;
                    case ReqEventType.confirmPass:
                        socketMsg = accountRequestMsg.ReqCheckPass(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.checkMoney:
                        socketMsg = accountRequestMsg.ReqCheckMoney(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.ExchangeCenter:
                        reqCommerceSocketMsg = commerceRequsetMsg.ReqExchangeCenterMsg(message);
                        if (reqCommerceSocketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(reqCommerceSocketMsg);
                        break;
                    case ReqEventType.Extract:
                        socketMsg = investRequestMsg.ReqExtractInfoMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.invest_info:
                        socketMsg = investRequestMsg.ReqInvestInfoMsg(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.GetTradeCord:
                        socketMsg = accountRequestMsg.ReqTradeRecord(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.GetMessage:
                        socketMsg = accountRequestMsg.ReqMessage(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.ReadState:
                        socketMsg = accountRequestMsg.ReqHasReadMessage(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.GetLike:
                        socketMsg = friendRequestMsg.ReqLikeFriend(message);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(socketMsg);
                        break;
                    case ReqEventType.exit:
                        socketMsg = accountRequestMsg.ReqExitMsg(null);
                        if (socketMsg == null)
                        {
                            return;
                        }
                        _wabData.SendMsg(socketMsg);
                        //_wabData.WebSocket.Close(1000, "Bye!");

                       // SceneManager.LoadScene("login");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Debug.LogError("连接断开");
                StartCoroutine(ReConnect());
            }

        }
        #endregion
        #region Private Fields

        /// <summary>  
        /// The WebSocket address to connect  
        /// </summary>  
        private string _address;

        /// <summary>  
        /// Debug text to draw on the gui  
        /// </summary>  
        private string _text;

        /// <summary>  
        /// GUI scroll position  
        /// </summary>  
        private Vector2 _scrollPos;

        private WebData _wabData;

        #endregion
        private void Start()
        {
            _wabData = WebData.Instance();
            _address = _wabData.Address;
        }

        private void Update()
        {
            if (_wabData.MsgQueue.Count > 0)
            {
                SocketMsg<Dictionary<string, object>> info = _wabData.MsgQueue.Dequeue();
                processSocketMsg(info);
                Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, false);
            }
            if (_wabData.SquareQueue.Count > 0)
            {
                SocketMsg<SquareUser> squareinfo = _wabData.SquareQueue.Dequeue();
                processSquareMsg(squareinfo);

            }
            if (_wabData.MenuQueue.Count > 0)
            {
                SocketMsg<MenuInfo> menuinfo = _wabData.MenuQueue.Dequeue();
                processMenuMsg(menuinfo);

            }

            if (_wabData.InvestQueue.Count > 0)
            {
                SocketMsg<InvestList> investinfo = _wabData.InvestQueue.Dequeue();
                processInvestSocketMsg(investinfo);
            }

            if (_wabData.MessageQueue.Count > 0)
            {
                SocketMsg<MessageInfoList> messageinfo = _wabData.MessageQueue.Dequeue();
                processMessageSocketMsg(messageinfo);
            }

            if (_wabData.ReplyQueue.Count > 0)
            {
                //Dispatch(UIEvent.);
                //TODO 同步数据及时处理
                _wabData.ReplyQueue.Dequeue();
                Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, false);
            }

            if (_wabData.CommerceQueue.Count > 0)
            {
                SocketMsg<CommerceInfo> membersInfo=_wabData.CommerceQueue.Dequeue();
                processCommerceSocketMsg(membersInfo);
            }
            if (_wabData.ExchangeQueue.Count > 0)
            {
                SocketMsg < ExchangeInfos > exchangeInfo = _wabData.ExchangeQueue.Dequeue();
                processsExchangeSocketMsg(exchangeInfo);
            }

            if (_wabData.TradeRecordQueue .Count> 0)
            {
                SocketMsg<TradeRecordList> tradeRecordInfo = _wabData.TradeRecordQueue.Dequeue();
                processsTradeRecordSocketMsg(tradeRecordInfo);
            }
        }


        private HintMsg promptMsg = new HintMsg();
        public bool ReConnectState = false;
        /// <summary>
        /// 断线重连
        /// </summary>
        public IEnumerator ReConnect()
        {
            ReConnectState = true;
            while (WebData.Instance().RecTimes <= 5)
            {
                yield return new WaitForSeconds(1);
                if (!WebData.Instance().IsReconnect)
                {
                    Debug.Log("第" + WebData.Instance().RecTimes + "次重连尝试");
                    //WebData.Instance().OpenWebSocket();
                }
                else
                {
                    ReConnectState = false;
                    break;
                }
                WebData.Instance().RecTimes += 1;
            }
            if (WebData.Instance().RecTimes > 5)
            {
                promptMsg.Change(LanguageService.Instance.GetStringByKey("与服务器断开连接", string.Empty),Color.white);
                Debug.LogError("网络断开");
                SceneMsg msg = new SceneMsg("login",
                    delegate ()
                    {
                        Debug.Log("场景加载完成");
                        Dispatch(AreaCode.UI, UIEvent.LOGINSELECT_PANEL_ACTIVE, true);
                    });
                WebData.isLogin = false;
                Dispatch(AreaCode.SCENE, SceneEvent.MENU_PLAY_SCENE, msg);
                StopAllCoroutines();
                WebData.Instance().RecTimes = 0;
            }


        }

        #region 处理接收到的服务器发来的消息

        private HandlerBase accountHandler = new AccoutHandler();
        private Dictionary<string, object> dicRegLogRespon;
        private SquareUser squareData;
        private InvestHandler investHandler = new InvestHandler();
        private HandlerBase setHandler = new SetHandler();
        private FriendHandler friendHandler = new FriendHandler();
        private CommerceHander commerceHander = new CommerceHander();
        /// <summary>
        /// 处理接收到的服务器发来的消息模块
        /// </summary>
        /// <param name="msg"></param>
        /// 
        private void processMsg(SocketMsg<MenuInfo> msg)
        {
            //test
            string jsonmsg = JsonMapper.ToJson(msg);
            //Dispatch(AreaCode.UI, UIEvent.TEST_PANEL_ACTIVE, "reciveMsg"+ MsgTool.utf8_gb2312(jsonmsg));
            switch (msg.data.type)
            {
                case "default":
                    //todo 缓存
                    Dispatch(AreaCode.UI, UIEvent.MENU_PANEL_VIEW, msg.data.data);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 交易明细
        /// </summary>
        /// <param name="msg"></param>
        private void processsTradeRecordSocketMsg(SocketMsg<TradeRecordList> msg)
        {
            switch (msg.data.type)
            {
                case SocketEventType.GetTradeList:
                    accountHandler.OnReceive(ReqEventType.GetTradeCord, msg.data.data);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        private void processsExchangeSocketMsg(SocketMsg<ExchangeInfos> msg)
        {
            switch (msg.data.type)
            {
                case SocketEventType.ExchangeCenter:
                    commerceHander.OnReceive(ReqEventType.ExchangeCenter,msg.data.data);
                    break;
            }
        }
        /// <summary>
        /// 商会
        /// </summary>
        /// <param name="msg"></param>
        private void processCommerceSocketMsg(SocketMsg<CommerceInfo> msg)
        {
            //判断商会升级音效

            if (PlayerPrefs.GetString("GameAudioIsOpen") == "open" && msg.data.type == SocketEventType.CommercePrompt)
            {
                Dispatch(AreaCode.AUDIO, AudioEvent.EXACTABLE_AUDIO, "CommercePrompt");
            }
            switch (msg.data.type)
            {
                case SocketEventType.Commerce:
                    //todo 缓存
                   // Dispatch(AreaCode.UI, UIEvent.MESSAGE_PANEL_VIEW, msg.data.data);
                    //Dispatch();
                    commerceHander.OnReceive(ReqEventType.commerce_member, msg.data.data);
                    // Dispatch();
                    break;
                    // Dispatch();

            }
        }

        private void processMessageSocketMsg(SocketMsg<MessageInfoList> msg)
        {
            switch (msg.data.type)
            {
                case SocketEventType.Message:
                    //todo 缓存
                    Dispatch(AreaCode.UI, UIEvent.MESSAGE_PANEL_VIEW, msg.data.data);

                    break;
            }
        }
        /// <summary>
        /// 主页
        /// </summary>
        /// <param name="msg"></param>
        private void processMenuMsg(SocketMsg<MenuInfo> msg)
        {
            switch (msg.data.type)
            {
                case "default":
                    //todo 缓存
                    CacheData.Instance().isHasTradePassword = msg.data.data.account.isHasTradePassword;
                    Dispatch(AreaCode.UI, UIEvent.MENU_PANEL_VIEW, msg.data.data);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 广场
        /// </summary>
        /// <param name="msg"></param>
        private void processSquareMsg(SocketMsg<SquareUser> msg)
        {

            switch (msg.data.type)
            {
                case SocketEventType.SquareFriends:
                    SquareUser squareUser = msg.data.data as SquareUser;
                    friendHandler.OnReceive(ReqEventType.squarefriend, squareUser);
                    break;
                case SocketEventType.ApplyList:
                    SquareUser applyUser = msg.data.data as SquareUser;
                    friendHandler.OnReceive(ReqEventType.applyfriend, applyUser);
                    break;
                case SocketEventType.FriendList:
                    SquareUser friendUser = msg.data.data as SquareUser;
                    friendHandler.OnReceive(ReqEventType.listfriend, friendUser);
                    break;
                case SocketEventType.SearchFriend:
                    SquareUser searchUser = msg.data.data as SquareUser;
                    friendHandler.OnReceive(ReqEventType.searchfriend, searchUser);
                    break;
                case SocketEventType.NextGround:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 投资
        /// </summary>
        /// <param name="msg"></param>
        private void processInvestSocketMsg(SocketMsg<InvestList> msg)
        {
            if (msg?.data == null)
            {
                Debug.Log("message is null");
                return;
            }

            switch (msg.data.type)
            {
                case SocketEventType.InvestInfo:

                    investHandler.OnReceive(ReqEventType.invest_info, msg.data.data);
                    break;
            }
        }
        /// <summary>
        /// 账户
        /// </summary>
        /// <param name="msg"></param>
        private void processSocketMsg(SocketMsg<Dictionary<string, object>> msg)
        {

            if (msg?.data == null)
            {
                Debug.Log("message is null");
                return;
            }

            dicRegLogRespon = msg.data.data as Dictionary<string, object>;

            switch (msg.data.type)
            {
                case SocketEventType.InitConnect:
                    accountHandler.OnReceive(ReqEventType.init, msg.target);
                    //_wabData.ThreadStart();
                    break;
                case SocketEventType.PassWordLogin:
                    if (accountHandler.OnReceive(ReqEventType.login, msg.data.code))
                    {
                        if (dicRegLogRespon.ContainsKey("token"))
                        {
                            //  CacheData.Instance().Token= dicRegLogRespon["token"].ToString();
                            PlayerPrefs.SetString("token", dicRegLogRespon["token"].ToString());
                            PlayerPrefs.SetString("playerId", dicRegLogRespon["playerId"].ToString());

                        }
                        Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_ACTIVE, true);
                        WebData.isLogin = true;
                    }
                    break;
                case SocketEventType.CodeLogin:
                   
                    if (accountHandler.OnReceive(ReqEventType.login, msg.data.code))
                    {
                        if (dicRegLogRespon.ContainsKey("token"))
                        {
                            //CacheData.Instance().Token= dicRegLogRespon["token"].ToString();
                            PlayerPrefs.SetString("token", dicRegLogRespon["token"].ToString());
                            PlayerPrefs.SetString("playerId", dicRegLogRespon["playerId"].ToString());
                        }
                        WebData.isLogin = true;
                    }
                    break;
                case SocketEventType.Regist:
                    
                    accountHandler.OnReceive(ReqEventType.regist, msg.data.code);
                    break;
                case SocketEventType.ChangPassWord:
                   
                    setHandler.OnReceive(ReqEventType.expw, msg.data.code);
                    break;
                case SocketEventType.SetExchangePassWord:
                    
                    setHandler.OnReceive(ReqEventType.expwshop, msg.data.code);
                    break;
                case SocketEventType.SendMt:
                    commerceHander.OnReceive(ReqEventType.commerce_sendmt, dicRegLogRespon);
                    break;
                case SocketEventType.AddFriend:
                   
                    friendHandler.OnReceive(ReqEventType.addfriend, msg.data.code);
                    break;
                case SocketEventType.LikeFriend:
                    // friendHandler.OnReceive(ReqEventType.likefriend, msg.data.t["code"]);
                    break;
                //case SocketEventType.SearchFriend:
                //    SquareUser searchUser = msg.data.t as SquareUser;
                //    friendHandler.OnReceive(ReqEventType.searchfriend, msg.data.t);
                //    break;
                case SocketEventType.GetCode:
                   
                    accountHandler.OnReceive(ReqEventType.identy, msg.data.data["code"]);
                    break;
                case SocketEventType.ForgerPassWord:
                   
                    accountHandler.OnReceive(ReqEventType.pwforget, msg.data.code);
                    break;
                case SocketEventType.PropertyInfo:
                    PropertyInfo propertyInfo = new PropertyInfo();
                    propertyInfo.accumulated_total_income = double.Parse(msg.data.data["total_income"].ToString());
                    propertyInfo.total_property = msg.data.data["total_property"].ToString();
                    propertyInfo.total_usdt = msg.data.data["total_usdt"].ToString();
                    propertyInfo.total_mt = msg.data.data["total_mt"].ToString();
                    propertyInfo.available_usdt = msg.data.data["available_usdt"].ToString();
                    propertyInfo.available_mt = msg.data.data["available_mt"].ToString();
                    propertyInfo.frozen_usdt = msg.data.data["frozen_usdt"].ToString();
                    propertyInfo.frozen_mt = msg.data.data["frozen_mt"].ToString();
                    //propertyInfo.commerce_lv = msg.data.data["commerce_lv"].ToString();
                    propertyInfo.commerce_member = msg.data.data["commerce_member"].ToString();
                    propertyInfo.invite = msg.data.data["invite"].ToString();
                    accountHandler.OnReceive(ReqEventType.property, propertyInfo);
                    break;
                case SocketEventType.TransferAccount:
                    accountHandler.OnReceive(ReqEventType.transfer, msg.data);
                    break;
                case SocketEventType.Recharge:
                    //accountHandler
                    break;
                case SocketEventType.PlayerInvest:
                    investHandler.OnReceive(ReqEventType.invest_req, msg.data.data);
                    break;
                case SocketEventType.AddTree:
                    commerceHander.OnReceive(ReqEventType.commerce_in, msg.data.code);
                    break;
                case SocketEventType.CommercePrompt:
                    commerceHander.OnReceive(ReqEventType.commercePrompt, msg.data.code);
                    break;
                case SocketEventType.JoinCommerce:
                    commerceHander.OnReceive(ReqEventType.permission_commerce, msg.data.code);
                    break;
                case SocketEventType.BuyMt:
                    commerceHander.OnReceive(ReqEventType.buyMt,msg.data.code);
                    break;
                case SocketEventType.CheckMoney:
                    accountHandler.OnReceive(ReqEventType.checkMoney, msg.data);
                    break;
                case SocketEventType.ConfirmPass:
                    accountHandler.OnReceive(ReqEventType.confirmPass, msg.data.code);
                    break;
                case SocketEventType.Reply:
                    accountHandler.OnReceive(ReqEventType.checkLogin, msg.data.code);
                    break;
                case SocketEventType.TokenLogin:
                    accountHandler.OnReceive(ReqEventType.checkLogin, msg.data.code);
                    break;
                //case SocketEventType.PlayerInvest:
                //    accountHandler.OnReceive(ReqEventType.invest_req, msg.data.data);
                //    break;
                case SocketEventType.InvestInfo:
                    investHandler.OnReceive(ReqEventType.invest_info, msg.data.data);
                    break;
                case SocketEventType.HasReadMessage:
                    accountHandler.OnReceive(ReqEventType.ReadState, msg.data.data["messages"]);
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
