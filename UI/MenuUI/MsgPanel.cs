
/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:  2019/09/11 11:22:14
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/

using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using Assets.Scripts.Model;
using Assets.Scripts.Net;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuUI
{
    /// <summary>
    /// 邮件信息
    /// </summary>
    public class MsgPanel : UIBase
    {
        private RectTransform content;//父物体的parent
        //private TextAsset textAsset;//所有菜单信息
        private RectTransform parentRect;//父菜单的prefab
        private RectTransform[] parentArr;//所有父菜单的数组
        private RectTransform childRect;//子菜单的prefab
        private Vector3 parentOffset;//单个父菜单的高度
        private Vector3 childOffset;//单个子菜单的高度
        private int[] cntArr;//所有父菜单拥有的子菜单个数
        private int MaliCount = 0;  //邮件数
        private string EmailContent = null; //邮件信息
        private Image BG;                          //邮件背景框
        private string language;                   //语言版本
        private GameObject MaliBox;                //邮件框

        private Sprite EmailUnread;
        private Sprite EmailHasRead;
        Button btnClose;
        private MessageInfoList msgInfos =new MessageInfoList();

        private int parentArrLength=0;
        private bool isInitMessage=false;
        private void Awake()
        {
            Bind(UIEvent.MSG_PANEL_ACTIVE, UIEvent.MESSAGE_PANEL_VIEW);
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.MSG_PANEL_ACTIVE:
                    if (isInitMessage)
                    {
                        setPanelActive((bool)message);
                    }
                    else
                    {
                        Dispatch(AreaCode.NET, ReqEventType.GetMessage, null);
                    }
                    break;
                case UIEvent.MESSAGE_PANEL_VIEW:
                    if (!isInitMessage)
                    {
                        setPanelActive(true);
                        isInitMessage = true;
                    }
                    msgInfos = message as MessageInfoList;
                    if (msgInfos.messageList.Count > 0)
                    {
                        CacheData.Instance().RedState = msgInfos.messages;
                        cntArr = new int[msgInfos.messageList.Count];
                        Dispatch(AreaCode.UI,UIEvent.MENU_UPDATE_VIEW,true);
                        parentArr = new RectTransform[msgInfos.messageList.Count];
                        //初始化content高度
                        content.sizeDelta = new Vector2(content.rect.width, (parentArrLength+parentArr.Length) * parentRect.rect.height);
                        parentArrLength = parentArr.Length;
                        for (int i = 0; i < msgInfos.messageList.Count; i++)
                        {
                            parentArr[i] = Instantiate(parentRect, content.transform);
                            //编写邮件的标题，发送时间，邮件内容
                            parentArr[i].transform.Find("MailTitle").GetComponent<Text>().text = msgInfos.messageList[i].title;
                            parentArr[i].transform.Find("Time").GetComponent<Text>().text = msgInfos.messageList[i].createtime;
                            EmailContent = msgInfos.messageList[i].content;
                            //根据已读状态改变图片
                            parentArr[i].GetComponent<Image>().sprite = msgInfos.messageList[i].readState ? EmailHasRead : EmailUnread;
                          
                            parentArr[i].localPosition -= i * parentOffset;
                            cntArr[i] = 1;
                            parentArr[i].GetComponent<ParentMenu>().Init(childRect, cntArr[i], EmailContent);
                            int j = i;
                            parentArr[j].GetComponent<Button>().onClick.AddListener(() =>
                            {
                                parentArr[j].GetComponent<Image>().sprite = EmailHasRead;
                                if (!msgInfos.messageList[j].readState)
                                {
                                    Dispatch(AreaCode.NET,ReqEventType.ReadState, msgInfos.messageList[j].id);
                                    msgInfos.messageList[j].readState = true;
                                }
                                OnButtonClick(j);
                            });
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            btnClose.onClick.RemoveAllListeners();
        }
        void Start()
        {

            Init();
            EmailUnread = Resources.Load<Sprite>("UI/menu/EmailUnread");
            EmailHasRead = Resources.Load<Sprite>("UI/menu/EmailHasRead");
            btnClose = transform.Find("bg/BtnClose").GetComponent<Button>();
            btnClose.onClick.AddListener(clickClose);
            setPanelActive(false);
            Multilingual();          
        }
        private void clickClose()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            setPanelActive(false);
            ConCamera.IsActivateTouch = true;
        }
        private void Multilingual()
        {
            language = PlayerPrefs.GetString("language");
            BG = transform.Find("bg").GetComponent<Image>();
            BG.sprite = Resources.Load<Sprite>("UI/menu/" + language + "/MailFrame");
        }
        private System.Collections.Generic.Queue<GameObject> m_queue_gPreObj = new System.Collections.Generic.Queue<GameObject>();          //对象池
        private Transform TempTrans;
        /// <summary>
        /// 创建预制体
        /// </summary>
        /// <param name="Prefab">预制体</param>
        /// <param name="m_transPerfab">预制体父物体的transform</param>
        /// <returns></returns>
        public GameObject CreatePreObj(GameObject Prefab, Transform m_transPerfab)
        {
            GameObject obj = null;
            if (m_queue_gPreObj.Count > 0)
            {
                obj = m_queue_gPreObj.Dequeue();
            }
            else
            {
                Transform trans = null;
                trans = GameObject.Instantiate(Prefab, m_transPerfab).transform;
                //trans.localPosition = Vector3.zero;
                trans.localRotation = Quaternion.identity;
                trans.localScale = Vector3.one;
                obj = trans.gameObject;
                obj.SetActive(false);
            }
            return obj;
        }
        /// <summary>
        /// 预制体回收
        /// </summary>
        /// <param name="obj">回收的预制体</param>
        private void RePreObj(GameObject obj)
        {
            if (obj != null)
            {
                obj.SetActive(false);
                obj.transform.SetParent(TempTrans);
                m_queue_gPreObj.Enqueue(obj);
            }
        }

        void Init()
        {

            content = transform.Find("bg/Emali/Viewport/Content").GetComponent<RectTransform>();
            //textAsset = Resources.Load<TextAsset>("Mail/MsgInfo");

            parentRect = Resources.Load<RectTransform>("Mail/MailBox");
            parentOffset = new Vector3(0, parentRect.rect.height);

            childRect = Resources.Load<RectTransform>("Mail/MailInfo");
            childOffset = new Vector3(0, childRect.rect.height);

            //var info = textAsset.text.Split(',');//获取子菜单个数信息

                  
        }

        void OnButtonClick(int i)
        {

            if (!parentArr[i].GetComponent<ParentMenu>().isCanClick) return;
            parentArr[i].GetComponent<ParentMenu>().isCanClick = false;
            if (!parentArr[i].GetComponent<ParentMenu>().isOpening)
                StartCoroutine(MenuDown(i));
            else
                StartCoroutine(MenuUp(i));
        }

        IEnumerator MenuDown(int index)
        {
            for (int i = 0; i < cntArr[index]; i++)
            {
                //更新content高度
                content.sizeDelta = new Vector2(content.rect.width,
                    content.rect.height + childOffset.y);
                for (int j = index + 1; j < parentArr.Length; j++)
                {
                    parentArr[j].localPosition -= childOffset;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        IEnumerator MenuUp(int index)
        {
            for (int i = 0; i < cntArr[index]; i++)
            {
                //更新content高度
                content.sizeDelta = new Vector2(content.rect.width,
                    content.rect.height - childOffset.y);
                for (int j = index + 1; j < parentArr.Length; j++)
                {
                    parentArr[j].localPosition += childOffset;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}

