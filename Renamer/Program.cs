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
            string path = "../../Debug/net7.0/Testdata";

            while (go) {
                Console.Write("Welche Funktion soll genutzt werden: ");
                command = Console.ReadLine();

                switch (command.ToLower())
                {
                    case "cngsuf":
                        Console.Write("Welches Suffix sollen geändert werden: ");
                        string sufCurrent = Console.ReadLine();
                        Console.Write("Wie soll das zukünftige Suffix heißen: ");
                        string sufFut = Console.ReadLine();
                        Methods.ChangeSuffix(path, sufCurrent, sufFut);
                        break;
                    case "cngpre":
                        Console.Write("Welches Prefix sollen geändert werden: ");
                        string preCurrent = Console.ReadLine();
                        Console.Write("Wie soll das zukünftige Prefix heißen: ");
                        string preFut = Console.ReadLine();
                        Methods.ChangePrefix(path, preCurrent, preFut);
                        break;
                    case "delpre":
                        Console.WriteLine("Welcher Präfix soll entfernt werden?");
                        string deletablePrefix = Console.ReadLine();
                        Methods.DeletePrefix(path, deletablePrefix);
                        break;
                    case "delsuf":
                        break;
                    case "commands":
                        Methods.commands();
                        break;
                    case "zero":
                        Console.WriteLine("Add or delete?");
                        string addOrDelete = Console.ReadLine();
                        Methods.zeroFunction(path, addOrDelete);
                        break;
                    case "ledzer":
                        Console.Write("Padding (nur ganze Zahlen): ");
                        if(!int.TryParse(Console.ReadLine(), out int length))
                        {
                            Console.WriteLine("Bitte ein Integer eingeben");
                            break;
                        }
                        Methods.LeadZeros(path, length);
                        break;
                    case "movnum":
                        Console.WriteLine("Soll ein Zahlenblock an den Anfang des Namens ('first') oder ans Ende ('last') geschoben werden: ");
                            string mode = Console.ReadLine().ToLower();
                            Methods.MoveNumbers(path, mode);
                            break;
                    case "undo":
                        Methods.Undo();
                        break;
                    case "exit":
                    case "stop":
                        go = false; 
                        break;
                    default:
                        Console.WriteLine("Unknown Command. Write 'commands' for more information.");
                        break;
                }
            } 
        }
    }
}