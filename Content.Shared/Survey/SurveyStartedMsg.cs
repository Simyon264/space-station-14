using Lidgren.Network;
using Robust.Shared.Network;
using Robust.Shared.Serialization;

namespace Content.Shared.Survey;

public sealed class SurveyStartedMsg : NetMessage
{
    public override MsgGroups MsgGroup { get; } = MsgGroups.Command;

    public string PrototypeId = null!;
    public TimeSpan Duration;

    public override void ReadFromBuffer(NetIncomingMessage buffer, IRobustSerializer serializer)
    {
        PrototypeId = buffer.ReadString();
        Duration = TimeSpan.FromTicks(buffer.ReadInt64());
    }

    public override void WriteToBuffer(NetOutgoingMessage buffer, IRobustSerializer serializer)
    {
        buffer.Write(PrototypeId);
        buffer.Write(Duration.Ticks);
    }
}
