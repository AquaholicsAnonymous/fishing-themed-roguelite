using Godot;

public partial class Enemy : CharacterBody2D
{
	[Export]
	public float Speed = 150.0f;

	private Player _player;
	private GameLoop _gameLoop;

	public override void _Ready()
	{
		_player = GetNode<Player>("../Player");
		_gameLoop = GetParent<GameLoop>();

		MotionMode = MotionModeEnum.Floating;
	}

	public override void _PhysicsProcess(double delta)
	{
		// Stop chasing when the player is dead.
		if (!_player.IsPlayerMovable)
		{
			Velocity = Vector2.Zero;
			return;
		}

		Vector2 direction = GetChaseDirection(
			GlobalPosition, _player.GlobalPosition);

		Velocity = direction * Speed;
		MoveAndSlide();

		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			KinematicCollision2D collision = GetSlideCollision(i);

			if (collision.GetCollider() == _player)
			{
				_gameLoop.TakeDamage(1);
				break;
			}
		}
	}

	public static Vector2 GetChaseDirection(
		Vector2 enemyPosition, Vector2 playerPosition)
	{
		return (playerPosition - enemyPosition).Normalized();
	}
}
