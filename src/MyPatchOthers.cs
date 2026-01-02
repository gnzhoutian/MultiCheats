using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;
using JSONClass;

namespace MCSMultiCheats
{
    [HarmonyPatch]
    internal class MyPatchOthers
    {
        [HarmonyPatch(typeof(NPCEx), "CalcZengLiX"), HarmonyPostfix]
        public static void CalcZengLiXPostfix(ref int __result, UINPCData npc)
        {
            if (PlayerEx.Player.Sex != npc.Sex && PlayerEx.Player.SelectTianFuID.HasItem(510))  // 天赋-命犯桃花
            {
                int rate = MCSUtils.OffsetNum(PlayerEx.Player.getLevelType());
                __result = (int)(__result / rate);
                MyModule.Inst.Log($"【命犯桃花】送礼时异性好感加成(x{rate})");
            }
        }
    }

    internal class MyJsonDataOthers
    {
        public static void PaiMaiPatch()
        {
            foreach (JSONObject jsonObject in jsonData.instance.PaiMaiBiao.list)
            {
                if (jsonObject.GetField("ItemNum").I < 12)
                    jsonObject.SetField("ItemNum", 12);

                if (jsonObject.GetField("jimainum").I < 3)
                    jsonObject.SetField("jimainum", 3);
            }
            MyModule.Inst.Log($"拍卖会数量调整: {jsonData.instance.PaiMaiBiao}");
        }

        public static void DropRatePatch()
        {
            int rate = 60;
            foreach (JSONObject jsonObject in jsonData.instance.DropInfoJsonData.list)
            {
                if (jsonObject.GetField("moneydrop").I < rate)
                    jsonObject.SetField("moneydrop", rate);

                if (jsonObject.GetField("backpack").I < rate)
                    jsonObject.SetField("backpack", rate);
            }
            MyModule.Inst.Log($"物品掉落调整({rate}%): {jsonData.instance.DropInfoJsonData}");
        }

        public static void WuDaoZhiDataPatch()
        {
            int max = 480;
            for (int i = 31; i <= max; i++)
            {
                JSONObject jsonObject = new JSONObject();
                jsonObject.SetField("id", i);
                jsonObject.SetField("LevelUpExp", 400000);
                jsonObject.SetField("LevelUpNum", 15);
                jsonData.instance.WuDaoZhiData.SetField(i.ToString(), jsonObject);
            }
            MyModule.Inst.Log($"论道最大悟道点调整({max}): {jsonData.instance.WuDaoZhiData[max.ToString()]}");
        }
    }
}
