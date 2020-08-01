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

            using (var game = new JamGame())
                UnityContainer.Resolve<JamGame>().Run();
        }

        static void Register()
        {
            UnityContainer.RegisterType<JamGame>();
            UnityContainer.RegisterType<>();

        }

    }
}
