
/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:  2019/09/10 09:22:48
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/

using System.Collections.Generic;
using Assets.Scripts.Model;
using Assets.Scripts.Net.Code;
using UnityEngine;

namespace Assets.Scripts.Net.Request
{
    public class FriendRequestMsg :RequestBase
    {

        SocketMsg<Dictionary<string, string>> socketMsg = new SocketMsg<Dictionary<string, string>>();
        MessageData<Dictionary<string, string>> messageData = new MessageData<Dictionary<string, string>>();
        /// <summary>
        /// 申请消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqAddFriendMsg(object msg)
        {
            string applyUserName = msg.ToString();
            // ApplyInfo.applyList.Add(applyUserName);
            Dictionary<string, string>t = new Dictionary<string, string>
            {
                ["friendId"] = applyUserName,
                ["username"] = PlayerPrefs.GetString("username"),
                ["token"] = PlayerPrefs.GetString("token")
                //["token"] = CacheData.Instance().Token
            };
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer/player/friend", SocketEventType.AddFriend, t);
            socketMsg.Change(LoginInfo.ClientId, "申请好友操作", messageData);
            return socketMsg;
        }
        /// <summary>
        /// 申请好友通过/拒绝
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqAgreeFriendMsg(object msg)
        {
            Dictionary<string, string> t = msg as Dictionary<string, string>;
            //TODO
            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer/player/friend", SocketEventType.ArgreeApply, t);
            socketMsg.Change(LoginInfo.ClientId, "添加好友操作", messageData);
           
            //TODO
            //Dispatch(AreaCode.UI,11111,"removeList");
            return socketMsg;
        }
        /// <summary>
        /// 好友点赞
        /// </summary>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqLikeFriendMsg(object msg)
        {
            //TODO点赞逻辑


            UserInfo userInfo = msg as UserInfo;
            Dictionary<string, string>t = new Dictionary<string, string>
            {
                ["nick"] = userInfo.NickName,
                ["likes"] = userInfo.Like,
                ["username"] = PlayerPrefs.GetString("username"),
                ["token"] = PlayerPrefs.GetString("token")
                //["token"] = CacheData.Instance().Token
            };
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer/player/friend", SocketEventType.LikeFriend, t);
            socketMsg.Change(LoginInfo.ClientId, "好友点赞", messageData);
            //Dispatch(AreaCode.UI,11111,"activefalse");
            return socketMsg;
        }
        /// <summary>
        /// 搜索用户消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqSearchUserMsg(object msg)
        {
            string nickName="";
            if (msg != null)
            {
                 nickName = msg.ToString();
            }
            Dictionary<string, string>t = new Dictionary<string, string>
            {
                ["nick"] = nickName,
                ["username"] = PlayerPrefs.GetString("username"),
                ["token"]=PlayerPrefs.GetString("token"),
            };
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer/player", SocketEventType.SearchFriend, t);
            socketMsg.Change(LoginInfo.ClientId, "搜索用户", messageData);
            return socketMsg;
        }
        /// <summary>
        /// 官場列表消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqSquareMsg(object msg)
        {
            Dictionary<string, string> t = new Dictionary<string, string>
            { 
                ["username"] = PlayerPrefs.GetString("username"),
                ["token"] = PlayerPrefs.GetString("token"),
            };
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer/player", SocketEventType.SquareFriends, t);
            socketMsg.Change(LoginInfo.ClientId, "廣場列表", messageData);
            return socketMsg;
        }
        public  SocketMsg<Dictionary<string, string>> ReqNextUserList(object msg)
        {
            Dictionary<string, string>t = new Dictionary<string, string>
            {
                ["username"] = PlayerPrefs.GetString("username"),
                ["token"] = PlayerPrefs.GetString("token"),
            };
            messageData.Change("consumer/player/friend", SocketEventType.SearchFriend, t);
            socketMsg.Change(LoginInfo.ClientId, "换一批广场玩家", messageData);
            return socketMsg;
        }
        /// <summary>
        /// 申请好友列表
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqApplyFriendList(object msg)
        {
            Dictionary<string, string> t = new Dictionary<string, string>
            {
                ["username"] = PlayerPrefs.GetString("username"),
                ["token"] = PlayerPrefs.GetString("token"),
            };
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer/player/friend", SocketEventType.ApplyList, t);
            socketMsg.Change(LoginInfo.ClientId, "申请好友列表", messageData);
            return socketMsg;
        }
        /// <summary>
        /// 为好友点赞请求
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqLikeFriend(object msg)
        {
            Dictionary<string, string> t = new Dictionary<string, string>
            {
                ["username"] = PlayerPrefs.GetString("username"),
                ["token"] = PlayerPrefs.GetString("token"),
            };
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            t.Add("friendId",msg.ToString());
            messageData.Change("consumer/player/friend", SocketEventType.ApplyList, t);
            socketMsg.Change(LoginInfo.ClientId, "申请好友列表", messageData);
            return socketMsg;
        }
        /// <summary>
        /// 好友列表
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqFriendList(object msg)
        {
            Dictionary<string, string> t = new Dictionary<string, string>
            {
                ["username"] = PlayerPrefs.GetString("username"),
                ["token"] = PlayerPrefs.GetString("token"),
            };
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer/player/friend", SocketEventType.FriendList, t);
            socketMsg.Change(LoginInfo.ClientId, "好友列表", messageData);
            return socketMsg;
        }
    }
}
