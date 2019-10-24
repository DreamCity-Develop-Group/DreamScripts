using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using Assets.Scripts.Model;
using Assets.Scripts.Net.Code;
using LitJson;
using UnityEngine;
using WebSocketSharp;
//using LitJson;

namespace Assets.Scripts.Net
{
    public class WebData
    {

        private static volatile WebData _instance = null;

        private static readonly object LockHelper = new object();

        private WebData() { }

        public static WebData Instance()

        {

            if (_instance == null)

            {

                lock (LockHelper)

                {

                    if (_instance == null)

                        _instance = new WebData();

                }

            }

            return _instance;

        }
        /// <summary>  
        /// The WebSocket address to connect  
        /// </summary>  
        //private readonly string address = "ws://192.168.0.102:8010/dream/city/";
        // private readonly string address = "ws://192.168.0.88:8010/dream/city/lili/你发";
        // private  string address = "ws://192.168.0.106:8010/dream/city/topic/none";
        public static string address = "ws://192.168.0.117:8010/dream/city/topic/none";

        //private string address;

        /// <summary>  
        /// Default text to send  
        /// </summary>  

        /// <summary>
        /// 重连次数
        /// </summary>
        public int RecTimes=1;

        /// <summary>
        /// 重连判断
        /// </summary>
        public  bool IsReconnect=false;
        /// <summary>  
        /// Debug text to draw on the gui  
        /// </summary>  
       

        public static string ClientId;

        /// <summary>  
        /// Saved WebSocket instance  
        /// </summary>  
        private WebSocket _webSocket;

        private Queue<SocketMsg<Dictionary<string, object>>> _msgQueue = new Queue<SocketMsg<Dictionary<string, object>>> ();

        private Queue<SocketMsg<SquareUser>> _squareQueue = new Queue<SocketMsg<SquareUser>>();

        private Queue<SocketMsg<MenuInfo>> _menuQueue = new Queue<SocketMsg<MenuInfo>>();

        private Queue<SocketMsg<InvestList>> _investQueue = new Queue<SocketMsg<InvestList>>();
        private Queue<SocketMsg<MessageInfoList>> _messageQueue = new Queue<SocketMsg<MessageInfoList>>();
        private Queue<SocketMsg<CommerceInfo>> _commerceQueue = new Queue<SocketMsg<CommerceInfo>>();
        private Queue<SocketMsg<ExchangeInfos>> _exchangeQueue = new Queue<SocketMsg<ExchangeInfos>>();
        private Queue<SocketMsg<TradeRecordList>> _tradeRecordQueue = new Queue<SocketMsg<TradeRecordList>>();
        private Queue<bool> _replyQueue = new Queue<bool>();
        public WebSocket WebSocket { get { return _webSocket; } }
        public string Address { get { return address; } }
        public Queue<SocketMsg<TradeRecordList>> TradeRecordQueue { get => _tradeRecordQueue; set => _tradeRecordQueue = value; }
        public Queue<bool> ReplyQueue { get=>_replyQueue; set =>_replyQueue = value; } 
        public Queue<SocketMsg<Dictionary<string, object>>> MsgQueue { get => _msgQueue; set => _msgQueue = value; }
        public Queue<SocketMsg<SquareUser>> SquareQueue { get => _squareQueue; set => _squareQueue = value; }
        public Queue<SocketMsg<MenuInfo>> MenuQueue { get => _menuQueue; set => _menuQueue = value; }

        public Queue<SocketMsg<CommerceInfo>> CommerceQueue { get => _commerceQueue; set => _commerceQueue = value; }
        public Queue<SocketMsg<MessageInfoList>> MessageQueue { get => _messageQueue; set => _messageQueue = value; }
        public Queue<SocketMsg<InvestList>> InvestQueue
        {
            get => _investQueue;
            set => _investQueue = value;
        }
        public Queue<SocketMsg<ExchangeInfos>> ExchangeQueue { get => _exchangeQueue; set => _exchangeQueue = value; }
        /// <summary>
        /// 登录消息依次加载存储队列
        /// </summary>
        //public Queue<string> CacheData = new Queue<string>() {"","" };
        public void OpenWebSocket()
        {
            //if (_webSocket == null)
            //{
               // address = "ws://"+"156.236.69.200"+":8010/dream/city/topic/"+"none";
               _webSocket = new WebSocket(address,null);
                //if (HTTPManager.Proxy != null)
                //    _webSocket.InternalRequest.Proxy = new HTTPProxy(HTTPManager.Proxy.Address, HTTPManager.Proxy.Credentials, false);
                // Subscribe to the WS events  
                _webSocket.OnOpen += (sender, e) => {
                    OnOpen();
                };
                _webSocket.OnMessage += (sender, e) => {
                    OnMessageReceived(e.Data);
                };
                _webSocket.OnClose += (sender, e) => {
                    OnClosed(e.Reason);
                };
                _webSocket.OnError += (sender, e) => {
                    OnError(e.Message);
                };
                // Start connecting to the server  
                _webSocket.Connect();

               // ThreadStart();
           // }
        }
        //readonly CancellationToken _cancellation = new CancellationToken();
        public   void SendMsg(object msg)
        {
            // Send message to the server  
            if (_webSocket == null)
            {
               // ReConnect();
               Debug.Log("SendMsg: _webSocket == null");
            }
            if (msg == null)
            {
                return;
            }
            string jsonmsg = JsonMapper.ToJson(msg);
            Debug.Log("sendMsg: "+jsonmsg);
            _webSocket.Send(jsonmsg);
        }
        string statusDescription;
        public void CloseSocket()
        {
            // Close the connection  
            threads.Enqueue(t1);
            t1.Abort();
            _webSocket.Close(1000, "Bye!");
            _webSocket = null;
        }
        /// 心跳检测
        /// </summary> 
        Thread t1;
        /// <summary>
        /// 一分钟发一次心跳判断服务器状态
        /// </summary>
        private int timeout = 30000;
        public static bool isLogin = false;
        private static object lockObj = new object();
        private void HeartCheck()
        {
            while (true)
            {
                lock (lockObj)
                {
                    Thread.Sleep(timeout);
                    IsReconnect = false;
                    if (isLogin)
                    {
                        //登入成功
                        //TODO
                         _webSocket.Send("ping_"+CacheData.Instance().Username);
                         Debug.Log("sendMsg"+ "ping&&" + CacheData.Instance().Username);
                    }
                    //else
                    //{
                    //    if (!_webSocket.IsAlive)
                    //    {
                    //        _webSocket.Connect();
                    //    }
                    //    else
                    //    {
                    //       // _webSocket.Send("ping");
                    //       Debug.Log("isConnected");
                    //    }
                    //}
                }
           
            }

        }

        /// <summary>  
        /// Called when the web socket is open, and we are ready to send and receive data  
        /// </summary>  
        void OnOpen()
        {
            RecTimes = 0;
            //客户端打开网络
            IsReconnect = true;
            //if (PlayerPrefs.HasKey("token"))
            //{
            //    Dictionary<string, object> logMsg = new Dictionary<string, object>()
            //    {
            //        ["token"] = PlayerPrefs.GetString("token")
            //    };
            //    SendMsg(logMsg);
            //}
            Debug.Log("-WebSocket Open!\n");
        }

        /// <summary>  
        /// Called when we received a text message from the server  
        /// </summary>
        ///
        public   void Test(string testmsg)
        {
            OnMessageReceived(testmsg);
        }
        /// <summary>
        /// test调成私有
        /// </summary>
        /// <param name="jsonmsg"></param>
        void OnMessageReceived(string jsonmsg)
        {
            Debug.Log("receiveMsg:  "+jsonmsg);
            if (jsonmsg.Equals("success"))
            {
                //if (jsonmsg.Length > 7)
                //{
                //PlayerPrefs.SetString("token", jsonmsg.Substring(8));
                //    // CacheData.Instance().Token = jsonmsg.Substring(8);
                //}
                //isLogin = true;
                Debug.Log("isconnect" + jsonmsg);
                return;
            }
            if (jsonmsg.Equals(""))
            {
                Debug.Log("msg null error");
                return;
            }
            SocketMsg<object> info = JsonMapper.ToObject<SocketMsg<object>>(jsonmsg);
            if (info.data.model == "socket")
            {
               // HeartStart();
            }

            if (info.data.model == "messageCenter")
            {
                if (info.data.type == "replay")
                {
                    ReplyQueue.Enqueue(true);
                }
            }
            //todo 建立一个响应字典
            if(info.data.type== SocketEventType.SquareFriends || info.data.type == SocketEventType.FriendList || info.data.type == SocketEventType.ApplyList||info.data.type==SocketEventType.SearchFriend)
            {
                SocketMsg<SquareUser> squareinfo = JsonMapper.ToObject<SocketMsg<SquareUser>>(jsonmsg);
                //TODO 过滤过时消息
                SquareQueue.Enqueue(squareinfo);
            
            }
            else if (info.data.type ==SocketEventType.Message)
            {
                SocketMsg<MessageInfoList> messageinfo = JsonMapper.ToObject<SocketMsg<MessageInfoList>>(jsonmsg);
                //TODO 过滤过时消息
                MessageQueue.Enqueue(messageinfo);
                
            }
            else if (info.data.type== SocketEventType.Default)
            {
                SocketMsg<MenuInfo> menuinfo = JsonMapper.ToObject<SocketMsg<MenuInfo>>(jsonmsg);
                //TODO 过滤过时消息
                MenuQueue.Enqueue(menuinfo);

            }
            else if (info.data.type== SocketEventType.InvestInfo)
            {
                SocketMsg<InvestList> investinfo = JsonMapper.ToObject<SocketMsg<InvestList>>(jsonmsg);
                //TODO 过滤过时消息
                InvestQueue.Enqueue(investinfo);
            }
            else if (info.data.type == SocketEventType.Commerce)
            {
                SocketMsg<CommerceInfo> membersinfo = JsonMapper.ToObject<SocketMsg<CommerceInfo>>(jsonmsg);
                CommerceQueue.Enqueue(membersinfo);
            }
            else if (info.data.type == SocketEventType.ExchangeCenter)
            {
                SocketMsg < ExchangeInfos > exchangeinfo  = JsonMapper.ToObject<SocketMsg<ExchangeInfos>>(jsonmsg);
                ExchangeQueue.Enqueue(exchangeinfo);
            }
            else if (info.data.type == SocketEventType.GetTradeList)
            {
                SocketMsg<TradeRecordList> traderecordinfo = JsonMapper.ToObject<SocketMsg<TradeRecordList>>(jsonmsg);
                TradeRecordQueue.Enqueue(traderecordinfo);
            }
            else
            {
                SocketMsg<Dictionary<string,object>> msginfo = JsonMapper.ToObject<SocketMsg<Dictionary<string, object>>>(jsonmsg);
                //TODO 过滤过时消息
                MsgQueue.Enqueue(msginfo);
            }
        }
        /// <summary>  
        /// Called when the web socket closed  
        /// </summary>  
        void OnClosed(string message)
        {
            t1?.Abort();
            IsReconnect = false;
            if (!WebSocketManager.Instance.ReConnectState)
            {
                WebSocketManager.Instance.StartCoroutine(WebSocketManager.Instance.ReConnect());
            }
            Debug.Log(string.Format("-WebSocket closed! Code:  Message: [0]\n", message));
        }
       

        /// <summary>
        ///线程处理
        /// </summary>
        Queue<Thread> threads = new Queue<Thread>();
        public void HeartStart()
        {
            if (threads.Count > 0)
            {
                t1 = threads.Dequeue();
            }
            else
            {
                t1 = new Thread(HeartCheck);
                t1.Start();
                //t1.IsBackground = true;
                //Application.runInBackground
            }
        }

        //public void ConnectStart()
        //{
        //    if (threads.Count > 0)
        //    {
        //        t1 = threads.Dequeue();
        //    }
        //    else
        //    {
        //        t1 = new Thread(WebSocketManager.Instance.ReConnect());
        //        t1.Start();
        //        t1.IsBackground = true;
        //    }
        //}
        /// <summary>  
        /// Called when an error occured on client side  
        /// </summary>  
        void OnError(string  error)
        {
            Debug.Log($"-An error occured: {error}\n");
            _webSocket = null;
        }
    }
}



