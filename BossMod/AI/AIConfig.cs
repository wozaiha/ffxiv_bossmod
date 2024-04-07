namespace BossMod.AI;

[ConfigDisplay(Name = "AI设置 (非常实验性!!!)", Order = 6)]
class AIConfig : ConfigNode
{
    [PropertyDisplay("启用 AI")]
    public bool Enabled = false;
    
    [PropertyDisplay("Draw UI")]
    public bool DrawUI = true;

    [PropertyDisplay("Follow Leader")]
    public bool FollowLeader = true;

    [PropertyDisplay("Focus Target Leader")]
    public bool FocusTargetLeader = true;

    [PropertyDisplay("广播按键到其他窗口")]
    public bool BroadcastToSlaves = false;
}
