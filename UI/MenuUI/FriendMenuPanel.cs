
/***
  * Title:    FriendPanel 
  *
  * Created:	zp
  *
  * CreatTime:  2019/09/10 16:50:31
  *
  * Description: 好友界面
  *
  * Version:    0.1
  *
  *
***/

using System.Collections.Generic;
using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using Assets.Scripts.Model;
using Assets.Scripts.Net;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuUI
{
    public class FriendMenuPanel : UIBase 
    {
        private GameObject FriendBtn;         //好友激活按钮
        private GameObject SquareBtn;         //广场激活按钮
        private GameObject AppyForBtn;        //申请激活按钮  
    private Image FriendClick;            //好友点击按钮换图
    private Image SquareBtnClick;         //广场点击按钮换图
    private Image AppyForClick;           //申请点击按钮换图
    private Image SearchClick;            //搜索点击按钮换图
        private void Awake()
        {
            Bind(UIEvent.FRIENDMENU_PANEL_ACTIVE);
            FriendBtn = transform.Find("bg/FriendActive").gameObject;
            SquareBtn = transform.Find("bg/SquareActive").gameObject;
            AppyForBtn = transform.Find("bg/ApplyForActive").gameObject;
            SquareBtn.SetActive(false);
            AppyForBtn.SetActive(false);
        }
        /// <summary>
        /// 用户名，个人信息
        /// str["123"].img
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="message"></param>
//Dictionary<string ,UserInfo>()
        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.FRIENDMENU_PANEL_ACTIVE:
                    bool flag = (bool) message;
                    setPanelActive(flag);
                    inputSearch.text = "";
                    if (flag)
                    {
                        clickFriend();
                    }
                    break;
                case UIEvent.FRIEND_LIST_PANEL_VIEW:
                    friendData = message as List<UserInfos>;
                    break;
                case UIEvent.SQUARE_LIST_PANEL_VIEW:
                    squareData = message as List<UserInfos>;
                    break;
                case UIEvent.APPLY_PANEL_VIEW:
                    applyData = message as List<UserInfos>;
                    break;
                default:
                    break;
            }
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        Button btnFriend;
        Button btnGround;
        Button btnSearch;
        Button btnApply;
        Button btnClose;
        InputField inputSearch;    //搜索输入

        List<UserInfos> friendData;
        List<UserInfos> squareData;
        List<UserInfos> applyData;

        string nickName;
        private GameObject FriendTitle;
        private GameObject SquareTitle;
        private GameObject ApplyTitle;
        private void Start()
        {
            inputSearch = transform.Find("bg/InputSearch").GetComponent<InputField>();
            btnFriend = transform.Find("bg/BtnFriend").GetComponent<Button>();
            btnApply = transform.Find("bg/BtnApply").GetComponent<Button>();
            btnGround = transform.Find("bg/BtnGround").GetComponent<Button>();
            btnSearch = transform.Find("bg/BtnSearch").GetComponent<Button>();
            btnClose = transform.Find("bg/BtnClose").GetComponent<Button>();
            FriendTitle = transform.Find("bg/FriendTitle").gameObject;
            SquareTitle = transform.Find("bg/SquareTitle").gameObject;
            ApplyTitle = transform.Find("bg/ApplyTitle").gameObject;

            SquareTitle.SetActive(false);
            FriendTitle.SetActive(true);
            ApplyTitle.SetActive(false);
            btnFriend.onClick.AddListener(clickFriend);
            btnSearch.onClick.AddListener(clickSearch);
            btnApply.onClick.AddListener(clickApply);
            btnGround.onClick.AddListener(clickGround);
            btnClose.onClick.AddListener(clickClose);
            setPanelActive(false);
            Multilingual();
        }
        /// <summary>
        /// 多语言
        /// </summary>
        private void Multilingual()
        {
            string language = PlayerPrefs.GetString("language");
            FriendClick = FriendBtn.GetComponent<Image>();
            SquareBtnClick = SquareBtn.GetComponent<Image>();
            AppyForClick = AppyForBtn.GetComponent<Image>();
            btnFriend.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/FrinedMin");
            btnGround.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/"+ language+"/Square");
            btnApply.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/"+ language+ "/ApplyFor");
            FriendClick.sprite = Resources.Load<Sprite>("UI/menu/" + language + "/FriedBig");
            AppyForClick.sprite = Resources.Load<Sprite>("UI/menu/" + language + "/ApplyForBig");
            SquareBtnClick.sprite = Resources.Load<Sprite>("UI/menu/" + language + "/SquareBig");
            SearchClick = btnSearch.GetComponent<Image>();
            SearchClick.sprite = Resources.Load<Sprite>("UI/menu/" + language + "/Search");
        }
        private void OnEnable()
        {
            if (FriendBtn.activeInHierarchy)
            {
                Dispatch(AreaCode.UI, UIEvent.FRIEND_LIST_PANEL_ACTIVE, true);
                Dispatch(AreaCode.UI, UIEvent.FRIEND_LIST_PANEL_VIEW, true);
            }
            else if (SquareBtn.activeInHierarchy)
            {
                Dispatch(AreaCode.UI, UIEvent.SQUARE_LIST_PANEL_ACTIVE, true);
                Dispatch(AreaCode.UI, UIEvent.SQUARE_LIST_PANEL_VIEW, true);
            }
            else if (AppyForBtn.activeInHierarchy)
            {
                Dispatch(AreaCode.UI, UIEvent.APPLYFOR_ACTIVE, true);
            }
        }
        private void clickClose()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            ConCamera.IsActivateTouch = true;
            setPanelActive(false);
            Dispatch(AreaCode.UI, UIEvent.FRIEND_LIST_PANEL_ACTIVE, false);
            Dispatch(AreaCode.UI, UIEvent.SQUARE_LIST_PANEL_ACTIVE, false);
            Dispatch(AreaCode.UI, UIEvent.APPLYFOR_ACTIVE, false);
        }
        private void clickGround()
        {
		
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            SquareTitle.SetActive(true);
            FriendTitle.SetActive(false);
            ApplyTitle.SetActive(false);
            SquareBtn.SetActive(true);
            AppyForBtn.SetActive(false);
            FriendBtn.SetActive(false);
            Dispatch(AreaCode.NET, ReqEventType.squarefriend, null);
            Dispatch(AreaCode.UI, UIEvent.FRIEND_LIST_PANEL_ACTIVE, false);
            Dispatch(AreaCode.UI, UIEvent.SQUARE_LIST_PANEL_ACTIVE, true);
            Dispatch(AreaCode.UI, UIEvent.APPLYFOR_ACTIVE, false);
            //
        }
        private void clickFriend()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            SquareTitle.SetActive(false);
            FriendTitle.SetActive(true);
            ApplyTitle.SetActive(false);
            SquareBtn.SetActive(false);
            AppyForBtn.SetActive(false);
            FriendBtn.SetActive(true);
            Dispatch(AreaCode.UI, UIEvent.FRIEND_LIST_PANEL_ACTIVE, true);
            Dispatch(AreaCode.UI, UIEvent.FRIEND_LIST_PANEL_VIEW, true);
            Dispatch(AreaCode.UI, UIEvent.SQUARE_LIST_PANEL_ACTIVE, false);
            Dispatch(AreaCode.UI, UIEvent.APPLYFOR_ACTIVE, false);
        }
        private void clickApply()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            SquareTitle.SetActive(false);
            FriendTitle.SetActive(false);
            ApplyTitle.SetActive(true);
            SquareBtn.SetActive(false);
            AppyForBtn.SetActive(true);
            FriendBtn.SetActive(false);
            Dispatch(AreaCode.UI, UIEvent.FRIEND_LIST_PANEL_ACTIVE, false);
            Dispatch(AreaCode.UI, UIEvent.SQUARE_LIST_PANEL_ACTIVE, false);
            Dispatch(AreaCode.UI, UIEvent.APPLYFOR_ACTIVE, true);
            Dispatch(AreaCode.NET, ReqEventType.applyfriend, null);
        }
        private void clickSearch()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            nickName = inputSearch.text;
            Dispatch(AreaCode.NET, ReqEventType.searchfriend, nickName);
        }
    }
}
