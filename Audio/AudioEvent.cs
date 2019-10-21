using System.CodeDom;

namespace Assets.Scripts.Audio
{
    public class AudioEvent
    {
       // public const int PLAY_EFFECT_AUDIO = 45646132;//播放音效文件
        /// <summary>
        /// 点击音效
        /// </summary>
        public const int PLAY_CLICK_AUDIO =1;
        /// <summary>
        /// 点赞音效
        /// </summary>
        public const int LIKE_CLICK_AUDIO = 2;
        /// <summary>
        /// 商会升级音效
        /// </summary>
        public const int COMMERCE_PROMPT_AUDIO = 3;
        /// <summary>
        /// 提现音效
        /// </summary>
        public const int EXACTABLE_AUDIO = 4;
        /// <summary>
        /// 背景音效
        /// </summary>
        public const int PLAY_BACKGROUND_AUDIO = 5;
    }
}
