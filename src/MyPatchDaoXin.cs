using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;
using JSONClass;
using KBEngine;
using PaiMai;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace MCSMultiCheats
{
    [HarmonyPatch]
    internal class MyPatchDaoXin
    {
        public static bool HasDaoXin = PlayerEx.Player.SelectTianFuID.HasItem(403);  // 天赋-道心坚定

        public static float GainRate => 1f + (PlayerEx.Player.wuXin + PlayerEx.Player.ZiZhi + PlayerEx.Player.LingGan) / 100f;

        [HarmonyPatch(typeof(Avatar), "ReduceLingGan"), HarmonyPrefix]
        public static void ReduceLingGanPrefix(Avatar __instance, ref int num)
        {
            if (__instance.isPlayer() && HasDaoXin && num > 20)
            {
                int rate = 8;
                num = Mathf.Max((int)(num / rate), 1);
                MyModule.Inst.Log($"【道心坚定】论道灵感消耗(/{rate}): {num}");
            }
        }

        [HarmonyPatch(typeof(LunDaoHuiHe), "Init"), HarmonyPostfix]
        public static void LunDaoHuiHePostfix(LunDaoHuiHe __instance)
        {
            if (HasDaoXin)
            {
                int rate = MCSUtils.OffsetPow(PlayerEx.Player.getLevelType());
                __instance.totalHui += rate;
                __instance.curHui = 0;
                __instance.shengYuHuiHe = __instance.totalHui - __instance.curHui;
                __instance.ReduceHuiHe(); // 重置文本显示
                MyModule.Inst.Log($"【道心坚定】论道最大回合+{rate}: {__instance.totalHui}");
            }
        }

        [HarmonyPatch(typeof(LunDaoManager), "EndRoundCallBack"), HarmonyPostfix]
        public static void EndRoundCallBackPrefix()
        {
            if (LunDaoManager.inst.gameState != LunDaoManager.GameState.论道结束 && HasDaoXin)
            {
                LunDaoManager.inst.lunDaoPanel.AddNullSlot();
                MyModule.Inst.Log("【道心坚定】论道回合结束空节点+1");
            }
        }

        [HarmonyPatch(typeof(Tools), "CalcLingWuOrTuPoTime"), HarmonyPostfix]
        public static void CalcLingWuOrTuPoTimePostfix(ref int __result)
        {
            if (HasDaoXin)
            {
                float rate = GainRate;
                int origin = __result;
                __result = (int)(__result / rate);
                MyModule.Inst.Log($"【道心坚定】功法领悟速率受【悟性+资质+灵感】加成(x{rate:P0}): {origin} -> {__result}");
            }
        }

        [HarmonyPatch(typeof(WuDaoMag), "CalcGanWuTime"), HarmonyPostfix]
        public static void CalcGanWuTimePostfix(ref int __result)
        {
            if (HasDaoXin)
            {
                float rate = GainRate;
                int origin = __result;
                __result = (int)(__result / rate);
                MyModule.Inst.Log($"【道心坚定】灵光感悟速率受【悟性+资质+灵感】加成(x{rate:P0}): {origin} -> {__result}");
            }
        }

        [HarmonyPatch(typeof(WuDaoMag), "addWuDaoEx"), HarmonyPrefix]
        public static void AddWuDaoExPrefix(ref int exNum)
        {
            if (HasDaoXin)
            {
                float rate = GainRate;
                int origin = exNum;
                exNum = (int)(exNum * rate);
                MyModule.Inst.Log($"【道心坚定】悟道经验受【悟性+资质+灵感】加成(x{rate:P0}): {origin} -> {exNum}");
            }
        }

        [HarmonyPatch(typeof(LunDaoManager), "AddWuDaoZhi"), HarmonyPrefix]
        public static void AddWuDaoZhiPrefix(ref int addNum)
        {
            if (HasDaoXin)
            {
                float rate = GainRate;
                int origin = addNum;
                addNum = (int)(addNum * rate);
                MyModule.Inst.Log($"【道心坚定】悟道值受【悟性+资质+灵感】加成(x{rate:P0}): {origin} -> {addNum}");
            }
        }
    }
}
