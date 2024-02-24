using System.IO;
using System.Reactive;
using Nana.Models;
using ReactiveUI;

namespace Nana.ViewModels;
public class HomeViewModel : ViewModelBase
{
    public MediaEntry Dummy { get; }
    public class MediaEntry
    {
        private readonly string _path;
        private readonly string _name;
        public string Name { get => _name ; }
        public MediaEntry(string path)
        {
            _path = path;
            _name = _path;
        }
    }
    public HomeViewModel()
    {
        Dummy = new("./Assets/orange-tesla.mp4");
            SalamancaBlood = ReactiveCommand.Create<string>(HesDead);           // the problem is the unique state of home in main window; make it more like modular
    }        
    private string _greeting = "salamonki";
        public string Greeting 
        {
            get => _greeting;
            set => this.RaiseAndSetIfChanged(ref _greeting, value);
        }
        public ReactiveCommand<string, Unit> SalamancaBlood { get; }
        private void HesDead(string msg)
        {
            Greeting = msg;
        }

}
