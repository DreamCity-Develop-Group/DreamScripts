using System;
using Assets.Scripts.Framework;
using Assets.Scripts.Model;
using Assets.Scripts.Scenes;
using Assets.Scripts.Scenes.Msg;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Msg;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Language;
using UnityEngine;

/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:          2019/09/21 14:57:52
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/
namespace Assets.Scripts.Net.Handler
{
   
    public class InvestHandler : HandlerBase
    {
        private HintMsg promptMsg = new HintMsg();
        private UserInfo userInfo = new UserInfo();
        private InvestList investInfos;
        public override bool OnReceive(int subCode, object value)
        {
            switch (subCode)
            {
                case ReqEventType.invest_info:
                    investInfos =value as InvestList;
                    if (investInfos != null && investInfos.playerId == PlayerPrefs.GetString("playerId"))
                    {
                        investInfoResponse(investInfos);
                    }
                    else
                    {
                        investFriendInfoResponse(investInfos);
                    }
                  
                    break;
                case ReqEventType.invest_req:
                    Dictionary<string, object> investInfo = value as Dictionary<string, object>;
                    investResponse(investInfo);
                    break;
                case ReqEventType.Extract:
                    Dictionary<string, object> extractInfo = value as Dictionary<string, object>;
                    extractInfoResponse(extractInfo);
                    break;
                default:
                    break;
            }
            return false;
        }

        /// <summary>
        /// 投资响应
        /// </summary>
        private bool investResponse(Dictionary<string, object> investInfo)
        {
            if (investInfo == null) return false;
            if (!investInfo.ContainsKey("investId")
                ||  !investInfo.ContainsKey("usdtFreeze")
                                                   || !investInfo.ContainsKey("mtFreeze") ||
                                                   !investInfo.ContainsKey("state"))
            {
                return false;
            }
            switch ((int)investInfo["state"])
            {
                case 701:
                    CacheData.Instance().frozenMt += (double)investInfo["mtFreeze"];
                    CacheData.Instance().frozenUsdt += (double) investInfo["usdtFreeze"];
                    CacheData.Instance().Mt -= CacheData.Instance().frozenMt;
                    CacheData.Instance().Usdt -= CacheData.Instance().frozenUsdt;
                    Dispatch(AreaCode.UI, UIEvent.MENU_UPDATE_VIEW, true);
                    Dispatch(AreaCode.UI, UIEvent.INVEST_REDY_VIEW, investInfo["investId"]);
                    break;
                case 702:
                    Dispatch(AreaCode.UI, UIEvent.INVESTED_REPLY_VIEW, investInfo["investId"]);
                    break;
                case 703:
                    Dispatch(AreaCode.UI, UIEvent.INVESTING_REPLY_VIEW, investInfo["investId"]);
                    Dispatch(AreaCode.UI, UIEvent.EARNINGS_PANEL_ACTVTE, investInfo["investId"]);
                    break;

            }
            return true;
          

            //TODO错误
            //promptMsg.Change(result.ToString(), Color.white);
            // Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
        }
        /// <summary>
        /// 投资信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool investInfoResponse(InvestList msg)
        {
            if (msg == null) return false;
            foreach (var itemInvestInfo in msg.investList)
            {
                if (!CacheData.Instance().InvestData.ContainsKey(itemInvestInfo.inType.ToString()))
                {
                    CacheData.Instance().InvestData.Add(itemInvestInfo.inType.ToString(), itemInvestInfo);
                }
            }
            Dispatch(AreaCode.UI, UIEvent.SELECCTINVEST_PANEL_VIEW, null);
            Dispatch(AreaCode.UI, UIEvent.EARNINGS_PANEL_ACTVTE, null);
            return true;
        }

        /// <summary>
        /// 好友投资信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool investFriendInfoResponse(InvestList msg)
        {
            if (msg == null) return false;
            Dispatch(AreaCode.UI, UIEvent.THUMBUP_PANEL_ACTVATE, msg.investList.Count);
            return true;
        }


        /// <summary>
        /// 解锁新项目
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool investInfoUpdate(List<InvestInfo> msg)
        {
            if (msg == null) return false;
            CacheData.Instance().InvestData.Clear();
            foreach (var itemInvestInfo in msg)
            {
                CacheData.Instance().InvestData.Add(itemInvestInfo.inType.ToString(), itemInvestInfo);
            }
            //Dispatch(AreaCode.UI, UIEvent.SENCE_INVEST_VIEW, msg);
            return true;
        }
        /// <summary>
        /// 提取信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool extractInfoResponse(Dictionary<string, object> extractInfo)
        {
            if (extractInfo == null) return false;
            // CacheData.Instance().InvestData = extractInfo;
            if (extractInfo.ContainsKey("income"))
            {
                CacheData.Instance().Usdt += System.Convert.ToDouble(extractInfo["income"]);
            }
            Dispatch(AreaCode.UI, UIEvent.MENU_UPDATE_VIEW, true);
            Dispatch(AreaCode.UI, UIEvent.INVESTED_REPLY_VIEW, true);
          
            return true;
        }
    }
}
