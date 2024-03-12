using System;
using System.Runtime.Serialization.Formatters;
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
        public async void UpdateSlider()
        {
        float timeSliderValueFloat;
            while (true)
            {
                await Task.Delay(1000);
                timeSliderValueFloat = (float)(timeSlider.Value);
                timevaluefloat.Text = timeSliderValueFloat.ToString();
                if (Math.Abs(timeSliderValueFloat / 100 - ((ModularPlayerViewModel)(this.DataContext)).CurrentPercentage) > 0.02)       // diference is 5% of total time;  wrong!
                {                                                                                                                       //CLEAN MATH AND MAKE FF WORK
                    ((ModularPlayerViewModel)(this.DataContext)).MediaPlayer.Position = timeSliderValueFloat / 100;
                }
                else
                {
                    timeSlider.Value = ((ModularPlayerViewModel)(this.DataContext)).CurrentPercentage * 100;                  // use the timer to check the value of the slider
                    CurrentPercentage = timeSlider.Value;
                }
            }
        }
        public void Play(object sender, RoutedEventArgs args)
        {
            ((ModularPlayerViewModel)(this.DataContext)).Play();
            UpdateSlider();
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
