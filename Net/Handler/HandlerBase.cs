using Assets.Scripts.Framework;

namespace Assets.Scripts.Net.Handler
{
    public abstract class HandlerBase
    {
        /// <summary>
        /// 发消息
        /// </summary>
        /// <param name="areaCode">Area code.</param>
        /// <param name="eventCode">Event code.</param>
        /// <param name="message">Message.</param>
        public void Dispatch(int areaCode, int eventCode, object message)
        {
            MsgCenter.Instance.Dispatch(areaCode, eventCode, message);
        }

        /// <summary>
        ///  接收消息
        /// </summary>
        /// <param name="subCode"></param>
        /// <param name="value"></param>
        public virtual bool OnReceive(int subCode, object value)
        {
            return false;
        }
    }
}
