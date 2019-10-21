using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    class PropertyInfo
    {
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string username;
        /// <summary>
        /// 积累总收入
        /// </summary>
        public double accumulated_total_income;
        /// <summary>
        /// 持仓总资产
        /// </summary>
        public string total_property;
        /// <summary>
        /// 持仓usdt
        /// </summary>
        public string total_usdt;
        /// <summary>
        /// 持仓mt
        /// </summary>
        public string total_mt;
        /// <summary>
        /// 可用usdt
        /// </summary>
        public string available_usdt;
        /// <summary>
        /// 可用mt
        /// </summary>
        public string available_mt;
        /// <summary>
        /// 冻结usdt
        /// </summary>
        public string frozen_usdt;
        /// <summary>
        /// 冻结mt
        /// </summary>
        public string frozen_mt;
        /// <summary>
        /// 商会等级
        /// </summary>
        public string commerce_lv;
        /// <summary>
        /// 商会成员数
        /// </summary>
        public string commerce_member;
        /// <summary>
        /// 商会邀请码
        /// </summary>
        public string invite;

    }
    [System.Serializable]
    public class TradeRecordList
    {

        public List<TradeRecord> tradeRecordList { get; set; }
    }
    [System.Serializable]
    public class TradeRecord
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public string createTime { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string tradeDetailType { get; set; }
        /// <summary>
        /// 收支
        /// </summary>
        public double  tradeAmount { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string tradeStatus { get; set; }
        /// <summary>
        /// usdt余额
        /// </summary>
        public double usdtSurplus { get; set; }
        /// <summary>
        /// mt余额
        /// </summary>
        public double mtSurplus { get; set; }
    }
}
