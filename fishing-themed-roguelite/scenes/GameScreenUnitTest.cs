using Godot;
using System;

public partial class GameScreenUnitTest : Node2D
{
	private int _passed;
	private int _failed;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TestAreaSelection();
		TestPlayerMovement();
	}

	private void TestAreaSelection()
	{
		PackedScene hubScene = GD.Load<PackedScene>("res://scenes/game_screen.tscn");
		Node2D hub = hubScene.Instantiate<Node2D>();
		AddChild(hub);
		AreaSelection areaSelection = hub.GetNode<AreaSelection>("AreaSelection");
		Player player = hub.GetNode<Player>("Player");
		Label prompt = hub.GetNode<Label>("CanvasLayer/Prompt");

		Check(
			!prompt.Visible,
			"Prompt is initially hidden");

		areaSelection.EmitSignal(Area2D.SignalName.BodyEntered, player);

		Check(
			prompt.Visible,
			"Prompt is visible after entering the area");

		hub.QueueFree();
	}

	private void TestPlayerMovement()
	{
		PackedScene hubScene = GD.Load<PackedScene>("res://scenes/game_screen.tscn");
		Node2D hub = hubScene.Instantiate<Node2D>();
		AddChild(hub);
		Player player = hub.GetNode<Player>("Player");

		// Test movement
		player.Velocity = new Vector2(100, 0);
		player.IsPlayerMovable = false;
		player._PhysicsProcess(1.0);
		Check(player.Velocity == Vector2.Zero, "Player does not move when movement is disabled");

		hub.QueueFree();
	}

	private void Check(bool condition, string testName)
	{
		if (condition)
		{
			_passed++;
			GD.Print($"PASS: {testName}");
		}
		else
		{
			_failed++;
			GD.PushError($"FAIL: {testName}");
		}
	}
}
