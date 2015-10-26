using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048
{
    class Program
    {
        static void Main()
        {
            var game = new Game();
            game.Start();

            Console.WriteLine("End");
            Console.ReadLine();
        }
    }
}
