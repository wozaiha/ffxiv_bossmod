﻿namespace BossMod.Autorotation;

// base class that simplifies implementation of physical ranged dps utility modules, contains shared track definitions
public abstract class RoleRangedUtility(RotationModuleManager manager, Actor player) : GenericUtility(manager, player)
{
    public enum SharedTrack { Sprint, LB, LegGraze, SecondWind, FootGraze, HeadGraze, ArmsLength, Count }

    protected static void DefineShared(RotationModuleDefinition def, ActionID lb3)
    {
        DefineSimpleConfig(def, SharedTrack.Sprint, "Sprint", "", 100, ClassShared.AID.Sprint, 10);

        var lb = DefineLimitBreak(def, SharedTrack.LB, ActionTargets.Hostile);
        lb.AddAssociatedActions(ClassShared.AID.BigShot, ClassShared.AID.Desperado);
        lb.AssociatedActions.Add(lb3);

        DefineSimpleConfig(def, SharedTrack.LegGraze, "Slow", "", -100, ClassShared.AID.LegGraze, 10);
        DefineSimpleConfig(def, SharedTrack.SecondWind, "SecondWind", "", 20, ClassShared.AID.SecondWind);
        DefineSimpleConfig(def, SharedTrack.FootGraze, "Bind", "", -150, ClassShared.AID.FootGraze, 10);
        DefineSimpleConfig(def, SharedTrack.HeadGraze, "Interrupt", "", -50, ClassShared.AID.HeadGraze);
        DefineSimpleConfig(def, SharedTrack.ArmsLength, "ArmsLength", "", 300, ClassShared.AID.ArmsLength, 6); // note: secondary effect 15s
    }

    protected void ExecuteShared(StrategyValues strategy, ActionID lb3)
    {
        ExecuteSimple(strategy.Option(SharedTrack.Sprint), ClassShared.AID.Sprint, Player);
        ExecuteSimple(strategy.Option(SharedTrack.LegGraze), ClassShared.AID.LegGraze, null);
        ExecuteSimple(strategy.Option(SharedTrack.SecondWind), ClassShared.AID.SecondWind, Player);
        ExecuteSimple(strategy.Option(SharedTrack.FootGraze), ClassShared.AID.FootGraze, null);
        ExecuteSimple(strategy.Option(SharedTrack.HeadGraze), ClassShared.AID.HeadGraze, null);
        ExecuteSimple(strategy.Option(SharedTrack.ArmsLength), ClassShared.AID.ArmsLength, Player);

        var lb = LBLevelToExecute(strategy.Option(SharedTrack.LB).As<LBOption>());
        if (lb > 0)
            Hints.ActionsToExecute.Push(lb == 3 ? lb3 : ActionID.MakeSpell(lb == 2 ? ClassShared.AID.Desperado : ClassShared.AID.BigShot), null, ActionQueue.Priority.VeryHigh);
    }
}
