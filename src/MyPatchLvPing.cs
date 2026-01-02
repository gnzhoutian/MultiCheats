using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;
using UnityEngine;
using YSGame.Fight;

namespace MCSMultiCheats
{
    [HarmonyPatch]
    internal class MyPatchLvPing
    {
        public static bool HasLvPing = PlayerEx.Player.SelectTianFuID.HasItem(410);  // 天赋-神秘绿瓶

        [HarmonyPatch(typeof(RoundManager), "RandomDrawCard"), HarmonyPrefix]
        public static bool LingQiRandomDrawPrefix(KBEngine.Avatar avatar, ref int count)
        {
            if (UIFightPanel.Inst.UIFightState == UIFightState.自己回合普通状态 && HasLvPing)
            {
                int rate = MCSUtils.OffsetPow(PlayerEx.Player.getLevelType());
                count += rate;
                MyModule.Inst.Log($"【小绿瓶】随机灵气生成(+N^2)+{rate}: {count}");
            }
            return count > 0 ? true : false;  // 判断是否继续执行原始函数
        }

        [HarmonyPatch(typeof(RoundManager), "drawCard"), HarmonyPrefix]
        public static void LingQidrawCardPrefix(KBEngine.Avatar avatar)
        {
            RoundManager.instance.RandomDrawCard(avatar, 0);
        }

        // 小绿瓶灵力充能 - 催生年限xN, 灵石效果(xN^2)
        [HarmonyPatch(typeof(UILingLiChongNeng), "Init"), HarmonyPostfix]
        public static void LingLiChongNengInitPostfix(UILingLiChongNeng __instance)
        {
            DongFuData dongFu = Traverse.Create(__instance).Field("df").GetValue<DongFuData>();

            int rate = MCSUtils.OffsetPow(dongFu.JuLingZhenLevel);
            int rate_year = MCSUtils.OffsetNum(dongFu.JuLingZhenLevel);
            int maxLinLi = Mathf.Min(UIDongFu.Inst.LingTian.CuiShengLingShi50Year * rate_year, Int32.MaxValue - dongFu.CuiShengLingLi);  // 催生年限xN
            if ((ulong)(maxLinLi / rate) < PlayerEx.Player.money)  // N^2灵力 -> 1灵石
                Traverse.Create(__instance).Field("maxLinshi").SetValue(maxLinLi);  // 实际显示为灵力
            else
                Traverse.Create(__instance).Field("maxLinshi").SetValue((int)PlayerEx.Player.money * rate);

            Traverse.Create(__instance).Method("UpdateUI", Array.Empty<object>()).GetValue();
        }

        // 小绿瓶灵力充能 - 生效结算
        [HarmonyPatch(typeof(UILingLiChongNeng), "Ok"), HarmonyPrefix]
        public static bool UILingLiChongNengOkPrefix(UILingLiChongNeng __instance)
        {
            DongFuData dongFu = Traverse.Create(__instance).Field("df").GetValue<DongFuData>();

            int rate = MCSUtils.OffsetPow(dongFu.JuLingZhenLevel);
            int addLingLi = Traverse.Create(__instance).Field("addLingShi").GetValue<int>();

            dongFu.CuiShengLingLi += addLingLi;
            dongFu.Save();

            Traverse.Create(__instance).Field("df").SetValue(dongFu);
            UIDongFu.Inst.InitData();
            UIDongFu.Inst.LingTian.RefreshUI();

            PlayerEx.Player.AddMoney(-(int)(addLingLi / rate));
            __instance.Close();
            return false;
        }
    }

    // // 调整原始数据: 小绿瓶相关
    internal static class MyJsonDataLvPing
    {
        public static void Reset()
        {
            foreach (JSONObject jsonObject in jsonData.instance.DFZhenYanLevel.list)
            {
                jsonObject.SetField("lingtiancuishengsudu", jsonObject.GetField("lingtiancuishengsudu").I * MCSUtils.OffsetPow(jsonObject.GetField("id").I));
            }
            MyModule.Inst.Log($"【小绿瓶】催生速度(xN^2): {jsonData.instance.DFZhenYanLevel}");

            jsonData.instance.CreateAvatarJsonData["410"].SetField("seid", MCSUtils.ArrayObject("10,15,19"));
            jsonData.instance.CrateAvatarSeidJsonData[15].SetField("410", MCSUtils.DictObject(410, 1, 6));
            jsonData.instance.CrateAvatarSeidJsonData[19].SetField("410", MCSUtils.DictObject(410, 12));
            MyModule.Inst.Log("【小绿瓶】解锁全部药性，天赋点+12");
        }
    }
}
