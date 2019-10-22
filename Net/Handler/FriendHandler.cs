/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:  2019/09/09 18:50:30
  *
  * Description: 好友添加，删除，点赞，搜索响应处理
  *
  * Version:    0.1
  *
***/

using System;
using System.Collections.Generic;
using Assets.Scripts.Framework;
using Assets.Scripts.Language;
using Assets.Scripts.Model;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Msg;
using UnityEngine;

namespace Assets.Scripts.Net.Handler
{
    public class FriendHandler : HandlerBase
    {
        SquareUser _squareData = new SquareUser();
        SquareUser _friendData = new SquareUser();
        SquareUser _applyData = new SquareUser();
        private UserInfo userInfo = new UserInfo();
        public override bool OnReceive(int subCode, object value)
        {
            switch (subCode)
            {
                case ReqEventType.addfriend:
                    //diFriendData = value as Dictionary<string, UserInfo>;
                    addfriendRespon();
                    break;
                case ReqEventType.listfriend:
                    _friendData = value as SquareUser;
                    listfriendRespon();
                    break;
                case ReqEventType.searchfriend:
                    searchfriendRespon(value);
                    break;
                case ReqEventType.applyfriend:
                    _applyData = value as SquareUser;
                    applyListRespon();
                    break;
                case ReqEventType.squarefriend:
                    _squareData = value as SquareUser;
                    dicSquareFriendRespon();
                    break;
                case ReqEventType.applytofriend:
                    isAgreedResonse(value.ToString());
                    break;
                default:
                    break;
            }
            return false;
        }

        private HintMsg promptMsg = new HintMsg();

        /// <summary>
        /// 广场列表
        /// </summary>
        private void dicSquareFriendRespon()
        {
            if (_squareData.list.Count < 1)
            {
                Debug.LogError("dicSquareFriend is null");
                return;
            }
            Dispatch(AreaCode.UI,UIEvent.SQUARE_LIST_PANEL_VIEW, _squareData.list);

        }

        /// <summary>
        /// 好友列表
        /// </summary>
        /// <param name="value"></param>
        private void listfriendRespon()
        {
            if (_friendData.list.Count < 1)
            {
                Debug.LogError("dicFriendData is null");
                return;
            }
            Dispatch(AreaCode.UI,UIEvent.FRIEND_LIST_PANEL_VIEW, _friendData);
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="value"></param>
        private void searchfriendRespon(object value)
        {
            SquareUser square = value as SquareUser;
            //todo 
            if (square.list.Count == 1)
            {
                Dispatch(AreaCode.UI, UIEvent.SEARCH_PANEL_VIEW, square.list[0]);
            }
            else
            {
                Dispatch(AreaCode.UI, UIEvent.SQUARE_LIST_PANEL_VIEW, square);
            }
           
        }
        private void addfriendRespon()
        {

        }
        /// <summary>
        /// 申请好友列表
        /// </summary>
        List<UserInfo> applyList = new List<UserInfo>();
        private void applyListRespon()
        {
            if (_applyData == null)
            {
                return;
            }
            Dispatch(AreaCode.UI, UIEvent.APPLYFOR_VIEW, _applyData.list);
        }
        /// <summary>
        /// 申请好友响应
        /// </summary>
        private void applyfriendRespon(object msg)
        {
            if (msg == null)
            {
                //对方拒绝
            }
            else
            {
                Dictionary<string,string> applyfriendDic = msg as Dictionary<string, string>;
                userInfo.Imgurl = applyfriendDic["imgurl"];
                userInfo.NickName = applyfriendDic["nick"];
                userInfo.FriendLink = applyfriendDic["friendlink"];
                //TODO
            }
        }
        /// <summary>
        /// 搜索应答
        /// </summary>
        /// <param name="msg"></param>
        private void searchuserReson(object msg)
        {

        }

        /// <summary>
        ///申请好友同意与否响应
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool isAgreedResonse(string result)
        {

            if (result == "200")
            {
                //promptMsg.Change(LanguageService.Instance.GetStringByKey("action", String.Empty), Color.white);
                //Dispatch(AreaCode.UI, UIEvent.HINT_ACTIVE, promptMsg);
                Dispatch(AreaCode.UI, UIEvent.Forget_ACTIVE, false);
                Dispatch(AreaCode.UI, UIEvent.LOG_ACTIVE, true);
                return true;
            }
            return false;
        }


    }
}
