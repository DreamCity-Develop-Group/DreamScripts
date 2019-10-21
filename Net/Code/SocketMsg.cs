using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Assets.Scripts.Net.Code
{
    /// <summary>
    /// 网络消息
    /// 作用：发送的时候 都要发送这个类
    /// </summary>
    [System.Serializable]
    public class SocketMsg<T>
    {
   
        /// <summary>
        ///  //源clientid
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// // 消息数据
        /// </summary>
        public MessageData<T> data { get; set; }
        /// <summary>
        ///  // 发送目的地
        /// </summary>
        public string target { get; set; }
        /// <summary>
        ///  // 消息保存时间
        /// </summary>
        public string createtime { get; set; }
        /// <summary>
        /// // 描述
        /// </summary>
        public string desc { get; set; }

        public int code { get; set; }
        //public  MessageData data=new MessageData() ;
        //public string desc;
        //public string target;
        //public string createtime;
        //public string source;
        public SocketMsg()
        {
        
        }

        public  SocketMsg(string Source,string desc, MessageData<T> data, string target="server")
        {
            source = Source;
            this.target = target;
            createtime = GetTimeStamp();
            this.desc = desc;
            this.data = data;
        }
        /// <summary>
        /// 防止重复创建socket
        /// </summary>
        public void Change(string Source, string desc, MessageData<T> data, string target = "server")
        {
            this.source = Source;
            this.target = target;
            this.createtime = GetTimeStamp();
            this.desc = desc;
            this.data = data;
        }
        /// <summary>
        ///  获取时间戳
        /// </summary>
        /// <returns></returns>
        private string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
    }

    public class MessageData<T>
    {
    

        /// <summary>
        /// //事件类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        ///  //接收事件处理的模块
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// //具体业务数据
        /// </summary>
        ///
        public int code { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T data { get; set; }

        public void Change(string model, string type, T t)
        {
            this.type = type;
            this.model = model;
            this.data = t;
        }
    }
}