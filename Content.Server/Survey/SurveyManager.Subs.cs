using Content.Server.Survey.Prototypes;
using Content.Shared.CCVar;
using Robust.Shared.Configuration;
using Robust.Shared.Network;
using Robust.Shared.Prototypes;

namespace Content.Server.Survey;

public sealed partial class SurveyManager
{
    [Dependency] private readonly ILogManager _logManager = default!;
    [Dependency] private readonly IConfigurationManager _config = default!;
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
    [Dependency] private readonly EntityManager _entityManager = default!;
    [Dependency] private readonly IServerNetManager _netManager = default!;

    private void SubscribeConfig()
    {
        _config.OnValueChanged(CCVars.SurveyEnabled, OnSurveyEnabledChanged, true);
        _config.OnValueChanged(CCVars.SurveyDetailEnabled, OnSurveyDetailEnabledChanged, true);
        _config.OnValueChanged(CCVars.SurveyTriggerInRound, OnSurveyTriggerInRoundChanged, true);
        _config.OnValueChanged(CCVars.SurveyTriggerDelay, OnSurveyTriggerDelayChanged, true);
        _config.OnValueChanged(CCVars.SurveyRecordInReplays, OnSurveyRecordInReplaysChanged, true);
        _config.OnValueChanged(CCVars.SurveyPrototype, OnSurveyPrototypeChanged, true);
    }

    private void UnsubscribeEvents()
    {
        _config.UnsubValueChanged(CCVars.SurveyEnabled, OnSurveyEnabledChanged);
        _config.UnsubValueChanged(CCVars.SurveyDetailEnabled, OnSurveyDetailEnabledChanged);
        _config.UnsubValueChanged(CCVars.SurveyTriggerInRound, OnSurveyTriggerInRoundChanged);
        _config.UnsubValueChanged(CCVars.SurveyTriggerDelay, OnSurveyTriggerDelayChanged);
        _config.UnsubValueChanged(CCVars.SurveyRecordInReplays, OnSurveyRecordInReplaysChanged);
        _config.UnsubValueChanged(CCVars.SurveyPrototype, OnSurveyPrototypeChanged);
    }

    private void OnSurveyEnabledChanged(bool enabled)
    {
        _surveyEnabled = enabled;
    }

    private void OnSurveyDetailEnabledChanged(bool enabled)
    {
        _surveyDetailEnabled = enabled;
    }

    private void OnSurveyTriggerInRoundChanged(bool enabled)
    {
        _surveyTriggerInRound = enabled;
    }

    private void OnSurveyTriggerDelayChanged(int delay)
    {
        _surveyTriggerDelay = delay;
    }

    private void OnSurveyRecordInReplaysChanged(bool record)
    {
        _surveyRecordInReplays = record;
    }

    private void OnSurveyPrototypeChanged(string prototype)
    {
        if (!_prototypeManager.TryIndex<SurveyPrototype>(prototype, out var proto))
        {
            _sawmill.Fatal($"Survey prototype {prototype} does not exist. Disabling survey system.");
            // could also use IConfigurationManager::SetCVar here, but if someone saves the config it will be saved as disabled, which is not what we want
            _surveyEnabled = false;
            return;
        }

        _surveyPrototype = proto;
    }
}
