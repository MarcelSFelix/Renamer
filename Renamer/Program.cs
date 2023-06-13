using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Renamer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string command = "";
            bool go = true;


            while (go) {
                Console.Write("Welche Funktion soll genutzt werden: ");
                command = Console.ReadLine();

                switch (command)
                {
                    case "cngsuf":
                        Console.WriteLine("Welche Suffix sollen geändert werden: ");
                        string cngCurrent = Console.ReadLine();
                        Console.WriteLine("Wie soll das zukünftige Suffix heißen: ");
                        string cngFut = Console.ReadLine();
                        break;
                    case "cngpre":
                        break;
                    case "delpre":
                        break;
                    case "delsuf":
                        break;
                    case "commands":
                        break;
                    case "stop":
                        go = false; 
                        break;
                }
            } 
        }
    }
}