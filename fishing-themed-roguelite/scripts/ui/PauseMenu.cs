using Godot;
using System;

public partial class PauseMenu : CanvasLayer
{
	public override void _Ready()
	{
		Hide();
	}

    public override void _UnhandledInput(InputEvent @event)
    {
		if (GetTree().CurrentScene?.SceneFilePath.EndsWith("main_menu.tscn") == true)
		{
			return;
		}
		
		if (@event.IsActionPressed("pause"))
		{
			TogglePause();
			GetViewport().SetInputAsHandled();
		}
    }

	public void TogglePause()
	{
		bool newPauseState = !GetTree().Paused;
		GetTree().Paused = newPauseState;
		Visible = newPauseState;
	}

	private void OnResumeButtonPressed()
	{
		TogglePause();
	}

	private void OnQuitToMainMenuButtonPressed()
	{
		TogglePause();
		GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}