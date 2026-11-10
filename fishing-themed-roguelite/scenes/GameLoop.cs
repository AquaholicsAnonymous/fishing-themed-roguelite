using Godot;

public partial class GameLoop : Node2D
{
	private const int StartingHealth = 6;
	private const double DamageCooldown = 1.0;

	private HealthComponent _health;
	private HeartBar _heartBar;
	private Player _player;

	private double _damageCooldownRemaining;
	private bool _isReturningToHub;

	public override void _Ready()
	{
		_health = GetNode<HealthComponent>("HealthComponent");
		_heartBar = GetNode<HeartBar>("HUD/MarginContainer/HeartBar");
		_player = GetNode<Player>("Player");

		_health.OnHealthChanged += OnHealthChanged;

		_health.CurrentHealth = StartingHealth;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_damageCooldownRemaining > 0)
		{
			_damageCooldownRemaining -= delta;
		}
	}

	public void TakeDamage(int amount)
	{
		if (_isReturningToHub ||
			_damageCooldownRemaining > 0 ||
			amount <= 0)
		{
			return;
		}

		_damageCooldownRemaining = DamageCooldown;
		_health.CurrentHealth = CalculateRemainingHealth(
			_health.CurrentHealth, amount);
	}

	public static int CalculateRemainingHealth(int health, int damage)
	{
		return Mathf.Max(0, health - Mathf.Max(0, damage));
	}

	private void OnHealthChanged(int newHealth, int oldHealth)
	{
		_heartBar.UpdateHealth(newHealth);

		GD.Print($"Health: {oldHealth} -> {newHealth}");

		if (newHealth <= 0 && !_isReturningToHub)
		{
			_isReturningToHub = true;
			_player.IsPlayerMovable = false;

			Callable.From(ReturnToHub).CallDeferred();
		}
	}

	private void ReturnToHub()
	{
		Error result = GetTree().ChangeSceneToFile(
			"res://scenes/game_screen.tscn");

		if (result != Error.Ok)
		{
			GD.PushError($"Could not return to hub: {result}");
		}
	}

	public override void _ExitTree()
	{
		if (GodotObject.IsInstanceValid(_health))
		{
			_health.OnHealthChanged -= OnHealthChanged;
		}
	}
}
