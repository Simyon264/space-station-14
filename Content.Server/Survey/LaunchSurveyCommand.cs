using System.Linq;
using Content.Server.Administration;
using Content.Shared.Administration;
using Content.Shared.Survey.Prototypes;
using Robust.Shared.Console;
using Robust.Shared.Prototypes;

namespace Content.Server.Survey;

[AdminCommand(AdminFlags.Moderator)]
public sealed class LaunchSurveyCommand : LocalizedCommands
{
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
    [Dependency] private readonly SurveyManager _surveyManager = default!;

    public override string Command { get; } = "launchsurvey";
    public override void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        if (args.Length != 1)
        {
            shell.WriteError(Loc.GetString("shell-wrong-arguments-number-need-specific", ("properAmount", 1), ("currentAmount", args.Length)));
            return;
        }

        if (!_prototypeManager.TryIndex<SurveyPrototype>(args[0], out var surveyPrototype))
        {
            shell.WriteError(Loc.GetString("survey-prototype-not-found", ("id", args[0])));
            return;
        }

        _surveyManager.TriggerSurvey(TimeSpan.FromMinutes(1), surveyPrototype);
        shell.WriteLine(Loc.GetString("survey-started", ("id", surveyPrototype.ID)));
    }

    public override CompletionResult GetCompletion(IConsoleShell shell, string[] args)
    {
        if (args.Length != 1)
            return CompletionResult.Empty;

        var gamePresets = _prototypeManager.EnumeratePrototypes<SurveyPrototype>()
            .OrderBy(p => p.ID);
        var options = gamePresets.Select(preset => preset.ID).ToList();

        return CompletionResult.FromHintOptions(options, "<id>");
    }
}
