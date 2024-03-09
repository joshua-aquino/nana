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
        private readonly LibVLC _libVlc = new LibVLC();
        private Media? media;
        private bool _isPlaying = false;
        public bool IsPlaying 
        {
            get => _isPlaying;
            set => this.RaiseAndSetIfChanged(ref _isPlaying, value);
        }
        private TimeSpan _length;
        public TimeSpan Length
        {
            get => _length;
            set => this.RaiseAndSetIfChanged(ref _length, value);
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
        private float _currentPercentage;
        public float CurrentPercentage
        {
            get => _currentPercentage;
            set => this.RaiseAndSetIfChanged(ref _currentPercentage, value);
        }
        public void PrintCurrentPercentage()
        {
            CurrentPercentage = MediaPlayer.Position * 100;
        }
        private Timer timer;
        private TimerCallback timerCallback;
        private void PositionTick(object state)
        {
            PrintCurrentPosition();
            PrintCurrentPercentage();
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
            Length = new TimeSpan(0, 0, (int)(media.Duration / 1000));
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
            MediaPlayer = new MediaPlayer(_libVlc);
            timerCallback = new TimerCallback(PositionTick);
        }
        public void Play()
        {
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
            PrintCurrentPercentage();
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
            PrintCurrentPercentage();
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
