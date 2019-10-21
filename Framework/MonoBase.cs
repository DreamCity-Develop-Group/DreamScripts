using UnityEngine;

namespace Assets.Scripts.Framework
{
    /// <summary>
    /// 扩展 MonoBehaviour
    /// </summary>
    public class MonoBase : MonoBehaviour 
    {

        /// <summary>
        /// 定义一个虚方法
        /// </summary>
        protected internal virtual void Execute(int eventCode,  object message)
        {
        }

    }
}
