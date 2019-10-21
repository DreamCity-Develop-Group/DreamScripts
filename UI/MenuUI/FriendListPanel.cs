
/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:  2019/09/17 09:51:03
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/

using System.Collections.Generic;
using Assets.Scripts.Framework;
using Assets.Scripts.Model;
using Assets.Scripts.Net;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuUI
{
    public class FriendListPanel : UIBase
    {
        private void Awake()
        {
            Bind(UIEvent.FRIEND_LIST_PANEL_ACTIVE, UIEvent.FRIEND_LIST_PANEL_VIEW);
            PersonalInformationBox0 = Resources.Load("PerFab/FriendFrame0") as GameObject;
            PersonalInformationBox1 = Resources.Load("PerFab/FriendFrame1") as GameObject;
            ListBox = transform.Find("FriendList/Viewport/Content");
        }
        /// <summary>
        /// 好友数据
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="message"></param>
    
        List<UserInfos> dicFriendData = new List<UserInfos>();
        private GameObject PersonalInformationBox0;           //列表信息框预制体
        private GameObject PersonalInformationBox1;           //列表信息框预制体
        private Transform ListBox;                           //列表框
        private List<GameObject> list_InformationBox = new List<GameObject>();

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.FRIEND_LIST_PANEL_ACTIVE:
                    setPanelActive((bool)message);
                    if((bool)message==false)
                    {
                        foreach (var t in list_InformationBox)
                        {
                            RePreObj(t);
                        }

                        list_InformationBox.Clear();
                    }
              
                    break;
                case UIEvent.FRIEND_LIST_PANEL_VIEW:
                
                    dicFriendData = message as List< UserInfos>;
                    if (dicFriendData != null && dicFriendData.Count > 0)
                    {
                        GameObject obj = null;
                        for (int i = 0; i < dicFriendData.Count; i++)
                        {
                            if(i%2==0)
                            {
                                obj = CreatePreObj(PersonalInformationBox0, ListBox);
                            }
                            else
                            {
                                obj = CreatePreObj(PersonalInformationBox1, ListBox);
                            }
                            obj.transform.SetParent(ListBox);
                            obj.SetActive(true);
                            list_InformationBox.Add(obj);
                            //obj里可以查找显示信息的物体，然后在赋值
                        obj.transform.Find("Name").GetComponent<Text>().text = dicFriendData[i].nick;
                        obj.transform.Find("LV").GetComponent<Text>().text = string.IsNullOrEmpty(dicFriendData[i].grade)?"Lv0":"Lv"+ dicFriendData[i].grade;
                        //obj.transform.Find("Hand").GetComponent<Image>().sprite=换头像
                        obj.GetComponent<Button>().onClick.AddListener(
                            () =>
                            {
                                Dispatch(AreaCode.UI,UIEvent.LOADING_ACTIVE, dicFriendData[i]);
                                setPanelActive(false);
                                Dispatch(AreaCode.UI, UIEvent.FRIENDMENU_PANEL_ACTIVE, false);
                                Dispatch(AreaCode.NET,ReqEventType.invest_info, dicFriendData[i].playerId);
                                ConCamera.IsActivateTouch = true;
                                
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
        public GameObject CreatePreObj(GameObject Prefab,Transform m_transPerfab)
        {
            GameObject obj = null;
            if(m_queue_gPreObj.Count>0)
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
            if(obj!=null)
            {
                obj.SetActive(false);
                obj.transform.SetParent(TempTrans);
                m_queue_gPreObj.Enqueue(obj);
            }
        }
    }
}
