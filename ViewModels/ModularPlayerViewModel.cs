using System;
using System.Diagnostics;
using System.Reactive;
using System.Threading;
using LibVLCSharp.Shared;
using ReactiveUI;

namespace Nana.ViewModels
{
    public class ModularPlayerViewModel : ViewModelBase, IDisposable
    {
        public ReactiveCommand<Unit, Unit> StopCommand { get; }
        private readonly LibVLC _libVlc = new LibVLC();
        private Media? media;
        private bool _sliderEnabled = false;
        public bool SliderEnabled 
        {
            get => _sliderEnabled;
            set => this.RaiseAndSetIfChanged(ref _sliderEnabled, value);
        }
        private bool _isPlaying;
        public bool IsPlaying 
        {
            get => _isPlaying;
            set => this.RaiseAndSetIfChanged(ref _isPlaying, value);
        }
        private TimeSpan _currentPosition;
        public TimeSpan CurrentPosition
        {
            get => _currentPosition;
            set => this.RaiseAndSetIfChanged(ref _currentPosition, value);
        }
        public void PrintCurrentPosition()
        {
            CurrentPosition = TimeSpan.FromMilliseconds(MediaPlayer.Time);
            CurrentPosition = TimeSpan.FromSeconds(Math.Round(CurrentPosition.TotalSeconds));
        }
        private Timer timer;
        private TimerCallback timerCallback;
        private void PositionTick(object state)
        {
            PrintCurrentPosition();
        }
        private void AdjustPlayingStatus(object? sender, EventArgs e)
        {
            Console.WriteLine("Event Read");
            if (MediaPlayer.IsPlaying)
            {
                Console.WriteLine("Playing");
                IsPlaying = true;
                timer = new Timer(timerCallback, null, 0, 333);
            }
            else
            {
                Console.WriteLine("Stopped");
                IsPlaying = false;
                timer?.Dispose();
            }
            WatchPlaybackEvent(false);
        }
        public void OpenFile(string path)
        {
            media = new Media(_libVlc, path, FromType.FromPath);
            IsPlaying = false;
        }
        private void WatchPlaybackEvent(bool watching)
        {
            if (watching)
            {
                MediaPlayer.Playing += AdjustPlayingStatus; 
                MediaPlayer.Paused += AdjustPlayingStatus; 
            }
            else
            {
                MediaPlayer.Playing -= AdjustPlayingStatus; 
                MediaPlayer.Paused -= AdjustPlayingStatus; 
            }
        }
        public ModularPlayerViewModel()
        {
            StopCommand = ReactiveCommand.Create(Stop);
            MediaPlayer = new MediaPlayer(_libVlc);
            timerCallback = new TimerCallback(PositionTick);
        }
        public void Load()
        {
            SliderEnabled = true;
            if (media != null)
            {
                WatchPlaybackEvent(true);
                MediaPlayer.Play(media);
            }
        }

        public void PlayPause()
        {
            if (media != null)
            {
                WatchPlaybackEvent(true);
                MediaPlayer.Pause();   
            }
        }
        public void Stop()
        {
            SliderEnabled = false;
            MediaPlayer.Stop();
        }
        public void FastForward()
        {
            if (MediaPlayer.IsPlaying)
            {
                WatchPlaybackEvent(true);
                MediaPlayer.Pause();
            }
            if (MediaPlayer.Position < 0.98)
            {
                MediaPlayer.Position = (float)(MediaPlayer.Position + 0.01);
            }
            PrintCurrentPosition();
        }
        public void Rewind()
        {
            if (MediaPlayer.IsPlaying)
            {
                WatchPlaybackEvent(true);
                MediaPlayer.Pause();
            }
            if (MediaPlayer.Position > 0.02)
            {
                MediaPlayer.Position = (float)(MediaPlayer.Position - 0.01);
            }
            PrintCurrentPosition();
        }
        public MediaPlayer MediaPlayer { get; }
        public void Dispose()
        {
            MediaPlayer?.Dispose();
            _libVlc?.Dispose();
            timer?.Dispose();
        }
    }
}
