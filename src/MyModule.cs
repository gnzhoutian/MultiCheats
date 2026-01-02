using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepInEx;
using HarmonyLib;

namespace MCSMultiCheats
{
    [BepInPlugin("MCSMultiCheats", "MCSMultiCheats", "1.0.0")]
    public class MyModule : BaseUnityPlugin
    {
        private void Awake()
        {
            Inst = this;
            new Harmony("MCSMultiCheats").PatchAll();
        }

        public void Log(string message)
        {
            Logger.LogMessage(message);
        }

        public static MyModule Inst;
    }

    // JSON数据初始化调整，优先级较高
    [HarmonyPatch(typeof(YSJSONHelper), "InitJSONClassData")]
    internal class InitJSONClassDataPatch
    {
        public static void Prefix()
        {
            MyJsonDataLianQi.Reset();  // 炼器调整
            MyJsonDataLianDan.Reset();  // 炼丹调整

            MyJsonDataNaiYao.Reset();  // 耐药性相关
            MyJsonDataLvPing.Reset();  // 小绿瓶相关

            MyJsonDataOthers.PaiMaiPatch();  // 拍卖会 12|3
            MyJsonDataOthers.DropRatePatch();  // 物品掉落 最低六成
            MyJsonDataOthers.WuDaoZhiDataPatch();  // 论道最大悟道点调整
        }
    }
}
