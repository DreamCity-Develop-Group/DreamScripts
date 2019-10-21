//using Assets.Scripts.Framework;
//using UnityEngine.UI;

///***
//  * Title:     
//  *
//  * Created:	zp
//  *
//  * CreatTime:          2019/09/20 18:46:00
//  *
//  * Description: 登入注册页面
//  *
//  * Version:    0.1
//  *
//  *
//***/
//namespace Assets.Scripts.UI.LoginUI
//{
//    public class LoginRegiestPanel : UIBase
//    {
//        Button btnRegist;
//        Button btnLogin;

//        private void Awake()
//        {
//            //Bind(UIEvent.LOGINSELECT_PANEL_ACTIVE);
//        }

//        protected internal override void Execute(int eventCode, object message)
//        {
//            switch (eventCode)
//            {
//                case UIEvent.LOGINSELECT_PANEL_ACTIVE:
//                    //setPanelActive((bool)message);
//                    break;
//                default:
//                    break;
//            }
//        }
//        // Start is called before the first frame update
//        void Start()
//        {
//            btnLogin = transform.Find("BtnLogin").GetComponent<Button>();
//            btnRegist = transform.Find("BtnRegist").GetComponent<Button>();

//            btnLogin.onClick.AddListener(clickLogin);
//            btnRegist.onClick.AddListener(clickRegist);
//        }

//        private void clickLogin()
//        {
        
//            Dispatch(AreaCode.UI,UIEvent.LOG_ACTIVE,true);
//        }

//        private void clickRegist()
//        {
//            Dispatch(AreaCode.UI,UIEvent.REG_ACTIVE,true);
//        }
//        // Update is called once per frame
//        void Update()
//        {
        
//        }
//    }
//}
