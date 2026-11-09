using Godot;
using System;

public partial class HealthComponent : Node
{
	[Signal]
	public delegate void OnHealthChangedEventHandler(int newHealth, int oldHealth);

	private int _currentHealth;

	public int CurrentHealth
	{
		// this is just setter/getter pattern, like in CS 202 you'd have to create
		// setter/getter methods but C# allows you to do it with assignment syntax:
		// object.CurrentHealth = 123 instead of object.SetCurrentHealth(123)
		get => _currentHealth;
		set
		{
			int oldHealth = _currentHealth;
			_currentHealth = value;
			EmitSignal(SignalName.OnHealthChanged, _currentHealth, oldHealth);
		}
	}
}
