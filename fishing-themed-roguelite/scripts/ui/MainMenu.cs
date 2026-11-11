using Godot;
using System;

public partial class MainMenu : Control, ISceneService
{
	private MainMenuLogic _logic;

    public override void _Ready()
    {
        _logic = new MainMenuLogic(this);
    }

	private void OnPlayButtonPressed() => _logic.Play();
	private void OnQuitButtonPressed() => _logic.Quit();

	public bool ChangeSceneToFile(string path)
	{
		return GetTree().ChangeSceneToFile(path) == Error.Ok;
	}

	public void Quit()
	{
		GetTree().Quit();
	}

	public void LogError(string message)
	{
		GD.PrintErr(message);
	}
}
