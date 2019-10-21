using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Framework;
using Assets.Scripts.Language;
using Assets.Scripts.Model;
using Assets.Scripts.Net.Code;
using Assets.Scripts.Scenes;
using Assets.Scripts.Scenes.Msg;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Msg;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = System.Object;

namespace Assets.Scripts.Net.Handler
{
    public class AccoutHandler: HandlerBase
    {
        // SocketMsg msg = new SocketMsg();

        public  override bool OnReceive(int subCode, object value)
        {
            switch (subCode)
            {
                case ReqEventType.init:
                    return initResponse(value.ToString());
                case ReqEventType.login:
                    return loginResponse(value.ToString());
                case ReqEventType.regist:
                    return registResponse(value.ToString());
                case ReqEventType.identy:
                    return getCodeResponse(value.ToString());
                case ReqEventType.transfer:
                    MessageData<Dictionary<string, object>> msg = value as MessageData<Dictionary<string, object>>;
                    return transferResponse(msg);
                case ReqEventType.property:
                    propertyResonse(value as PropertyInfo);
                    break;
                case ReqEventType.pwforget:
                    forgetpwReponse(value.ToString());
                    break;
                case ReqEventType.checkMoney:
                    checkMoneyResponse(value);
                    break;
                case ReqEventType.confirmPass:
                    checkPassResponse(value.ToString());
                    break;
                case ReqEventType.checkLogin:
                    tokenReponse(value.ToString());
                    break;
                case ReqEventType.GetTradeCord:
                    TradeRecordList tradeRecordList = value as TradeRecordList;
                    
                    GetRecordResponse(tradeRecordList);
                    break;
                case ReqEventType.ReadState:
                    CacheData.Instance().RedState=(bool)value;
                  
                        Dispatch(AreaCode.UI, UIEvent.MENU_UPDATE_VIEW, true);
                    
                    break;
                default:
                    break;
            }
            return false;
        }

        private HintMsg promptMsg = new HintMsg();

        private bool  GetRecordResponse(TradeRecordList msg )
        {
            if (msg == null)
            {
                return false;
            }
            Dispatch(AreaCode.UI,UIEvent.TRADERECORD_VIEW,msg);
            return true;
        }


        private bool initResponse(string msg)
        {
            if (msg != null)
            {
                LoginInfo.ClientId = msg;
                Debug.Log(LoginInfo.ClientId);
                //Dispatch(AreaCode.UI, UIEvent.LOGINSELECT_PANEL_ACTIVE, true);
                
               // Dispatch(AreaCode.UI, UIEvent.LOGINSELECT_PANEL_ACTIVE, true);
            }
            return true;
        }
       
        /// <summary>
        /// 登录响应
        /// </summary>
        private bool loginResponse(string result)
        {
            promptMsg.Change(LanguageService.Instance.GetStringByKey(result, String.Empty), Color.white);
            if (result == "200")
            {
               
                promptMsg.Change(LanguageService.Instance.GetStringByKey("login", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                //跳转场景 TODO
                Dispatch(AreaCode.UI,UIEvent.LOG_ACTIVE,false);
                
                return true;
            }
            else if (result == "301")
            {
                SceneManager.LoadSceneAsync("login");
            }
            Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
            return false;
            //登录错误
            //promptMsg.Change(result.ToString(), Color.white);
            //Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
        }
        /// <summary>
        /// 验证码响应
        /// </summary>
        private bool getCodeResponse(string result)
        {
            //todo
            promptMsg.Change(result, Color.white);

            Dispatch(AreaCode.UI, UIEvent.REG_PANEL_CODEVIEW, result);
       
            return true;
        }
        /// <summary>
        /// 注册响应
        /// </summary>
        private bool registResponse(string result)
        {
            promptMsg.Change(LanguageService.Instance.GetStringByKey(result, String.Empty), Color.white);
            if (result == "200")
            {
                if (PlayerPrefs.GetInt("reg")== 1)
                {
                    PlayerPrefs.SetInt("reg", 0);
                }
                else
                {
                    PlayerPrefs.SetInt("reg", 1);
                }
                promptMsg.Change(LanguageService.Instance.GetStringByKey("regist", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                Dispatch(AreaCode.UI, UIEvent.REG_ACTIVE, false);
                Dispatch(AreaCode.UI,UIEvent.LOG_ACTIVE,true);
                return true;
            }
            Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
            return false;
            //注册错误
            // promptMsg.Change(result.ToString(), Color.white);
            //Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
        }
        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool forgetpwReponse(string result)
        {
            promptMsg.Change(LanguageService.Instance.GetStringByKey(result, String.Empty), Color.white);
            if (result == "200")
            {
                promptMsg.Change(LanguageService.Instance.GetStringByKey("modify", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                Dispatch(AreaCode.UI, UIEvent.Forget_ACTIVE, false);
                Dispatch(AreaCode.UI, UIEvent.LOG_ACTIVE, true);
                return true;
            }
            Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
            return false;
        }
        /// <summary>
        /// 转账响应
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool transferResponse(MessageData<Dictionary<string, object>> msg)
        {
           
            promptMsg.Change(LanguageService.Instance.GetStringByKey(msg.code.ToString(), String.Empty), Color.white);
          
            if (msg.data["code"].ToString() == "200")
            {  //***********转账成功啦>@^_^@<*****************
                promptMsg.Change(LanguageService.Instance.GetStringByKey("action", String.Empty), Color.white);

                CacheData.Instance().Usdt -= (double) msg.data["money"];
                CacheData.Instance().Mt -= (double)msg.data["mt"];
                Dispatch(AreaCode.UI,UIEvent.MENU_UPDATE_VIEW,true);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return true;
            }
            else if (msg.data["code"].ToString()=="600")
            { //***********收到转账啦>@^_^@<*****************
                promptMsg.Change(LanguageService.Instance.GetStringByKey("600", String.Empty), Color.white);
                CacheData.Instance().Usdt+= (double)msg.data["money"];
                Dispatch(AreaCode.UI, UIEvent.MENU_UPDATE_VIEW, true);
            }
            Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
            return false;
        }
        public bool checkMoneyResponse(object msg)
        {
            string number="";
                ////Dispatch(AreaCode.UI, UIEvent.Forget_ACTIVE, false);
            if (msg is MessageData<Dictionary<string, object>> data)
            {
                if(data.data!=null&&data.data.ContainsKey("amount"))
                    number = data.data["amount"].ToString();
                promptMsg.Change(LanguageService.Instance.GetStringByKey(data.code.ToString(), String.Empty),
                    Color.white);
                if (data.code == 200)
                {
                    Dispatch(AreaCode.UI, UIEvent.EnterTradeCode_Panel_Active, true);
                    return true;
                }
                else if (data.code == 210)
                {
                    Dispatch(AreaCode.UI, UIEvent.EnterTradeCode_Panel_Active, true);
                    return true;
                }
                else
                {
                    string text = LanguageService.Instance.GetStringByKey(data.code.ToString(), String.Empty)
                        .Replace("number", number);
                    promptMsg.Change(text, Color.white);
                    Dispatch(AreaCode.UI, UIEvent.EnterTradeCode_Panel_Active, true);
                }
            }
            Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
            return false;
        }
        private bool checkPassResponse(string  result)
        {
            promptMsg.Change(LanguageService.Instance.GetStringByKey(result, String.Empty), Color.white);
            if (result == "200")
            {
                Dispatch(AreaCode.UI, UIEvent.EnterTradeCode_Panel_Active, false);
                return true;
            }
            else if (result == "210")
            {
                Dispatch(AreaCode.UI, UIEvent.BusinessesAreUnderfunded_Panle_Active, true);
                return false;
            }
            Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool rechargeReponse(string result)
        {
            promptMsg.Change(LanguageService.Instance.GetStringByKey(result, String.Empty), Color.white);
            if (result == "200")
            {
                promptMsg.Change(LanguageService.Instance.GetStringByKey("action", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                Dispatch(AreaCode.UI, UIEvent.Forget_ACTIVE, false);
                Dispatch(AreaCode.UI, UIEvent.LOG_ACTIVE, true);
                return true;
            }
            Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
            return false;
        }
        /// <summary>
        /// token登入响应
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool tokenReponse(string result)
        {
            if(result == "200")
            {
                Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_ACTIVE, true);
                Dispatch(AreaCode.UI,UIEvent.LOG_ACTIVE,false);
                Dispatch(AreaCode.UI,UIEvent.REG_ACTIVE,false);
                WebData.isLogin = true;
            }
            else if (result == "212")
            {
                promptMsg.Change(LanguageService.Instance.GetStringByKey("212", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                PlayerPrefs.DeleteKey("token");
                SceneMsg msg = new SceneMsg("login", () =>
                {
                    Dispatch(AreaCode.UI, UIEvent.LOG_ACTIVE, true);
                });
                Dispatch(AreaCode.SCENE, SceneEvent.MENU_PLAY_SCENE, msg);
                return false;
            }
            else if (result =="301")
            {
                promptMsg.Change(LanguageService.Instance.GetStringByKey("301", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                PlayerPrefs.DeleteKey("token");
                SceneMsg msg = new SceneMsg("login", () =>
                {
                    Dispatch(AreaCode.UI, UIEvent.LOG_ACTIVE, true);
                });
                Dispatch(AreaCode.SCENE, SceneEvent.MENU_PLAY_SCENE, msg);
                return false;
            }
            return false;
        }
        /// <summary>
        /// 资产信息
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool propertyResonse(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                //todo测试
                
                return false;
            }
            Dispatch(AreaCode.UI, UIEvent.CHARGE_PANEL_ACTIVE, propertyInfo);

            return false;
        }
       
        //private bool propertyResonse(string result)
        //{
        //    if (result == "修改成功!")
        //    {
        //        promptMsg.Change(result.ToString(), Color.green);
        //        Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
        //        Dispatch(AreaCode.UI, UIEvent.Forget_ACTIVE, false);
        //        Dispatch(AreaCode.UI, UIEvent.LOG_ACTIVE, true);
        //        return true;
        //    }
        //    return false;
        //}
    }
}
