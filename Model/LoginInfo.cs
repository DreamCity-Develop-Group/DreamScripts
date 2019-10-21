namespace Assets.Scripts.Model
{
    [System.Serializable]
    public class LoginInfo 
    {
        public LoginInfo()
        {

        }

        public static string ClientId { get; set; }
        public string IsIdentityLog { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Identity { get; set; }
    }
}
