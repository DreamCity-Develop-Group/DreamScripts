using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using Assets.Scripts.Language;
using Assets.Scripts.Model;
using Assets.Scripts.Net;
using Assets.Scripts.Scenes;
using Assets.Scripts.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuUI
{
    public class MenuPanel : UIBase
    {
        Button btnFriends;   //好友
        Button btnSet;       //设置
        Button btnMsg;      //邮件
        Button btnCommerce; //商会
        Button btnTreasure; //资产
        Button btnAdd;      //充值USDT + 号按钮 
        Button btnHome;   //主页
        /// <summary>
        /// 
        /// </summary>
        GameObject homePanel;
        /// <summary>
        /// 投资
        /// </summary>
        private Button btnManage;

        private Image imageNoticeImage;
        Image imageBtnCommerce;
        Image imageBtnTreasure;
        Image imageBtnFriends;
        private Image imageMT;
        private Image imageUSDT;
        private Image imageAdd;

        private Image head0Image;
        private Image head1Image;
        private Image head2Image;
        private Image head3Image;
        private Image head4Image;
        private Image head5Image;
        private Image head6Image;
        private Image head7Image;
        private Button headCloseBtn;



        private Text textNickName;
        private Text textLv;

        private GameObject HandPortrait;                    //头像选择
        private Button changeHand;                          //换头像
        private Button[] handArray = new Button[8];         //头像数组
        private int HandID = 0;                             //旋转头像ID

        Transform usdtCharge;
        Text txtUsdt;
        Transform mtCharge;
        Text txtMt;
        string textForEncoding;
        GameObject gameobjectRed;


        GameObject notice;
        float txtLength;
        float txtNoticeLength;
        Text txtNotice1;
        //通知数
        int noticeCount = 2;
        //
        Queue<MenuNoticesItem> noticeQueue=new Queue<MenuNoticesItem>();
        float t = 0;

        private Button test;
        private void Awake()
        {
            Bind(UIEvent.MENU_PANEL_VIEW,UIEvent.MENU_UPDATE_VIEW,UIEvent.PlayerMenu_Panel);
            LanguageService.Instance.Language = new LanguageInfo(PlayerPrefs.GetString("language"));
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.MENU_PANEL_VIEW:
                    MenuInfo menuInfo = message as MenuInfo;

                    if (menuInfo != null)
                    {
                        Dispatch(AreaCode.NET, ReqEventType.invest_info, true);
                        CacheData.Instance().RedState = (bool) menuInfo.messages;
                        //********更新红点状态******//
                        gameobjectRed.SetActive(CacheData.Instance().RedState);
                        InitInfo(menuInfo);
                    }
                    break;
                case UIEvent.MENU_UPDATE_VIEW:
                    //********更新红点状态******//
                    gameobjectRed.SetActive(CacheData.Instance().RedState);
                    txtMt.text =CacheData.Instance().Mt.ToString("#0.00");
                    txtUsdt.text = CacheData.Instance().Usdt.ToString("#0.00");
                    break;
                case UIEvent.PlayerMenu_Panel:
                    UserInfos user = message as UserInfos;
                    if (user != null)
                    {
                        homePanel.SetActive(false);
                        textNickName.text =user.nick;
                        textLv.text = user.grade;
                        btnHome.gameObject.SetActive(true);
                    }
                    else
                    {
                        textNickName.text = CacheData.Instance().nick;
                        textLv.text = CacheData.Instance().CommerceLevel.ToString();
                        homePanel.SetActive(true);
                    }
                    break;
                default:
                    break;
            }
        }

        private void Start()
        {
            //CacheData.Instance();
            test = transform.Find("Button").GetComponent<Button>();
            test.onClick.AddListener(clickTest);
            HandPortrait = transform.Find("HandFrame").gameObject;
            btnTreasure = transform.Find("HomePanel/BtnTreasure").GetComponent<Button>();
            btnCommerce = transform.Find("HomePanel/BtnCommerce").GetComponent<Button>(); 
            btnSet = transform.Find("BtnSet").GetComponent<Button>();
            btnMsg = transform.Find("BtnMsg").GetComponent<Button>();
            btnFriends = transform.Find("HomePanel/BtnFriends").GetComponent<Button>();
            btnAdd = transform.Find("BtnAdd").GetComponent<Button>();
            btnManage = transform.Find("HomePanel/BtnManage").GetComponent<Button>();
            btnHome = transform.Find("BtnHome").GetComponent<Button>();
            headCloseBtn = HandPortrait.transform.Find("Frame/CloseBtn").GetComponent<Button>();
            headCloseBtn.onClick.AddListener(clickCloseHead);
            homePanel = transform.Find("HomePanel").gameObject;

            imageBtnCommerce = btnCommerce.transform.GetComponent<Image>();
            imageBtnFriends = btnFriends.transform.GetComponent<Image>();
            imageBtnTreasure = btnTreasure.transform.GetComponent<Image>();
            imageNoticeImage = transform.Find("Notice").GetComponent<Image>();

            imageMT = transform.Find("MTCharge/Image").GetComponent<Image>();
            imageUSDT = transform.Find("USDTCharge/Image").GetComponent<Image>();
          
            head0Image = transform.Find("HandFrame/Frame/Hand0").GetComponent<Image>();
            head1Image = transform.Find("HandFrame/Frame/Hand1").GetComponent<Image>();
            head2Image = transform.Find("HandFrame/Frame/Hand2").GetComponent<Image>();
            head3Image = transform.Find("HandFrame/Frame/Hand3").GetComponent<Image>();
            head4Image = transform.Find("HandFrame/Frame/Hand4").GetComponent<Image>();
            head5Image = transform.Find("HandFrame/Frame/Hand5").GetComponent<Image>();
            head6Image = transform.Find("HandFrame/Frame/Hand6").GetComponent<Image>();
            head7Image = transform.Find("HandFrame/Frame/Hand7").GetComponent<Image>();
          
            txtUsdt = transform.Find("USDTCharge/Text").GetComponent<Text>() ;
            txtMt = transform.Find("MTCharge/Text").GetComponent<Text>();
            btnAdd = transform.Find("BtnAdd").GetComponent<Button>();
            
            textNickName = transform.Find("BtnPersonInfo/NickName").GetComponent<Text>();
            textLv = transform.Find("BtnPersonInfo/Lv").GetComponent<Text>();


            notice = transform.Find("Notice").gameObject;
            txtLength = notice.GetComponent<RectTransform>().rect.width;
      
            txtNotice1 = transform.Find("Notice/TxtNotice").GetComponent<Text>();
            txtNoticeLength = txtNotice1.GetComponent<RectTransform>().rect.width;
            gameobjectRed = transform.Find("BtnMsg/ImgRed").gameObject;
            changeHand = transform.Find("BtnPersonInfo/BtnHead").GetComponent<Button>();
            for (int i = 0; i < handArray.Length; i++)
            {
                handArray[i] = HandPortrait.transform.Find("Frame/Hand" + i).GetComponent<Button>();
            }

            btnHome.onClick.AddListener(()=>
            {
                btnHome.gameObject.SetActive(false);
                //TODO展示一个加载页面
                Dispatch(AreaCode.UI,UIEvent.LOADING_ACTIVE,null);
                homePanel.SetActive(true);
                //TODO 获取本身数据
            });
            btnManage.onClick.AddListener(clickManage);
            HandPortrait.SetActive(false);
            btnAdd.onClick.AddListener(clickAdd);
            btnTreasure.onClick.AddListener(clickTreasure);
            btnSet.onClick.AddListener(clickSet);
            btnFriends.onClick.AddListener(clickFriend);
            btnMsg.onClick.AddListener(clickEmali);
            btnCommerce.onClick.AddListener(clickCommerce);
            changeHand.onClick.AddListener(clickChangeHand);
            handArray[0].onClick.AddListener(clickHand0);
            handArray[1].onClick.AddListener(clickHand1);
            handArray[2].onClick.AddListener(clickHand2);
            handArray[3].onClick.AddListener(clickHand3);
            handArray[4].onClick.AddListener(clickHand4);
            handArray[5].onClick.AddListener(clickHand5);
            handArray[6].onClick.AddListener(clickHand6);
            handArray[7].onClick.AddListener(clickHand7);
            txtNotice1.gameObject.SetActive(false);

            MangLange();
            HandID = PlayerPrefs.GetInt("HandID");
            changeHand.GetComponent<Image>().sprite = handArray[HandID].GetComponent<Image>().sprite;
          
            Dispatch(AreaCode.NET, ReqEventType.menu_req, null);
            //initSource();
            LanguageService.Instance.Language = new LanguageInfo(PlayerPrefs.GetString("language"));
            Dispatch(AreaCode.UI, UIEvent.LANGUAGE_VIEW, PlayerPrefs.GetString("language"));
        }


        
        /// <summary>
        /// 多语言切换
        /// </summary>
        private void MangLange()
        {
            string language = PlayerPrefs.GetString("language");
            //language = "Chinese";
            SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("UI/HeadSpriteAlta");
            head0Image.sprite = spriteAtlas.GetSprite("Head0");
            head1Image.sprite = spriteAtlas.GetSprite("Head1");
            head2Image.sprite = spriteAtlas.GetSprite("Head2");
            head3Image.sprite = spriteAtlas.GetSprite("Head3");
            head4Image.sprite = spriteAtlas.GetSprite("Head4");
            head5Image.sprite = spriteAtlas.GetSprite("Head5");
            head6Image.sprite = spriteAtlas.GetSprite("Head6");
            head7Image.sprite = spriteAtlas.GetSprite("Head7");

            //btnAdd.transform.GetComponent<Image>().sprite = CacheData.CommonSpriteAtlas.GetSprite("Add");
            //imageMT.sprite = CacheData.CommonSpriteAtlas.GetSprite("MT");
            //imageUSDT.sprite = CacheData.CommonSpriteAtlas.GetSprite("USDT");
            //imageNoticeImage.sprite = CacheData.CommonSpriteAtlas.GetSprite("InformFrame");
            btnManage.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/Investment");
            btnFriends.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/Friend");
            btnCommerce.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/Chamber");
            //btnTreasure.GetComponent<Image>().sprite = CacheData.BtnSpriteAtlas.GetSprite("Asset");
            btnTreasure.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/Asset");
            HandPortrait.transform.Find("Frame").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/HandFrame");
            //btnManage.GetComponent<Image>().sprite = CacheData.BtnSpriteAtlas.GetSprite("Investment"); //
            //btnFriends.GetComponent<Image>().sprite = CacheData.BtnSpriteAtlas.GetSprite("Friend");//
            //btnCommerce.GetComponent<Image>().sprite = CacheData.BtnSpriteAtlas.GetSprite("Chamber");//
            //btnTreasure.GetComponent<Image>().sprite = CacheData.BtnSpriteAtlas.GetSprite("Asset");/ 
            //HandPortrait.transform.Find("Frame").GetComponent<Image>().sprite = CacheData.BtnSpriteAtlas.GetSprite("HandFrame"); // 
        }

        void clickTest()
        {
            Dispatch(AreaCode.NET, ReqEventType.invest_info, true);
            //Dispatch(AreaCode.NET, ReqEventType.Extract, 3);
            //Dispatch(AreaCode.NET, ReqEventType.menu_req, null);
        }
        private void InitInfo(MenuInfo menuInfo)
        {
          
            CacheData.Instance().ExchangeRate = menuInfo.rate;
            CacheData.Instance().CommerceLevel = menuInfo.level;
            CacheData.Instance().Mt = menuInfo.account.mt;
            CacheData.Instance().Usdt =menuInfo.account.usdt;
            CacheData.Instance().Address=menuInfo.account.address;
            CacheData.Instance().CommerceCode = menuInfo.profile.invite;
            CacheData.Instance().nick = menuInfo.profile.nick;
            CacheData.Instance().CommerceLevel = menuInfo.level;
            txtMt.text = menuInfo.account.mt.ToString("#0.00");
            txtUsdt.text = menuInfo.account.usdt.ToString("#0.00");
            noticeCount = menuInfo.notices.Count;
            textNickName.text = menuInfo.profile.nick;
            textLv.text = menuInfo.profile.level.ToString();
            foreach (var item in menuInfo.notices)
            {
                noticeQueue.Enqueue(item);
            }
            //txtNotice1.text = menuInfo.notices
            if (noticeCount > 0)
            {
                notice.gameObject.SetActive(true);
                StartCoroutine(NoticeStart());
            }
            CacheData.Instance().CommerceState = menuInfo.commerce;

            //menuInfo.data.notices
        }

        private void UpdateMenuInfo()
        {

        }

        //private void initSource()
        //{
        //    //string language = PlayerPrefs.GetString("language");
        //    string language = "chinese";
        //    imageBtnCommerce.sprite = Resources.Load<Sprite>("UI/menu/"+language+"/"+"商会@2x");
        //    imageBtnFriends.sprite = Resources.Load<Sprite>("UI/menu/" + language + "/" + "好友@2x");
        //    imageBtnTreasure.sprite = Resources.Load<Sprite>("UI/menu/" + language + "/" + "资产@2x");
        //}

        private void IsHasMsg(bool hasCount)
        {
            if (hasCount)
            {
                gameobjectRed.SetActive(true);
            }
            else
            {
                gameobjectRed.SetActive(false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>


        IEnumerator NoticeStart()
        {
            while (noticeCount>0)
            {
                noticeCount--;
                txtNotice1.gameObject.SetActive(true);
                t = 0;
                txtNotice1.text = noticeQueue.Dequeue().noticeContent;
                StartCoroutine(NoticeRuning());
                yield return new WaitForSeconds(15);
            }
            notice.gameObject.SetActive(false);
        }

        IEnumerator NoticeRuning()
        {
            while (t<=1)
            {
                txtNotice1.transform.localPosition = new Vector2(Mathf.Lerp((txtLength+txtNoticeLength)/2,-(txtLength + txtNoticeLength)/ 2, t),0);
                t+= Time.deltaTime/10;
                yield return new WaitForEndOfFrame();
            }
        }

        private void clickManage()
        {
            //投资
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.UI,UIEvent.SELECTINVEST_PANEL_ACTIVE, true);
            ConCamera.IsActivateTouch = false;
        }
        private void clickAdd()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.UI,UIEvent.QRECODE_PANEL_ACTIVE,MsgTool.CreatQRcode(CacheData.Instance().Address));

        }
        private void initSource()
        {
            
            //string language = "chinese";
          //  Debug.Log(language);
            Image image=null;
            //随机生成海报
            string language = PlayerPrefs.GetString("language");
            int num = Random.Range(1, 3);
            image.sprite = Resources.Load<Sprite>("UI/menu/" + language + "/" + "Poster"+num);
            //headImage.sprite = Resources.Load<Sprite>("UI/login/" + language + "/" + "HeadTitle");
            //loginImage.sprite = Resources.Load<Sprite>("UI/login/" + language + "/" + "dengluhuang");
            //getIndentityImage.sprite = Resources.Load<Sprite>("UI/login/" + language + "/" + "huoquyanzhengma@2x");
        }
       
        private void clickFriend()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.UI, UIEvent.FRIENDMENU_PANEL_ACTIVE, true);
            ConCamera.IsActivateTouch = false;
        }
        private void clickTreasure()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.NET,ReqEventType.property,true);
            ConCamera.IsActivateTouch = false;
        }

        private void clickSet()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.UI,UIEvent.SET_PANEL_ACTIVE,true);
            ConCamera.IsActivateTouch = false;

        }

        private void clickEmali()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            Dispatch(AreaCode.UI, UIEvent.MSG_PANEL_ACTIVE, true);
            ConCamera.IsActivateTouch = false;
        }
        /// <summary>
        /// 点击商会图标
        /// </summary>
        private void clickCommerce()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            switch (CacheData.Instance().CommerceState)
            {
                case 0:
                    Dispatch(AreaCode.UI, UIEvent.COMMERCE_NOJIONPANEL_ACTIVE, true);
                   
                    break;
                case 1:
                    Dispatch(AreaCode.UI, UIEvent.CHAMBERCODECRRECT, true);
                    break;
                case 2:
                    Dispatch(AreaCode.UI, UIEvent.COMMERCE_PANEL_ACTIVE, true);
                    break;
            }
            ConCamera.IsActivateTouch = false;
        }
        private void clickChangeHand()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            HandPortrait.SetActive(true);
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            btnTreasure.onClick.RemoveAllListeners();
            btnSet.onClick.RemoveAllListeners();
        }
        private void clickHand0()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            changeHand.GetComponent<Image>().sprite = handArray[0].GetComponent<Image>().sprite;
            HandPortrait.SetActive(false);
            HandID = 0;
            PlayerPrefs.SetInt("HandID", HandID);
        }
        private void clickHand1()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            changeHand.GetComponent<Image>().sprite = handArray[1].GetComponent<Image>().sprite;
            HandPortrait.SetActive(false);
            HandID = 1;
            PlayerPrefs.SetInt("HandID", HandID);
        }
        private void clickHand2()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            changeHand.GetComponent<Image>().sprite = handArray[2].GetComponent<Image>().sprite;
            HandPortrait.SetActive(false);
            HandID = 2;
            PlayerPrefs.SetInt("HandID", HandID);
        }
        private void clickHand3()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            changeHand.GetComponent<Image>().sprite = handArray[3].GetComponent<Image>().sprite;
            HandPortrait.SetActive(false);
            HandID = 3;
            PlayerPrefs.SetInt("HandID", HandID);
        }
        private void clickHand4()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            changeHand.GetComponent<Image>().sprite = handArray[4].GetComponent<Image>().sprite;
            HandPortrait.SetActive(false);
            HandID = 4;
            PlayerPrefs.SetInt("HandID", HandID);
        }
        private void clickHand5()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            changeHand.GetComponent<Image>().sprite = handArray[5].GetComponent<Image>().sprite;
            HandPortrait.SetActive(false);
            HandID = 5;
            PlayerPrefs.SetInt("HandID", HandID);
        }
        private void clickHand6()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            changeHand.GetComponent<Image>().sprite = handArray[6].GetComponent<Image>().sprite;
            HandPortrait.SetActive(false);
            HandID = 6;
            PlayerPrefs.SetInt("HandID", HandID);
        }
        private void clickHand7()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            changeHand.GetComponent<Image>().sprite = handArray[7].GetComponent<Image>().sprite;
            HandPortrait.SetActive(false);
            HandID = 7;
            PlayerPrefs.SetInt("HandID", HandID);
        }
        private void clickCloseHead()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            HandPortrait.SetActive(false);
        }

    }
}
