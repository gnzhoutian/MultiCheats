using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;
using KBEngine;
using script.NewLianDan.LianDan;

namespace MCSMultiCheats
{
    [HarmonyPatch]
    internal class MyPatchLianDan
    {
        [HarmonyPatch(typeof(LianDanPanel), "PutDanLu"), HarmonyPostfix]
        public static void PutDanLuPostfix(LianDanPanel __instance)
        {
            int rate = MCSUtils.OffsetNum(__instance.DanLu.Item.GetBaseQuality());
            __instance.MaxNum *= rate;
            MyModule.Inst.Log($"丹炉草药上限调整(x{rate}): {__instance.MaxNum}");
        }

        [HarmonyPatch(typeof(LianDanPanel), "FinishAddItem"), HarmonyPrefix]
        public static void FinishAddItemPrefix(int id, ref int lianzhicishu)
        {
            if (lianzhicishu > 0 && id > 5000 && id < 5900 && PlayerEx.Player.checkHasStudyWuDaoSkillByID(2141))
            {
                int rate = MCSUtils.OffsetNum(PlayerEx.Player.getLevelType());
                PlayerEx.Player.addItem(id, lianzhicishu * (rate - 1), Tools.CreateItemSeid(id));
                lianzhicishu *= rate;
                MyModule.Inst.Log($"丹圣效果调整(x{rate}): {lianzhicishu}");
            }
        }

        [HarmonyPatch(typeof(LianDanPanel), "Success"), HarmonyPrefix]
        public static void SuccessPrefix(ref int num)
        {
            if (num > 0 && PlayerEx.Player.checkHasStudyWuDaoSkillByID(2141))
            {
                int rate = MCSUtils.OffsetNum(PlayerEx.Player.getLevelType());
                num *= rate;
                MyModule.Inst.Log($"丹圣效果显示(x{rate}): {num}");
            }
        }
    }

    internal class MyJsonDataLianDan
    {
        public static void Reset()
        {
            List<int> ycIds = new List<int> { };
            foreach (JSONObject jsonObject in jsonData.instance.DFBuKeZhongZhi.list)
            {
                int ycId = jsonObject.GetField("id").I;
                if (ycId > 6000 && ycId < 7000)
                    ycIds.Add(ycId);
            }
            foreach (int ycId in ycIds)
            {
                jsonData.instance.DFBuKeZhongZhi.RemoveField(ycId.ToString());
            }
            MyModule.Inst.Log($"药材全部可种植, 妖丹除外: {jsonData.instance.DFBuKeZhongZhi}");
        }
    }
}