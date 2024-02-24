using System;
using System.Reactive;
using LibVLCSharp.Shared;
using ReactiveUI;

namespace Nana.ViewModels
{
    public class ModularPlayerViewModel : ViewModelBase, IDisposable
    {
        private readonly LibVLC _libVlc = new LibVLC();
        private Media? media;
        private TimeSpan length;
        public void OpenFile(string path)
        {
            media = new Media(_libVlc, path, FromType.FromPath);
            length = new TimeSpan(0, 0, (int)(media.Duration / 1000));
        }
        public ModularPlayerViewModel()
        {
            MediaPlayer = new MediaPlayer(_libVlc);
        }
        public void Play()
        {
            if (media != null)
                MediaPlayer.Play(media);
        }
        public void PlayPause()
        {
            MediaPlayer.Pause();   
        }
        public void Stop()
        {
            MediaPlayer.Stop();
        }
        public void FastForward()
        {
            if (MediaPlayer.IsPlaying)
                MediaPlayer.Pause();
            if (MediaPlayer.Position < 0.98)
            {
                MediaPlayer.Position = (float)(MediaPlayer.Position + 0.01);
            }
        }
        public void Rewind()
        {
            if (MediaPlayer.IsPlaying)
                MediaPlayer.Pause();
            if (MediaPlayer.Position > 0.02)
            {
                MediaPlayer.Position = (float)(MediaPlayer.Position - 0.01);
            }
        }
        public MediaPlayer MediaPlayer { get; }
        public void Dispose()
        {
            MediaPlayer?.Dispose();
            _libVlc?.Dispose();
        }
    }
}
