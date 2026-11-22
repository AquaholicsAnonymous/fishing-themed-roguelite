using Godot;
using System;

public partial class AreaSelection : Area2D
{	

	[Export]
	private AreaSelectScreen AreaSelectUI;

	[Export]
	private Label Prompt;

	[Export]
	private Player Player;

	private bool IsPlayerInArea = false;

    public override void _Ready()
    {
        AreaSelectUI.MenuClosed += OnAreaSelectClosed;
    }

	private void OnBodyEntered(Node2D body){
		if (body is Player)
		{
			IsPlayerInArea = true;
			Prompt.Visible = true;
		}
	}
	
	private void OnBodyExited(Node2D body){
		if (body is Player)
		{
			IsPlayerInArea = false;
			Prompt.Visible = false;
		}
	}

	public override void _Process(double delta)
	{
		if (IsPlayerInArea && Input.IsActionJustPressed("interact"))
		{
			AreaSelectUI.Visible = true;
			Prompt.Visible = false;
			Player.IsPlayerMovable = false;
		}


	}

	private void OnAreaSelectClosed(){
		Player.IsPlayerMovable = true;
		if (IsPlayerInArea)
		{
			Prompt.Visible = true;
		}
	}
}
