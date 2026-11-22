using Godot;
using System;
using System.Collections.Generic;

public partial class UpgradeShop : Control
{
	[Export] private TextureButton _rodButton;
	[Export] private TextureButton _hookButton;
	[Export] private TextureButton _shopButton;

	[Export] BaseButton BackButton {get; set;}
	
	private Dictionary<UpgradeType, (TextureButton Button, Label Label)> _upgradeButtons;

	public override void _Ready()
	{
		AddToGroup("closable_menus");

		if(BackButton != null)
		{
			BackButton.Pressed += CloseMenu;
		}

		_upgradeButtons = new()
		{
			{UpgradeType.RodStrength, (_rodButton, _rodButton.GetNode<Label>("Label"))},
			{UpgradeType.HookStrength, (_hookButton, _hookButton.GetNode<Label>("Label"))},
			{UpgradeType.ShopQuality, (_shopButton, _shopButton.GetNode<Label>("Label"))}
		};

		foreach(var (type, entry) in _upgradeButtons)
		{
			entry.Button.Pressed += () => OnUpgradeButtonPressed(type);
		}

		UpdateButtonStates();

		VisibilityChanged += () =>
		{
			if(Visible) UpdateButtonStates();
		};
	}

	public void OnUpgradeButtonPressed(UpgradeType type)
	{
		if (UpgradesManager.Instance.TryPurchaseUpgrade(type))
		{
			UpdateButtonStates();
		}
	}

	public void UpdateButtonStates()
	{
		foreach(var (type, entry) in _upgradeButtons)
		{
			bool canAfford = UpgradesManager.Instance.CanAfford(type);
			bool canPurchase = UpgradesManager.Instance.CanPurchase(type);
			entry.Button.Disabled = !canAfford || !canPurchase;

			int currentLevel = UpgradesManager.Instance.GetLevel(type);

			if (!canPurchase)
			{
				entry.Label.Text = $"Lv {currentLevel}: MAX";
			}
			else
			{
				int nextLevel = currentLevel + 1;
				int cost = UpgradesManager.Instance.GetUpgradeCost(type);
				entry.Label.Text = $"Lv {nextLevel}: {cost}";
			}
		}
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
	}
}
