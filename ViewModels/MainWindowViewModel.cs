﻿using System;
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
    public ReactiveCommand<string, Unit> OpenModularPlayer { get; }
    public HomeViewModel? Home { get; }
    public ModularPlayerViewModel? ModularPlayer { get; }
    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        private set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }
    public void _openModularPlayer(string path)
    {
        if (ModularPlayer != null)
        {
            ContentViewModel = ModularPlayer;
            ModularPlayer.OpenFile(path);
        }
    }
    public MainWindowViewModel()
    {
        Home = Locator.Current.GetService<HomeViewModel>();
        ModularPlayer = Locator.Current.GetService<ModularPlayerViewModel>(); 
        _contentViewModel = Home;
        OpenModularPlayer = ReactiveCommand.Create<string>(_openModularPlayer);
    }
#pragma warning restore CA1822 // Mark members as static
}
