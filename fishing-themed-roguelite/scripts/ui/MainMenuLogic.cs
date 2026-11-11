public class MainMenuLogic
{
    private readonly ISceneService _sceneService;
    public const string GameScenePath = "res://scenes/game_screen.tscn";

    public MainMenuLogic(ISceneService sceneService)
    {
        _sceneService = sceneService;
    }

    public void Play()
    {
        bool success = _sceneService.ChangeSceneToFile(GameScenePath);
        if (!success)
        {
            _sceneService.LogError($"Failed to change scene to {GameScenePath}");
        }
    }

    public void Quit()
    {
        _sceneService.Quit();
    }
}