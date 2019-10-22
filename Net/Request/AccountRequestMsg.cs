using System;
using System.Collections.Generic;
using Assets.Scripts.Framework;
using Assets.Scripts.Language;
using Assets.Scripts.Model;
using Assets.Scripts.Net.Code;
using Assets.Scripts.Tools;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Msg;
using UnityEngine;

namespace Assets.Scripts.Net.Request
{
    public class AccountRequestMsg:RequestBase
    {

        private HintMsg promptMsg = new HintMsg();
        SocketMsg<Dictionary<string, string>> socketMsg = new SocketMsg<Dictionary<string, string>>();
        MessageData<Dictionary<string, string>> messageData = new MessageData<Dictionary<string, string>>();
        /// <summary>
        /// 转账消息
        /// </summary>
        SocketMsg<TransferInfo> transferSocketMsg = new SocketMsg<TransferInfo>();
        MessageData<TransferInfo> transferData = new MessageData<TransferInfo>();
       
        public  SocketMsg<Dictionary<string, string>> ReqTokenLoginMsg(object msg)
        {
            Dictionary<string, string> t = new Dictionary<string, string>()
            {
                ["token"] = msg.ToString()
            };
            messageData.Change("consumer/player", SocketEventType.PassWordLogin, t);
            socketMsg.Change(LoginInfo.ClientId, "登入操作", messageData);
            return socketMsg;
        }

        /// <summary>
        /// 密码登入消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string,string>> ReqPWLoginMsg(object msg)
        {
            //登入检验TODO
             LoginInfo loginInfo = msg as LoginInfo;
            if (loginInfo.UserName==""|| loginInfo.Password=="")
            {
                //promptMsg.Change("请输入用户名和密码", Color.white);
                promptMsg.Change(LanguageService.Instance.GetStringByKey("401", String.Empty), Color.white);
                Dispatch(AreaCode.UI,UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            if (!MsgTool.CheckMobile(loginInfo.UserName))
            {
                //promptMsg.Change("请输入正确的手机号码", Color.white);
                promptMsg.Change(LanguageService.Instance.GetStringByKey("402", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            //string userpass = loginInfo.Password;
            string userpass= MsgTool.MD5Encrypt(loginInfo.Password);
            Dictionary<string, string> t = new Dictionary<string, string>
            {
                // ["IsIdentityLog"] = loginInfo.Identity,
                ["username"] = loginInfo.UserName,
                ["userpass"] = userpass,
                //["Identity"] = loginInfo.Identity
            };
            messageData.Change("consumer/player", SocketEventType.PassWordLogin,t);
            socketMsg.Change(LoginInfo.ClientId, "登入操作", messageData);
            PlayerPrefs.SetString("username", loginInfo.UserName);
            CacheData.Instance().Username = loginInfo.UserName;
            if (PlayerPrefs.HasKey("token"))
                PlayerPrefs.DeleteKey("token");
            return socketMsg;
          
        }
        /// <summary>
        /// 忘记密码消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string,string>> ReqForgetMsg(object msg)
        {
            LoginInfo loginInfo = msg as LoginInfo;
            if (loginInfo.UserName == "")
            {
                promptMsg.Change(LanguageService.Instance.GetStringByKey("401", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            if (!MsgTool.CheckMobile(loginInfo.UserName))
            {
                promptMsg.Change(LanguageService.Instance.GetStringByKey("402", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            string userpass = MsgTool.MD5Encrypt(loginInfo.Password);
            Dictionary<string, string> t = new Dictionary<string, string>
            {
                // ["IsIdentityLog"] = loginInfo.Identity,
                ["username"] = loginInfo.UserName,
                ["newpw"] = userpass,
                ["code"] = loginInfo.Identity,
                ["token"] = PlayerPrefs.GetString("token"),
                  
            };
            messageData.Change("consumer/player", SocketEventType.ForgerPassWord, t);
            socketMsg.Change(LoginInfo.ClientId, "忘记密码消息", messageData);
            return socketMsg;
        }


        /// <summary>
        /// 获取验证码请求消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string,string>> ReqGetIdentityMsg(object msg)
        {
            if (WebData.isLogin)
            {
                Dictionary<string, string> t1 = new Dictionary<string, string>
                {
                    ["username"] = PlayerPrefs.GetString("username"),
                    ["token"] = PlayerPrefs.GetString("token")
                   // ["token"] = CacheData.Instance().Token

                };
                messageData.Change("consumer/message", SocketEventType.GetCode, t1);
                socketMsg.Change(LoginInfo.ClientId, "获取验证码操作", messageData);
            }
            else
            {
                promptMsg.Color = Color.white;
                if (msg==null||msg.Equals(""))
                {
                    promptMsg.Change(LanguageService.Instance.GetStringByKey("403", String.Empty), Color.white);
                    
                    Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                    return null;
                }
                if (!MsgTool.CheckMobile(msg.ToString()))
                {
                    promptMsg.Change(LanguageService.Instance.GetStringByKey("402", String.Empty), Color.white);
                    //promptMsg.Change("请输入正确的手机号码", Color.white);
                    Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                    return null;
                }
                Dictionary<string, string> t = new Dictionary<string, string>
                {
                    ["username"] = msg.ToString()
                };
                messageData.Change("consumer/message", SocketEventType.GetCode, t);
                //messageData.t = null;
                socketMsg.Change(LoginInfo.ClientId, "获取验证码操作", messageData);
            }
            return socketMsg;
        }
    
        /// <summary>
        /// 验证码登入消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string,string>> ReqIDLoginMsg(object msg)
        {
            LoginInfo loginInfo = msg as LoginInfo;
            //TODO
            if (loginInfo.UserName == "" || loginInfo.Password == "")
            {
              //  promptMsg.Change("请输入用户名和验证码", Color.white);
                promptMsg.Change(LanguageService.Instance.GetStringByKey("404", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            if (!MsgTool.CheckMobile(loginInfo.UserName))
            {
              //  promptMsg.Change("请输入正确的手机号码", Color.white);
                promptMsg.Change(LanguageService.Instance.GetStringByKey("402", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            Dictionary<string, string> t = new Dictionary<string, string>
            {
                // ["IsIdentityLog"] = loginInfo.Identity,
                ["username"] = loginInfo.UserName,
                ["code"] = loginInfo.Password,
                //["Identity"] = loginInfo.Identity
            };
            messageData.Change("consumer/player", SocketEventType.CodeLogin, t);
            socketMsg.Change(LoginInfo.ClientId, "登入操作", messageData);
            CacheData.Instance().Username = loginInfo.UserName;

            if (PlayerPrefs.HasKey("token"))
                PlayerPrefs.DeleteKey("token");
            return socketMsg;
        }
        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string,string>> ReqRegMsg(object msg)
        {
            UserInfo userinfo = msg as UserInfo;

            if (userinfo.Phone == "" || userinfo.Password == "")
            {
                // promptMsg.Change("请输入用户名和验证码", Color.white);
                promptMsg.Change(LanguageService.Instance.GetStringByKey("404", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            if (!MsgTool.CheckMobile(userinfo.Phone))
            {
                // promptMsg.Change("请输入正确的手机号码", Color.white);
                promptMsg.Change(LanguageService.Instance.GetStringByKey("402", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            if (!MsgTool.CheckPass(userinfo.Password))
            {
                // promptMsg.Change("8-16位字符,可包含数字,字母,下划线", Color.white);
                promptMsg.Change(LanguageService.Instance.GetStringByKey("408", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            if (!MsgTool.CheckNickName(userinfo.NickName))
            {
                // promptMsg.Change("2-10位字符,可包含数字,字母,下划线,汉字", Color.white);
                promptMsg.Change(LanguageService.Instance.GetStringByKey("405", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return null;
            }
            Dictionary<string, string>t = new Dictionary<string, string>
            {
                ["username"] = userinfo.Phone,
                ["userpass"] = MsgTool.MD5Encrypt(userinfo.Password),
                ["code"] = userinfo.Identity,
                ["nick"] = userinfo.NickName,
                ["invite"] = userinfo.InviteCode
            };
            messageData.Change("consumer/player", SocketEventType.Regist, t);
            Debug.LogError(LoginInfo.ClientId);
            socketMsg.Change(LoginInfo.ClientId,  "注册操作", messageData);
            return socketMsg;
        }

        /// <summary>
        /// 资产请求消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqPropertyTestMsg(object msg)
        {
            Dictionary<string, string> t = new Dictionary<string, string>
            {
                // ["IsIdentityLog"] = loginInfo.Identity,
                ["username"] = PlayerPrefs.GetString("username"),
               ["token"] = PlayerPrefs.GetString("token")
            };
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer/account", SocketEventType.PropertyInfo, t);
            socketMsg.Change(LoginInfo.ClientId, "资产信息", messageData);
            return socketMsg;
        }

        /// <summary>
        /// 转账请求消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<TransferInfo> ReqTransferMsg(object msg)
        {
            //msg金额todo
            if (msg == null)
            {
                return null;
            }
            
            transferData.Change("consumer", SocketEventType.TransferAccount, msg as TransferInfo);
            transferSocketMsg.Change(LoginInfo.ClientId, "转账请求消息", transferData);
            return transferSocketMsg;
        }
        /// <summary>
        /// 充值请求消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqRechargeMsg(object msg)
        {
            //msg金额，钱包地址todo，
            Dictionary<string, string> t = msg as Dictionary<string, string>;
            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer/player", SocketEventType.Recharge, t);
            socketMsg.Change(LoginInfo.ClientId, "充值请求", messageData);
            return socketMsg;
        }
        /// <summary>
        /// 提现请求消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqCachWithdrawalMsg(object msg)
        {
            //msg金额，钱包地址,type,todo,
            Dictionary<string, string> t = msg as Dictionary<string, string>;
            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer/player", SocketEventType.Withdraw, t);
            socketMsg.Change(LoginInfo.ClientId, "提现请求消息", messageData);
            return socketMsg;
        }
        
        /// <summary>
        /// 主界面信息请求
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqMenuMsg(object msg)
        {
            Dictionary<string, string> t = new Dictionary<string, string>();
            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer/main", SocketEventType.Default, t);
            socketMsg.Change(LoginInfo.ClientId, "主界面信息请求", messageData);
            return socketMsg;
        }
        public SocketMsg<Dictionary<string, string>> ReqCheckMoney(object msg)
        {
            Dictionary<string, string> t = new Dictionary<string, string>();
            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            t.Add("amount",msg.ToString());
            messageData.Change("consumer/tree", SocketEventType.CheckMoney, t);
            socketMsg.Change(LoginInfo.ClientId, "核对金额请求", messageData);
            return socketMsg;
        }
        /// <summary>
        /// 校验交易密码
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqCheckPass(object msg)
        {
            Dictionary<string, string> t = new Dictionary<string, string>();
            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            t.Add("confirmPass",MsgTool.MD5Encrypt( msg.ToString()));
            messageData.Change("consumer/tree", SocketEventType.ConfirmPass, t);
            socketMsg.Change(LoginInfo.ClientId, "核对密码请求", messageData);
            return socketMsg;
        }
        /// <summary>
        /// 获取交易明细
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqTradeRecord(object msg)
        {
            Dictionary<string, string> t = new Dictionary<string, string>();
            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer", SocketEventType.GetTradeList, t);
            socketMsg.Change(LoginInfo.ClientId, "获取交易明细", messageData);
            return socketMsg;
        }

        /// <summary>
        /// 获取邮件信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqMessage(object msg)
        {
            Dictionary<string, string> t = new Dictionary<string, string>();
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            t.Add("username", PlayerPrefs.GetString("username"));
            messageData.Change("consumer/player", SocketEventType.Message, t);
            socketMsg.Change(LoginInfo.ClientId, "获取邮件信息", messageData);
            return socketMsg;
        }

        /// <summary>
        /// 已读邮件信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqHasReadMessage(object msg)
        {
            Dictionary<string, string> t = new Dictionary<string, string>();
            t.Add("token", PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            t.Add("username",PlayerPrefs.GetString("username"));
            t.Add("id",msg.ToString());
            messageData.Change("consumer/player", SocketEventType.HasReadMessage, t);
            socketMsg.Change(LoginInfo.ClientId, "获取邮件信息", messageData);
            return socketMsg;
        }

        /// <summary>
        /// 退出登入
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SocketMsg<Dictionary<string, string>> ReqExitMsg(object msg)
        {
            Dictionary<string, string> t = new  Dictionary<string, string>();
            t.Add("username", PlayerPrefs.GetString("username"));
            t.Add("token",PlayerPrefs.GetString("token"));
            t.Add("playerId", PlayerPrefs.GetString("playerId"));
            messageData.Change("consumer/player", SocketEventType.Exit, t);
            socketMsg.Change(LoginInfo.ClientId, "退出登入请求", messageData);
            return socketMsg;
        }
    }
}
