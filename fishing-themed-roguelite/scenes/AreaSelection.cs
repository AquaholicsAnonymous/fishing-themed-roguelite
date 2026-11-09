using Godot;
using System;

public partial class AreaSelection : Area2D
{	

	[Export]
	private PanelContainer PanelContainer;

	[Export]
	private Label Prompt;

	bool IsPlayerInArea = false;

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
		}
		
	}

	private void OnCancelPressed(){
		PanelContainer.Visible = false;
		if (IsPlayerInArea)
		{
			Prompt.Visible = true;
		}
	}

}
