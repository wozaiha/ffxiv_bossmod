using Dalamud.Utility.Signatures;
using FFXIVClientStructs.FFXIV.Client.Game.Group;
using FFXIVClientStructs.Interop.Attributes;
using System;
using System.Runtime.InteropServices;

namespace BossMod
{
    // similar to dalamud's PartyList, except that it works with alliances properly
    class PartyAlliance
    {
        public static int MaxAllianceMembers = 20;

        private unsafe GroupManager* _groupManager = GroupManager.Instance();

        public unsafe GroupManager* _replayGroupManager = (GroupManager*) IntPtr.Zero;

        public unsafe PartyAlliance()
        {
            _replayGroupManager = (GroupManager*) Service.SigScanner.GetStaticAddressFromSig("48 8D 0D ?? ?? ?? ?? 48 83 C4 28 E9 ?? ?? ?? ?? 8B 05 ?? ?? ?? ?? 89 05 ?? ?? ?? ?? C3 CC CC CC 8B 05 ?? ?? ?? ?? 89 05 ?? ?? ?? ?? C3 CC CC CC 8B 05 ?? ?? ?? ?? 89 05 ?? ?? ?? ?? C3 CC CC CC 8B 05 ?? ?? ?? ??");
            Service.Log($"Playback GroupManager address = 0x{(IntPtr) _replayGroupManager:X}");
        }

        private bool InPlayback => Service.Condition[Dalamud.Game.ClientState.Conditions.ConditionFlag.DutyRecorderPlayback];

        public unsafe int NumPartyMembers => InPlayback ? _replayGroupManager->MemberCount : _groupManager->MemberCount;
        public unsafe bool IsAlliance => InPlayback ? (_replayGroupManager->AllianceFlags & 1) != 0 : (_groupManager->AllianceFlags & 1) != 0;
        public unsafe bool IsSmallGroupAlliance => InPlayback ? (_replayGroupManager->AllianceFlags & 2) != 0 : (_groupManager->AllianceFlags & 2) != 0; // alliance containing 6 groups of 4 members rather than 3x8

        public unsafe PartyMember* PartyMember(int index) => (index >= 0 && index < NumPartyMembers) ? ArrayElement(InPlayback ? _replayGroupManager->PartyMembers : _groupManager->PartyMembers, index) : null;
        public unsafe PartyMember* AllianceMember(int rawIndex) => (rawIndex is >= 0 and < 20) ? AllianceMemberIfValid(rawIndex) : null;
        public unsafe PartyMember* AllianceMember(int group, int index)
        {
            if (IsSmallGroupAlliance)
                return group is >= 0 and < 5 && index is >= 0 and < 4 ? AllianceMemberIfValid(4 * group + index) : null;
            else
                return group is >= 0 and < 2 && index is >= 0 and < 8 ? AllianceMemberIfValid(8 * group + index) : null;
        }

        public unsafe PartyMember* FindPartyMember(ulong contentID)
        {
            for (int i = 0; i < NumPartyMembers; ++i)
            {
                var m = ArrayElement(InPlayback ? _replayGroupManager->PartyMembers : _groupManager->PartyMembers, i);
                if ((ulong)m->ContentID == contentID)
                    return m;
            }
            return null;
        }

        public unsafe PartyMember* FindPartyMemberByOID(uint objectID)
        {
            for (int i = 0; i < NumPartyMembers; ++i)
            {
                var m = ArrayElement(InPlayback ? _replayGroupManager->PartyMembers : _groupManager->PartyMembers, i);
                if (m->ObjectID == objectID)
                    return m;
            }
            return null;
        }

        private static unsafe PartyMember* ArrayElement(byte* array, int index) => ((PartyMember*)array) + index;
        private unsafe PartyMember* AllianceMemberIfValid(int rawIndex)
        {
            var p = ArrayElement(InPlayback ? _replayGroupManager->AllianceMembers : _groupManager->AllianceMembers, rawIndex);
            return (p->Flags & 1) != 0 ? p : null;
        }
    }
}
