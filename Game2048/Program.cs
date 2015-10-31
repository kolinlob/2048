using System;

namespace Game2048
{
    class Program
    {
        static void Main()
        {
            var game = new Game();
            game.Start();

            Console.WriteLine("\r\n" +
                              "\tEnd");
            Console.ReadLine();
        }
    }
}
