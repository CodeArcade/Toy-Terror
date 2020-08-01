using brackeys_2020_2_jam.Models;
using brackeys_2020_2_jam.States;
using System;
using Unity;

namespace brackeys_2020_2_jam
{
    public static class Program
    {
        public static IUnityContainer UnityContainer = new UnityContainer();

        [STAThread]
        static void Main()
        {
            Register();

            using JamGame game = UnityContainer.Resolve<JamGame>();
            game.Run();
        }

        static void Register()
        {
            UnityContainer.RegisterType<MenuState>();
            UnityContainer.RegisterType<GameState>();
            UnityContainer.RegisterType<StateManager>();
            UnityContainer.RegisterInstance(new JamGame());
        }

    }
}
