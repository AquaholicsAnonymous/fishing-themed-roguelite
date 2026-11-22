using Godot;
using System;

public partial class InteractableMenuZone : Area2D
{
	[Export] private Node _targetMenuUI;
	[Export] private Label _prompt;
	[Export] private Player _player;

	private bool _isPlayerInArea = false;

    public override void _Ready()
    {
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;

		if (_targetMenuUI is CanvasItem control)
		{
			control.VisibilityChanged += OnMenuVisibilityChanged;
		}
		else if(_targetMenuUI is CanvasLayer layer)
		{
			layer.VisibilityChanged += OnMenuVisibilityChanged;
		}
    }

	private void OnBodyEntered(Node2D body){
		if (body is Player)
		{
			_isPlayerInArea = true;
			_prompt.Visible = true;
		}
	}
	
	private void OnBodyExited(Node2D body){
		if (body is Player)
		{
			_isPlayerInArea = false;
			_prompt.Visible = false;
		}
	}

	public override void _Process(double delta)
	{
		if (_isPlayerInArea && !IsMenuVisible() && Input.IsActionJustPressed("interact"))
		{
			SetMenuVisible(true);
			_prompt.Visible = false;
			_player.IsPlayerMovable = false;
		}
	}

	private void OnMenuVisibilityChanged(){
		if (!IsMenuVisible())
		{
			_player.IsPlayerMovable = true;
			if (_isPlayerInArea)
			{
				_prompt.Visible = true;
			}
		}
	}

	private bool IsMenuVisible() => _targetMenuUI switch
	{
		CanvasItem item => item.Visible,
		CanvasLayer layer => layer.Visible,
		_ => false
	};

	private void SetMenuVisible(bool visible)
	{
		if(_targetMenuUI is CanvasItem item) item.Visible = visible;
		else if(_targetMenuUI is CanvasLayer layer) layer.Visible = visible;
	}
}
