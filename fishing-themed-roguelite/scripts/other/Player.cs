using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public const float SprintSpeed = 450.0f;
	public bool IsPlayerMovable = true;

	public bool IsPlayerSprinting = false;

	public override void _PhysicsProcess(double delta)
	{
		if (!IsPlayerMovable)
		{
			Velocity = Vector2.Zero;
			IsPlayerSprinting = false;
			return;
		}
		Vector2 velocity = Velocity;

		// Add the gravity.
		/*if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}*/

		// Handle Jump.
		/*if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}*/

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");
		if (direction != Vector2.Zero)
		{
			if (Input.IsActionJustPressed("dash"))
			{
				IsPlayerSprinting = true;
			}

			if (IsPlayerSprinting)
			{
				velocity.X = direction.X * SprintSpeed;
				velocity.Y = direction.Y * SprintSpeed;
			}
			else
			{
				velocity.X = direction.X * Speed;
				velocity.Y = direction.Y * Speed;
			}
		}
		else
		{
			IsPlayerSprinting = false;
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
