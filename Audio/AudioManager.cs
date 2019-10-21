using Assets.Scripts.Framework;

namespace Assets.Scripts.Audio
{
    /// <summary>
    /// 声音模块管理器
    /// </summary>
    public class AudioManager : ManagerBase
    {
        public const int PLAY_AUDIO = 0;

        public static AudioManager Instance = null;

        void Awake()
        {
            Instance = this;
        }

    }
}
