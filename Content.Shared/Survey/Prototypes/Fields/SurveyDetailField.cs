namespace Content.Shared.Survey.Prototypes.Fields;

/// <summary>
/// Provides a text field for a survey.
/// </summary>
public sealed partial class SurveyDetailField : SurveyFieldOption
{
    /// <summary>
    /// The placeholder text for the text field.
    /// </summary>
    [DataField]
    public string Placeholder { get; set; } = string.Empty;

    /// <summary>
    /// Whats the maximum length of the text field.
    /// </summary>
    [DataField]
    public int MaxLength { get; set; } = 100;

    /// <summary>
    /// If true, the text field will allow multiple lines.
    /// </summary>
    [DataField]
    public bool Multiline { get; set; } = false;

    [DataField]
    public override string RequiredCvar { get; set; } = "survey.detail_enabled";
}
