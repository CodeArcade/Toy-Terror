using System;

namespace brackeys_2020_2_jam
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new JamGame())
                game.Run();
        }
    }
}
