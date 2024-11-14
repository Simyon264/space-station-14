using Robust.Shared.Configuration;

namespace Content.Shared.CCVar;

public sealed partial class CCVars
{
    /// <summary>
    /// If the round survey system is enabled.
    /// </summary>
    public static readonly CVarDef<bool> SurveyEnabled =
        CVarDef.Create("survey.enabled", true, CVar.SERVERONLY);

    /// <summary>
    /// When enabled, provides users with an input field to provide more detailed feedback.
    /// </summary>
    public static readonly CVarDef<bool> SurveyDetailEnabled =
        CVarDef.Create("survey.detail_enabled", true, CVar.SERVERONLY);

    /// <summary>
    /// If true, the survey will periodically trigger during the round. Survey won't trigger via this method if the emergency shuttle has launched or arrived.
    /// </summary>
    public static readonly CVarDef<bool> SurveyTriggerInRound =
        CVarDef.Create("survey.trigger_in_round", true, CVar.SERVERONLY);

    /// <summary>
    /// How many minutes to wait before a survey is triggered. Does nothing if <see cref="SurveyTriggerInRound"/> is false.
    /// </summary>
    public static readonly CVarDef<int> SurveyTriggerDelay =
        CVarDef.Create("survey.trigger_delay", 30, CVar.SERVERONLY);

    /// <summary>
    /// If true, the survey results will be recorded in replays.
    /// </summary>
    public static readonly CVarDef<bool> SurveyRecordInReplays =
        CVarDef.Create("survey.record_in_replays", true, CVar.SERVERONLY);

    /// <summary>
    /// Defines the survey prototype to use. This will be used to create the questions and possible answers.
    /// </summary>
    public static readonly CVarDef<string> SurveyPrototype =
        CVarDef.Create("survey.prototype", "defaultSurvey", CVar.SERVERONLY);
}
