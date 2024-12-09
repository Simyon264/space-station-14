using Content.Server.Shuttles.Systems;
using Content.Shared.CCVar;
using Content.Shared.Survey;
using Content.Shared.Survey.Prototypes;
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
    private TimeSpan _surveyEndTime;
    private List<List<SurveyResponse>> _surveyResponses = new();

    public void Initialize()
    {
        SubscribeConfig();

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
        if (!_surveyActive)
        {
            return;
        }

        if (_gameTiming.CurTime >= _surveyEndTime)
        {
            _surveyActive = false;
            _surveyEndTime = TimeSpan.Zero;

            var responses = _surveyResponses[^1];
            _surveyResponses[^1] = responses;

            Log.Debug("Survey ended with {Responses} responses", responses.Count);
        }
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

    public void TriggerSurvey(TimeSpan maxTime, SurveyPrototype? prototype = null)
    {
        if (!_surveyEnabled || _surveyActive)
        {
            return;
        }

        _surveyActive = true;

        var prototypeId = prototype?.ID ?? _surveyPrototype.ID;

        var start = _gameTiming.RealTime;
        var end = start + maxTime;

        _surveyEndTime = end;

        var msg = new SurveyStartedMsg()
        {
            PrototypeId = prototypeId,
            EndTime = end,
            StartTime = start,
        };
        _netManager.ServerSendToAll(msg);

        _surveyResponses.Add([]);
    }
}
