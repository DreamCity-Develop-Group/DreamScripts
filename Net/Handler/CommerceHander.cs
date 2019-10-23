using Assets.Scripts.Framework;
using Assets.Scripts.Net.Handler;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Model;
using Assets.Scripts.UI.Msg;
using UnityEngine;
using System;
using System.Linq;
using Assets.Scripts.Language;

namespace Assets.Scripts.Net.Handler
{
    public class CommerceHander : HandlerBase
    {

        CommerceInfo _commerceData = new CommerceInfo();
        ExchangeInfos _exchangeInfo = new ExchangeInfos();
        private HintMsg promptMsg = new HintMsg();

        public override bool OnReceive(int subCode, object value)
        {
            switch (subCode)
            {
                case ReqEventType.commerce_member:
                    _commerceData = value as CommerceInfo;
                    dicCommerceDataRespon();
                    break;
                case ReqEventType.commerce_sendmt:
                    CommerceMtBuyRespon(value.ToString());
                    break;
                case ReqEventType.commerce_in:
                    CommerceComeInRespon(value.ToString());
                    break;
                case ReqEventType.permission_commerce:
                    CommercePermissionRespon(value.ToString());
                    break;
                case ReqEventType.commercePrompt:
                    CommercePromptRespon(value.ToString());
                    break;
                case ReqEventType.exchangNotice:
                    CommerceExchangeNOTICE(value);
                    break;
                case ReqEventType.buyMt:
                    CommerceMtBuyRespon(value.ToString());
                    break;
                case ReqEventType.ExchangeCenter:
                    _exchangeInfo = value as ExchangeInfos;
                    CommerceExchangeRespon();
                break;
                default:
                    break;
            }
            return false;
        }

       // private HintMsg promptMsg = new HintMsg();

        /// <summary>
        ///商会数据
        /// </summary>
        private void dicCommerceDataRespon()
        {
            if (_commerceData==null)
            {
                Debug.LogError("_commerceData is null");
                return;
            }
            Dispatch(AreaCode.UI, UIEvent.COMMERCE_PANEL_VIEW, _commerceData);
        }
        /// <summary>
        /// 商会升级
        /// </summary>
        private void CommercePromptRespon(string code)
        {
            if (code.Equals("666"))
            {
                promptMsg.Change(LanguageService.Instance.GetStringByKey("666", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, true);
            }

        }
        /// <summa
        /// <summary>
        /// Mt购买
        /// </summary>
        private void CommerceMtBuyRespon(string code)
        {
            //TODO
            //1,
            if (code.Equals("200"))
            {

            }
            Dispatch(AreaCode.UI, UIEvent.COMMERCE_PANEL_VIEW, _commerceData);

        }
        /// <summary>
        /// 商会许可证
        /// </summary>
        /// <param name="msg"></param>
        private void CommercePermissionRespon(object msg)
        {
            promptMsg.Change(LanguageService.Instance.GetStringByKey(msg.ToString(),string.Empty),Color.white);
            if (msg.Equals("200"))
            {
                CacheData.Instance().CommerceState = 2;
                CacheData.Instance().Usdt -= 10;
                Dispatch(AreaCode.UI, UIEvent.MENU_UPDATE_VIEW, true);
                //TODO 创建商会成功，在许可前同步屏蔽停止，取消屏蔽
                promptMsg.Change(LanguageService.Instance.GetStringByKey("你已成功获得经营许可证", string.Empty), Color.white);
                //Dispatch(AreaCode.UI, UIEvent.LOAD_PANEL_HINDED, false);
                Dispatch(AreaCode.UI,UIEvent.CREATCOMMERCE_CHEXCKPASS, false);
                Dispatch(AreaCode.UI, UIEvent.COMMERCE_NOJIONPANEL_ACTIVE, false);
                Dispatch(AreaCode.UI, UIEvent.ConfirmationPaymentPanel_Active, false);
                Dispatch(AreaCode.UI, UIEvent.COMMERCE_PANEL_ACTIVE, true);
            }
            Dispatch(AreaCode.UI,UIEvent.HINT_ACTIVE,promptMsg);
            //else
            //{
            //    Dispatch(AreaCode.UI, UIEvent.COMMERCE_NOJIONPANEL_ACTIVE, msg);

            //}
        }
        /// <summary>
        /// 商会加入
        /// </summary>
        private void CommerceComeInRespon(string msg)
        {
            if (msg.Equals("200"))
            {
                CacheData.Instance().CommerceState = 1;
                Dispatch(AreaCode.UI, UIEvent.CHAMBERCODECRRECT, true);
            }
            if (msg.Equals("512"))
            {
                CacheData.Instance().CommerceState = 1;
                promptMsg.Change(LanguageService.Instance.GetStringByKey("512", String.Empty), Color.white);
                Dispatch(AreaCode.UI,UIEvent.CHAMBERCODECRRECT, true);
            }
        }

        /// <summary>
        /// 兑换中心
        /// </summary>
        private void CommerceExchangeRespon()
        {
            if (_exchangeInfo == null)
            {
                Debug.LogError("_exchangeInfo is null");
                return;
            }
            CacheData.Instance().ExchangePage = _exchangeInfo.page;
            Dispatch(AreaCode.UI, UIEvent.EXCHANGECENTER_STATE_VIEW, _exchangeInfo.list);

        }
        /// <summary>
        /// 交易密码验证响应
        /// </summary>
        private void TradePassWordConfirmRespon(object msg)
        {
            if (msg.ToString()== "success")
            {
                Dispatch(AreaCode.UI, UIEvent.CHAMBEROFCOMMERRULES, true);
            }
            else
            {
                //DOTO 密码错误提示
                promptMsg.Change("密码错误提示", Color.white);
            }

        }


        private void CommerceExchangeNOTICE(object msg)
        {
            Dictionary<int, int> notice = msg as Dictionary<int, int>;
            if (notice == null) return;
            int num = notice.First().Key;
            string  noticeStr = LanguageService.Instance.GetStringByKey(num.ToString(), String.Empty);
            string text = noticeStr.Replace("number", num.ToString());
            //switch (notice.First().Key)
            //{
            //    case 660:
            //         num=notice[660];
            //         noticeStr=LanguageService.Instance.GetStringByKey("660", String.Empty);
            //         text = noticeStr.Replace("number", num.ToString());
            //         //text.Replace("\\n", "\n");
            //        break;
            //    case 661:
            //         num = notice[661];

            //        break;
            //    default:
            //        break;
            //}
            Dispatch(AreaCode.UI,UIEvent.EXECHANGE_PANEL_ACTIVE,text);
        }
    }
}
