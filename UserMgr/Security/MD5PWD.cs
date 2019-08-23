using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace UserMgr.Security
{
    public static class MD5PWD
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string GetMD5PWD(string pwd)
        {
            //新建对象
            MD5 mD5 = new MD5CryptoServiceProvider();
            //字符串转字节组后计算HASH值
            byte[] date = mD5.ComputeHash(Encoding.Default.GetBytes(pwd));
            mD5.Clear();
            return Convert.ToBase64String(date);
        }
    }
}