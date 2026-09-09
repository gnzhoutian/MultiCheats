# PanzerCorps2 修改笔记

## TODO

1. MineSweeper 和 MineKiller 区别？
2. 俘获流用什么兵种比较合适？ 步兵，侦察兵？
3. 俘获流能触发碾压吗？ 如果能则可以保留弹药，如果不能则加硬攻
4. 装甲车模式下，是不能进行扫雷的

## 一、参考链接

- [《装甲军团2》指挥官特质的个人简评](https://www.bilibili.com/opus/945112312332681220)
- [《装甲军团2》英雄技能单位特性表及个人使用经验](https://www.bilibili.com/opus/965338112174587908/?from=readlist)

- [wiki 装备详情](https://panzercorps.fandom.com/wiki/12.8_cm_FlaK_40)
- [wiki 单位特质](https://panzercorps.fandom.com/wiki/Unit_traits)
- [装甲军团2伤害机制](https://forum.slitherine.com/viewtopic.php?t=104832)
- [构建一支符合历史原貌的核心/军团](https://forum.slitherine.com/viewtopic.php?t=46302)
- [资源编辑工具 - 未使用](https://github.com/atenfyr/UAssetGUI/releases)

## 二、修改大纲

### 2.1 姓名番号

```shell
- 莱因哈特·冯·林德曼
    - 第2轻装师第1装甲营
    - 第2航空师第1战斗大队
    - 第2航空师第3轰炸大队

    - 第7装甲师第1装甲营
    - 第8航空军第1战斗大队
    - 第8航空军第3轰炸大队
```

### 2.2 规则修改

```json
// 机制：将指挥官特质消耗调整为负数，可以在勾选后获得大量点数，继而勾选其它正面特质
// Panzer Corps 2\PanzerCorps2\Content\Data\Rules.json
"dismiss_hero_prestige": 1000,      // 英雄退钱(可选)   8000
"max_heroes_per_unit": 3,           // 英雄上限(可选)   8
"KillerTeam": 2,                    // 杀手小队(建议)   -32
"related_classes" : [               // 侦查类互转(建议)
    {
        "classes" : [
            "Infantry",
            "Recon",
            "TacticalBomber"
        ],
        "penalty" : 1000
    },
]
```

### 2.3 英雄继承

```lua
-- 机制: 在战役某个节点通过 SaveCore 保存状态（保存至本地文件），在另一个战役的设置中通过 core_sets 引用该状态
-- Main/script.lua                  -- 参考 AO1939/script.lua
PlayScenario("PolandNorth2")
SaveCore("end_of_MainInit")

-- Main/campaign.json               -- 参考 AO1939/campaign.json
"player_icon": "de",
"core_sets": ["end_of_MainInit"],
```

### 2.4 英雄创建

```lua
------------------------------------------------------------------------------------------------------------------------
-- 机制：通过内置函数 OnMapStart()触发英雄创建函数，lua参考dlc中写法，需要勾选杀手小队特质后，才会允许首回合挂载英雄
-- Cheat Hero Code: PanzerCorps2\Content\Campaigns\Main\PolandNorth1.lua
function OnMapStart()
    AddPlayerHero()
end

function AddPlayerHero()
    -- 新增英雄，分别为特种王牌、坦克王牌、空战王牌
    local heroesBasicInfo = {
        {"Arno von Hochfeld", "/Game/Gui/Common/Heroes/DE/de_in_05.de_in_05"},              -- "阿诺·冯·霍赫费尔德"
        {"Dietrich von Eckhardt", "/Game/Gui/Common/Heroes/DE/de_in_06.de_in_06"},          -- "迪特里希·冯·埃克哈特"
        {"Georg von Kaiser", "/Game/Gui/Common/Heroes/DE/de_in_07.de_in_07"},               -- "格奥尔格·冯·凯泽"
        {"Gernot von Ritter", "/Game/Gui/Common/Heroes/DE/de_in_08.de_in_08"},              -- "格诺特·冯·里特"

        {"Gerold von Ritter", "/Game/Gui/Common/Heroes/DE/de_in_pnzr03.de_in_pnzr03"},      -- "格罗尔德·冯·里特"
        {"Konrad von Hartmann", "/Game/Gui/Common/Heroes/DE/de_in_pnzr05.de_in_pnzr05"},    -- "康拉德·冯·哈特曼"
        {"Oskar von Graf", "/Game/Gui/Common/Heroes/DE/de_in_stug01.de_in_stug01"},         -- "奥斯卡·冯·格拉夫"
        {"Peter von Berghoff", "/Game/Gui/Common/Heroes/DE/de_in_stug02.de_in_stug02"},     -- "彼得·冯·伯格霍夫"

        {"Reinhardt von Herzog", "/Game/Gui/Common/Heroes/DE/de_af_02.de_af_02"},           -- "莱因哈特·冯·赫佐格"
        {"Roland von Baum", "/Game/Gui/Common/Heroes/DE/de_af_03.de_af_03"},                -- "罗兰·冯·鲍姆"
        {"Siegmund von Lindner", "/Game/Gui/Common/Heroes/DE/de_af_16.de_af_16"},           -- "西格蒙德·冯·林德纳"
        {"Volker von Lindemann", "/Game/Gui/Common/Heroes/DE/de_af_17.de_af_17"},           -- "福尔克·冯·林德曼"
    }

    for index, heroBasicInfo in ipairs(heroesBasicInfo) do
        if index > 4 then
            CreateHeroBasic(heroBasicInfo[1] .. " I", heroBasicInfo[2])                     -- 属性类 歼灭流
        else
            CreateHeroCapture(heroBasicInfo[1] .. " I", heroBasicInfo[2])                   -- 属性类 俘获流
        end
        CreateHeroDamage(heroBasicInfo[1] .. " II", heroBasicInfo[2])                       -- 伤害类
        CreateHeroImmune(heroBasicInfo[1] .. " III", heroBasicInfo[2])                      -- 免疫类
    end
end

function CreateHeroBasic(name, portrait)
    -- 属性类 歼灭流
    local hero = NewHero()
    hero.portrait = portrait
    hero.name = NSLOCTEXT("cheat_heroes", string.gsub(name, " ", "_"), name)
    hero.modifiers = {
        {type = Spotting,           mod = 4},       -- 视野 +4
        {type = Ammo,               mod = 8},       -- 弹药 +8
        {type = Speed,              mod = 80},      -- 移动 +8
        {type = Fuel,               mod = 80},      -- 燃料 +8
    }
    hero.attack_modifiers = {
        {type = TargetType.Soft,    mod = 8},       -- 对软攻击 +8
        {type = TargetType.Hard,    mod = 8},       -- 对硬攻击 +8
        {type = TargetType.Air,     mod = 8},       -- 对空攻击 +8
        {type = TargetType.Naval,   mod = 8},       -- 对海攻击 +8
    }
    hero.defense_modifiers = {{
        type = DefenseType.Ground,  mod = 8},       -- 地面防御 +8
    }
    local action = world:MakeNewHeroAction(0, hero)
    action.silent = true
    world:Exec(action)
end

function CreateHeroCapture(name, portrait)
    -- 属性类 俘获流
    local hero = NewHero()
    hero.portrait = portrait
    hero.name = NSLOCTEXT("cheat_heroes", string.gsub(name, " ", "_"), name)
    hero.modifiers = {
        {type = Spotting,           mod = 4},       -- 视野 +4
        -- {type = Ammo,               mod = 8},       -- 弹药 +8
        {type = Speed,              mod = 80},      -- 移动 +8
        {type = Fuel,               mod = 80},      -- 燃料 +8
    }
    hero.attack_modifiers = {
        -- {type = TargetType.Soft,    mod = 8},       -- 对软攻击 +8
        {type = TargetType.Hard,    mod = 8},       -- 对硬攻击 +8
    }
    hero.extra_traits = {
        UnitTrait.SuppressingFire,          -- 火力压制（火炮特性）：该单位造成伤害的大部分将以压制，而不是击杀的形式呈现
        UnitTrait.OverwhelmingAttack,       -- 压倒性攻击（英雄技能）：任何对敌方单位的攻击都将被迫使其撤退
        UnitTrait.Envelopment,              -- 包围（英雄技能）：阻止敌人逃跑，因此他们只能投降
        UnitTrait.EntKiller4,               -- 工事杀手 4X（英雄技能）：每次攻击摧毁4点工事等级
        UnitTrait.Scavenger,                -- 搜刮者（英雄技能）：从投降敌人身上能够俘获双倍装备
    }
    local action = world:MakeNewHeroAction(0, hero)
    action.silent = true
    world:Exec(action)
end

function CreateHeroDamage(name, portrait)
    -- 伤害类
    local hero = NewHero()
    hero.portrait = portrait
    hero.name = NSLOCTEXT("cheat_heroes", string.gsub(name, " ", "_"), name)
    hero.extra_traits = {
        UnitTrait.StrikesFirst,             -- 率先开火（英雄技能）：单位总是会抢先攻击
        UnitTrait.DoubleAttack,             -- 双倍攻击（英雄技能）：单位每回合获得两次攻击动作
        UnitTrait.QuadripleGun,             -- 急速攻击 2X（部分坦克装甲车、防空单位特性，英雄技能）：对目标进行2倍的攻击并造成2的伤害，该特性不会影响弹药消耗，单位依然每回合消耗1点弹药
        UnitTrait.OnTheRoll,                -- 好运连连（英雄技能）：该战役中每歼灭一个单位，+1攻击，直至战役结束

        UnitTrait.IncMaxOverstrength,       -- 整合者（英雄技能）：可以将超出常规的兵力整合至一个单位，使其兵力加强上限+5
        UnitTrait.CombatLuck,               -- 战斗运势（英雄技能）：战斗结果绝对不会低于预期
        UnitTrait.MineKiller,               -- 地雷克星（工兵单位技能）：xxxx 排雷
        UnitTrait.PhasedMovement,           -- 阶段性移动（侦查单位特性）：可在其移动点限制范围内逐步移动，该能力同样允许单位悄悄通过敌人的控制区域
    }
    local action = world:MakeNewHeroAction(0, hero)
    action.silent = true
    world:Exec(action)
end

function CreateHeroImmune(name, portrait)
    -- 免疫类
    local hero = NewHero()
    hero.portrait = portrait
    hero.name = NSLOCTEXT("cheat_heroes", string.gsub(name, " ", "_"), name)
    hero.extra_traits = {
        UnitTrait.RiverAssault,             -- 河流攻击（英雄技能）：无视来自河流的战斗惩罚效果
        UnitTrait.IgnoresEntrenchment,      -- 无视工事（英雄技能）：无视敌人的工事等级（单位的准确度不会受到敌人工事等级的影响）
        UnitTrait.LightningAttack,          -- 闪电攻击（英雄技能）：免疫所有类型的支援火力
        UnitTrait.Unyielding,               -- 顽强不屈（英雄技能）：单位不会被敌方火力压制

        UnitTrait.Vigilant,                 -- 机警（英雄技能）：阻止敌人利用单位的近身防御力
        UnitTrait.SixthSense,               -- 第六感（英雄技能）：免疫伏击
        UnitTrait.NoRetaliation,            -- 无法反击（英雄技能）：攻击时，敌方单位不会回击
        UnitTrait.ReducedSlots,             -- 减少栏位（英雄技能）：单位栏位的消耗降低50%
    }
    local action = world:MakeNewHeroAction(0, hero)
    action.silent = true
    world:Exec(action)
end
------------------------------------------------------------------------------------------------------------------------
```

## 三、游玩攻略

```shell
# 控制台 - 聊天框中执行
setany iseeyou 1                        -- 设置战争迷雾，只开图不显示单位
setany prestige 8888                    -- 设置威望
setany core 8888                        -- 设置核心
setany uber_units 1                     -- 单位无敌
runany Victory 0                        -- 直接胜利，进入下一个章节

# 基础操作
- 设置
    - 开启自动存档
    - 开启六角网格显示
    - 关闭边缘移动
- 快捷键
    - PageUp/PageDown 放大缩小
    - WASD 移动视角
    - [,.] 单位切换

    - F 隐藏界面
    - I 单位信息
    - U 单位升级
    - Z 单位休眠
    - L 战斗详情

# 编制说明
- 在游戏中主要扮演一个师长在前线指挥下属核心营级部队战斗
- 德军第2轻装师（波兰战役）: 团级x5 营级x10 连级x43
- 德军第7装甲师（库尔斯克）: 团级x5 营级x13 连级x65
- 兵力:人数 ~= 1:50 （营级约500 ~ 1000人）
- 10点兵力 ~= 陆军普通营、空军战斗大队
- 20点兵力 ~= 陆军加强营、空军轰炸大队

# 游玩技巧
- 核心单位在列表中背景为深蓝色
- 部分单位可以切换形态
- 强制单位放弃运输载具后需要重新购买
- 交换位置: 选中单位后，按Shift点击另一个
- 属性中 [1] 表示只能在反击时使用

# 常见问答
- 【英雄】常规战役中如何创建英雄?       -- 在场景lua中添加英雄创建函数，可在现存函数中调用，也可在通用函数 OnMapStart()中调用
- 【英雄】英雄是如何实现跨战役传递的？  -- 通过 SaveCore 和 core_sets 实现，新战役初始部队会被删除，继承的部队进入预备役，指挥官特质需要重选
- 【英雄】部队拆分后英雄如何分配？      -- 英雄跟随原地的部队
- 【英雄】单位被击毁后英雄怎么办？      -- 不会消失，下一场景可用
- 【英雄】部队真的需要改名吗？          -- 不会消失，下一场景可用

- 【缴获】免核心特质是否需要？          -- 8坦4空，核心减半情况下，基础版26、强化版50~62、顶配版88，在可控范围内，因而核心减半即可
- 【缴获】压倒性攻击+包围这两如何？     -- 缴获流神技，压倒性攻击会迫使敌方单位撤退，攻击时如果敌方单位撤退，包围战术将导致其被俘获
- 【缴获】我想大量缴获应该怎么办？      -- 炮兵+战略轰炸机压制敌方， 压倒性攻击+包围的中低伤害坦克进攻
- 【缴获】是否可以替换为纯缴获流？      -- 不建议，缴获流碾压不生效，持续作战能力不足

- 【缴获】缴获流推荐带哪些技能？        -- 火力压制、压倒性攻击、包围、工事杀手4X，这四个强烈推荐，搜刮者、无法反击一般推荐，不带的损失也可以接受
- 【缴获】伤害过高会影响缴获吗？        -- 有了火力压制后，急速攻击 和 整合者基本不影响伤害
- 【缴获】缴获流不推荐带哪些技能？      -- 压路机单独没有用，碾压和俘获相冲，扫雷和火力压制相冲，多个双倍攻击不会叠加
- 【缴获】缴获流对飞机生效吗？          -- 飞机不能缴获，飞机攻击飞机不能缴获，飞机攻击地面也不能缴获

- 【ZOC】侦察车过ZOC时移动点如何消耗?   -- 仍然按地形消耗移动点数，不对存在额外消耗，只是限制移动一格
- 【ZOC】免疫区域控制可以去掉？         -- 可以去掉，不影响移动范围，阶段性移动已经基本可以替代了，还能更容易区分战线
- 【ZOC】区域控制如何影响补给险？       -- 从“现实角度”来理解，你的补给线必须经过一个未被争夺的六边形格子，即敌方单位周边六格

- 【属性】主动性、视野的属性需要加成？  -- 主动性可以去掉，率先开火主动性直接99，视野还是需要的，现在的开图太窄了
- 【属性】属性加成的数值应该如何选？    -- 如果是16，则必然一击必杀，如果是8，精锐敌军需要两击，一般还是一击必杀，建议选8，视野8基本是全图了，还是选4吧
- 【属性】三个防御属性如何选？          -- 进攻流为主，自身地面防御、对空防御基本够用，但近身防御一般为0，不过实际体验中，视野更重要，近防有机警，飞机只空战就行
- 【属性】软攻便采用近身防御吗？        -- 不一定，看地形，如果是近战地形就是近身防御，如果是其它就是地面防御，近身防御是战斗工兵的天下
- 【属性】好运连连加的是什么攻击？      -- 任意类型攻击，属性面板不显示，在战斗计算时生效

- 【空战】飞机应该如何选？              -- 选战斗机和战术轰炸机即可，由于攻击对象略有差异，建议1:1，战略轰炸机类似于火炮，主要是压制伤害
- 【空战】低空攻击特质讲解？            -- 同时拥有低空特质的单位使用近身防御(一般为0)结算伤害，战斗机、战术轰炸机、20mm防空炮都有该特性，因而可以克制飞机
- 【空战】防空老兵这个特质如何？        -- 并不需要，防空炮除了对空攻击，其它伤害只能用于反击，不能代替火炮

- 【免疫】免疫包围可以去掉？            -- 包围谨慎一些、注意侦查和战线即可，其负面也只是弹药和补给，
- 【免疫】免疫伏击和机警可以去掉？      -- 实际体验下来，免疫伏击还是需要的，容易碰到地雷，视野开图感觉比近身防御重要，因而机警也是需要的
- 【免疫】免疫反击和现场维修可以去掉？  -- 偶尔的被敌方残血反击无伤大雅，部署阶段维修即可，不太影响战力，也不差钱
```

## 四、英雄特质（简化）

```lua
-- 基础类: 主动(Initiative) 射程(Range) 视野(Spotting) 弹药(Ammo) 移动(Speed) 燃料(Fuel)
hero.modifiers = {{type = Initiative, mod = 8}}

-- 攻击类: 对软攻击(Soft) 对硬攻击(Hard) 对空攻击(Air) 对海攻击(Naval)
hero.attack_modifiers = {{type = TargetType.Soft, mod = 8}}

-- 防御类: 地面防御(Ground) 对空防御(Air) 近身防御(Close)
hero.defense_modifiers = {{type = DefenseType.Ground, mod = 8}}

-- 特质类: 预定义效果的特质，此处仅挑选一些常用的
hero.extra_traits = {{
    -- 伤害类
    UnitTrait.StrikesFirst,             -- 率先开火（英雄技能）：单位总是会抢先攻击
    UnitTrait.DoubleAttack,             -- 双倍攻击（英雄技能）：单位每回合获得两次攻击动作
    UnitTrait.QuadripleGun,             -- 急速攻击 2X（部分坦克装甲车、防空单位特性，英雄技能）：对目标进行2倍的攻击并造成2的伤害，该特性不会影响弹药消耗，单位依然每回合消耗1点弹药
    UnitTrait.OnTheRoll,                -- 好运连连（英雄技能）：该战役中每歼灭一个单位，+1攻击，直至战役结束

    UnitTrait.Envelopment,              -- 包围（英雄技能）：阻止敌人逃跑，因此他们只能投降
    UnitTrait.Ambusher,                 -- 伏击（英雄技能，刷不到）：受到攻击时必定触发一次伏击
    UnitTrait.Overrun,                  -- 碾压（坦克单位特性，英雄技能）：能够摧毁（“碾压”）虚弱的敌方单位而无需消耗移动或攻击动作，其工作方式是，在进行碾压攻击后，单位的移动和攻击动作将会重新补充
    UnitTrait.PhasedMovement,           -- 阶段性移动（侦查单位特性）：可在其移动点限制范围内逐步移动，该能力同样允许单位悄悄通过敌人的控制区域

    UnitTrait.PreciseOptics,            -- 精准武器（英雄技能）：+20%基础准确度
    UnitTrait.CombatLuck,               -- 战斗运势（英雄技能）：战斗结果绝对不会低于预期
    UnitTrait.IncMaxOverstrength,       -- 整合者（英雄技能）：可以将超出常规的兵力整合至一个单位，使其兵力加强上限+5
    UnitTrait.FieldRepairs,             -- 现场维修（英雄技能）：单位没有攻击时每回合恢复一点兵力，车辆同样适用

    -- 免疫类
    UnitTrait.Alpine,                   -- 山地（山地步兵单位特性）：在丘陵和山地地区时获得+5攻击和+5防御，此外还能够无视敌人位于高地时的加成效果和这类地形的工事
    UnitTrait.Camouflage,               -- 迷彩（游击队单位特性，英雄技能）：只能够被侦察单位或邻近地面单位发现
    UnitTrait.IgnoresEntrenchment,      -- 无视工事（英雄技能）：无视敌人的工事等级（单位的准确度不会受到敌人工事等级的影响）
    UnitTrait.RiverAssault,             -- 河流攻击（英雄技能）：无视来自河流的战斗惩罚效果

    UnitTrait.LightningAttack,          -- 闪电攻击（英雄技能）：免疫所有类型的支援火力
    UnitTrait.NoRetaliation,            -- 无法反击（防空单位特性，英雄技能）：攻击时，敌方单位不会回击
    UnitTrait.Unyielding,               -- 顽强不屈（英雄技能）：单位不会被敌方火力压制
    UnitTrait.LastStand,                -- 背水一战（英雄技能）：无视被包围时的惩罚效果

    UnitTrait.Vigilant,                 -- 机警（英雄技能）：阻止敌人利用单位的近身防御力
    UnitTrait.SixthSense,               -- 第六感（英雄技能）：免疫伏击
    UnitTrait.ZeroSlots,                -- 无需栏位（英雄技能）：单位的栏位消耗降低至0
    UnitTrait.ReducedSlots,             -- 减少栏位（英雄技能）：单位栏位的消耗降低50%
    UnitTrait.IgnoresZOC,               -- 无视区域控制（英雄技能）：移动时无视敌方单位的区域控制效果
    
    UnitTrait.SuppressingFire,          -- 火力压制（火炮特性）：该单位造成伤害的大部分将以压制，而不是击杀的形式呈现
    UnitTrait.OverwhelmingAttack,       -- 压倒性攻击（英雄技能）：任何对敌方单位的攻击都将被迫使其撤退
    UnitTrait.Scavenger,                -- 搜刮者（英雄技能）：从投降敌人身上能够俘获双倍装备
    UnitTrait.EntKiller4,               -- 工事杀手 4X（重型火炮单位特性，英雄技能）：每次攻击摧毁4点工事等级
}}
```

## 五、指挥官特质（大全）

```json
// 杀手小队未修改时，选择优先级
"Retrograde" : -2,                  // 回退             后勤类，获得新装备的时间会比平时推迟六个月
"AggressiveDeployment": 2,          // 进攻部署         战术类，友方单位从载具上部署时将不会损失其攻击动作
"TrophiesOfWar": 2,                 // 战争奖杯         后勤类，迫使敌方单位投降时获得双倍俘获装备和双倍威望
"ForceConcentration": 1,            // 力量集中         后勤类，每个单位能够额外指派一名英雄
"KillerTeam": 2,                    // 杀手小队         后勤类，游戏开始时获得额外的三名英雄

// 正面特质
"InfantryGeneral": 2,               // 步兵将领         后勤类，降低25%所有步兵单位栏位消耗。
"PanzerGeneral": 2,                 // 装甲之王         后勤类，降低25%所有坦克单位栏位消耗
"IndustryConnections": 1,           // 工业连接         后勤类，在每个任务中获得15或20个原型装备（第一个任务除外）
"Liberator": 2,                     // 解放者           后勤类，占领旗帜时+50%威望
"DeepRecon": 1,                     // 深入侦察         战术类，所有位于主要目标附近的地图总是能够显示、+5%来自所有侦查单位的准确度加成
"OperationalInitiative": 1,         // 作战主动         战术类，所有单位在前三回合分别获得+4、+3以及+2主动性，后续回合获得+1主动性
"MasterOfBlitzkrieg": 1,            // 闪电战大师       战术类，所有坦克获得+1移动力并能够轻松地穿过小型河流
"BattleAcademy": 2,                 // 战争学院         后勤类，+25%经验增长速度
"AuxiliaryForce": 1,                // 辅助部队         战术类，在每场战斗中获得额外的辅助栏位，相当于50%可用核心栏位
"TrophiesOfWar": 2,                 // 战争奖杯         后勤类，迫使敌方单位投降时获得双倍俘获装备和双倍威望
"DeadlyGrasp": 1,                   // 死亡之握         战术类，被包围的地方单位收到双倍惩罚
"FlexibleCommand": 1,               // 灵活命令         战术类，拆分单位不会消耗单位栏位，任意单位能在一回合内同时进行拆分和合并的行动
"KillerTeam": 2,                    // 杀手小队         后勤类，游戏开始时获得额外的三名英雄
"AntiAirVeteran": 2,                // 防空老兵         战术类，防空单位能够直接歼灭而不是压制
"PerimeterControl": 1,              // 周边控制         战术类，能够使敌人的控制区域在友方单位所在位置效果失效
"MeticulousPlanning": 2,            // 缜密计划         战术类，所有地面单位拥有2个移动动作，允许其能够按两步骤移动（而无需提高总移动点数）
"ForceConcentration": 1,            // 力量集中         后勤类，每个单位能够额外指派一名英雄
"TerrainExpert": 1,                 // 地形专家         战术类，友方单位在任何地形下都将获得+1基础工事等级，无论是开阔地形还是河流渡口
"AggressiveDeployment": 2,          // 进攻部署         战术类，友方单位从载具上部署时将不会损失其攻击动作
"OldGuard": 1,                      // 老近卫军         后勤类，在每个任务结束时所有死去的单位都将得到整编，拥有1点兵力和一半经验值

// 负面特质
"IneptLogistics" : -2,              // 后勤无能         后勤类，-10核心栏位
"NoOverstrength" : -2,              // 无兵力加强       后勤类，单位无法获得兵力加强
"NoAirForce" : -3,                  // 拒绝空军         后勤类，无法组建空中单位
"NoArtillery" : -3,                 // 拒绝火炮         后勤类，无法组建火炮单位
"Retrograde" : -2,                  // 回退             后勤类，获得新装备的时间会比平时推迟六个月
"GreenArmy" : -3,                   // 新军             后勤类，每个任务中精锐补充只能使用三次
"DelayedReinforcements" : -2,       // 延迟补充         后勤类，无法在战斗中进行补充
"PoorMaintenance" : -2,             // 无力维护         战术类，由于维护不利，单位将在每一次回合开始时随机失去移动点数或攻击动作
"ChaoticFire" : -1,                 // 混沌之火         战斗类，单位每次攻击花费两点弹药而不是一点，如果单位只剩下一点弹药，则攻击效果只有原来的一半
"InefficientSupply" : -1,           // 补给不足         后勤类，单位在其回合开始时获得的补给降低
"PoorGroundControl" : -3,           // 无力控制         战术类，敌方单位可以自由地通过我方控制区域
"BadLuck" : -1,                     // 坏运气           战斗类，战斗结果永远不会比预期更好
"TrenchSlog" : -2,                  // 无力面对工事     战斗类，每次攻击对敌方工事等级的伤害减少1点
"SlowModernization" : -2,           // 缓慢现代化       后勤类，每个任务只能升级不超过三个单位，运输单位升级和兵力加强不计算在内
"FearOfUnknown" : -1,               // 未知恐惧         战术类，无法命令单位穿过战争迷雾
"Arrogant" : -1,                    // 傲慢             战术类，不会获得任何战斗预测
"ForceDispersion" : -2,             // 力量分散         战术类，每个单位最多只能指派1名英雄
"Ruthless" : -1,                    // 无情             战术类，绝不接受投降
"SlowReaction" : -2,                // 反应迟钝         战术类，友方单位不会在收到敌人攻击后反击
```

## 六、英雄特质（大全）

```lua
-- 部分特质描述与实际有出入，此处仅供参考
hero.extra_traits = {
    UnitTrait.ATSupport,                -- 反坦克支援（重型火炮、反坦克单位特性，英雄技能）：在临近友方单位受到敌方武装单位（即“硬”目标类型）攻击时提供火力支援
    UnitTrait.AggressiveCounterattack,  -- 猛烈回击（英雄技能）：防御时+3攻击
    UnitTrait.AimingAssistance,         -- 瞄准辅助（英雄技能）：使临近友方单位+10%准确度
    UnitTrait.Alpine,                   -- 山地（山地步兵单位特性）：在丘陵和山地地区时获得+5攻击和+5防御，此外还能够无视敌人位于高地时的加成效果和这类地形的工事
    UnitTrait.Ambusher,                 -- 伏击（英雄技能，刷不到）：受到攻击时必定触发一次伏击
    UnitTrait.ArtySupport,              -- 火炮支援（轻型火炮单位特性）：在临近友方单位受到敌方非武装单位（即“软”目标类型）攻击时提供支援火力
    UnitTrait.Avenger,                  -- 复仇者（英雄技能）：每失去一点兵力时获得攻击加成
    UnitTrait.BunkerKiller,             -- 地堡杀手（工兵、喷火坦克等单位特性）：对建筑+5攻击
    UnitTrait.Butcher,                  -- 屠夫（英雄技能）：对步兵单位+5攻击
    UnitTrait.Camouflage,               -- 迷彩（游击队单位特性，英雄技能）：只能够被侦察单位或邻近地面单位发现
    UnitTrait.CheapReplacements,        -- 低成本补充（英雄技能）：在战斗中进行补充的花费与在部署阶段时相同
    UnitTrait.CityFighter,              -- 巷战斗士（dlc史诗英雄特性）：在城市地形中获得战斗加成
    UnitTrait.CloseFighter,             -- 近距离作战（步兵特性）：在近战地形中计算敌军近身防御力
    UnitTrait.CombatLuck,               -- 战斗运势（英雄技能）：战斗结果绝对不会低于预期
    UnitTrait.CounterBattery,           -- 反炮台火力（重型火炮单位特性，英雄技能）：提供对范围内敌方火炮的支援火力
    UnitTrait.CripplingBlow,            -- 残废打击（英雄技能）：对满兵力单位+5攻击
    UnitTrait.Distraction,              -- 扰乱（英雄技能）：位于该单位旁的地方单位将无法提供支援火力
    UnitTrait.DoubleAttack,             -- 双倍攻击（英雄技能）：单位每回合获得两次攻击动作
    UnitTrait.DoubleMassAttack,         -- 双倍支援（支援单位达到一定支援次数获得，英雄技能）：敌人每次攻击提供双倍支援火力
    UnitTrait.DoubleMove,               -- 双倍移动（英雄技能）：单位每回合获得两次移动动作，但不增加移动点数
    UnitTrait.DoubleSupport,            -- 双倍支援（支援单位达到一定支援次数获得，英雄技能）：敌人每次攻击提供双倍支援火力（与DoubleMassAttack同义）
    UnitTrait.EntKiller2,               -- 工事杀手 2X（轻型火炮、战略轰炸机单位特性）：每次攻击摧毁2点工事等级
    UnitTrait.EntKiller3,               -- 工事杀手 3X（重型火炮单位特性，英雄技能）：每次攻击摧毁3点工事等级
    UnitTrait.EntrenchmentSupport,      -- 工事支援（工兵单位特性）：所有临近友方单位以两倍速度增加工事等级
    UnitTrait.Envelopment,              -- 包围（英雄技能）：阻止敌人逃跑，因此他们只能投降
    UnitTrait.Evasive,                  -- 闪避（英雄技能）：攻击该单位时敌人-10%准确度
    UnitTrait.ExpertRecon,              -- 侦察（侦察单位特性）：其他友方单位在攻击位于侦察单位临近的敌方单位时获得+10%准确度（侦察单位每拥有一个星级还将额外+3%准确度）——此处为专家级侦察，效果更佳
    UnitTrait.ExpertSupport,            -- 专家级支援（支援单位达到一定支援次数获得）：提供支援火力时+20%准确度
    UnitTrait.Exterminator,             -- 毁灭者（英雄技能）：攻击兵力低于50%的单位时+5攻击
    UnitTrait.Famous,                   -- 名声在外（dlc史诗英雄特性）：每回合产生20威望
    UnitTrait.FastDeployment,           -- 快速部署（英雄技能）：移动后能够从运输单位上离开并攻击
    UnitTrait.FastEntrenchment,         -- 快速工事（游戏中实际效果：单位每回合获得的工事等级额外+1，帮助更快构筑防御）
    UnitTrait.FastLearner,              -- 快速学习（dlc史诗英雄特性）：双倍经验值增长
    UnitTrait.FastRebase,               -- 快速转场（英雄技能）：飞机在转场时不消耗移动动作
    UnitTrait.FastRetreat,              -- 快速撤退（游戏中实际效果：单位撤退时不会被追击或压制，能更安全地脱离战斗）
    UnitTrait.FastRetreater,            -- 快速撤退（同上，拼写变体FastRetreater）：单位撤退时行动更加迅速，减少被拦截的风险
    UnitTrait.FearsomeReputation,       -- 威震四方（英雄技能）：所有临近地方单位在其回合开始时获得+3压制
    UnitTrait.FerociousDefense,         -- 凶猛防御（英雄技能）：该单位的工事等级无法被忽视
    UnitTrait.FieldRepairs,             -- 现场维修（英雄技能）：单位没有攻击时每回合恢复一点兵力，车辆同样适用
    UnitTrait.FierceFighter,            -- 凶猛斗士（英雄技能）：临近位置每有一名敌方单位时获得+1攻击
    UnitTrait.FiledRepair,              -- （拼写变体，同FieldRepairs）现场维修（英雄技能）：单位没有攻击时每回合恢复一点兵力，车辆同样适用
    UnitTrait.FirstAid,                 -- 急救（英雄技能）：单位没有攻击时每回合恢复一点兵力，步兵同样适用
    UnitTrait.FlagKiller,               -- 旗帜杀手（英雄技能）：攻击敌方旗帜时+5攻击
    UnitTrait.HitAndRun,                -- 打带跑（英雄技能）：攻击时，若敌方单位主动性较低则不会回击
    UnitTrait.IgnoreMassAttack,         -- 顽强不屈（英雄技能）：单位不会被敌方火力压制（此处英文直译为“忽略群体攻击”，即不受多方围攻时的士气/压制惩罚）
    UnitTrait.IgnoresEntrenchment,      -- 无视工事（英雄技能）：无视敌人的工事等级（单位的准确度不会受到敌人工事等级的影响）
    UnitTrait.IgnoresZOC,               -- 无视区域控制（英雄技能）：移动时无视敌方单位的区域控制效果
    UnitTrait.IncMaxOverstrength,       -- 整合者（英雄技能）：可以将超出常规的兵力整合至一个单位，使其兵力加强上限+5
    UnitTrait.LastStand,                -- 顽强防御方（英雄技能）：临近每有一名敌方单位则+1防御（LastStand直译“背水一战”，效果与顽强防御方对应）
    UnitTrait.Leadership,               -- 领导力（英雄技能）：为所有临近友方单位+1主动性
    UnitTrait.LearnsFromMistakes,       -- 吸取教训（英雄技能）：承受伤亡时获得3倍经验值
    UnitTrait.Legendary,                -- 传奇（英雄技能）：每回合产生50点威望
    UnitTrait.LethalAttack,             -- 致命攻击（英雄技能）：该单位造成压制的一半将转化为击杀
    UnitTrait.Liberator,                -- 解放者（英雄技能）：该单位占领的所有旗帜将获得2X威望
    UnitTrait.LightningAttack,          -- 急速攻击 1.5X（部分坦克装甲车、防空单位特性，英雄技能）：对目标进行1.5倍的攻击并造成1.5的伤害，该特性不会影响弹药消耗，单位依然每回合消耗1点弹药
    UnitTrait.LowProfile,               -- 低空飞行（战斗机、俯冲轰炸机单位特性，dlc史诗英雄特性）：攻击该单位时-20%准确度
    UnitTrait.MachineGun,               -- 机枪射击（游戏中实际效果：该单位对步兵类软目标的攻击力大幅提升，适合装备机枪的装甲单位使用）
    UnitTrait.NoEntrenchment,           -- 无法构筑工事（游戏中实际效果：该单位无法获得工事等级加成，通常为负面特性）
    UnitTrait.NoManagement,             -- 无法管理单位（炮台、碉堡、临时机场等单一单位特性）：该单位无法被组建，升级或补充
    UnitTrait.NoReplace,                -- 无法补充（卡尔、临时机场等单一单位特性）：该单位无法进行补充
    UnitTrait.NoRetaliation,            -- 无法反击（防空单位特性，英雄技能）：攻击时，敌方单位不会回击
    UnitTrait.NoRetreat,                -- 绝不撤退（游戏中实际效果：该单位在防御时永远不会被迫撤退，死守阵地）
    UnitTrait.NoSplit,                  -- 无法拆分（卡尔、临时机场等单一单位特性）：单位无法被拆分
    UnitTrait.NoSupply,                 -- 无法补给（dlc史诗英雄特性）：单位不需要补给，也不会受到包围时的负面效果
    UnitTrait.NoSurrender,              -- 绝不投降（部分不可控单位特性，英雄技能）：单位绝不会投降
    UnitTrait.NoUpgrade,                -- 无法升级（炮台、碉堡、临时机场等单一单位特性）：该单位无法进行装备升级
    UnitTrait.OnTheRoll,                -- 全面准备（英雄技能）：每个任务前五回合分别获得+5...+1主动性加成（OnTheRoll直译“滚雪球”，对应全面准备的递进加成）
    UnitTrait.Overrun,                  -- 碾压（坦克单位特性，英雄技能）：能够摧毁（“碾压”）虚弱的敌方单位而无需消耗移动或攻击动作，其工作方式是，在进行碾压攻击后，单位的移动和攻击动作将会重新补充
    UnitTrait.OverwhelmingAttack,       -- 压倒性攻击（英雄技能）：任何对敌方单位的攻击都将被迫使其撤退
    UnitTrait.PhasedMovement,           -- 阶段性移动（侦查单位特性）：可在其移动点限制范围内逐步移动，该能力同样允许单位悄悄通过敌人的控制区域
    UnitTrait.PreciseOptics,            -- 精密光学设备（游戏中实际效果：增加单位的视野范围，使其能更早发现敌人）
    UnitTrait.Provocator,               -- 挑衅者（英雄技能）：临近地方单位在攻击其他友方单位时将转为攻击该单位本身
    UnitTrait.Prudent,                  -- 谨慎（英雄技能）：每失去一点兵力时获得防御加成
    UnitTrait.QuadripleGun,             -- 四联装火炮（游戏中实际效果：该单位每次攻击可进行四次射击判定，大幅提升火力输出，常见于四联装防空炮）
    UnitTrait.Readiness,                -- 准备就绪（英雄技能）：在防御时将抢先进行攻击
    UnitTrait.Recon,                    -- 侦察（侦察单位特性）：其他友方单位在攻击位于侦察单位临近的敌方单位时获得+10%准确度（侦察单位每拥有一个星级还将额外+3%准确度）
    UnitTrait.ReducedSlots,             -- 减少栏位（英雄技能）：单位栏位的消耗降低50%
    UnitTrait.ReducesSlots,             -- 减少栏位（拼写变体，同ReducedSlots）：单位栏位的消耗降低50%
    UnitTrait.Resilient,                -- 复原（英雄技能）：一次行动中不会损失最大兵力的一半
    UnitTrait.RiverAssault,             -- 河流攻击（英雄技能）：无视来自河流的战斗惩罚效果
    UnitTrait.SafeTransit,              -- 安全通行（游戏中实际效果：单位在移动时不会受到敌方单位的区域控制影响，可安全通过危险区域）
    UnitTrait.Scavenger,                -- 搜刮者（英雄技能）：从投降敌人身上能够俘获双倍装备
    UnitTrait.ShockTactics,             -- 休克战术（英雄技能）：攻击时，能够摧毁敌人所有的移动点数使其无法动弹
    UnitTrait.SingleEntity,             -- 单一实体（海军等单位特性）：总是能够以最大兵力来计算攻击
    UnitTrait.SixthSense,               -- 第六感（英雄技能）：免疫伏击
    UnitTrait.SkilledRecon,             -- 熟练侦察（游戏中实际效果：侦察距离增加且侦察效果提升，比普通侦察更早发现敌军）
    UnitTrait.SkilledSupport,           -- 巧妙支援（支援单位达到一定支援次数获得）：提供支援火力时+10%准确度
    UnitTrait.SmartEntrenchment,        -- 快速工事（游戏中实际效果：该单位每回合自动获得额外的工事等级加成，比普通单位更快构筑防御）
    UnitTrait.Steamroller,              -- 压路机（碾压达到一定次数后获得）：任何击杀都将触发碾压
    UnitTrait.StrikesFirst,             -- 率先开火（英雄技能）：单位总是会抢先攻击
    UnitTrait.SuperiorManeuver,         -- 高级机动（英雄技能）：单位移动速度每比敌方单位多一点，获得+1主动性
    UnitTrait.SuppressingFire,          -- 火力压制（火炮、轰炸机、防空单位特性）：该单位造成伤害的大部分将以压制，而不是击杀的形式呈现，每个星级将略微提高彻底摧毁单位的几率，因此具有该特性的单位将愈发变得致命
    UnitTrait.Survivor,                 -- 幸存者（单位一回合遭受6次攻击存活后获得）：只要单位拥有1点以上兵力，就永远能够从敌方攻击下存活
    UnitTrait.TankKiller,               -- 坦克杀手（英雄技能）：对坦克+5攻击
    UnitTrait.TenaciousDefender,        -- 顽强防御方（英雄技能）：临近每有一名敌方单位则+1防御（TenaciousDefender直译“顽强的防御者”，与LastStand效果相近）
    UnitTrait.ThoroughPreparation,      -- 全面准备（英雄技能）：每个任务前五回合分别获得+5...+1主动性加成
    UnitTrait.Unyielding,               -- 顽强不屈（英雄技能）：单位不会被敌方火力压制（Unyielding直译“不屈不挠”，与IgnoreMassAttack对应同一效果）
    UnitTrait.Vigilant,                 -- 机警（英雄技能）：阻止敌人利用单位的近身防御力
    UnitTrait.VulnerableTarget,         -- 易受攻击目标（游戏中实际效果：该单位受到攻击时准确度判定降低，即敌人更容易命中，通常为负面特性）
    UnitTrait.ZeroSlots                 -- 无需栏位（英雄技能）：单位的栏位消耗降低至0
}
```
