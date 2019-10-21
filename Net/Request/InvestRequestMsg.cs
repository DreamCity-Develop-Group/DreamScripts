using System.Collections.Generic;
using Assets.Scripts.Model;
using Assets.Scripts.Net.Code;
using Assets.Scripts.UI.Msg;
using UnityEngine;

/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:          2019/09/28 12:26:40
  *
  * Description: 投资请求
  *
  * Version:    0.1
  *
  *
***/
namespace Assets.Scripts.Net.Request
{
    public class InvestRequestMsg:RequestBase
    {
        private HintMsg promptMsg = new HintMsg();
        SocketMsg<Dictionary<string, string>> socketMsg = new SocketMsg<Dictionary<string, string>>();
        MessageData<Dictionary<string, string>> messageData = new MessageData<Dictionary<string, string>>();
        /// <summary>
        /// 投资请求
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqInvestMsg(object msg)
        {
            Dictionary<string, string> t = new Dictionary<string, string>();
            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("investId", msg.ToString());
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer", SocketEventType.Invest, t);
            socketMsg.Change(LoginInfo.ClientId, "投资请求", messageData);
            return socketMsg;
        }
        /// <summary>
        /// 投资信息请求消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqInvestInfoMsg(object msg)
        {
            Dictionary<string, string> t =new Dictionary<string, string>();
            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer", SocketEventType.InvestInfo, t);
            socketMsg.Change(LoginInfo.ClientId, "投资信息请求消息", messageData);
            return socketMsg;
        }
        /// <summary>
        /// 提取请求消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqExtractInfoMsg(object msg)
        {
            Dictionary<string, string> t = new Dictionary<string, string>();
            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            t.Add("investId",msg.ToString());
            messageData.Change("consumer", SocketEventType.Extract, t);
            socketMsg.Change(LoginInfo.ClientId, "提取请求消息", messageData);
            return socketMsg;
        }

        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqLikeInfoMsg(object msg)
        {
            Dictionary<string, string> t = new Dictionary<string, string>();
            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            t.Add("friendId", msg.ToString());
            messageData.Change("consumer", SocketEventType.LikeFriend, t);
            socketMsg.Change(LoginInfo.ClientId, "提取请求消息", messageData);
            return socketMsg;
        }
    }
}
