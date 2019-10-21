/***
  * Title:    TransferPanel
  *
  * Created:	zzg
  *
  * CreatTime:  2019/09/23 10:00
  *
  * Description: 转账界面
  *
  * Version:    0.1
  *
  *
***/

using System;
using System.Collections.Generic;
using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using Assets.Scripts.Language;
using Assets.Scripts.Model;
using Assets.Scripts.UI.Msg;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    /// <summary>
    /// 转账界面
    /// </summary>
    public class TransferPanel : UIBase
    {
        private InputField TransactionMoney;                        //转账金额
        private InputField TransferTheAddress;                      //转账地址
        private Button BtnConfirm;                                  //确定按钮
        private Button BtnClose;                                    //关闭按钮
        private string language;                                   //语言版本
        private TransferInfo transferInfo; //转账信息
        private GameObject OutOfServiceHours;                     //不在服务时间
        private Button OutOfServiceBtn;
        HintMsg promptMsg = new HintMsg();
        //不在服务时间的OK按钮 
        private void Awake()
        {
            Bind(UIEvent.TRANSFERACCOUNTS_ACTIVE);
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.TRANSFERACCOUNTS_ACTIVE:
                    setPanelActive(true);
                    TransactionMoney.text = "";
                    TransferTheAddress.text = "";
                    break;
                default:
                    break;
            }
        }
        void Start()
        {
            transferInfo = new TransferInfo();
            TransactionMoney = transform.Find("bg/InputChargeField").GetComponent<InputField>();
            TransferTheAddress = transform.Find("bg/InputFieldAddress").GetComponent<InputField>();
            BtnConfirm = transform.Find("bg/BtnDetermine").GetComponent<Button>();
            BtnClose = transform.Find("bg/BtnClose").GetComponent<Button>();
            OutOfServiceHours = transform.Find("TransfeTime").gameObject;
            OutOfServiceBtn = OutOfServiceHours.transform.Find("BtnConfig").GetComponent<Button>();
            BtnConfirm.onClick.AddListener(clickConfirm);
            BtnClose.onClick.AddListener(clickClose);
            OutOfServiceBtn.onClick.AddListener(clickOK);
            OutOfServiceHours.SetActive(false);
            setPanelActive(false);
            language = PlayerPrefs.GetString("language");
            BtnConfirm.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/ConfirmBig");
            OutOfServiceBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu/" + language + "/OK");

        }
        /// <summary>
        /// 关闭面板
        /// </summary>
        private void clickClose()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            setPanelActive(false);
        }
        /// <summary>
        /// 确定按钮
        /// </summary>
        private void clickConfirm()
        {
            string money = TransactionMoney.text;
            string address = TransferTheAddress.text;
            transferInfo.money = System.Convert.ToDouble(money);
            transferInfo.accAddr = address;
            if (string.IsNullOrEmpty(money))
            {
                promptMsg.Change(LanguageService.Instance.GetStringByKey("请输入转账金额", String.Empty), Color.white);
                Dispatch(AreaCode.UI,UIEvent.HINT_ACTIVE,promptMsg);
                return;
            }
            if (string.IsNullOrEmpty(address))
            {
                promptMsg.Change(LanguageService.Instance.GetStringByKey("请输入钱包地址", String.Empty), Color.white);
                Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                return;
            }
            Dispatch(AreaCode.UI, UIEvent.TRANSACTIONCODE_ACTIVE, transferInfo);
          
        }
        private void clickOK()
        {
            Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_CLICK_AUDIO, "ClickVoice");
            OutOfServiceHours.SetActive(false);
        }
    }
}
