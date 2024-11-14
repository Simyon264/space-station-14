using Robust.Shared.Prototypes;

namespace Content.Server.Survey.Prototypes;

/// <summary>
/// Used by <see cref="SurveyManager"/> to create surveys.
/// </summary>
[Prototype("survey")]
public sealed class SurveyPrototype : IPrototype
{
    [IdDataField]
    public string ID { get; } = default!;

    [DataField]
    public SurveyField[] Fields { get; set; } = default!;
}
