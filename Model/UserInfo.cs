namespace Assets.Scripts.Model
{
    [System.Serializable]
//注册用户信息
    public class UserInfo
    {
       
        public UserInfo()
        {

        }
        public UserInfo(string phone, string password, string identity, string inviteCode, string nickName)
        {
            this.Phone = phone;
            this.Password = password;
            this.Identity = identity;
            this.InviteCode = inviteCode;
            this.NickName = nickName;
        }
        public string Like;
        public string Imgurl;
        public string FriendLink;
        public string Phone{ get ; set; }
        public string Password { get; set; }
        public string Identity { get; set; }
        public string InviteCode { get; set; }
        public string NickName { get; set; }
    }
}
