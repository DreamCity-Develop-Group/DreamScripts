
/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:  2019/09/11 09:23:12
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/

using System;
using System.Collections.Generic;
using Assets.Scripts.Framework;
using Assets.Scripts.Language;
using Assets.Scripts.Model;
using Assets.Scripts.Net.Code;
using Assets.Scripts.Net.Handler;
using Assets.Scripts.Tools;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Msg;
using UnityEngine;
using WebSocketSharp;

namespace Assets.Scripts.Net.Request
{
    /// <summary>
    ///TODO Multilingualism
    /// </summary>
    public class SetRequestMsg :HandlerBase
    {
        MessageData<Dictionary<string, string>> messageData = new MessageData<Dictionary<string, string>>();
        SocketMsg<Dictionary<string, string>> socketMsg = new SocketMsg<Dictionary<string, string>>();
        private HintMsg promptMsg = new HintMsg();
        /// <summary>
        /// 设置交易密码
        /// </summary>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqExPwShopMsg(object msg)
        {
            string pass  = msg.ToString();
            if (!MsgTool.CheckExPass(pass))
            {
                promptMsg.Change(LanguageService.Instance.GetStringByKey("417",string.Empty),Color.white);
                Dispatch(AreaCode.UI,UIEvent.HINT_ACTIVE,promptMsg);
                return null;
            }
            Dictionary<string, string> t = new Dictionary<string, string>
            {
                ["newpwshop"] = MsgTool.MD5Encrypt(pass),
            };
            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer/player", SocketEventType.ChangeShopPass, t);
            socketMsg.Change(LoginInfo.ClientId, "设置交易密码", messageData);
            return socketMsg;
        }

        public SocketMsg<Dictionary<string, string>> ReqJoinCommerceMsg(object msg)
        {
            UserInfo userinfo = msg as UserInfo;
            //TODO 校验交易密码
            if (!MsgTool.CheckExPass(userinfo.Password))
            {
                promptMsg.Change(LanguageService.Instance.GetStringByKey("", string.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            Dictionary<string, string> t = new Dictionary<string, string>
            {
                ["pwshop"] = MsgTool.MD5Encrypt(userinfo.Password),
                ["code"] = userinfo.Identity,
                ["invite"] = userinfo.InviteCode
            };

            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer/player", SocketEventType.JoinCommerce, t);
            socketMsg.Change(LoginInfo.ClientId, "注册操作", messageData);
            return socketMsg;
        }

        /// <summary>
        /// 音效设置
        /// </summary>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqVoiceSetMsg(object msg)
        {
            SetInfo setInfo = msg as SetInfo;

            Dictionary<string, string> t = new Dictionary<string, string>
            {
                ["bg"] = setInfo.BgVoice,
                ["game"] = setInfo.GameVoice

            };
            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.model = "consumer/player";
            messageData.type = "voice";
            socketMsg.Change(LoginInfo.ClientId, "音效设置", messageData);
            return socketMsg;
        }
        /// <summary>
        /// 修改密码消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqPWChangeMsg(object msg)
        {

            Dictionary<string, string> t = msg as Dictionary<string, string>;
            //todo配置
            if (t["oldpw"].IsNullOrEmpty())
            {
                // promptMsg.Change("请输入当前密码", Color.white);
                promptMsg.Change(LanguageService.Instance.GetStringByKey("417", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            if (t["newpw"].IsNullOrEmpty())
            {
                // promptMsg.Change("请输入新密码", Color.white);
                promptMsg.Change(LanguageService.Instance.GetStringByKey("418", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            if (t["code"].IsNullOrEmpty())
            {
                //promptMsg.Change("请输入验证码", Color.white);
                promptMsg.Change(LanguageService.Instance.GetStringByKey("404", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            if (!MsgTool.CheckPass(t["newpw"]))
            {
                promptMsg.Change(LanguageService.Instance.GetStringByKey("408", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            t["oldpw"] = MsgTool.MD5Encrypt(t["oldpw"]);
            t["newpw"] = MsgTool.MD5Encrypt(t["newpw"]);
            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer/player", SocketEventType.ChangPassWord, t);
            socketMsg.Change(LoginInfo.ClientId, "修改登入密码操作", messageData);
            return socketMsg;
        }

        /// <summary>
        /// 修改交易密码消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqPWShopChangeMsg(object msg)
        {

            Dictionary<string, string> t = msg as Dictionary<string, string>;
            //todo配置
            if (t["oldpwshop"].IsNullOrEmpty())
            {
                //promptMsg.Change("请输入当前密码", Color.white);
                promptMsg.Change(LanguageService.Instance.GetStringByKey("请输入当前交易密码", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            if (t["newpwshop"].IsNullOrEmpty())
            {
                // promptMsg.Change("请输入新密码", Color.white);
                promptMsg.Change(LanguageService.Instance.GetStringByKey("请输入新交易密码", String.Empty), Color.white);

                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            if (t["code"].IsNullOrEmpty())
            {
                //promptMsg.Change("请输入验证码", Color.white);
                promptMsg.Change(LanguageService.Instance.GetStringByKey("请输入验证码", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }

            if (CacheData.Instance().Mt < CacheData.Instance().ChangExPassWordMt)
            {
                //promptMsg.Change("你的MT不足", Color.white);
                promptMsg.Change(LanguageService.Instance.GetStringByKey("你的MT不足", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            //TODO 校验交易密码
            if (!MsgTool.CheckExPass(t["newpwshop"]))
            {
                promptMsg.Change(LanguageService.Instance.GetStringByKey("417", string.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            t["oldpwshop"] = MsgTool.MD5Encrypt(t["oldpwshop"]);
            t["newpwshop"] = MsgTool.MD5Encrypt(t["newpwshop"]);
            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer/player", SocketEventType.ChangeShopPass, t);
            socketMsg.Change(LoginInfo.ClientId, "修改交易密码操作", messageData);
            return socketMsg;
        }

    }
}
