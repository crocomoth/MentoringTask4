using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileReveiver.Service;

namespace FileReveiver
{
    class Program
    {
        private static FileReceiver fileReceiver;

        static void Main(string[] args)
        {
            fileReceiver = new FileReceiver();
            var input = "";
            while (input != "exit")
            {
                input = Console.ReadLine();
            }
        }
    }
}
