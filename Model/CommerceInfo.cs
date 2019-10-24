using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class CommerceInfo
    {
         public List<MermberInfo> members { get; set; }
         /// <summary>
         /// 普通成员数量
         /// </summary>
         public int num { get; set; }
       
    }
    /// <summary>
    /// 商会成员信息
    /// </summary>
     [System.Serializable]
    public class MermberInfo
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string playerName { get; set; }
        /// <summary>
        /// 加入时间
        /// </summary>
        public string createTime { get; set; }

        public string playerId { get; set; }

    }
    [System.Serializable]
    public class ExchangeInfos
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; }
        public  List<ExchangeInfo> list{get; set; }
    }

    /// <summary>
    /// 兑换记录
    /// </summary>
    [System.Serializable]
    public class ExchangeInfo
    {
        
       

        /// <summary>
        /// 昵称
        /// </summary>
        public string player { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderId { get; set; }
        /// <summary>
        /// 支付usdt数
        /// </summary>
        public double pay { get; set; }
        /// <summary>
        /// 兑换数量
        /// </summary>
        public double amount { get; set; }
    /// <summary>
    /// 兑换状态
    /// </summary>
        public int state { get; set; }

    }
    public enum OrderState
    {
        /// <summary>
        /// 取消
        /// </summary>
        CANCEL=0,

        CREATE=1,
        /// <summary>
        /// 
        /// </summary>
        PAID=2,
        /// <summary>
        /// 
        /// </summary>
        PAY=3,
        /// <summary>
        /// 待审核
        /// </summary>
        WAITVERIFY=4,
        /// <summary>
        /// 待发货
        /// </summary>
        TOBESHIPPED = 5,
        /// <summary>
        /// 已发货
        /// </summary>
        SHIPPED = 6,
        /// <summary>
        /// 已收货
        /// </summary>
        RECEIVED=7,

        CLOSE=8,
        /// <summary>
        /// 完成
        /// </summary>
        FINISHED=9,
        /// <summary>
        /// 过期
        /// </summary>
        EXPIRED=10,
        /// <summary>
        /// 拒发货
        /// </summary>
        REFUSE =11,
        /// <summary>
        ///作废
        /// </summary>
        INVALID=-1

    }
    [System.Serializable]
    public class ReqCommerceInfo
    {
        /// <summary>
        /// 成员手机号
        /// </summary>
        public string member_name;
        /// <summary>
        /// 请求结果
        /// </summary>
        public string exchange_result;
        /// <summary>
        /// 企业商会标识
        /// </summary>
        public string invite;
        /// <summary>
        /// mt兑换数
        /// </summary>
        public string mt_count;
        /// <summary>
        /// usdt消耗数
        /// </summary>
        public string usdt_count;

        /// <summary>
        /// 交易密码
        /// </summary>
        public int page;

        public string tradePass;

        public string username;

        public string token;

        public string playerId;

        public List<string> orders;

        public string isAuto;


        public void Change(string ust_count = null, string mt_count = null, string commerce_name = null, string member_name = null, string exchange_result = null, string tradePassword = null, List<string>orderId= null,int page=1,string isAuto=null)
        {
            this.tradePass = tradePassword;
            this.usdt_count = ust_count;
            this.mt_count = mt_count;
            this.invite = commerce_name;
            this.member_name = member_name;
            this.exchange_result = exchange_result;
            this.orders = orderId;
            this.page = page;
            username = PlayerPrefs.GetString("username");
            token = PlayerPrefs.GetString("token");
            playerId = PlayerPrefs.GetString("playerId");
            this.isAuto = isAuto;
        }
    }
}