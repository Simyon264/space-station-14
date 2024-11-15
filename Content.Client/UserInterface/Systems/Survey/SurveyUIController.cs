using Content.Client.Survey;
using Content.Client.UserInterface.Screens;
using Content.Client.UserInterface.Systems.Gameplay;
using Robust.Client.UserInterface.Controllers;

namespace Content.Client.UserInterface.Systems.Survey;

public sealed class SurveyUIController : UIController
{
    [Dependency] private readonly SurveyManager _survey = default!;
    public override void Initialize()
    {
        base.Initialize();
        var gameplayStateLoad = UIManager.GetUIController<GameplayStateLoadController>();
        gameplayStateLoad.OnScreenLoad += OnScreenLoad;
        gameplayStateLoad.OnScreenUnload += OnScreenUnload;
    }

    private void OnScreenLoad()
    {
        switch (UIManager.ActiveScreen)
        {
            case DefaultGameScreen game:
                _survey.SetPopupContainer(game.VoteMenu);
                break;
            case SeparatedChatGameScreen separated:
                _survey.SetPopupContainer(separated.VoteMenu);
                break;
        }
    }

    private void OnScreenUnload()
    {
        _survey.ClearPopupContainer();
    }
}
