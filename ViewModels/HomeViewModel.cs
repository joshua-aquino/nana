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
    }        
}
