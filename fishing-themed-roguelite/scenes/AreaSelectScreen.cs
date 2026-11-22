using Godot;
using System;

public partial class AreaSelectScreen : CanvasLayer
{
	[Signal] public delegate void MenuClosedEventHandler();

	[Export] Container ButtonContainer {get; set;}
	[Export] BaseButton BackButton {get; set;}

	public override void _Ready()
	{
		AddToGroup("closable_menus");
		
		if(BackButton != null)
		{
			BackButton.Pressed += CloseMenu;
		}
		
		for(int i = 0; i < ButtonContainer.GetChildCount(); i++)
		{
			if(ButtonContainer.GetChild(i) is TextureButton button)
			{
				int levelNum = i + 1;
				button.Disabled = !LevelManager.Instance.IsLevelUnlocked(levelNum);
				button.Pressed += () => OnLevelSelected(levelNum);
			}
		}
	}

	private void OnLevelSelected(int levelNum)
	{
		GetTree().ChangeSceneToFile($"res://scenes/levels/area{levelNum}.tscn");
	}

    public override void _Input(InputEvent @event)
    {
        if(Visible && !GetTree().Paused && @event.IsActionPressed("pause"))
		{
			CloseMenu();
			GetViewport().SetInputAsHandled();
		}
    }

	public void CloseMenu()
	{
		if(!Visible) return;
		
		Visible = false;
		EmitSignal(SignalName.MenuClosed);
	}

}
