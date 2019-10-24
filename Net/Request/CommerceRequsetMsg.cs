using System.Collections.Generic;
using Assets.Scripts.Model;
using Assets.Scripts.Net.Code;
using Assets.Scripts.Tools;
using Assets.Scripts.UI.Msg;
using UnityEngine;

/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:          2019/09/19 18:21:25
  *
  * Description:   
  *
  * Version:    0.1
  *
  *
***/
namespace Assets.Scripts.Net.Request
{
    public class CommerceRequsetMsg 
    {
        private HintMsg promptMsg = new HintMsg();
        SocketMsg<ReqCommerceInfo> socketMsg = new SocketMsg<ReqCommerceInfo>();
        MessageData<ReqCommerceInfo> messageData = new MessageData<ReqCommerceInfo>();
        ReqCommerceInfo reqCommerceInfo = new ReqCommerceInfo();
        /// <summary>
        /// 同意发货列表
        /// </summary>
        private List<string> agreedOrderList = new List<string>();
        /// <summary>
        /// 拒绝发货列表
        /// </summary>
        private List<string> refuseOrderList = new List<string>();
        /// <summary>
        /// 商会请求加入消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<ReqCommerceInfo> ReqComeCommerceMsg(object msg)
        {
            if (msg == null || msg.Equals(""))
            {
                //TODO提示
                promptMsg.Change("null",Color.white);
                return null;
            }
            //TODO
            reqCommerceInfo.Change(null,null,msg.ToString(),null,null);
            //t.Add("commerce_name");
            //t.Add("username", PlayerPrefs.GetString("username"));
            //t.Add("token",PlayerPrefs.GetString("token"));
            messageData.Change("consumer/tree", SocketEventType.AddTree, reqCommerceInfo);
            socketMsg.Change(LoginInfo.ClientId, "商会请求加入消息", messageData);
            return socketMsg;
        }
        /// <summary>
        /// 经营许可证
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<ReqCommerceInfo> ReqPermissionCommerceMsg(object msg)
        {
            string pass = MsgTool.MD5Encrypt(msg.ToString());
            reqCommerceInfo.Change(null, null, null, null,null, pass);
            messageData.Change("consumer/tree", SocketEventType.JoinCommerce, reqCommerceInfo);
            socketMsg.Change(LoginInfo.ClientId, "商会请求经营许可消息", messageData);
            return socketMsg;
        }

        /// <summary>
        /// 购买MT消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<ReqCommerceInfo> ReqBuyMTMsg(object msg)
        {
            //Dictionary<string, string> t = msg as Dictionary<string, string>;
            //t.Add("username", PlayerPrefs.GetString("username"));
            //t.Add("token",PlayerPrefs.GetString("token"));
            reqCommerceInfo.Change(null, msg.ToString(), null, null, null);
            messageData.Change("consumer/tree", SocketEventType.BuyMt, reqCommerceInfo);
            socketMsg.Change(LoginInfo.ClientId, "购买MT消息请求", messageData);
            return socketMsg;
        }

        /// <summary>
        /// 会长拒绝发货
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<ReqCommerceInfo> ReqRefuseMTMsg(object msg)
        {
            refuseOrderList.Add(msg.ToString());
            reqCommerceInfo.Change(null, null, null, null, msg.ToString(),null,refuseOrderList);
            messageData.Change("consumer/tree", SocketEventType.SendMt, reqCommerceInfo);
            socketMsg.Change(LoginInfo.ClientId, "会长拒绝发货", messageData);
            return socketMsg;
        }
       
        /// <summary>
        /// 会长同意发货
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<ReqCommerceInfo> ReqSendMTMsg(object msg)
        {
            agreedOrderList.Clear();
            agreedOrderList.Add(msg.ToString());
            reqCommerceInfo.Change(null, null, null, null,null, null, agreedOrderList);
            messageData.Change("consumer/tree", SocketEventType.SendMt, reqCommerceInfo);
            socketMsg.Change(LoginInfo.ClientId, "会长同意发货", messageData);
            return socketMsg;
        }

        /// <summary>
        /// 会长设置一键发货
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<ReqCommerceInfo> ReqSendAllMsg(object msg)
        {
            agreedOrderList.Clear();
            agreedOrderList = msg as List<string>;
            reqCommerceInfo.Change(null, null, null, null, null, null, agreedOrderList);
            messageData.Change("consumer/tree", SocketEventType.SendMt, reqCommerceInfo);
            socketMsg.Change(LoginInfo.ClientId, "会长一键发货", messageData);
            return socketMsg;
        }
        /// <summary>
        /// 会长设置自动发货
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<ReqCommerceInfo> ReqSendAutoMsg(object msg)
        {
            reqCommerceInfo.Change(null, null, null, null, null,null,null,0,msg.ToString());
            messageData.Change("consumer/tree", SocketEventType.SetAutoSend, reqCommerceInfo);
            socketMsg.Change(LoginInfo.ClientId, "会长设置自动发货", messageData);
            return socketMsg;
        }
        /// <summary>
        /// 商会信息请求
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<ReqCommerceInfo> ReqCommerceMsg(object msg)
        {
            //Dictionary<string, string> t = msg as Dictionary<string, string>;
            //t.Add("username", PlayerPrefs.GetString("username"));
            //t.Add("token",PlayerPrefs.GetString("token"));
            reqCommerceInfo.Change(null, null, null, null, null);
            messageData.Change("consumer/tree", SocketEventType.Commerce, reqCommerceInfo);
            socketMsg.Change(LoginInfo.ClientId, "商会信息请求", messageData);
            return socketMsg;
        }
        public SocketMsg<ReqCommerceInfo> ReqExchangeCenterMsg(object msg)
        {
            int page = 1;
            if (msg != null)
            {
                page = CacheData.Instance().ExchangePage + 1;
            }
            reqCommerceInfo.Change(null, null, null, null, null,null,null,page);
            messageData.Change("consumer/tree", SocketEventType.ExchangeCenter, reqCommerceInfo);
            socketMsg.Change(LoginInfo.ClientId, "兑换中心信息请求", messageData);
            return socketMsg;
        }
       

    }
}
