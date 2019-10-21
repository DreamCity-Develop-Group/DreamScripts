/***
  * Title:    ChargePanel 
  *
  * Created:	zzg
  *
  * CreatTime:  2019/10/8 11:58:34
  *
  * Description: Òýµ¼Ò³
  *
  * Version:    0.1
  *
  *
***/
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class GuidePagePanel : UIBase
{
    private Sprite[] Page = new Sprite[9];
    private string language;
    private Button PageBtn;
    private Image ImaPage;
    private int index;
    void Start()
    {
      
        language = PlayerPrefs.GetString("language");
        language = "chinese";
        for (int i = 0; i < Page.Length; i++)
        {
            Page[i] = Resources.Load<Sprite>("UI/menu/" + language + "/Page" + i);
        }
        PageBtn = transform.Find("Page").GetComponent<Button>();
        ImaPage = PageBtn.GetComponent<Image>();
        ImaPage.sprite = Page[0];
        PageBtn.onClick.AddListener(ClickPage);
      
        if ((PlayerPrefs.GetInt("reg")^PlayerPrefs.GetInt("guide")) ==1)
        {
            setPanelActive(true);
            PlayerPrefs.SetInt("guide", Mathf.Abs(PlayerPrefs.GetInt("guide")-1));
        }
        else
        {
            setPanelActive(false);
        }
        //PlayerPrefs.DeleteAll();
    }
    private void ClickPage()
    {
        Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
        index++;
        if(index >= Page.Length)
        {
            transform.gameObject.SetActive(false);
        }
        else
        {
            ImaPage.sprite = Page[index];
        }

    }
}
