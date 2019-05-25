using System;

namespace Advanced_Lesson_6_AppDomain_Player
{
    public class Program
    {
        static void Main(string[] args)
        {
            var defaultName = args.Length > 0 ? args[0] : "noname";
            var player = new Player($"Player {defaultName}");
            player.Play();            
        }
    }
}
