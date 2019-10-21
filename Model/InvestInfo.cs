using System.Collections.Generic;
// ReSharper disable InconsistentNaming

/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:          2019/09/21 14:44:23
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/
namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class InvestList
    {
        public string playerId { get; set; }
        public  List<InvestInfo> investList { get; set; }
    }
    [System.Serializable]
    public class InvestInfo
    {

        ///// <summary>
        ///// 好友点赞数
        ///// </summary>
        //public int likeCount { get; set; }
        /// <summary>
        /// 投资项目id
        /// </summary>
        public int investId { get; set; }
        /// <summary>
        /// 可提取usdt
        /// </summary>
        public double extractable { get; set; }
        /// <summary>
        /// 收益余额
        /// </summary>
        public double incomeLeft { get; set; }
        /// <summary>
        /// 投资状态
        /// </summary>
        public int state { get; set; }
        /// <summary>
        /// 开放状态
        /// </summary>
        public string openState { get; set; }

        public int inType { get; set; }
        /// <summary>
        /// 投资金额
        /// </summary>
        public double investMoney { get; set; }
        /// <summary>
        /// 预计收益
        /// </summary>
        public double expectIncome { get; set; }
        /// <summary>
        /// 企业所得税
        /// </summary>
        public double enterpriseTax { get; set; }
        /// <summary>
        /// 个人所得税
        /// </summary>
        public double personTax { get; set; }
        /// <summary>
        /// 定额税
        /// </summary>
        public double quotaTax { get; set; }
        /// <summary>
        /// 预约结果时间
        /// </summary>
        public string resultTime { get; set; }
    }

    public class InvestState
    {
        /// <summary>
        /// 预定中
        /// </summary>
        public const int Ording = 0;

        /// <summary>
        /// 经营中
        /// </summary>
        ///  
        public const int Managing=1;

        /// <summary>
        /// 可提取
        /// </summary>
        public const int Extractable=2;
    }

    public class InvestRequsetInfo
    {

    }
}

