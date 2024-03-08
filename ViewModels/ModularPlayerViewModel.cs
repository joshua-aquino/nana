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
        private string _dummy = "wow";
        public string Dummy
        {
            get => _dummy;
            set => this.RaiseAndSetIfChanged(ref _dummy, value);
        }
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
        private Timer timer;
        private TimerCallback timerCallback;
        private void AdjustPlayingStatus(object? sender, EventArgs e) // I want to check playable, but there is no playable event args
        {
            Console.WriteLine("Event Read");
            if (MediaPlayer.IsPlaying)
            {
                Console.WriteLine("PlAYING");
                IsPlaying = true;
                timer = new Timer(timerCallback, null, 0, 1000);
            }
            else
            {
                Console.WriteLine("Stopped");
                IsPlaying = false;
                timer?.Dispose();
            }
            MediaPlayer.Paused -= AdjustPlayingStatus; 
            MediaPlayer.Playing -= AdjustPlayingStatus; 
        }
        public void OpenFile(string path)
        {
            media = new Media(_libVlc, path, FromType.FromPath);
            Length = new TimeSpan(0, 0, (int)(media.Duration / 1000));
        }
        public ModularPlayerViewModel()
        {
            MediaPlayer = new MediaPlayer(_libVlc);
            timerCallback = new TimerCallback(PositionTick);
        }
        private void PositionTick(object state)
        {
            CurrentPosition = TimeSpan.FromMilliseconds(MediaPlayer.Time);
        }

        public void Play()
        {
            if (media != null)
            {
                MediaPlayer.Playing += AdjustPlayingStatus; 
                MediaPlayer.Play(media);
            }
        }

        public void PlayPause()
        {
            if (media != null)
            {
                MediaPlayer.Playing += AdjustPlayingStatus; 
                MediaPlayer.Paused += AdjustPlayingStatus; 
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
                MediaPlayer.Playing += AdjustPlayingStatus; 
                MediaPlayer.Paused += AdjustPlayingStatus; 
                MediaPlayer.Pause();
            }
            if (MediaPlayer.Position < 0.98)
            {
                MediaPlayer.Position = (float)(MediaPlayer.Position + 0.01);
            }
            CurrentPosition = TimeSpan.FromMilliseconds(MediaPlayer.Time);
        }
        public void Rewind()
        {
            if (MediaPlayer.IsPlaying)
            {
                MediaPlayer.Playing += AdjustPlayingStatus; 
                MediaPlayer.Paused += AdjustPlayingStatus; 
                MediaPlayer.Pause();
            }
            if (MediaPlayer.Position > 0.02)
            {
                MediaPlayer.Position = (float)(MediaPlayer.Position - 0.01);
            }
            CurrentPosition = TimeSpan.FromMilliseconds(MediaPlayer.Time);
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
