
/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:  2019/09/16 11:28:49
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/

namespace Assets.Scripts.UI.MenuUI
{
    public class SecurityPanel : UIBase
    {

        private void Awake()
        {
            Bind(UIEvent.VOICE_PANEL_ACTIVE);
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.VOICE_PANEL_ACTIVE:
                    setPanelActive((bool)message);
                    break;
                default:
                    break;
            }
        }



        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
