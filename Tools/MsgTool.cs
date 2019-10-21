using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

namespace Assets.Scripts.Tools
{
    public static class MsgTool 
    {

        /// <summary>   
        /// MD5加密   
        /// </summary>   
        /// <param name="strSource">需要加密的字符串</param>   
        /// <returns>MD5加密后的字符串</returns>   
        public static string Md5Encrypt(string strSource)
        {
            //把字符串放到byte数组中   
            byte[] bytIn = System.Text.Encoding.Default.GetBytes(strSource);
            //建立加密对象的密钥和偏移量           
            byte[] iv = { 102, 16, 93, 156, 78, 4, 218, 32 };//定义偏移量   
            byte[] key = { 55, 103, 246, 79, 36, 99, 167, 3 };//定义密钥   
            //实例DES加密类   
            DESCryptoServiceProvider mobjCryptoService = new DESCryptoServiceProvider();
            mobjCryptoService.Key = iv;
            mobjCryptoService.IV = key;
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            //实例MemoryStream流加密密文件   
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();

            string strOut = System.Convert.ToBase64String(ms.ToArray());
            return strOut;
        }

        /// <summary>
        /// 用MD5加密字符串，可选择生成16位或者32位的加密字符串
        /// </summary>
        /// <param name="password">待加密的字符串</param>
        /// <param name="bit">位数，一般取值16 或 32</param>
        /// <returns>返回的加密后的字符串</returns>
        public static string Md5Encrypt(string password, int bit)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("UTF-8").GetBytes(password));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            if (bit == 16)
                return tmp.ToString().Substring(8, 16);
            else
            if (bit == 32) return tmp.ToString();//默认情况
            else return string.Empty;
        }
        /// <summary>
        /// 用MD5加密字符串
        /// </summary>
        /// <param name="password">待加密的字符串</param>
        /// <returns></returns>
        public static string MD5Encrypt(string password)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("UTF-8").GetBytes(password));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            return tmp.ToString();
        }
        /// <summary>
        /// 匹配手机格式
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool CheckMobile(string mobile)
        {
            Regex regex = new Regex(@"^[1][3-9]\d{9}$");
            return regex.IsMatch(mobile);
        }
        /// <summary>
        /// 匹配登入密码格式
        /// </summary>
        /// <param name="pw"></param>
        /// <returns></returns>
        public static bool CheckPass(string pw)
        {
            Regex regex = new Regex(@"^(?=.*\d)(?=.*[A-Za-z])[\x20-\x7e]{6,12}$");
            return regex.IsMatch(pw);
        }

        /// <summary>
        /// 匹配交易密码格式
        /// </summary>
        /// <param name="pw"></param>
        /// <returns></returns>
        public static bool CheckExPass(string pw)
        {
            Regex regex = new Regex(@"^([0-9A-Za-z]){6,12}$");
            return regex.IsMatch(pw);
        }
        /// <summary>
        /// 匹配昵称格式
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool CheckNickName(string nick)
        {
            Regex regex = new Regex(@"^.{2,10}$");
            return regex.IsMatch(nick);
        }
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="textForEncoding"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Color32[] Encode(string textForEncoding, int width, int height)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = 2,
                    PureBarcode = true
                }
            };
            return writer.Write(textForEncoding);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textForEncoding"></param>
        /// <param name="logo"></param>
        /// <returns></returns>
        public static Texture2D CreatQRcode(string textForEncoding, Texture2D logo = null)
        {
            if (string.IsNullOrEmpty(textForEncoding))
            {
                return null;
            }
            Texture2D encode = new Texture2D(100, 100, TextureFormat.RGBA32, false);
            var colors = MsgTool.Encode(textForEncoding, encode.width, encode.height);
            encode.SetPixels32(colors);
            //if (logo != null)
            //{
            //    int x = (encode.width-logo.width) / 2;
            //    int y = (encode.height-logo.height) / 2;
            //    Color32[] colorlogo = logo.GetPixels32();
            //    encode.SetPixels32(x,y,logo.width,logo.height,colorlogo);
            //}
            encode.Apply();
            return encode;
        }
        /// <summary>
        /// 点赞星级
        /// </summary>
        public static void ClickLikeToday()
        {

        }

        public static void ClickInvest()
        {

        }

        //void DoPicture()//存储为png
        //{

        //    if (!File.Exists(filespath))//首先判断一下该图片文件是否存在
        //    {
        //        if (!Directory.Exists(folderpath))//再判断一下该文件夹是否存在，若不存在先创建
        //        {
        //            Directory.CreateDirectory(folderpath);//创建目录
        //        }
        //        var bys = encoded.EncodeToPNG();//转换图片资源
        //        File.WriteAllBytes(filespath, bys);//保存图片到写好的目录下
        //    }
        //}
        /// <summary>
        /// GB2312转换成UTF8
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string gb2312_utf8(string text)
        {
            //声明字符集   
            System.Text.Encoding utf8, gb2312;
            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            byte[] gb;
            gb = gb2312.GetBytes(text);
            gb = System.Text.Encoding.Convert(gb2312, utf8, gb);
            //返回转换后的字符   
            return utf8.GetString(gb);
        }

        /// <summary>
        /// UTF8转换成GB2312
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string utf8_gb2312(string text)
        {
            //声明字符集   
            System.Text.Encoding utf8, gb2312;
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            byte[] utf;
            utf = utf8.GetBytes(text);
            utf = System.Text.Encoding.Convert(utf8, gb2312, utf);
            //返回转换后的字符   
            return gb2312.GetString(utf);
        }
    }
}
