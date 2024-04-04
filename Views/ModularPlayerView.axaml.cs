using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
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
            ((ModularPlayerViewModel)(this.DataContext)).MediaPlayer.Position = ((float)(timeSlider.Value)) / 100;
        }
        public async void UpdateSlider()// the problem comes from the here
        {
            while (((ModularPlayerViewModel)(this.DataContext)).SliderEnabled)
            {
                timevaluefloat.Text = ((float)(timeSlider.Value)).ToString();
                if (Math.Abs(((float)(timeSlider.Value)) / 100 - ((ModularPlayerViewModel)(this.DataContext)).MediaPlayer.Position) > 0.02
                    && sliderUpdatable == true)       // diference is 5% of total time;  wrong!
                {                                                                                                                       //CLEAN MATH AND MAKE FF WORK
                    MatchMediaToSlider();
                }
                else
                {
                    MatchSliderToMedia();
                    sliderUpdatable = true;
                }
                await Task.Delay(1000); 
            }
        }
        public void Load(object sender, RoutedEventArgs args)
        {
            ((ModularPlayerViewModel)(this.DataContext)).Load();
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
