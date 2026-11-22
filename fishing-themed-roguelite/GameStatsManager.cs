using Godot;
using System;

public partial class GameStatsManager : Node
{
	public static GameStatsManager Instance {get; private set;}

	public event Action<int> CurrencyChanged;
	public event Action<int> CatchesChanged;

	public int Currency {get; private set;} = 0;
	public int TotalCatches {get; private set;} = 0;

    public override void _EnterTree()
    {
        Instance = this;
    }

	public void AddCurrecny(int amount)
	{
		if(amount <= 0) return;

		Currency += amount;
		CurrencyChanged?.Invoke(Currency);
	}

	public bool TrySpendCurrency(int amount)
	{
		if(amount <= 0 || Currency < amount) return false;

		Currency -= amount;
		CurrencyChanged?.Invoke(Currency);
		return true;
	}

	public void RegisterCatch()
	{
		TotalCatches++;
		CatchesChanged?.Invoke(TotalCatches);
	}

	public bool TrySellCatch(int amount)
	{
		if(amount <= 0 || TotalCatches < amount) return false;

		TotalCatches -= amount;
		CatchesChanged?.Invoke(TotalCatches);
		return true;
	}
}
