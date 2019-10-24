

/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:          2019/09/28 13:51:30
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/
using Assets.Scripts.Model;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.U2D;
using WebSocketSharp;

namespace Assets.Scripts.Net
{
    public class CacheData 
    {
        private static volatile CacheData _instance = null;

        private static readonly object LockHelper = new object();

      
        public static void CleanCache()
        {
            _instance = null;
        }

        public static CacheData Instance()

        {

            if (_instance == null)

            {

                lock (LockHelper)

                {

                    if (_instance == null)

                        _instance = new CacheData();

                }

            }

            return _instance;

        }

        private CacheData()
        {
            //CommonSpriteAtlas = Resources.Load<SpriteAtlas>("SpriteAltas/CommonAltas");
            logo = Resources.Load<Texture2D>("");
            //switch (PlayerPrefs.GetString("language"))
            //{
            //    case "Chinese":
            //        BtnSpriteAtlas = Resources.Load<SpriteAtlas>("SpriteAltas/ChineseBtnSpriteAtlas");
            //        break;
            //    case "English":
            //        BtnSpriteAtlas = Resources.Load<SpriteAtlas>("SpriteAltas/EnglishBtnSpriteAtlas");
            //        break;
            //    case "Korean":
            //        BtnSpriteAtlas = Resources.Load<SpriteAtlas>("SpriteAltas/KoreanBtnSpriteAtlas");
            //        break;
            //}
        }
        public string language = "Chinese";

        private Dictionary<string,InvestInfo> _investData=new Dictionary<string, InvestInfo>();
        /// <summary>
        /// 场景个人投资信息
        /// </summary>
        public Dictionary<string, InvestInfo> InvestData { get=>_investData; set=>_investData=value; }
        /// <summary>
        /// 物业信息
        /// </summary>
        private Dictionary<string,StoreInfo> storeInfoDic=new Dictionary<string, StoreInfo>();

        

        private List< StoreInfo> storeInfoList;
        /// <summary>
        /// 是否设置了交易密码
        /// </summary>
        public bool isHasTradePassword;
        /// <summary>
        /// 初始化投资数据
        /// </summary>
        public Dictionary<string, StoreInfo> GetStoreInfo()
        {
            if (storeInfoDic.Count>0)
            {
                return storeInfoDic;
            }
            var investStorePath = "Localization/" + language + "/" + "Scene" + "/" + "InvestStore";

            TextAsset investStoreText = Resources.Load<TextAsset>(investStorePath);
            string investStoreJson = investStoreText.text;
            if (investStoreJson.IsNullOrEmpty())
            {
                return null;
            }
            else
            {
                storeInfoList = JsonMapper.ToObject<List<StoreInfo>>(investStoreJson);
                foreach (var itemStoreInfo in storeInfoList)
                {
                    storeInfoDic.Add(itemStoreInfo.inType,itemStoreInfo);
                }
                return storeInfoDic;
            }
        }

        public Texture2D logo;
        /// <summary>
        /// 公共精灵
        /// </summary>
        public static SpriteAtlas CommonSpriteAtlas;
        /// <summary>
        /// 
        /// </summary>
        public static SpriteAtlas BtnSpriteAtlas;
        /// <summary>
        /// 下载地址
        /// </summary>
        public const string QrCode = "http://156.236.69.200/app/download/";
    /// <summary>
    /// 商会备货金定额
    /// </summary>
    public const double CommerceRestoreMT = 100;
		/// <summary>
 	   /// 昵称
        /// </summary>
        public string nick;
        /// <summary>
        /// 登入成功token
        /// </summary>
        public string Token;

        /// <summary>
        /// 个人账户号
        /// </summary>
        public string Username;

        public string playerId;

        public double Usdt;
        /// <summary>
        /// 显示Mt数
        /// </summary>
        public double Mt;

        public double frozenUsdt;

        public double frozenMt;
        /// <summary>
        /// 修改交易密码的费用
        /// </summary>
        public double ChangExPassWordMt=5;
        /// <summary>
        /// 变化后的Mt数
        /// </summary>
        public double ConsumerThenMt;
        /// <summary>
        /// 充值地址
        /// </summary>
        public string Address;
    /// <summary>
    /// 自动发货按钮状态
    /// </summary>
    public bool SET_AutoState;
	/// <summary>
    /// 0-未加入，1-加入商会，2-已经创建商会
    /// </summary>
	 public int CommerceState;
        /// <summary>
        /// 我的商会邀请码
        /// </summary>
    public string CommerceCode;
        /// <summary>
        /// 商会等级
        /// </summary>
     public int CommerceLevel;
        /// <summary>
        /// 兑换比率
        /// </summary>
        public double ExchangeRate;

     public bool IsPermission;
        /// <summary>
        /// 红点状态
        /// </summary>
     public bool RedState;
    //public 
    /// <summary>
    /// 兑换记录
    /// </summary>
    public List<ExchangeInfo> CommerceExchangeMembers = new List<ExchangeInfo>();
       
    private int _exchangePage;
    /// <summary>
    /// 兑换页码
    /// </summary>
        public int ExchangePage
    {
        get =>_exchangePage;
        set => _exchangePage = value;
    }

    }
}
