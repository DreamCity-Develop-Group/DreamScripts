
/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:  2019/09/10 10:18:34
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/

using Assets.Scripts.Framework;

namespace Assets.Scripts.Net.Request
{
    public abstract class RequestBase 
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
    }
}
