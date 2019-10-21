/***
  * Title:    ApplyForFriend 
  *
  * Created:	zzg
  *
  * CreatTime:  2019/09/17 09:45:07
  *
  * Description: 申请列表界面
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
    /// <summary>
    /// 申请界面
    /// </summary>
    public class ApplyForFriend : UIBase
    {
        private void Awake()
        {
            Bind(UIEvent.APPLYFOR_ACTIVE, UIEvent.APPLYFOR_VIEW);
        }
        /// <summary>
        /// 广场用户数据
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="message"></param>
        List<UserInfos> dicSquareData = new List<UserInfos>();
        private GameObject PersonalInformationBox;           //列表信息框预制体
        private Transform ListBox;                           //列表框
        private List<GameObject> list_InformationBox = new List<GameObject>();
        Dictionary<string, string> t = new Dictionary<string, string>()
        {
            ["nick"] = "",
            ["agree"] = ""
        };
        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.APPLYFOR_ACTIVE:
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
                case UIEvent.APPLYFOR_VIEW:
                    dicSquareData = message as List<UserInfos>;
                    if (dicSquareData != null && dicSquareData.Count > 0)
                    {
                        GameObject obj = null;
                        for (int i = 0; i < dicSquareData.Count; i++)
                        {
                            obj = CreatePreObj(PersonalInformationBox, ListBox);
                            obj.transform.SetParent(ListBox);
                            obj.SetActive(true);
                            list_InformationBox.Add(obj);
                            //obj里可以查找显示信息的物体，然后在赋值
                            string nick = dicSquareData[i].nick;
                        obj.transform.Find("Name").GetComponent<Text>().text = dicSquareData[i].nick;
                        obj.transform.Find("LV").GetComponent<Text>().text = dicSquareData[i].grade;
                        //obj.transform.Find("Hand").GetComponent<Image>().sprite = 
                         obj.transform.Find("Agreed").GetComponent<Button>().onClick.AddListener(()=>
                         {
                             Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
                             t["nick"] = nick;
                             t["agree"] = "agreed";
                             Dispatch(AreaCode.NET,ReqEventType.applytofriend,t);

                             RePreObj(obj);

                         });
                         obj.transform.Find("DontAgree").GetComponent<Button>().onClick.AddListener(() =>
                         {
                             Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
                             t["nick"] = nick;
                             t["agree"] = "disagreed";
                             Dispatch(AreaCode.NET, ReqEventType.applytofriend, t);

                             RePreObj(obj);
                         });
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
            PersonalInformationBox = Resources.Load("PerFab/AppLYFriend") as GameObject;
            ListBox = transform.Find("Viewport/Content");
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
    }
}
