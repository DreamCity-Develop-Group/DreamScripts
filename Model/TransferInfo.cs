using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class TransferInfo
    {

        public  TransferInfo()
        {
           
        }

        public string playerId { get; set; }
        public string token { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public double money { get; set; }

        public string username { get; set; }
        /// <summary>
        /// 转账地址
        /// </summary>
        public string accAddr { get; set; }

        /// <summary>
        /// 交易密码
        /// </summary>
        public string oldpwshop { get; set; }

        public void Change(double money ,string moneyaddress,string oldpwshop)
        {
            this.money = money;
            this.accAddr = moneyaddress;
            this.oldpwshop = MsgTool.MD5Encrypt(oldpwshop);
            this.username = PlayerPrefs.GetString("username");
            token = PlayerPrefs.GetString("token");
            playerId = PlayerPrefs.GetString("playerId");
        }
      
    }
}