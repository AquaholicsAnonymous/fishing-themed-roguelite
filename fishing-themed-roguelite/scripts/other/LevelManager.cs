using Godot;
using System;
using System.Collections.Generic;

public partial class LevelManager : Node
{
	public static LevelManager Instance {get; private set;}

	private Dictionary<int, bool> _unlockedLevels = new()
	{
		{1, true},
		{2, false},
		{3, false},
		{4, false}
	};
	
	public override void _Ready()
	{
		Instance = this;
	}

	public void UnlockLevel(int levelNum)
	{
		_unlockedLevels[levelNum] = true;
	}

	public bool IsLevelUnlocked(int levelNum)
	{
		return _unlockedLevels.TryGetValue(levelNum, out bool unlocked) && unlocked;
	}
}
