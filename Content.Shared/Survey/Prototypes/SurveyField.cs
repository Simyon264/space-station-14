using JetBrains.Annotations;

namespace Content.Shared.Survey.Prototypes;

/// <summary>
/// Root class for all survey fields. Classes inheriting from this will be used to define the fields of a survey. For example a text field, a multiple choice field, etc.
/// </summary>
[ImplicitDataDefinitionForInheritors]
[MeansImplicitUse]
public partial class SurveyField
{
    /// <summary>
    /// The label of the field. This is put above the UI element that the field represents.
    /// This supports rich text and is sent through Loc.
    /// </summary>
    [DataField]
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// If set to any value, a corresponding boolean cvar must be set to true for this field to be enabled.
    /// </summary>
    [DataField]
    public virtual string RequiredCvar { get; set; } = string.Empty;
}
