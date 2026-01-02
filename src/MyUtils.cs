using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSMultiCheats
{
    internal static class MCSUtils
    {
        // 境界标识简化
        public static string MiniXiuWei(string level)
        {
            return level.Replace("初", "☳").Replace("中", "☱").Replace("后", "☰").Substring(0, 3);
        }

        // 指数偏移运算 -> 底数加1
        public static int OffsetPow(int num, int power = 2, int offset = 1)
        {
            return (int)Math.Pow((double)(num + offset), (double)power);
        }

        // 整数偏移运算 - > 加1
        public static int OffsetNum(int num, int offset = 1)
        {
            return num + offset;
        }

        // 英文逗号分隔的字符串 -> 整型列表
        public static List<int> StringToList(string strNums)
        {
            List<int> listNums = new List<int>();
            foreach (string num in strNums.Split(','))
            {
                if (float.TryParse(num, out float n))
                {
                    listNums.Add((int)n);
                }
            }
            return listNums;
        }

        // 基于英文逗号分割的字符串，生成可用Int32数组对象
        public static JSONObject ArrayObject(string nums)
        {
            JSONObject jsonObject = new JSONObject(JSONObject.Type.ARRAY);

            foreach (int num in StringToList(nums))
            {
                jsonObject.Add(num);
            }
            return jsonObject;
        }

        // 创建ID关联表
        public static JSONObject DictObject(int id, int value1, int value2 = 0)
        {
            JSONObject jsonObject = new JSONObject();

            jsonObject.SetField("id", id);
            jsonObject.SetField("value1", value1);
            if (value2 > 0)
                jsonObject.SetField("value2", value2);
            return jsonObject;
        }
    }
}
