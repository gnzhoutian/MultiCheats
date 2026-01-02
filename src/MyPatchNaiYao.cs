using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;
using JSONClass;
using UnityEngine;
using Bag;
using TuPo;


namespace MCSMultiCheats
{
    [HarmonyPatch]
    internal class MyPatchNaiYao
    {
        // 玩家耐药Perk效果 Min(x4, 999)
        [HarmonyPatch(typeof(GUIPackage.item), "GetItemCanUseNum"), HarmonyPostfix]
        public static void GetItemCanUseNumPostfix(ref int __result, int ItemID)
        {
            if (_ItemJsonData.DataDict[ItemID].type == 5 && PlayerEx.Player.checkHasStudyWuDaoSkillByID(2131))
            {
                __result = Mathf.Min(__result * 2, 999);
            }
        }

        // 耐药Perk物品图标显示调整
        [HarmonyPatch(typeof(DanYaoItem), "SetItem"), HarmonyPostfix]
        public static void SetItemPostfix(DanYaoItem __instance)
        {
            if (PlayerEx.Player.checkHasStudyWuDaoSkillByID(2131))
            {
                __instance.CanUse = Mathf.Min(__instance.CanUse * 2, 999);
            }
        }

        // 突破大境界耐药性重置
        [HarmonyPatch(typeof(BigTuPoResultIMag), "ShowSuccess"), HarmonyPostfix]
        public static void ShowSuccessPostfix(BigTuPoResultIMag __instance)
        {
            MyModule.Inst.Log($"突破大境界耐药性重置前: {PlayerEx.Player.NaiYaoXin}");

            List<string> keys = new List<string>() { };  // 先将键遍历另存，再遍历修改
            foreach (string key in PlayerEx.Player.NaiYaoXin.keys)
            {
                keys.Add(key);
            }

            foreach (string key in keys)
            {
                if (PlayerEx.Player.NaiYaoXin.GetField(key).I > 0)
                {
                    PlayerEx.Player.NaiYaoXin.SetField(key, 0);
                }
            }
            MyModule.Inst.Log($"突破大境界耐药性重置后: {PlayerEx.Player.NaiYaoXin}");
        }
    }

    // 调整原始数据: 耐药性相关
    internal class MyJsonDataNaiYao
    {
        public static void Reset()
        {
            jsonData.instance._ItemJsonData["5414"].SetField("CanUse", 8);  // 道源丹
            jsonData.instance._ItemJsonData["5512"].SetField("CanUse", 8);  // 天道源丹
            MyModule.Inst.Log("道源丹、天道源丹耐药调整: 8");
        }
    }
}
