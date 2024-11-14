using Content.Server.Shuttles.Systems;
using Content.Server.Survey.Prototypes;
using Content.Shared.CCVar;
using Content.Shared.Survey;
using Robust.Shared.Configuration;
using Serilog;

namespace Content.Server.Survey;

public sealed partial class SurveyManager : IPostInjectInit, IEntityEventSubscriber
{
    private ISawmill _sawmill = default!;

    private bool _surveyEnabled;
    private bool _surveyDetailEnabled;
    private bool _surveyTriggerInRound;
    private int _surveyTriggerDelay;
    private bool _surveyRecordInReplays;
    private SurveyPrototype _surveyPrototype = null!;

    private bool _surveyActive;
    private List<List<SurveyResponse>> _surveyResponses = new();

    public void Initialize()
    {
        SubscribeConfig();

        _entityManager.EventBus.SubscribeEvent<EmergencyShuttleDockedEvent>(EventSource.Local, this, OnEmergencyShuttleDocked);

        _netManager.RegisterNetMessage<SurveyStartedMsg>();
        _netManager.RegisterNetMessage<SurveyRespondedMsg>(ReceiveResponse);
    }

    private void ReceiveResponse(SurveyRespondedMsg message)
    {
        if (!_surveyActive)
        {
            return;
        }

        var responses = _surveyResponses[^1];
        responses.Add(new SurveyResponse(message.MsgChannel.UserId, message.Answers));
        Log.Debug("Received survey response from {UserId}", message.MsgChannel.UserId);
    }

    public void Shutdown()
    {
        UnsubscribeEvents();

        _entityManager.EventBus.UnsubscribeEvent<EmergencyShuttleDockedEvent>(EventSource.Local, this);
    }

    public void Update()
    {

    }

    public void PostInject()
    {
        _sawmill = _logManager.GetSawmill("survey");
    }

    private void OnEmergencyShuttleDocked(EmergencyShuttleDockedEvent ev)
    {
        var roundEndTime = _config.GetCVar(CCVars.RoundRestartTime);
        var time = roundEndTime + ev.DockTime;
        TriggerSurvey(TimeSpan.FromSeconds(time));
    }

    public void TriggerSurvey(TimeSpan maxTime)
    {
        if (!_surveyEnabled || _surveyActive)
        {
            return;
        }

        _surveyActive = true;

        var msg = new SurveyStartedMsg()
        {
            PrototypeId = _surveyPrototype.ID,
            Duration = maxTime
        };
        _netManager.ServerSendToAll(msg);

        _surveyResponses.Add([]);
    }
}
