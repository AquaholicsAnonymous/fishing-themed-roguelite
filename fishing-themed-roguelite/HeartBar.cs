using Godot;
using System;

public partial class HeartBar : HBoxContainer
{
	private Texture2D _heartFull;
    private Texture2D _heartHalf;
    private Texture2D _heartEmpty;

    public override void _Ready()
    {
        _heartFull = GD.Load<Texture2D>("res://assets/hud/hud_heartFull.png");
        _heartHalf = GD.Load<Texture2D>("res://assets/hud/hud_heartHalf.png");
        _heartEmpty = GD.Load<Texture2D>("res://assets/hud/hud_heartEmpty.png");
    }

	public void UpdateHealth(int value)
	{
		for(int i = 0; i < GetChildCount(); i++)
		{
			if(value > i * 2 + 1)
			{
				((TextureRect)GetChild(i)).Texture = _heartFull;
			}
			else if (value > i * 2)
			{
				((TextureRect)GetChild(i)).Texture = _heartHalf;
			}
			else
			{
				((TextureRect)GetChild(i)).Texture = _heartEmpty;
			}
		}
	}
}
