using System;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Nana.ViewModels;

namespace Nana.Views
{
    public partial class ModularPlayerView : UserControl
    {
        public ModularPlayerView()
        {
            InitializeComponent();
        }
        public void Play(object sender, RoutedEventArgs args)
        {
            ((ModularPlayerViewModel)(this.DataContext)).Play();
        }
        public void PlayPause(object sender, RoutedEventArgs args)
        {
            ((ModularPlayerViewModel)(this.DataContext)).PlayPause();
        }
        public void Stop(object sender, RoutedEventArgs args)
        {
            ((ModularPlayerViewModel)(this.DataContext)).Stop();
        }
        public void FastForward(object sender, RoutedEventArgs args)
        {
            ((ModularPlayerViewModel)(this.DataContext)).FastForward();
        }
        public void Rewind(object sender, RoutedEventArgs args)
        {
            ((ModularPlayerViewModel)(this.DataContext)).Rewind();
        }
    }
}
