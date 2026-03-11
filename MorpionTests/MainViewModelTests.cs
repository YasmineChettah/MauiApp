using MAUI_TD.ViewModels;
using MAUI_TD.Services;

namespace MorpionTests;

public class MainViewModelTests
{
    [Fact]
    public void Play_ShouldPlaceX_InCell1()
    {
        // Arrange
        var vm = new MainViewModel(new FakeGameHistory());

        // Act
        vm.PlayCommand.Execute("1");

        // Assert
        Assert.Equal("X", vm.B1);
    }

    [Fact]
    public void Play_ShouldDetectWin_WhenFirstRowCompleted()
    {
        // Arrange
        var vm = new MainViewModel(new FakeGameHistory());

        // Act
        vm.PlayCommand.Execute("1"); // X
        vm.PlayCommand.Execute("4"); // O
        vm.PlayCommand.Execute("2"); // X
        vm.PlayCommand.Execute("5"); // O
        vm.PlayCommand.Execute("3"); // X

        // Assert
        Assert.Equal("", vm.B1);
        Assert.Equal("", vm.B2);
        Assert.Equal("", vm.B3);
    }

    [Fact]
    public void Play_ShouldAlternatePlayers()
    {
        var vm = new MainViewModel(new FakeGameHistory());

        vm.PlayCommand.Execute("1");
        vm.PlayCommand.Execute("2");

        Assert.Equal("X", vm.B1);
        Assert.Equal("O", vm.B2);
    }

    [Fact]
    public void Play_ShouldNotOverwriteCell()
    {
        var vm = new MainViewModel(new FakeGameHistory());

        vm.PlayCommand.Execute("1");
        vm.PlayCommand.Execute("1");

        Assert.Equal("X", vm.B1);
    }
}