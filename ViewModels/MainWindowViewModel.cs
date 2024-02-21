using System;
using System.Diagnostics;
using System.Reactive;
using System.Threading;
using ReactiveUI;
using Splat;
using Nana.Models;
using Nana.Views;

namespace Nana.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    private ViewModelBase _contentViewModel;
    public HomeViewModel? Home { get; }
    public ModularPlayerViewModel? ModularPlayer { get; }
    public MainWindowViewModel()
    {
        Home = Locator.Current.GetService<HomeViewModel>();
        ModularPlayer = Locator.Current.GetService<ModularPlayerViewModel>();
        _contentViewModel = Home;
    }
    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }
    public void OpenModularPlayer()
    {
        ContentViewModel = new ModularPlayerViewModel();
    }
#pragma warning restore CA1822 // Mark members as static
}
