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
        private ModularPlayerViewModel GetModularPlayer()
        {
            return (ModularPlayerViewModel)(this.DataContext);
        }
        public ModularPlayerView()
        {
            InitializeComponent();
        }
        private void MatchSliderToMedia()
        {
            timeSlider.Value = GetModularPlayer().MediaPlayer.Position * 100;                  // use the timer to check the value of the slider
        }
        private void MatchMediaToSlider()
        {
            GetModularPlayer().MediaPlayer.Position = ((float)(timeSlider.Value)) / 100;
        }
        public async void UpdateSlider()                // currently, changing to home doesn't delete the view instance
        {
            while (GetModularPlayer().SliderEnabled)
            {
                timevaluefloat.Text = ((float)(timeSlider.Value)).ToString();
                if (Math.Abs(((float)(timeSlider.Value)) / 100 - GetModularPlayer().MediaPlayer.Position) > 0.02
                    && GetModularPlayer().SliderUpdatable == true)       // diference is 5% of total time;  wrong!
                {                                                                                                                       //CLEAN MATH AND MAKE FF WORK
                    MatchMediaToSlider();
                }
                else
                {
                    MatchSliderToMedia();
                    GetModularPlayer().SliderUpdatable = true;
                }
                await Task.Delay(1000); 
            }
        }
        public void Load(object sender, RoutedEventArgs args)
        {
            GetModularPlayer().Load();
            UpdateSlider();
        }
        public void PlayPause(object sender, RoutedEventArgs args)
        {
            GetModularPlayer().PlayPause();
        }
        public void Stop(object sender, RoutedEventArgs args)
        {
            GetModularPlayer().Stop();
        }
        public void FastForward(object sender, RoutedEventArgs args)
        {
            GetModularPlayer().SliderUpdatable = false;
            GetModularPlayer().FastForward();
            MatchSliderToMedia();
        }
        public void Rewind(object sender, RoutedEventArgs args)
        {
            GetModularPlayer().SliderUpdatable = false;
            GetModularPlayer().Rewind();
            MatchSliderToMedia();
        }
        public void ClickSlider(object sender, PointerPressedEventArgs args)
        {
            GetModularPlayer().PrintCurrentPosition();
        }
    }
}
