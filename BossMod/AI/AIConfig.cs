namespace BossMod.AI
{
    [ConfigDisplay(Name = "AI设置 (非常实验性!!!)", Order = 5)]
    class AIConfig : ConfigNode
    {
        [PropertyDisplay("启用 AI")]
        public bool Enabled = false;

        [PropertyDisplay("广播按键到其他窗口")]
        public bool BroadcastToSlaves = false;
    }
}
