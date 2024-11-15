namespace Content.Shared.Survey.Prototypes;

public abstract partial class SurveyFieldOption : SurveyField
{
    /// <summary>
    /// A unique identifier for this field for saving.
    /// This should be unique within the survey for external parsing and such. Will error if empty or not unique.
    /// </summary>
    [DataField]
    public string SurveyId { get; set; } = string.Empty;

    /// <summary>
    /// If true, this field is required to be filled out.
    /// </summary>
    [DataField]
    public bool Required { get; set; } = false;
}
