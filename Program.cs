using brackeys_2020_2_jam.Factory;
using brackeys_2020_2_jam.Manager;
using brackeys_2020_2_jam.States;
using System;
using Unity;
using Unity.Injection;

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
            UnityContainer.RegisterType<AnimationManager>(new InjectionConstructor()); // creates new manager object at resolve
            UnityContainer.RegisterType<ContentManager>();
            UnityContainer.RegisterType<AudioManager>();

            UnityContainer.RegisterType<ObstacleFactory>();

            UnityContainer.RegisterInstance(new JamGame());
        }

    }
}
