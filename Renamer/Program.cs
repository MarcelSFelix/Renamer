﻿using System;
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
                Console.Write("Welche Funktion soll genutzt werden: (\"commands\" um Commands zu sehen) ");
                command = Console.ReadLine();

                switch (command.ToLower())
                {
                    case "commands":
                        Methods.commands();
                        break;
                    case "cngpre":
                        Console.Write("Welches Präfix sollen geändert werden: ");
                        string preCurrent = Console.ReadLine();
                        Console.Write("Wie soll das zukünftige Prefix heißen: ");
                        string preFut = Console.ReadLine();
                        Methods.ChangePrefix(path, preCurrent, preFut);
                        break;
                    case "cngsuf":
                        Console.Write("Welches Suffix sollen geändert werden: ");
                        string sufCurrent = Console.ReadLine();
                        Console.Write("Wie soll das zukünftige Suffix heißen: ");
                        string sufFut = Console.ReadLine();
                        Methods.ChangeSuffix(path, sufCurrent, sufFut);
                        break; 
                    case "delpre":
                        Console.WriteLine("Welcher Präfix soll entfernt werden?");
                        string deletablePrefix = Console.ReadLine();
                        Methods.DeletePrefix(path, deletablePrefix);
                        break;
                    case "delsuf":
                        Console.WriteLine("Welcher Suffix soll entfernt werden?");
                        string deletableSuffix = Console.ReadLine();
                        Methods.DeleteSuffix(path, deletableSuffix);
                        break;
                    case "zero":
                        int amount = 0;
                        Console.WriteLine("Add or delete?");
                        string addOrDelete = Console.ReadLine().ToLower();
                        if (addOrDelete == "delete")
                        {
                            Console.Write("Wie viele Nullen sollen gelöscht werden:");
                            if (!int.TryParse(Console.ReadLine(), out int output)) 
                            {
                                Console.WriteLine("Keine zulässige Menge.");
                                amount = output;
                            }
                        }
                        Methods.zeroFunction(path, addOrDelete, amount);
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
                    case "count":
                        Methods.Count(path);
                        break;
                    case "undo":
                        Methods.Undo();
                        break;
                    case "exit":
                    case "stop":
                        go = false; 
                        break;
                    case "path":
                        Console.Write("Bitte Pfad zum Ordner eingeben, welcher bearbeitet werden soll: ");
                        //path = Console.ReadLine();
                        Environment.CurrentDirectory = Console.ReadLine().Trim('"');
                        path = "";
                        Console.Title = Environment.CurrentDirectory;
                        break;
                    default:
                        Console.WriteLine("Unknown Command. Write 'commands' for more information.");
                        break;
                }
            } 
        }
    }
}