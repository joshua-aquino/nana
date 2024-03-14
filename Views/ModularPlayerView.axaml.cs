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
        private bool sliderUpdatable;
        public ModularPlayerView()
        {
            sliderUpdatable = true;
            InitializeComponent();
        }
        private void MatchSliderToMedia()
        {
            timeSlider.Value = ((ModularPlayerViewModel)(this.DataContext)).MediaPlayer.Position * 100;                  // use the timer to check the value of the slider
        }
        private void MatchMediaToSlider()
        {
            ((ModularPlayerViewModel)(this.DataContext)).MediaPlayer.Position = timeSliderValueFloat / 100;
        }
        private float timeSliderValueFloat;
        public async void UpdateSlider()
        {
            while (true)
            {
                await Task.Delay(1000);
                timeSliderValueFloat = (float)(timeSlider.Value);
                timevaluefloat.Text = timeSliderValueFloat.ToString();
                if (Math.Abs(timeSliderValueFloat / 100 - ((ModularPlayerViewModel)(this.DataContext)).MediaPlayer.Position) > 0.02
                    && sliderUpdatable == true)       // diference is 5% of total time;  wrong!
                {                                                                                                                       //CLEAN MATH AND MAKE FF WORK
                    MatchMediaToSlider();
                }
                else
                {
                    MatchSliderToMedia();
                    sliderUpdatable = true;
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
            sliderUpdatable = false;
            ((ModularPlayerViewModel)(this.DataContext)).FastForward();
            MatchSliderToMedia();
        }
        public void Rewind(object sender, RoutedEventArgs args)
        {
            sliderUpdatable = false;
            ((ModularPlayerViewModel)(this.DataContext)).Rewind();
            MatchSliderToMedia();
        }
        public void ClickSlider(object sender, PointerPressedEventArgs args)
        {
            ((ModularPlayerViewModel)(this.DataContext)).PrintCurrentPosition();
        }
    }
}
