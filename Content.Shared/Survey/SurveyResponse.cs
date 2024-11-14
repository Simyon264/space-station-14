using Robust.Shared.Network;

namespace Content.Server.Survey;

public sealed class SurveyResponse
{
    public NetUserId UserId { get; }
    public Dictionary<string, object> Answers { get; }

    public SurveyResponse(NetUserId userId, Dictionary<string, object> answers)
    {
        UserId = userId;
        Answers = answers;
    }
}
