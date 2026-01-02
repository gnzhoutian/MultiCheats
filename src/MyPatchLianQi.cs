using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCSMultiCheats
{
    internal class MyJsonDataLianQi
    {
        public static void Reset()
        {
            int rate = 4;

            foreach (JSONObject jsonObject in jsonData.instance.LianQiWuWeiBiao.list)
            {
                jsonObject.SetField("value1", jsonObject.GetField("value1").I * rate);
                jsonObject.SetField("value2", jsonObject.GetField("value2").I * rate);
                jsonObject.SetField("value3", jsonObject.GetField("value3").I * rate);
                jsonObject.SetField("value4", jsonObject.GetField("value4").I * rate);
                jsonObject.SetField("value5", jsonObject.GetField("value5").I * rate);
            }
            MyModule.Inst.Log($"炼器材料权重调整(x{rate}): {jsonData.instance.LianQiWuWeiBiao}");

            foreach (JSONObject jsonObject in jsonData.instance.LianQiJieSuanBiao.list)
            {
                jsonObject.SetField("time", MCSUtils.OffsetNum(jsonObject.GetField("id").I));
            }
            MyModule.Inst.Log($"炼器消耗时间调整(N): {jsonData.instance.LianQiJieSuanBiao}");

            foreach (JSONObject jsonObject in jsonData.instance.CaiLiaoNengLiangBiao.list)
            {
                jsonObject.SetField("value1", jsonObject.GetField("value1").I * MCSUtils.OffsetNum(jsonObject.GetField("id").I));
            }
            MyModule.Inst.Log($"炼器材料灵气调整(xN): {jsonData.instance.CaiLiaoNengLiangBiao}");
        }
    }
}
