using System;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Framework;
using Assets.Scripts.Language;
using UnityEngine;
using UnityEngine.UI;
/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:          2019/10/11 16:09:12
  *
  * Description: 
  *
  * Version:    0.1
  *
  *
***/
public class ExchangeRequestPanel : UIBase
{
   
    Text _myText;
    private Button btnClose;
    private Button btnExchange;
    

    private void Awake()
    {
        Bind(UIEvent.EXECHANGE_PANEL_ACTIVE);
    }

    protected internal override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.EXECHANGE_PANEL_ACTIVE:
                setPanelActive(true);
                _myText.text = message.ToString();
                break;
            default:
                break;
        }
    }

    void Start()
    {
        _myText = transform.Find("Bg/Text").GetComponent<Text>();
        btnClose = transform.Find("Bg/BtnClose").GetComponent<Button>();
        btnExchange = transform.Find("Bg/BtnExchange").GetComponent<Button>();

        btnClose.onClick.AddListener(() =>
        {
            setPanelActive(false);
        });
        btnExchange.onClick.AddListener(() =>
        {
            Dispatch(AreaCode.UI,UIEvent.EXECHANGECENTER_PANEL_ACTIVE,true);
        });
        setPanelActive(false);
    }

    private void InitSource()
    {
        string language = PlayerPrefs.GetString("language");

        btnExchange.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/ToChange");
    }
}
