namespace BossMod
{
    [ConfigDisplay(Name = "常规设置", Order = 0)]
    public class GeneralConfig : ConfigNode
    {
        [PropertyDisplay("转储世界状态事件")]
        public bool DumpWorldStateEvents = false;

        [PropertyDisplay("转储服务器发包")]
        public bool DumpServerPackets = false;

        [PropertyDisplay("转储客户端发包")]
        public bool DumpClientPackets = false;
    }
}
