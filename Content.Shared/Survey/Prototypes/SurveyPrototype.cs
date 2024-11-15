using Robust.Shared.Prototypes;

namespace Content.Shared.Survey.Prototypes;

/// <summary>
/// Used by <see cref="SurveyManager"/> to create surveys.
/// </summary>
[Prototype("survey")]
public sealed partial class SurveyPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; } = default!;

    [DataField]
    public SurveyField[] Fields { get; set; } = default!;
}
