using System;
using LibVLCSharp.Shared;

namespace Nana.ViewModels
{
    public class ModularPlayerViewModel : ViewModelBase, IDisposable
    {
        private readonly LibVLC _libVlc = new LibVLC();
        private Media media;
        private TimeSpan length;
        public ModularPlayerViewModel()
        {
            media = new Media(_libVlc, new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"));
            MediaPlayer = new MediaPlayer(_libVlc);
            length = new TimeSpan(0, 0, (int)(media.Duration / 1000));
        }
        public void Play()
        {
            MediaPlayer.Play(media);
        }
        public void Pause()
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
