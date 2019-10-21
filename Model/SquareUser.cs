using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace Assets.Scripts.Model
{
    /***
      * Title:     
      *
      * Created:	zp
      *
      * CreatTime:          2019/09/21 14:41:14
      *
      * Description:
      *
      * Version:    0.1
      *
      *
    ***/
    [System.Serializable]
    public class SquareUser
    {
        public int pageNum { get; set; }

        public int pageSize { get; set; }
      
        public int startRow { get; set; }

      
        /// <summary>
        /// 
        /// </summary>
        public List<UserInfos> list { get; set; }
    }
    [System.Serializable]
    public class UserInfos
    {
        /// <summary>
        /// 图片
        /// </summary>
        public string imgurl { get; set; }
        /// <summary>
        /// 好友id
        /// </summary>
        public string friendId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nick { get; set; }
        /// <summary>
        /// 是否好友
        /// </summary>
        public bool agree { get; set; }
        /// <summary>
        /// 本身id
        /// </summary>
        public string playerId { get; set; }
        /// <summary>
        /// 用户等级
        /// </summary>
        public string grade { get; set; }
    }
}