using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using System.Reflection;
using DynamicData;
using Nana.Models;
using ReactiveUI;

namespace Nana.ViewModels;
public class HomeViewModel : ViewModelBase
{
    public MediaEntry Dummy { get; }
    public ObservableCollection<MediaEntry> Videos { get; set; }
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
        Videos = [];
        string[] files = System.IO.Directory.GetFiles("./Assets", "*.mp4");
        foreach (var file in System.IO.Directory.GetFiles("./Assets", "*.mp4"))
        {
            Videos.Add(new MediaEntry(file));
        }
        Dummy = Videos[0];
    }        
}
