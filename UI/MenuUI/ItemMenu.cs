using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuUI
{
    /// <summary>
    /// 帮助折叠二级
    /// </summary>
    public class ItemMenu : MonoBehaviour
    {        
        private GameObject childMenu;//子菜单的parent
        private RectTransform[] childs;//所有子菜单的rect
        private RectTransform itemRect;//子菜单的prefab
        private Vector3 offset;//单个子菜单的高度
        private int count;//子菜单的个数
        private RectTransform TwoChildRect;//第三级菜单预制体
        public bool IsOpening { get; private set; }//父菜单是否展开
        public bool IsCanClick { get; set; }//父菜单是否可以点击
        private RectTransform Content;           //容器

        private void Start()
        {
            Content = transform.parent.parent.parent.GetComponent<RectTransform>();
            TwoChildRect = Resources.Load<RectTransform>("Mail/Items");
            InitItem(TwoChildRect, 1);
        }
        public void InitItem(RectTransform rect, int count, string EmailContent = null)
        {
            childMenu = transform.Find("ChildItem").gameObject;
            itemRect = rect;
            this.count = count;
            childs = new RectTransform[this.count];
            offset = new Vector3(0, itemRect.rect.height);
            for (int i = 0; i < this.count; i++)
            {
                childs[i] = Instantiate(itemRect, childMenu.transform);
            }
            childMenu.gameObject.SetActive(false);
            IsOpening = false;
            IsCanClick = true;
            GetComponent<Button>().onClick.AddListener(OnButtonClick);
        }

        void OnButtonClick()
        {
            if (!IsCanClick) return;
            if (!IsOpening)
            {
                StartCoroutine(ShowChildMenu());
            }
               
            else
                StartCoroutine(HideChildMenu());
        }

        IEnumerator ShowChildMenu()
        {
            childMenu.gameObject.SetActive(true);
            for (int i = 0; i < count; i++)
            {
                childs[i].localPosition -= i * offset;
                yield return new WaitForSeconds(0.1f);
            }
            IsCanClick = true;
            IsOpening = true;
        }

        IEnumerator HideChildMenu()
        {
            for (int i = count - 1; i >= 0; i--)
            {
                childs[i].localPosition += i * offset;
                yield return new WaitForSeconds(0.1f);
            }
            childMenu.gameObject.SetActive(false);
            IsCanClick = true;
            IsOpening = false;
        }
    }
}
