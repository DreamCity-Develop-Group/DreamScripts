

/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:          2019/09/30 19:42:02
  *
  * Description: 邮箱信息
  *
  * Version:    0.1
  *
  *
***/

using System;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class MessageInfoList
    {
        public bool messages { get; set; }
        public List<MessageInfo> messageList { get; set; }
    }
    [System.Serializable]
    public class MessageInfo
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string createtime { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 读取状态
        /// </summary>
        public bool readState { get; set; }
        /// <summary>
        /// 邮件id
        /// </summary>
        public int id { get; set; }
    }
}
