namespace Fishing_themedRoguelite.Tests;

using Xunit;

public class MainMenuLogicTests
{
	private class FakeSceneService : ISceneService
	{
		public bool ShouldSucceed {get; set;} = true;
		public string? LastSceneLoaded {get; private set;}
		public string? LastErrorLogged {get; private set;}
		public bool DidQuit {get; private set;}

		public bool ChangeSceneToFile(string path)
		{
			LastSceneLoaded = path;
			return ShouldSucceed;
		}

		public void Quit() => DidQuit = true;
		public void LogError(string message) => LastErrorLogged = message;
	}

	[Fact]
	public void Play_WhenSceneLoadsSuccessfully_DoesNotLogError()
	{
		var fakeService = new FakeSceneService {ShouldSucceed = true};
		var logic = new MainMenuLogic(fakeService);

		logic.Play();

		Assert.Equal("res://scenes/game_screen.tscn", fakeService.LastSceneLoaded);
		Assert.Null(fakeService.LastErrorLogged);
	}

	[Fact]
	public void Play_WhenSceneFailsToLoad_LogsError()
	{
		var fakeService = new FakeSceneService {ShouldSucceed = false};
		var logic = new MainMenuLogic(fakeService);

		logic.Play();

		Assert.NotNull(fakeService.LastErrorLogged);
		Assert.Contains("Failed to change scene", fakeService.LastErrorLogged);
	}

	[Fact]
	public void Quit_CallsQuitOnSceneService()
	{
		var fakeService = new FakeSceneService {ShouldSucceed = true};
		var logic = new MainMenuLogic(fakeService);

		logic.Quit();

		Assert.True(fakeService.DidQuit);
	}
}
