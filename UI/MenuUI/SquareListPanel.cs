
/***
  * Title:    SquareListPanel 
  *
  * Created:	zp
  *
  * CreatTime:  2019/09/17 09:45:07
  *
  * Description: 广场列表界面
  *
  * Version:    0.1
  *
  *
***/

using System;
using System.Collections.Generic;
using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using Assets.Scripts.Model;
using Assets.Scripts.Net;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuUI
{
    public class SquareListPanel : UIBase
    {
        private void Awake()
        {
            Bind(UIEvent.SQUARE_LIST_PANEL_ACTIVE, UIEvent.SQUARE_LIST_PANEL_VIEW);
        }
        /// <summary>
        /// 广场用户数据
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="message"></param>
        List<UserInfos> squareData;
        private GameObject PersonalInformationBox;           //列表信息框预制体
        private Transform ListBox;                           //列表框
        private List<GameObject> list_InformationBox = new List<GameObject>();
        private Button InABatchBtn;                          //换一批按钮
        private string language;



        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.SQUARE_LIST_PANEL_ACTIVE:
                    setPanelActive((bool)message);
                    if ((bool)message == false)
                    {
                        for (int i = 0; i < list_InformationBox.Count; i++)
                        {
                            RePreObj(list_InformationBox[i]);
                        }
                        list_InformationBox.Clear();
                    }
                    break;
                case UIEvent.SQUARE_LIST_PANEL_VIEW:
                    squareData = message as  List<UserInfos>;
                    if (squareData != null && squareData.Count > 0)
                    {
                        GameObject obj = null;
                        foreach (var t in squareData)
                        {
                            obj = CreatePreObj(PersonalInformationBox, ListBox);
                            obj.transform.SetParent(ListBox);
                            obj.SetActive(true);
                            list_InformationBox.Add(obj);
                            //obj里可以查找显示信息的物体，然后在赋值
                            string friendNick= t.nick;
                            obj.transform.Find("Name").GetComponent<Text>().text = t.nick;
                            obj.transform.Find("LV").GetComponent<Text>().text = string.IsNullOrEmpty(t.grade) ? "Lv0" : "Lv" + t.grade;
                            obj.transform.Find("Add").GetComponent<Image>().sprite= Resources.Load<Sprite>("UI/menu/" + language + "/AddFriend");
                            var obj1 = obj;
                            obj.transform.Find("Add").GetComponent<Button>().onClick.AddListener(()=>
                            {

                                obj1.transform.Find("Add").gameObject.SetActive(false);
                                obj1.transform.Find("Aplied").GetComponent<Image>().sprite= Resources.Load<Sprite>("UI/menu/" + language + "/Applied");
                                obj1.transform.Find("Aplied").gameObject.SetActive(true);
                                Dispatch(AreaCode.NET,ReqEventType.addfriend, friendNick);
                            });
                            obj.GetComponent<Button>().onClick.AddListener(
                                () =>
                                {
                                    Dispatch(AreaCode.UI, UIEvent.LOADING_ACTIVE, t);
                                    setPanelActive(false);
                                    Dispatch(AreaCode.UI,UIEvent.FRIENDMENU_PANEL_ACTIVE,false);
                                    Dispatch(AreaCode.NET, ReqEventType.invest_info, t.playerId);
                                }
                            );
                        }
                    }
                    //TODO
                    break;
                default:
                    break;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            PersonalInformationBox = Resources.Load("PerFab/SquareFriend1") as GameObject;
            ListBox = transform.Find("SquareFriendsList/Viewport/Content");
            InABatchBtn = transform.Find("BtnNextGround").GetComponent<Button>();
            InABatchBtn.onClick.AddListener(clickInABatch);
            language = PlayerPrefs.GetString("language");
            InABatchBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/"+ language+ "/InABatch");
            setPanelActive(false);
        }

        private Queue<GameObject> m_queue_gPreObj = new Queue<GameObject>();          //对象池
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
        /// <summary>
        /// 换一批
        /// </summary>
        private void clickInABatch()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        }     
        /// <summary>
        /// 点击加好友做什么
        /// </summary>
        private void clickAddFriend()
        {
        }
    }
}
