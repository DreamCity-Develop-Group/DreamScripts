using Assets.Scripts.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:          2019/10/10 10:47:23
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/
public class AccountInfo
{
    public void Change(string newpw, string code, string token, string userpass, string nick, string invite=null)
    {
        this.newpw = newpw;
        this.code = code;       
        this.userpass = userpass;
        this.nick = nick;
        this.invite = invite;
        this.username = PlayerPrefs.GetString("username");
        this.playerId = PlayerPrefs.GetString("playerId");
        this.token = PlayerPrefs.GetString("token");
    }

    public string username { get; set; }
    public string newpw { get; set; }
    public string code { get; set; }
    public string token { get; set; }
    public string userpass { get; set; }
    public string nick { get; set; }
    public string invite { get; set; }
    public string playerId { get; set; }
  
}
