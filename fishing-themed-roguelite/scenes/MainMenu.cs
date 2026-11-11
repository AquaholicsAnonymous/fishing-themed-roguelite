using Godot;
using System;

public partial class MainMenu : Control
{
	private void OnPlayButtonPressed()
	{
		Error error = GetTree().ChangeSceneToFile("res://scenes/game_screen.tscn");
		if (error != Error.Ok)
		{
			GD.PrintErr($"Failed to change scene: {error}");
		}
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
