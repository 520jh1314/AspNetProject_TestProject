using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QProject.Application.Tool
{
    public class DataFormatTools
    {
        /// <summary>
        /// 数据中间部分显示为特殊字符   可用与身份证号、姓名等
        /// </summary>
        /// <param name="value">需要替换的字符串</param>
        /// <param name="startLen">前保留长度</param>
        /// <param name="endLen">尾保留长度</param>
        /// <param name="specialChar">特殊字符</param>
        /// <returns>被特殊字符替换的字符串</returns>
        public static string ReplaceWithSpecialChar(string value, int startLen, int endLen, char specialChar = '*')
        {
            try
            {
                int lenth = value.Length - startLen - endLen;

                string replaceStr = value.Substring(startLen, lenth);

                string specialStr = string.Empty;

                for (int i = 0; i < replaceStr.Length; i++)
                {
                    specialStr += specialChar;
                }

                value = value.Replace(replaceStr, specialStr);
            }
            catch (Exception)
            {
                throw;
            }

            return value;
        }
    }
}
