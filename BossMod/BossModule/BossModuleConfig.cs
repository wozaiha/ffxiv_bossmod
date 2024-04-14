﻿namespace BossMod;

[ConfigDisplay(Name = "Boss 模块窗口设置", Order = 1)]
public class BossModuleConfig : ConfigNode
{
    [PropertyDisplay("地图缩放")]
    [PropertySlider(0.1f, 10, Speed = 0.1f, Logarithmic = true)]
    public float ArenaScale = 1;

    [PropertyDisplay("启用Boss模块")]
    public bool Enable = true;

    [PropertyDisplay("锁定移动和鼠标交互")]
    public bool Lock = false;

    [PropertyDisplay("旋转地图与视角一致")]
    public bool RotateArena = true;

    [PropertyDisplay("拓展为小地图保留的空间以允许自由旋转")]
    public bool AddSlackForRotations = true;

    [PropertyDisplay("显示边框")]
    public bool ShowBorder = true;

    [PropertyDisplay("如果玩家处于危险时更改边框颜色")]
    public bool ShowBorderRisk = true;

    [PropertyDisplay("显示方向")]
    public bool ShowCardinals = false;

    [PropertyDisplay("指南针字体大小")]
    [PropertySlider(0.1f, 100, Speed = 1)]
    public float CardinalsFontSize = 17;

    [PropertyDisplay("在雷达上显示标点")]
    public bool ShowWaymarks = false;

    [PropertyDisplay("显示机制序列和计时器")]
    public bool ShowMechanicTimers = true;

    [PropertyDisplay("显示全屏AOE提示")]
    public bool ShowGlobalHints = true;

    [PropertyDisplay("显示警告和提示")]
    public bool ShowPlayerHints = true;

    [PropertyDisplay("在另外一个窗口显示文字提示")]
    public bool HintsInSeparateWindow = false;

    [PropertyDisplay("透明模式: 仅雷达, 不显示窗体")]
    public bool TrishaMode = false;

    [PropertyDisplay("给地图增加不透明背景")]
    public bool OpaqueArenaBackground = false;

    [PropertyDisplay("在世界内显示移动指示")]
    public bool ShowWorldArrows = false;

    [PropertyDisplay("在副本外显示Boss模块窗口 (可用于配置窗口)")]
    public bool ShowDemo = false;

    [PropertyDisplay("显示带有冷却计划计时器的窗口")]
    public bool EnableTimerWindow = false;

    [PropertyDisplay("总是显示所有活着的队伍成员")]
    public bool ShowIrrelevantPlayers = false;

    [PropertyDisplay("在各式小地图标记显示描边和阴影")]
    public bool ShowOutlinesAndShadows = false;

    [PropertyDisplay("要加载的模块的最低完成度")]
    public BossModuleInfo.Maturity MinMaturity = BossModuleInfo.Maturity.Contributed;
}
