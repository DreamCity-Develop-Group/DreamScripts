using UnityEngine;
using UnityEngine.UI;

/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:          2019/09/21 16:17:48
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/
namespace Assets.Scripts.UI
{
    public class TestClickLike : UIBase
    {
        Button btnClickLike;
        public GameObject[] imgStart;
        int value = 0;
        // Start is called before the first frame update
        void Start()
        {
            btnClickLike = transform.Find("BtnClickLike").GetComponent<Button>();
            btnClickLike.onClick.AddListener(clickLike);
        }
        void clickLike()
        {
            if (value < 20)
            {
                imgStart[value].SetActive(true);
            }
            value++;
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
