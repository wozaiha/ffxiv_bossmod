﻿namespace BossMod;

public static class BossModuleInfo
{
    public enum Maturity
    {
        [PropertyDisplay("使用尚在开发中的模块; 可能会不完整或存在致命BUG")]
        WIP,

        [PropertyDisplay("使用第三方尚未被作者验证过的模块; 也许可以正常使用, 可能会与其他模块有不一致性 - YMMV")]
        Contributed,

        [PropertyDisplay("使用作者制作的第一方模块, 或经由插件作者验证过的第三方制作的模块")]
        Verified
    }

    public enum Expansion
    {
        RealmReborn,
        Heavensward,
        Stormblood,
        Shadowbringers,
        Endwalker,
        Global,

        Count
    }

    public enum Category
    {
        Uncategorized,
        Dungeon,
        Trial,
        Extreme,
        Raid,
        Savage,
        Ultimate,
        Unreal,
        Alliance,
        Foray,
        Criterion,
        DeepDungeon,
        FATE,
        Hunt,
        Quest,
        TreasureHunt,
        PVP,
        MaskedCarnivale,
        GoldSaucer,

        Count
    }

    public enum GroupType
    {
        None,
        CFC, // group id is ContentFinderCondition row
        MaskedCarnivale, // group id is ContentFinderCondition row
        RemovedUnreal, // group id is ContentFinderCondition row
        Quest, // group id is Quest row
        Fate, // group id is Fate row
        Hunt, // group id is HuntRank
        BozjaCE, // group id is ContentFinderCondition row, name id is DynamicEvent row
        BozjaDuel, // group id is ContentFinderCondition row, name id is DynamicEvent row
        GoldSaucer, // group id is GoldSaucerTextData row
    }

    public enum HuntRank : uint { B, A, S, SS }

    // shorthand expansion names
    public static string ShortName(this Expansion e) => e switch
    {
        Expansion.RealmReborn => "ARR",
        Expansion.Heavensward => "HW",
        Expansion.Stormblood => "SB",
        Expansion.Shadowbringers => "ShB",
        Expansion.Endwalker => "EW",
        _ => e.ToString()
    };
}

// attribute that allows customizing boss module's metadata; it is optional, each field has some defaults that are fine in most cases
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ModuleInfoAttribute : Attribute
{
    public Type? StatesType; // default: ns.xxxStates
    public Type? ConfigType; // default: ns.xxxConfig
    public Type? ObjectIDType; // default: ns.OID
    public Type? ActionIDType; // default: ns.AID
    public Type? StatusIDType; // default: ns.SID
    public Type? TetherIDType; // default: ns.TetherID
    public Type? IconIDType; // default: ns.IconID
    public uint PrimaryActorOID; // default: OID.Boss
    public BossModuleInfo.Maturity Maturity = BossModuleInfo.Maturity.WIP;
    public string Contributors = "";
    public BossModuleInfo.Expansion Expansion = BossModuleInfo.Expansion.Count; // default: second namespace level
    public BossModuleInfo.Category Category = BossModuleInfo.Category.Count; // default: third namespace level
    public BossModuleInfo.GroupType GroupType = BossModuleInfo.GroupType.None;
    public uint GroupID;
    public uint NameID; // usually BNpcName row, unless GroupType uses it differently
    public int SortOrder; // default: first number in type name

    public ModuleInfoAttribute(BossModuleInfo.Maturity maturity) => Maturity = maturity;
}
