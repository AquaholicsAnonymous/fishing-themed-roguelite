public interface ISceneService
{
    bool ChangeSceneToFile(string path);
    void Quit();
    void LogError(string message);
}