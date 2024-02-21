using LibVLCSharp.Shared;
using Splat;
using Nana.Models;
using Nana.ViewModels;

namespace Nana;

public static class SplatRegisteration
{
    public static void RegisterAll()
    {
        Locator.CurrentMutable.RegisterConstant(new HomeViewModel(), typeof(HomeViewModel));
        Locator.CurrentMutable.RegisterLazySingleton(() => new ModularPlayerViewModel(), typeof(ModularPlayerViewModel));
    }
}
