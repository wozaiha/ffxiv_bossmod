﻿namespace BossMod.RealmReborn.Extreme.Ex2Garuda;

class WickedWheel : Components.CastCounter
{
    private DateTime _expectedNext;
    private static readonly float _radius = 8.7f;

    public WickedWheel() : base(ActionID.MakeSpell(AID.WickedWheel)) { }

    public override void Init(BossModule module)
    {
        _expectedNext = WorldState.FutureTime(25);
    }

    public override void AddGlobalHints(GlobalHints hints)
    {
        if (_expectedNext != default)
            hints.Add($"Wicked wheel in ~{Math.Max((_expectedNext - WorldState.CurrentTime).TotalSeconds, 0)}s");
    }

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        // note: suparna also casts this, but we generally ignore it...
        if (_expectedNext != default && Module.PrimaryActor.TargetID != actor.InstanceID && (_expectedNext - WorldState.CurrentTime).TotalSeconds < 3)
            hints.AddForbiddenZone(ShapeDistance.Circle(Module.PrimaryActor.Position, _radius), _expectedNext);
    }

    public override void DrawArenaForeground(int pcSlot, Actor pc)
    {
        if (_expectedNext != default && (_expectedNext - WorldState.CurrentTime).TotalSeconds < 3)
            arena.AddCircle(Module.PrimaryActor.Position, _radius, ArenaColor.Danger);
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        base.OnEventCast(caster, spell);
        if (spell.Action == WatchedAction)
        {
            // not sure about this ...
            _expectedNext = module.Enemies(OID.Suparna).Any(a => a.IsTargetable && !a.IsDead) ? WorldState.FutureTime(25) : new();
        }
    }
}
