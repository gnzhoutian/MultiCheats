# 【天国：拯救】 KC Multi Cheats

> 本MOD全方位提升玩家能力，涉及升级速度、攻击速度、耐力消耗、背包容量、物品修复、幸运之骰、弓箭准星等调整。欢迎大家试用。


## 一、前言

本模组为个人游玩时研究融合而成，适配最新版本。遵循MIT协议，实现方式力求简练，欢迎个性化修改。

译文由谷歌翻译，不足之处请见谅。

## 二、安装方式

模组解压安装后，目录结构如下：

```
<KC_INSTALL_DIR>/
└── Mods
    └── KCMultiCheats
        ├── Data
        │   └── KCMultiCheats.pak
        ├── mod.cfg
        ├── mod.manifest
        └── README.md
```

## 三、功能说明

格式说明：基础容量提升10倍（66 -> 660），表示原版数值为66，MOD调整为660

- 花之力Perk调整
    1. 生效机制：全局生效
    2. 快速升级：经验增益64倍
    3. 伤害调整：近战伤害1.6倍
    4. 耐力调整：恢复提升6.4倍，消耗降低100倍（攻击、冲刺）
    
- 全局通用调整
    1. 攻击速度：敌我攻速增益提升3倍（0.4 -> 1.2）
    2. 采药调整：采药经验提升10倍（7 -> 70）
    3. 保持清洁：持续周期提升5倍（5000 -> 20000）
    4. 背包负重：基础容量提升10倍（66 -> 660），马匹允许超重上限（1 -> 100）
    5. 物品修复：去除物品最低修复限制（0.6 -> ""），修复工具容量提升10倍（200 -> 2000）
    6. 幸运之骰：只出1、5点，在古地图1、藏宝图（6、12、16、18、24）可以找到
    7. 装备调整：比安卡戒指属性调整，显眼/可见/噪音（0 -> -27），魅力（1 -> 54），价格（540 -> 54000）
    8. 弓箭调整：开启准星

## 附录一、MOD制作方式

1. pak文件解压：通过7-zip解压
2. pak内部文件命名要求：<table_name>__<mod_name>.xml
3. pak文件压缩：通过7-zip以zip格式压缩，重命名为pak后缀，推荐使用工具[KCD PAK Builder](https://www.nexusmods.com/kingdomcomedeliverance2/mods/78)


## 附录二、我的其它MOD，欢迎试用

- [【天国：拯救】 KC Multi Cheats](https://www.nexusmods.com/kingdomcomedeliverance/mods/1763)
- [【天国：拯救2】 KC2 Multi Cheats](https://www.nexusmods.com/kingdomcomedeliverance2/mods/402)
- [【骑马与砍杀2】 MB2 Multi Cheats](https://www.nexusmods.com/mountandblade2bannerlord/mods/7470)

## 致谢

- [重要参考 - 原版参数查询 rpg-parameters](https://wiki.fireundubh.com/kingdomcome/rpg-parameters)
- [重要参考 - Modding guide for KCD](https://wiki.nexusmods.com/index.php/Modding_guide_for_KCD)
- [重要参考 - Tutorial Tweaking RPG Parameters](https://forums.nexusmods.com/topic/6409661-tutorial-tweaking-rpg-parameters/)
- [重要参考 - Lucky Lucky Die](https://www.nexusmods.com/kingdomcomedeliverance/mods/763)
- [重要参考 - Level Up](https://www.nexusmods.com/kingdomcomedeliverance/mods/167)
- [重要参考 - Better Combat](https://www.nexusmods.com/kingdomcomedeliverance/mods/1070)
- [重要参考 - Stay Clean Longer](https://www.nexusmods.com/kingdomcomedeliverance/mods/390)
- [重要工具 - 控制台 Cheat](https://www.nexusmods.com/kingdomcomedeliverance/mods/106)
- [重要工具 - 眩晕优化 Disable Headbob](https://www.nexusmods.com/kingdomcomedeliverance/mods/594)

- [攻略 - 新手必读指南](https://tieba.baidu.com/p/9179874810)
- [攻略 - 藏宝图、古地图+强盗营地分布图](https://tieba.baidu.com/p/7039774928)
- [攻略 - 菌砸教你黑吃黑——销赃流](https://www.bilibili.com/video/BV1ro4y1U76e)

## 游玩Tips

- 眩晕问题 -> 使用Disable Headbob模组 or 关闭动态模糊，垂直视野 CL_FOV 92.5 (85~98)，调整图形质量，保证帧率60帧
- 完美开局 -> 开局加敏捷，采集草药，四维刷满，战斗刷满
- 赃物处理 -> 洗赃(卖给磨坊主后，买回)、变现(卖给磨坊主后，等72小时，小屋箱子取走金币)
- 控制台指令参考
    ```
        Cheat_add_money amount:100                                       # 钱
        Cheat_add_item id:bd819743-3e32-474e-8333-bebe92b16e98 amount:6  # 幸运之骰
        Cheat_add_item id:deb5276d-4926-4545-bdda-f457cac8bc01           # 村民弓
    ```
- 最强装备
    - 头部 -> (25)佐尔头盔 + (15)贵族的链甲头巾 + (11)黑色软甲帽
    - 身体 -> (26)佐尔胸甲 + (17)长的精美锁子甲 + (12)宽大的战斗夹克(红围巾) + (17魅)橙色上衣内衬(绿衬衫)
    - 手臂 -> (23)佐尔肩甲 + (12)贵族护手
    - 腿部 -> (21)佐尔胫甲 + (17)链甲护胫 + (19魅)装饰过的黑色紧身裤(绿裤子)
    - 饰品 -> 图章戒指 + 项链 + 镀金的马刺
    - 武器 -> 圣乔治之剑 + 执政官之杖 + 肌腱弓 + 卡朋弓
