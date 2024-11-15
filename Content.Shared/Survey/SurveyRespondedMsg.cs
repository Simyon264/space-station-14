using Lidgren.Network;
using Robust.Shared.Network;
using Robust.Shared.Serialization;

namespace Content.Shared.Survey;

public sealed class SurveyRespondedMsg : NetMessage
{
    public override MsgGroups MsgGroup { get; } = MsgGroups.Command;

    public Dictionary<string, object> Answers = null!;

    public override void ReadFromBuffer(NetIncomingMessage buffer, IRobustSerializer serializer)
    {
        var count = buffer.ReadInt32();
        Answers = new Dictionary<string, object>(count);
        for (var i = 0; i < count; i++)
        {
            var key = buffer.ReadString();
            var type = buffer.ReadString();
            object value;
            switch (type)
            {
                case "System.String":
                    value = buffer.ReadString();
                    break;
                case "System.Int32":
                    value = buffer.ReadInt32();
                    break;
                default:
                    throw new InvalidOperationException("Unsupported type in survey response.");
            }
            Answers.Add(key, value);
        }
    }

    public override void WriteToBuffer(NetOutgoingMessage buffer, IRobustSerializer serializer)
    {
        buffer.Write(Answers.Count);
        foreach (var (key, value) in Answers)
        {
            buffer.Write(key);
            // We now write the type of the value so we can deserialize it properly.
            buffer.Write(value.GetType().FullName);
            switch (value)
            {
                // for simplicity, we only support strings and ints. There probably wont be survey fields that have MORE than that.
                case string str:
                    buffer.Write(str);
                    break;
                case int i:
                    buffer.Write(i);
                    break;
                default:
                    throw new InvalidOperationException("Unsupported type in survey response.");
            }
        }
    }
}
