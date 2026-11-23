using Godot;

public partial class PrototypeTests : Node
{
	private int _passed;
	private int _failed;

	public override void _Ready()
	{
		Check(
			GameLoop.CalculateRemainingHealth(6, 1) == 5,
			"One damage removes one health");

		Check(
			GameLoop.CalculateRemainingHealth(1, 5) == 0,
			"Overkill damage stops at zero health");

		Check(
			GameLoop.CalculateRemainingHealth(6, 0) == 6,
			"Zero damage leaves health unchanged");

		Check(
			GameLoop.CalculateRemainingHealth(6, -2) == 6,
			"Negative damage does not heal the player");

		Vector2 direction = Enemy.GetChaseDirection(
			Vector2.Zero, new Vector2(3, 4));

		Check(
			direction.IsEqualApprox(new Vector2(0.6f, 0.8f)),
			"Enemy direction points toward player and is normalized");

		Check(
			Enemy.GetChaseDirection(
				Vector2.One, Vector2.One) == Vector2.Zero,
			"Same position produces zero movement direction");

		TestHealthSignal();

		GD.Print($"TEST RESULTS: {_passed} passed, {_failed} failed.");
	}

	private void TestHealthSignal()
	{
		HealthComponent health = new HealthComponent();
		AddChild(health);

		health.CurrentHealth = 6;

		int receivedNewHealth = -1;
		int receivedOldHealth = -1;

		health.OnHealthChanged += (newHealth, oldHealth) =>
		{
			receivedNewHealth = newHealth;
			receivedOldHealth = oldHealth;
		};

		health.CurrentHealth = 5;

		Check(
			health.CurrentHealth == 5 &&
			receivedNewHealth == 5 &&
			receivedOldHealth == 6,
			"Health change stores value and signals old/new health");

		health.QueueFree();
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
