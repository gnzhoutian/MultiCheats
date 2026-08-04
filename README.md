# 钢铁雄心4修改笔记【MOD精华版】【适配1.19+】

## 说明

1. 论坛帖子地址： [钢铁雄心4修改笔记【MOD精华版】【适配1.19+】](https://bbs.52pcgame.net/forum.php?mod=viewthread&tid=551078) 
2. 最新修改数据以本仓库源码为准
3. 游玩技巧详见 `_docs/HOI4.md`
4. 模组文件结构简要说明如下: 

```shell
.
├── Hoi4MultiCheats
│   ├── common
│   │   ├── on_actions
│   │   │   └── my_cheat_actions.txt                # 开局事件触发
│   │   ├── technologies
│   │   │   └── 00_my_cheat_technologies.txt        # 科技修改，含研究时间年限调整、全局增益调整
│   │   └── units
│   │       ├── equipment
│   │       │   ├── modules
│   │       │   │   └── zz_my_cheat_modules.txt     # 飞机修改，含火箭引擎III调整
│   │       │   └── tank_chassis.txt                # 坦克修改，含现代坦克底盘调整
│   │       └── modern_armor.txt                    # 坦克修改，含现代坦克兵种调整
│   ├── events
│   │   └── my_cheats_events.txt                    # 开局事件，含省份修改、国家修改
│   ├── _docs
│   │   ├── HOI4.md                                 # 游玩技巧说明
│   │   └── 52PCGAME.md                             # 52论坛帖子源码
│   └── descriptor.mod                              # MOD初始化后自带
│── Hoi4MultiCheats.mod                             # MOD初始化后自带，含MOD路径
└── README.md                                       # 仓库说明文件
```


## 参考链接

- [2016-07-13 王国风云2修改笔记【精华版】](https://bbs.52pcgame.net/forum.php?mod=viewthread&tid=66436)
- [2016-09-05 钢铁雄心4修改笔记【精华版】](https://bbs.52pcgame.net/forum.php?mod=viewthread&tid=68947)
- [2016-09-20 钢铁雄心4修改笔记【Mod版】](https://bbs.52pcgame.net/forum.php?mod=viewthread&tid=69565)

- [MOD加载机制](https://hoi4.paradoxwikis.com/Modding#Loading_files)
- [MOD条件动作](https://hoi4.paradoxwikis.com/On_actions)
- [MOD事件编写](https://hoi4.paradoxwikis.com/Event_modding)
- [MOD开局增益](https://hoi4.paradoxwikis.com/Modifiers)
