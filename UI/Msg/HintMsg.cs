using UnityEngine;

namespace Assets.Scripts.UI.Msg
{
    public class HintMsg
    {
        public string Text;
        public Color Color;

        public HintMsg()
        {

        }

        public HintMsg(string text, Color color)
        {
            this.Text = text;
            this.Color = color;
        }

        /// <summary>
        /// 避免了频繁new对象
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public void Change(string text, Color color)
        {
            this.Text = text;
            this.Color = color;
        }
    }
}
