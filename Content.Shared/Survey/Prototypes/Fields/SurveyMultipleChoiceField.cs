namespace Content.Server.Survey.Prototypes.Fields;

/// <summary>
/// Provides a multiple choice field for a survey.
/// </summary>
public sealed partial class SurveyMultipleChoiceField : SurveyFieldOption
{
    /// <summary>
    /// The options that the user can choose from.
    /// </summary>
    [DataField]
    public string[] Options { get; set; } = new[]
    {
        "Yes",
        "No"
    };

    /// <summary>
    /// If true, the user can select multiple options.
    /// </summary>
    [DataField]
    public bool AllowMultiple { get; set; } = false;
}
