using System;
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
        private bool isPlaying = false;
        private Timer timer;
        private TimerCallback timerCallback;
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
        private void AdjustPlayingStatus()
        {
            if (MediaPlayer.IsPlaying)
                isPlaying = true;
                timer = new Timer(timerCallback, null, 0, 1000);
            else
                isPlaying = false;
                timer?.Dispose;
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
            CurrentPosition = CurrentPosition.Add(TimeSpan.FromSeconds(1));
        }
        public void Play()
        {
            if (media != null)
                MediaPlayer.Play(media);
            AdjustPlayingStatus();
        }
        public void PlayPause()
        {
            MediaPlayer.Pause();   
            AdjustPlayingStatus();
        }
        public void Stop()
        {
            MediaPlayer.Stop();
            AdjustPlayingStatus();
        }
        public void FastForward()
        {
            if (MediaPlayer.IsPlaying)
                MediaPlayer.Pause();
            if (MediaPlayer.Position < 0.98)
            {
                MediaPlayer.Position = (float)(MediaPlayer.Position + 0.01);
            }
            AdjustPlayingStatus();
        }
        public void Rewind()
        {
            if (MediaPlayer.IsPlaying)
                MediaPlayer.Pause();
            if (MediaPlayer.Position > 0.02)
            {
                MediaPlayer.Position = (float)(MediaPlayer.Position - 0.01);
            }
            AdjustPlayingStatus();
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
