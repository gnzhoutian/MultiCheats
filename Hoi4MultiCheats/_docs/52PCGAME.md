# 钢铁雄心4修改笔记【MOD精华版】【适配1.19+】

## 说明


此处仅存放源码，与论坛基本保持一致


- [钢铁雄心4修改笔记【MOD精华版】【适配1.19+】](https://bbs.52pcgame.net/forum.php?mod=viewthread&tid=551078) 


## 一楼：前言


```
[font=微软雅黑][size=3][align=left][color=#ff0000][size=4][b]本人改派，无修改，不游戏![/b][/size][/color]

[color=#ff00ff][size=4][b]写在前面：[/b][/size][/color]
[b]时光荏苒，距我第一次写修改笔记，已经过去十年了，钢4也出十周年了！[/b]
[b]很多变了，也有很多一直没变，不管是钢4、修改笔记，亦或是我、大家！[/b]
[b]钢4的机制已经趋于稳定，而当年的笔记还有很多不成熟、不兼容的地方，本次我就一并适配了。[/b]

[color=#ff00ff][size=4][b]楼层说明：[/b][/size][/color]
[list]
[*][b]二楼：[/b]MOD说明（含文件结构、读取机制）
[*][b]三楼：[/b]新增事件（含省份修改、国家修改）
[*][b]四楼：[/b]科技修改（含研究时间、开局增益）
[*][b]五楼：[/b]坦克修改（含坦克兵种、坦克底盘）
[*][b]六楼：[/b]飞机修改（含火箭引擎）
[*][b]七楼：[/b]碎碎念
[/list]

[color=#ff00ff][size=4][b]主要变更：[/b][/size][/color]
[list]
[*][b]模组通用化：[/b]将原版的省份修改、国家修改合并至一个通用开局事件，适配[b]任意国家开局[/b]
[*][b]省份修改：[/b]新增资源煤、[b]精简省份建筑类型[/b]、去除冗余修改（胜利点、地区建筑）
[*][b]国家修改：[/b]新增开局科技（[b]现代坦克、火箭引擎III[/b]）、去除冗余修改（凝聚力、执政政党、支持率、国家理念、海陆将领）
[*][b]科技修改：精简全局增益[/b]、去除冗余修改（现代自行火炮、现代自行反坦克、现代自行防空）
[*][b]坦克修改：[/b]新增[b]现代坦克底盘[/b]修改、调整[b]现代坦克兵种[/b]修改（适配衍生兵种）
[*][b]飞机修改：[/b]新增[b]火箭引擎[/b]修改，去除冗余修改（飞机类型、飞机任务、空投），仅适配小型飞机（战斗、支援、侦查）
[/list]

[color=#ff00ff][size=4][b]附录：历史修改笔记[/b][/color][/size]
[list]
[*][b]2016-07-13[/b] [url=https://bbs.52pcgame.net/forum.php?mod=viewthread&tid=66436][i]王国风云2修改笔记【精华版】[/i][/url]
[*][b]2016-09-05[/b] [url=https://bbs.52pcgame.net/forum.php?mod=viewthread&tid=68947][i]钢铁雄心4修改笔记【精华版】[/i][/url]
[*][b]2016-09-20[/b] [url=https://bbs.52pcgame.net/forum.php?mod=viewthread&tid=69565][i]钢铁雄心4修改笔记【Mod版】[/i][/url]
[/list]


[b]如果各位看官觉得不错的话，[color=#ff0000]欢迎版主加精[/color]，欢迎同学们打赏。[/b]
[color=#ff0000][b]附上MOD版下载，老规矩，回复可见，还收小费~[/b][/color]

[hide][attach]319973[/attach][/hide]

[b]另：[/b][b]前 [color=#ff0000]88[/color] 名回帖随机 [color=#ff0000]88PB[/color]，小小心意，不成敬意~[/b][/align][/size][/font]
```


## 二楼：MOD说明


```
[font=微软雅黑][size=3][align=left][b]本次最大的调整是将 [color=#ff0000]模组通用化[/color]，支持任意国家开局，免除去每个国家下面修改的麻烦。[/b]
[b]MOD 使用官方启动器 [color=#0000ff]`dowser.exe`[/color] 创建，空模组创建很简单，此处不再赘述。[/b]
[b]模组内有详尽的注释，语法缩进也简洁明了，以便于大家按需调整。[/b]

[color=#ff00ff][size=4][b]MOD 文件结构及文件说明如下：[/b][/size][/color]

[code].
├── Hoi4MultiCheats
│   ├── common
│   │   ├── on_actions
│   │   │   └── my_cheat_actions.txt                # 开局事件触发
│   │   ├── technologies
│   │   │   └── 00_my_cheat_technologies.txt        # 科技修改，含研究时间年限调整、全局增益调整
│   │   └── units
│   │       ├── equipment
│   │       │   ├── modules
│   │       │   │   └── zz_my_cheat_modules.txt     # 飞机修改，含火箭引擎III调整
│   │       │   └── tank_chassis.txt                # 坦克修改，含现代坦克底盘调整
│   │       └── modern_armor.txt                    # 坦克修改，含现代坦克兵种调整
│   ├── events
│   │   └── my_cheats_events.txt                    # 开局事件，含省份修改、国家修改
│   └── descriptor.mod                              # MOD初始化后自带
└── Hoi4MultiCheats.mod                             # MOD初始化后自带，含MOD路径[/code]

[color=#ff00ff][size=4][b]MOD 文件读取机制简要说明：[/b][/size][/color]

[list=1]
[*][b]同路径同名文件[/b]
[color=#708090]- MOD覆盖本体，如：[/color][color=#0000ff]`modern_armor.txt`[/color][color=#708090]、[/color][color=#0000ff]`tank_chassis.txt`[/color]
[*][b]同路径不同名，但是对象相同[/b]
[color=#708090]- 先按文件名排序(00_xx > zz_xx > ZZ_xx)，再决定读取机制[/color]
[color=#708090]- 以首次读取的为准，后续的跳过，如：[/color][color=#0000ff]`00_my_cheat_technologies.txt`[/color]
[color=#708090]- 以末次读取的为准，后续的完全覆盖前面的，如：[/color][color=#0000ff]`zz_my_cheat_modules.txt`[/color]
[color=#708090]- 依次读取同对象后，后续的合并覆盖至前面的，该情况较复杂，本次未用到[/color]
[/list]

[color=#ff00ff][size=4][b]参考链接：[/b][/size][/color]

[list]
[*][url=https://paradoxwikis.com][color=#0000ff][i]MOD 加载机制[/i][/color][/url]
[*][url=https://paradoxwikis.com][color=#0000ff][i]MOD 条件动作[/i][/color][/url]
[*][url=https://paradoxwikis.com][color=#0000ff][i]MOD 事件编写[/i][/color][/url]
[*][url=https://paradoxwikis.com][color=#0000ff][i]MOD 开局增益[/i][/color][/url]
[/list][/align][/size][/font]
```


## 三楼：新增事件（含省份修改、国家修改）


```
[font=微软雅黑][size=3][align=left][b]事件由两个部分组成（[color=#ff0000]事件触发、事件内容[/color]），具体写法如下：[/b]

[color=#ff00ff][size=4][b]事件触发：[/b][/size][/color][color=#0000ff][b]`common/on_actions/my_cheat_actions.txt`[/b][/color]

[code]on_actions = {
    # 开局触发事件
    on_startup = {
        effect = {
            every_country = {
                limit = {
                    is_ai = no
                }
                country_event = my_cheat_events.1
            }
        }
    }
}[/code]

[color=#ff00ff][size=4][b]事件内容：[/b][/size][/color][color=#0000ff][b]`events/my_cheats_events.txt`[/b][/color]

[code]add_namespace = my_cheat_events

country_event = {
    id = my_cheat_events.1                                      # 事件ID
    hidden = yes                                                # 隐藏事件
    is_triggered_only = yes                                     # 触发机制

    immediate = {
        add_political_power = 800                               # 增加政治点数
        set_research_slots = 8                                  # 设置科研槽数
        add_equipment_to_stockpile = {                          # 增加运输船数
            type = convoy_1 amount = 800 producer = ROOT
        }
        
        set_technology = {                                      # 激活科技
            main_battle_tank_chassis = 1                        # 现代坦克
            sp_rockets_dual_chamber_rocket_engine_2 = 1         # 火箭引擎III
            popup = no                                          # 弹出窗口
        }

        # 玩家首都修改
        capital_scope = {
            add_manpower = 8000000                              # 增加省份人口
            set_state_category = megalopolis                    # 设置省份规模
            add_extra_state_shared_building_slots = 25          # 增加建筑槽位

            # 增加省份资源
            add_resource = { type = oil       amount = 80 }     # 油
            add_resource = { type = aluminium amount = 80 }     # 铝
            add_resource = { type = rubber    amount = 80 }     # 橡胶
            add_resource = { type = tungsten  amount = 80 }     # 钨
            add_resource = { type = steel     amount = 80 }     # 钢
            add_resource = { type = chromium  amount = 80 }     # 铬
            add_resource = { type = coal      amount = 80 }     # 煤
            
            # 设置省份建筑
            set_building_level = { type = infrastructure     level = 5  instant_build = yes }   # 基础设施
            set_building_level = { type = air_base           level = 10 instant_build = yes }   # 空军基地
            set_building_level = { type = anti_air_building  level = 5  instant_build = yes }   # 防空炮
            set_building_level = { type = radar_station      level = 6  instant_build = yes }   # 雷达
            set_building_level = { type = arms_factory       level = 15 instant_build = yes }   # 军用工厂
            set_building_level = { type = industrial_complex level = 10 instant_build = yes }   # 民用工厂
        }
    }
}[/code][/align][/size][/font]
```

## 四楼：科技修改（含研究时间、开局增益）


```
[font=微软雅黑][size=3][align=left][b]科技修改采用[color=#ff0000]增量修改[/color]的方式，将本体中下述两个文件中的指定对象提取出来，合并放在一个文件中，再做少量修改即可，[color=#ff0000]注意文件命名排序[/color]。[/b]

[list]
[*][b]现代坦克科技：[/b][color=#0000ff]`Hearts of Iron IV\common\technologies\NSB_armor.txt`[/color] -> [color=#ff0000]`main_battle_tank_chassis`[/color]
[*][b]火箭引擎 III 科技：[/b][color=#0000ff]`Hearts of Iron IV\common\technologies\electronic_mechanical_engineering.txt`[/color] -> [color=#ff0000]`sp_rockets_dual_chamber_rocket_engine_2`[/color]
[/list]

[b]关键内容如下，修改位置均有详细注释，未变更处以 [color=#0000ff]`....`[/color] 指代。[/b]

[code]technologies = {

    @1945 = 12
    @rockets_1946 = 10
    
    main_battle_tank_chassis = { #E-50
        ....

        research_cost = 24                              # 研究基础天数 24*100
        start_year = 2012                               # 科技超前年数,有很高的研究惩罚
        ai_will_do = { factor = 0 }                     # 电脑研究几率

        folder = {
            name = nsb_armour_folder
            position = { x = 2 y = @1945 }
        }
        ....
    }

    sp_rockets_dual_chamber_rocket_engine_2 = {
        ....
        
        research_cost = 24                              # 研究基础天数 24*100
        start_year = 2012                               # 科技超前年数,有很高的研究惩罚
        ai_will_do = { factor = 0 }                     # 电脑研究几率
        
        folder = {
            name = electronics_folder
            position = { x = -2 y = @rockets_1946 }
        }

        ai_will_do = { factor = 0 }                     # 电脑研究几率

        
        ## 开局变量修正，或可放在 static_modifiers.txt ##
        # 国家
        political_power_factor = 4                      # 政治点数 +400%
        justify_war_goal_time = -4                      # 宣战时间 -400%
        research_speed_factor = 8                       # 研究效率 +800%

        # 工业
        local_resources_factor = 4                      # 战略资源 +400%
        production_speed_buildings_factor = 4           # 建造速度 +400%
        industrial_capacity_dockyard = 4                # 船坞产出 +400%
        industrial_capacity_factory = 4                 # 工厂产出 +400%
        production_factory_max_efficiency_factor = 4    # 最大产能 +400%
        production_factory_efficiency_gain_factor = 4   # 产能效率 +400%
        line_change_production_efficiency_factor = 4    # 效率保持 +400%

        # 经验
        experience_gain_factor = 4                      # 将领经验 +400%
        command_power_gain_mult = 4                     # 指挥点数 +400%
        experience_gain_army_factor = 4                 # 陆军经验 +400%
        experience_gain_navy_factor = 4                 # 海军经验 +400%
        experience_gain_air_factor = 4                  # 空军经验 +400%
        experience_gain_army_unit_factor = 4            # 陆军单位经验 +400%
        experience_gain_navy_unit_factor = 4            # 海军单位经验 +400%
        air_mission_xp_gain_factor = 4                  # 空军单位经验 +400%

        # 局势
        generate_wargoal_tension = -0.88                # 宣战借口 -88%
        join_faction_tension = -0.88                    # 加入阵营 -88%
        send_volunteers_tension = -0.88                 # 派志愿军 -88%
        lend_lease_tension = -0.88                      # 租借法案 -88%

        # 陆军
        attrition = -0.88                               # 基础损耗 -88%
        supply_consumption_factor = -0.88               # 补给消耗 -88%
        out_of_supply_factor = -0.88                    # 补给惩罚 -88%
        org_loss_when_moving = -0.88                    # 移动组织度损耗 -88%

        # 海军
        naval_hit_chance = 0.88                         # 命中几率 +88%
        naval_detection = 0.88                          # 侦测能力 +88%
        sortie_efficiency = 0.88                        # 舰载出击 +88%
        critical_receive_chance = -0.88                 # 致命几率 -88%
        naval_speed_factor = 4                          # 最大航速 +400%
        navy_max_range_factor = 4                       # 最大航程 +400%

        # 空军
        air_detection = 0.88                            # 侦测能力 +88%
        air_mission_efficiency = 0.88                   # 任务效率 +88%
        air_ace_generation_chance_factor = 0.88         # 王牌几率 +88%
        air_accidents_factor = -0.88                    # 空难几率 -88%
    }
}[/code][/align][/size][/font]
```


## 五楼：坦克修改（含坦克兵种、坦克底盘）


```
[font=微软雅黑][size=3][align=left][b]坦克修改采用[color=#ff0000]全量[/color]的方式，将本体中下述两个文件中的内容[color=#ff0000]拷贝一份[/color]，再做少量修改即可。[/b]
[b]关键内容如下，修改位置均有详细注释，未变更处以 [color=#0000ff]`....`[/color] 指代。[/b]

[color=#708090][i][u]P.S. 使用增量模式时，由于存在部分参数未被更新，我放弃增量方案，采用全量模式，此为遗憾。[/u][/i][/color]

[b][color=#ff00ff]现代坦克兵种：[/color][/b][color=#0000ff]`Hearts of Iron IV\common\units\modern_armor.txt`[/color] -> [color=#ff0000]`modern_armor`[/color]

[code]sub_units = {

    modern_armor = {
        ....
        categories = {
            category_tanks
            category_front_line
            category_all_armor
            category_army
        }
        
        # 营级属性, 部分属性会乘N（满编营数量N=8）
        # Basic Abilities
        need = { modern_tank_chassis = 50 }     # 装备需求 xN
        combat_width = 2                        # 战斗宽度 xN
        supply_consumption = 0.2                # 补给消耗 xN
        weight = 2                              # 运输重量 xN
        can_be_parachuted = yes                 # 可否空投

        # Important Ability
        manpower = 1000                         # 人力需求 xN
        max_strength = 80                       # 最大HP xN
        max_organisation = 800                  # 最大组织度
        default_morale = 80                     # 组织度恢复速度
        training_time = 8                       # 训练天数

        # Misc Abilities
        casualty_trickleback = 0.2              #伤兵复原 xN
        initiative = 0.2                        # 主动性 xN
        reliability_factor = 0.2                # 可靠性 xn
        experience_loss_factor = -0.2           # 经验损失 xN

        # 地形影响（移动、攻击、防御）
        forest = { movement = 1 attack = 1 defence = 1 }
        hills = { movement = 1 attack = 1 defence = 1 }
        mountain = { movement = 1 attack = 1 defence = 1 }
        jungle = { movement = 1 attack = 1 defence = 1 }
        marsh = { movement = 1 attack = 1 defence = 1 }
        plains = { movement = 1 attack = 1 defence = 1 }
        desert = { movement = 1 attack = 1 defence = 1 }
        urban = { movement = 1 attack = 1 defence = 1 }
        fort = { movement = 1 attack = 1 defence = 1 }
        river = { movement = 1 attack = 1 defence = 1 }
        amphibious = { movement = 1 attack = 1 defence = 1 }
    }
}[/code]

[b][color=#ff00ff]现代坦克底盘：[/color][/b][color=#0000ff]`Hearts of Iron IV\common\units\equipment\tank_chassis.txt`[/color] -> [color=#ff0000]`modern_tank_chassis`[/color]

[code]equipments = {
    ....

    modern_tank_chassis = {
        ....
        default_modules = {
            main_armament_slot = empty
            turret_type_slot = tank_modern_tank_turret
            suspension_type_slot = tank_bogie_suspension
            armor_type_slot = tank_riveted_armor
            engine_type_slot = tank_gasoline_engine
        }

        # 坦克属性, 部分属性会乘N（满编营数量N=8）
        # Basic Abilities
        maximum_speed = 80                  # 最大速度
        reliability = 2                     # 可靠性

        # Defensive Abilities
        breakthrough = 80                   # 突破 xN
        defense = 80                        # 防御 xN
        hardness = 1                        # 装甲率
        armor_value = 800                   # 装甲厚度
        
        # Offensive Abilities
        soft_attack = 80                    # 人员杀伤 xN
        hard_attack = 80                    # 装甲杀伤 xN
        air_attack = 80                     # 对空攻击 xN
        ap_attack = 800                     # 穿甲深度

        # Important Ability
        fuel_capacity = 800                 # 燃油容量 xN
        fuel_consumption = 2                # 燃油消耗 xN
        suppression = 80                    # 镇压能力 xN
        entrenchment = 80                   # 堑壕能力 xN
        recon = 80                          # 侦查能力 xN
        
        # Misc Abilities
        manpower = 2                        # 人力需求 xN
        lend_lease_cost = 80                # 租借运输 xN
        build_cost_ic = 2                   # 建造花费 xN
        resources = {                       # 生产资源 xN
            oil = 1
            steel = 1
            chromium = 1
        }
    }
    ....
}[/code][/align][/size][/font]
```


## 六楼：飞机修改（含火箭引擎）


```
[font=微软雅黑][size=3][align=left][b]飞机修改采用 [color=#ff0000]增量[/color] 的方式，将本体中下述文件中的指定对象提取出来，再做少量修改即可， [color=#ff0000]注意文件命名排序[/color] 。[/b]
[b]关键内容如下，修改位置均有详细注释，未变更处以 [color=#0000ff]`....`[/color] 指代。[/b]

[b][color=#ff00ff]火箭引擎 III：[/color][/b][color=#0000ff]`Hearts of Iron IV\common\units\equipment\modules\00_plane_modules.txt`[/color] -> [color=#ff0000]`rocket_engine_3`[/color]

[code]equipment_modules = {

    rocket_engine_3 = { #rocket engines only come in sets of 1
        abbreviation = "re3"
        category = plane_rocket_engine_type
        sfx = sfx_ui_sd_module_engine

        add_stats = {
            # Basic Abilities
            maximum_speed = 800            # 最大速度
            air_range = 8000               # 作战半径
            thrust = 80                    # 引擎推力

            # Defensive Abilities
            air_defence = 80               # 空中防御
            air_agility = 80               # 机动
            air_superiority = 80           # 空优
            
            # Offensive Abilities
            air_attack = 80                # 对空攻击
            naval_strike_attack = 80       # 对海攻击
            naval_strike_targetting = 80   # 对海瞄准
            air_bombing = 80               # 战略轰炸
            air_ground_attack = 80         # 对地攻击

            # Misc Abilities
            reliability = 2                 # 可靠性
            night_penalty = -2              # 夜间惩罚
            surface_detection = 80         # 对海探测
            sub_detection = 80             # 对潜探测
            mines_planting = 80            # 布雷能力
            mines_sweeping = 80            # 扫雷能力
        }
        build_cost_resources = {            # 资源消耗
            aluminium = 1
            tungsten = 1
            rubber = 1
        }
        dismantle_cost_ic = 1               # 改装成本
    }
}[/code][/align][/size][/font]
```


## 七楼：碎碎念


```
[font=微软雅黑][size=3][align=left][color=#ff00ff][size=4][b]后记[/b][/size][/color]

[b]先发个牢骚：十年过去了，论坛的编辑器依然一言难尽呀，幸亏有 AI 帮忙才勉强搞定~[/b]

[color=#ff00ff][size=4][b]友情提示：[/b][/size][/color]

[list=1]
[*][b]文本编辑器建议：[/b][color=#0000ff]VSCode[/color] （支持语法高亮和段落折叠）
[*][b]善用 AI：[/b]可以是论坛帖子样式自动优化，也可是游戏修改方法探讨
[*]该模组我自己可以准完美运行（尚未出错）
[*]装甲部队无需配置支援连队
[/list]

[color=#ff00ff][size=4][b]常用信息备忘：[/b][/size][/color]

[code]# 控制台
tdebug                      # 进入调试模式，查询相关信息
research_on_icon_click      # 单击快速研究
xp                          # 三军经验增加

# 特殊值
2147482     # 装备溢出值
4294967     # 装备归零值

# 部队番号
SS.01 "元首警卫旗队" 装甲师
SS.02 "帝国" 装甲师
SS.03 "骷髅" 装甲师
SS.05 "维京" 装甲师

SS.09 "霍亨斯陶芬" 装甲师
SS.10 "弗伦斯贝格" 装甲师
SS.11 "诺德兰北欧" 志愿装甲掷弹兵师
SS.12 "铁血青年团" 装甲师[/code]

[color=#ff0000][size=4][b]至此，笔记收工，预祝大家玩的痛快~[/b][/size][/color][/align][/size][/font]
```
