/***
  * Title:     邮件折叠功能
  *
  * Created:	zzg
  *
  * CreatTime:  2019/09/21
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuUI
{
    /// <summary>
    ///  邮件折叠功能
    /// </summary>
    public class MsgFoldableMenu : MonoBehaviour
    {
        private RectTransform content;//父物体的parent
        private TextAsset textAsset;//所有菜单信息
        private RectTransform parentRect;//父菜单的prefab
        private RectTransform[] parentArr;//所有父菜单的数组
        private RectTransform childRect;//子菜单的prefab
        private Vector3 parentOffset;//单个父菜单的高度
        private Vector3 childOffset;//单个子菜单的高度
        private int[] cntArr;//所有父菜单拥有的子菜单个数
        private int MaliCount = 0;  //邮件数
        private string EmailContent = null; //邮件信息



        private void Start()
        {      
            Init();
        }

        void Init()
        {
            MaliCount = 10;
            content = transform.Find("Viewport/Content").GetComponent<RectTransform>();
            textAsset = Resources.Load<TextAsset>("Mail/MsgInfo");

            parentRect = Resources.Load<RectTransform>("Mail/MailBox");
            parentOffset = new Vector3(0, parentRect.rect.height);

            childRect = Resources.Load<RectTransform>("Mail/MailInfo");
            childOffset = new Vector3(0, childRect.rect.height);

            var info = textAsset.text.Split(',');//获取子菜单个数信息
            cntArr = new int[info.Length];
            parentArr = new RectTransform[MaliCount];
            //初始化content高度
            content.sizeDelta = new Vector2(content.rect.width, parentArr.Length * parentRect.rect.height);

            if(MaliCount>0)
            {
                for (int i = 0; i < MaliCount; i++)
                {
                    parentArr[i] = Instantiate(parentRect, content.transform);
                    //编写邮件的标题，发送时间，邮件内容
                    switch (i)
                    {
                        case 0:
                            parentArr[i].transform.Find("MailTitle").GetComponent<Text>().text = "邮件标题";
                            parentArr[i].transform.Find("Time").GetComponent<Text>().text = "发送时间";
                            break;
                        case 1:
                            parentArr[i].transform.Find("MailTitle").GetComponent<Text>().text = "邮件标题";
                            parentArr[i].transform.Find("Time").GetComponent<Text>().text = "发送时间";
                            break;
                        case 2:
                            parentArr[i].transform.Find("MailTitle").GetComponent<Text>().text = "邮件标题";
                            parentArr[i].transform.Find("Time").GetComponent<Text>().text = "发送时间";
                            break;
                        case 3:
                            parentArr[i].transform.Find("MailTitle").GetComponent<Text>().text = "邮件标题";
                            parentArr[i].transform.Find("Time").GetComponent<Text>().text = "发送时间";
                            break;
                    }
                    parentArr[i].localPosition -= i * parentOffset;
                    cntArr[i] = int.Parse(info[i]);
                    parentArr[i].GetComponent<ParentMenu>().Init(childRect, cntArr[i], EmailContent);
                    int j = i;
                    parentArr[i].GetComponent<Button>().onClick.AddListener(() => { OnButtonClick(j); });
                }
            }
        
           
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
