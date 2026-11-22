using Godot;
using System;
using System.Collections.Generic;

public enum UpgradeType
{
	RodStrength,
	HookStrength,
	ShopQuality
}

public partial class UpgradesManager : Node
{
	public static UpgradesManager Instance {get; private set;}

	public event Action<UpgradeType, int> UpgradePurchased;

	private readonly Dictionary<UpgradeType, int> _levels = new()
	{
		{UpgradeType.RodStrength, 1},
		{UpgradeType.HookStrength, 1},
		{UpgradeType.ShopQuality, 1}
	};

	private const int MaxLevel = 10;

    public override void _EnterTree()
    {
        Instance = this;
    }

	public int GetLevel(UpgradeType type) => _levels[type];

	public int GetUpgradeCost(UpgradeType type)
	{
		int currentLevel = _levels[type];
		return currentLevel * 50;
	}

	public bool CanAfford(UpgradeType type)
	{
		int cost = GetUpgradeCost(type);
		return GameStatsManager.Instance.Currency >= cost;
	}

	public bool CanPurchase(UpgradeType type)
	{
		int currentLevel = _levels[type];
		return MaxLevel > currentLevel;
	}

	public bool TryPurchaseUpgrade(UpgradeType type)
	{
		int currentLevel = _levels[type];
		if(currentLevel >= MaxLevel) return false;

		int cost = GetUpgradeCost(type);

		if(!GameStatsManager.Instance.TrySpendCurrency(cost)) return false;

		_levels[type] = currentLevel + 1;
		UpgradePurchased?.Invoke(type, _levels[type]);
		return true;
	}
}
