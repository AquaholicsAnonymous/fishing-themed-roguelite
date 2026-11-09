using Godot;
using System;

public partial class AreaSelection : Area2D
{	

	[Export]
	private PanelContainer PanelContainer;

	[Export]
	private Label Prompt;

	[Export]
	private Player Player;

	private bool IsPlayerInArea = false;

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
			PanelContainer.Visible = true;
			Prompt.Visible = false;
			Player.IsPlayerMovable = false;
		}


	}

	private void OnCancelPressed(){
		PanelContainer.Visible = false;
		Player.IsPlayerMovable = true;
		if (IsPlayerInArea)
		{
			Prompt.Visible = true;
		}
	}

	private void OnPP4Pressed(){
	}
}
