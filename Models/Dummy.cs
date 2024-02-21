using System;
using System.Linq;

namespace Nana.Models;

public class Dummy : IDummy
{
    private string _name = "dummy";
    public string Name 
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    public void ReverseName()
    {
        this.Name = new string(Name.Reverse().ToArray());
    }
}
