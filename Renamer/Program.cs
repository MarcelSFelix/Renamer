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
            string path = "C:/Users/jruebsam/source/repos/Renamer/Renamer/bin/Debug/net7.0/Testdata";

            while (go) {
                Console.Write("Welche Funktion soll genutzt werden: ");
                command = Console.ReadLine();

                switch (command)
                {
                    case "cngsuf":
                        Console.Write("Welche Suffix sollen geändert werden: ");
                        string sufCurrent = Console.ReadLine();
                        Console.Write("Wie soll das zukünftige Suffix heißen: ");
                        string sufFut = Console.ReadLine();
                        Methods.ChangeSuffix(path, sufCurrent, sufFut);
                        break;
                    case "cngpre":
                        Console.Write("Welche Prefix sollen geändert werden: ");
                        string preCurrent = Console.ReadLine();
                        Console.Write("Wie soll das zukünftige Prefix heißen: ");
                        string preFut = Console.ReadLine();
                        Methods.ChangeSuffix(path, preCurrent, preFut);
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
                    default:
                        Console.WriteLine("Unknown Command. Use command for more information.");
                        break;
                }
            } 
        }
    }
}