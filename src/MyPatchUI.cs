using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using YSGame.Fight;

namespace MCSMultiCheats
{
    [HarmonyPatch]
    internal class MyPatchUI
    {
        // 战斗右键快捷施法
        [HarmonyPatch(typeof(UIFightSkillItem), "ClickSkill"), HarmonyPostfix]
        public static void RightClickSkillPostfix(UIFightSkillItem __instance)
        {
            if (__instance.IsSelected && Input.GetKeyUp(KeyCode.Mouse1))
            {
                if (RoundManager.instance.UseSkill("", true))
                {
                    UIFightPanel.Inst.CacheLingQiController.ChangeCacheSlotNumber(0);
                    UIFightPanel.Inst.CancelSkillHighlight();
                }
                else
                {
                    UIFightPanel.Inst.UIFightState = UIFightState.释放技能准备灵气阶段;
                }
            }
        }

        // 论道池滚动显示
        [HarmonyPatch(typeof(LunDaoPanel), "Show"), HarmonyPrefix]
        public static void LunDaoPanelShowPrefix()
        {
            LunDaoPanel panel = LunDaoManager.inst.lunDaoPanel;
            RectTransform lunDaoQiuList = panel.transform.Find("LunDaoQiuList") as RectTransform;
            if (lunDaoQiuList != null)
            {
                ScrollRect scroll = panel.gameObject.AddComponent<ScrollRect>();
                scroll.vertical = false;
                scroll.content = lunDaoQiuList;
            }
        }

        // 传音符物品自动领取
        [HarmonyPatch(typeof(EmailDataMag), "AddNewEmail"), HarmonyPostfix]
        public static void AddNewEmailPostfix(EmailDataMag __instance, string npcId, EmailData data)
        {
            if (data.item != null && data.item.Count >= 2 && data.item[0] > 0 && data.item[1] > 0 && data.actionId == 1)
            {
                PlayerEx.Player.addItem(data.item[0], data.item[1], null, true);
                List<EmailData> list = __instance.newEmailDictionary[npcId];
                __instance.newEmailDictionary[npcId][list.Count - 1].item[1] = 0;
            }
        }

        // 人物修为显示: 侧边栏
        [HarmonyPatch(typeof(UINPCSVItem), "RefreshUI"), HarmonyPostfix]
        public static void UINPCSVItemPostfix(UINPCSVItem __instance)
        {
            float rate = (float)__instance.NPCData.Exp / jsonData.instance.LevelUpDataJsonData[__instance.NPCData.Level.ToString()]["MaxExp"].I;
            __instance.NPCTitle.text = $"{__instance.NPCData.Title} {MCSUtils.MiniXiuWei(__instance.NPCData.LevelStr)}{rate:P0}";

            __instance.NPCTitle.fontSize = 24;
            __instance.NPCTitle.horizontalOverflow = HorizontalWrapMode.Overflow;
        }

        // 人物修为显示: 传音符
        [HarmonyPatch(typeof(CyFriendCell), "Init"), HarmonyPostfix]
        public static void CyFriendCellPostfix(CyFriendCell __instance)
        {
            __instance.chengHao.fontSize = 24;
            __instance.chengHao.horizontalOverflow = HorizontalWrapMode.Overflow;

            if (!__instance.isDeath && __instance.npcData.Favor > 20 && !__instance.IsFly)
            {
                float rate = (float)__instance.npcData.Exp / jsonData.instance.LevelUpDataJsonData[__instance.npcData.Level.ToString()]["MaxExp"].I;
                __instance.chengHao.text = $"{__instance.npcData.Title} {MCSUtils.MiniXiuWei(__instance.npcData.LevelStr)}{rate:P0}";
            }
        }
    }
}
