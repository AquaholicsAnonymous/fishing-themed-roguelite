using Godot;
using System;

public partial class Hud : CanvasLayer
{
	[Export] private Label _currencyLabel;
	[Export] private Label _catchesLabel;

	public override void _Ready()
	{
		var stats = GameStatsManager.Instance;

		UpdateCurrencyText(stats.Currency);
		UpdateCatchesText(stats.TotalCatches);

		stats.CurrencyChanged += UpdateCurrencyText;
		stats.CatchesChanged += UpdateCatchesText;
	}

    public override void _ExitTree()
    {
        if(GameStatsManager.Instance != null)
		{
			GameStatsManager.Instance.CurrencyChanged -= UpdateCurrencyText;
			GameStatsManager.Instance.CatchesChanged -= UpdateCatchesText;
		}
    }

	public void UpdateCurrencyText(int newAmount) => _currencyLabel.Text = $"{newAmount}";
	public void UpdateCatchesText(int newCount) => _catchesLabel.Text = $"{newCount}";
}
