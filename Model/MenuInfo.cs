using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:          2019/09/23 09:08:12
  *
  * Description:主界面数据
  *
  * Version:    0.1
  *
  *
***/
    public class MenuInfo
    {
       

        /// <summary>
        /// 
        /// </summary>
        public Profile profile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int commerce { get; set; }
        /// <summary>
        /// 商会等级
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 兑换比率
        /// </summary>
        public double rate { get; set; }
        /// <summary>
        /// 红点状态
        /// </summary>
        public bool messages { get; set; }
        /// <summary>
        /// mt usdt
        /// </summary>
        public MenuAccount account { get; set; }


        /// <summary>
        /// ֪ͨ
        /// </summary>
        public List<MenuNoticesItem> notices { get; set; }
    }
    public class Profile
    {
        private string Nick;

        private int Level;
        /// <summary>
        /// 
        /// </summary>
        public string nick { get =>Nick ; set=>Nick=value; }
        /// <summary>
        /// 
        /// </summary>
        public int level { get => Level; set => Level = value; }
    }

    public class MenuAccount
    {

        public bool isHasTradePassword { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double mt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double usdt { get; set; }

        public string address { get; set; }
    }

    public class MenuNoticesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int noticeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string noticeContent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int noticeState { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createTime { get; set; }
    }

}