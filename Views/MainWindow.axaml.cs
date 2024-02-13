using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Nana.ViewModels;

namespace Nana.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void OnOpened(object sender, EventArgs e)
    {
        var vm = DataContext as MainWindowViewModel;
        vm?.Play();
    }
}