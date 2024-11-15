using Content.Shared.Survey.Prototypes;

namespace Content.Client.Survey;

public sealed class ActiveSurvey
{
    public SurveyPrototype Prototype;
    public bool Active;
    public TimeSpan EndTime;
    public TimeSpan StartTime;
    public int Id;

    public ActiveSurvey(SurveyPrototype prototype, bool active, TimeSpan endTime, int id, TimeSpan startTime)
    {
        Prototype = prototype;
        Active = active;
        EndTime = endTime;
        Id = id;
        StartTime = startTime;
    }
}
