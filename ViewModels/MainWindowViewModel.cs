using System;
using LibVLCSharp.Shared;
using Nana.Views;

namespace Nana.ViewModels;

public class MainWindowViewModel : ViewModelBase, IDisposable
{
#pragma warning disable CA1822 // Mark members as static
//    public string Greeting => "Sorry! This still under construction.";
#pragma warning restore CA1822 // Mark members as static
    private readonly LibVLC _libvlc = new LibVLC();
    public MainWindowViewModel()
    {
        MediaPlayer = new MediaPlayer(_libvlc);
    }
    public void Play()
    {
        using var media = new Media(_libvlc, new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"));
        MediaPlayer.Play(media);
    }
    public MediaPlayer MediaPlayer { get; }
    public void Dispose()
    {
        MediaPlayer?.Dispose();
        _libvlc?.Dispose();
    }
}
