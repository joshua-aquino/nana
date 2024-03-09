using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Nana.ViewModels;

namespace Nana.Views
{
    public partial class ModularPlayerView : UserControl
    {
        public double CurrentPercentage;
        public ModularPlayerView()
        {
            InitializeComponent();
            isPlayingText.Text = "wow";
            AttachedToVisualTree += OnAttachedToVisualTree;
            timeSlider.KeyDown += OnTimeSliderPointerReleased;
        }
        private async void OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
        {
        }
        
        private async void OnTimeSliderPointerReleased(object sender, KeyEventArgs args)
        {
            timeSlider.KeyDown -= OnTimeSliderPointerReleased;
        }
        public void DummyClicc(object sender, RoutedEventArgs args)
        {
            ((ModularPlayerViewModel)(this.DataContext)).MediaPlayer.Position = (float)0.69;
        }
        public async void UpdateSlider(object sender, RoutedEventArgs args)
        {
            while (true)
            {
                await Task.Delay(1000);
                timeSlider.Value = ((ModularPlayerViewModel)(this.DataContext)).CurrentPercentage;                  // use the timer to check the value of the slider
                CurrentPercentage = timeSlider.Value;
            }
            else
            {
                ((ModularPlayerViewModel)(this.DataContext)).MediaPlayer.Position = (float)(timeSlider.Value * 0.01);
            }
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
