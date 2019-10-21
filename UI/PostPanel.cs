using Assets.Scripts.Net;
using Assets.Scripts.Tools;
using UnityEngine;
using UnityEngine.UI;

/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:          2019/10/01 20:26:11
  *
  * Description:  海报界面
  *
  * Version:    0.1
  *
  *
***/
namespace Assets.Scripts.UI
{
    public class PostPanel : UIBase
    {
        // Start is called before the first frame update
        private RawImage _post1Image;
        private RawImage _post2Image;
        private RawImage _post3Image;
        private RawImage _qrCode1Image;
        private RawImage _qrCode2Image;
        private RawImage _qrCode3Image;
        private Text _inviteCode1;
        private Text _inviteCode2;
        private Text _inviteCode3;
        private Texture2D encode;
        private Button CloseBtn;
        private void Awake()
        {
            Bind(UIEvent.SHARKEPOST_PANEL_VIEW);
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.SHARKEPOST_PANEL_VIEW:
                    setPanelActive(true);
                   
                    ShowPost(message as Texture2D);
                    break;
                default:
                    break;
            }
        }
        void Start()
        {
            _post1Image = transform.Find("Post1").GetComponent<RawImage>();
            _post2Image = transform.Find("Post2").GetComponent<RawImage>();
            _post3Image = transform.Find("Post3").GetComponent<RawImage>();

            _qrCode1Image = transform.Find("Post1/QRCode").GetComponent<RawImage>();
            _qrCode2Image = transform.Find("Post2/QRCode").GetComponent<RawImage>();
            _qrCode3Image = transform.Find("Post3/QRCode").GetComponent<RawImage>();

            _inviteCode1 = transform.Find("Post1/QRCode/InviteCode").GetComponent<Text>();
            _inviteCode2 = transform.Find("Post2/QRCode/InviteCode").GetComponent<Text>();
            _inviteCode3 = transform.Find("Post3/QRCode/InviteCode").GetComponent<Text>();

            CloseBtn = transform.Find("Image").GetComponent<Button>();
            CloseBtn.onClick.AddListener(clickClose);

            setPanelActive(false);
            InitSource();
        }
        /// <summary>
        /// 多语言选择初始化
        /// </summary>
        private void InitSource()
        {
            string language = PlayerPrefs.GetString("language");
            _post1Image.texture = Resources.Load<Texture2D>("UI/menu/" + language + "/" + "Poster1");
            _post2Image.texture = Resources.Load<Texture2D>("UI/menu/" + language + "/" + "Poster2");
            _post3Image.texture = Resources.Load<Texture2D>("UI/menu/" + language + "/" + "Poster3");
        }
        /// <summary>
        /// 展示海报
        /// </summary>
        /// <param name="logo"></param>
        private void ShowPost(Texture2D logo)
        {
            if (CacheData.Instance().CommerceState == 2)
            {
                _inviteCode1.text = CacheData.Instance().CommerceCode;
                _inviteCode2.text = CacheData.Instance().CommerceCode;
                _inviteCode3.text = CacheData.Instance().CommerceCode;

            }
            else
            {
                _inviteCode1.text = null;
                _inviteCode2.text = null;
                _inviteCode3.text = null;

            }
            int num = Random.Range(1, 4);
            switch (num)
            {
                case 1:
                    _post1Image.gameObject.SetActive(true);
                    _qrCode1Image.texture=CreatQRcode(CacheData.QrCode, (int)_qrCode1Image.rectTransform.rect.width , (int)_qrCode1Image.rectTransform.rect.height, logo);
                    break;
                case 2:
                    _post2Image.gameObject.SetActive(true);
                    _qrCode2Image.texture = CreatQRcode(CacheData.QrCode, (int)_qrCode2Image.rectTransform.rect.width, (int)_qrCode2Image.rectTransform.rect.height, logo);
                    break;
                case 3:
                     _post3Image.gameObject.SetActive(true);
                    _qrCode3Image.texture = CreatQRcode(CacheData.QrCode, (int)_qrCode3Image.rectTransform.rect.width, (int)_qrCode3Image.rectTransform.rect.height, logo);
                    break;
            }
        }
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="textForEncoding"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="logo"></param>
        /// <returns></returns>
        private Texture2D CreatQRcode(string textForEncoding, int width, int height, Texture2D logo = null)
        {
            if (string.IsNullOrEmpty(textForEncoding))
            {
                return null;
            }
            encode = new Texture2D(width, height, TextureFormat.RGBA32, false);
            var colors = MsgTool.Encode(textForEncoding, encode.width, encode.height);
            encode.SetPixels32(colors);
            if (logo != null)
            {
                int x = (encode.width - logo.width) / 2;
                int y = (encode.height - logo.height) / 2;
                Color32[] colorlogo = logo.GetPixels32();
                encode.SetPixels32(x, y, logo.width, logo.height, colorlogo);
            }
            encode.Apply();
            return encode;
        }

        private void clickClose()
        {
            _post1Image.gameObject.SetActive(false);
            _post2Image.gameObject.SetActive(false);
            _post3Image.gameObject.SetActive(false);
            setPanelActive(false);
        }
    }
}
