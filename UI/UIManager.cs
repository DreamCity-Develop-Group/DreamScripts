using Assets.Scripts.Framework;

namespace Assets.Scripts.UI
{
    public class UIManager : ManagerBase 
    {

        public static UIManager Instance = null;

        void Awake()
        {
            Instance = this;
        }
    }
}
