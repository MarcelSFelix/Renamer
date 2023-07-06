using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Renamer
{
    public class Methods
    {

        static List<(string oldP, string newP)> history = new List<(string oldP, string newP)>(); 
        public static void commands()
        {
            string[] commands = { "", "cngpre - Prefix ändern",
            "cngsuf - Suffix ändern",
            "delpre - Prefix löschen",
            "delsuf - Suffix löschen",
            "ledzer - Führende Nullen hinzufügen",
            "movnum - Den ersten Zahlenblock entweder an den Anfang oder das Ende des Dateinamens verschieben",
            "count - Alle Dateien im Ordner durchnummerieren",
            "undo - Die letzte Aktion rückgängig machen",
            "commands - Alle Befehle anzeigen",
            "stop/exit - Das Programm beenden",
            "path - Einen neuen Pfad zu einem Order angeben"
        };
            Console.WriteLine("List of all commands");
            foreach (string command in commands)
            {
                Console.WriteLine(command);
            }
            Console.WriteLine();
        }
        
        public static void ChangeSuffix(string directoryPath, string oldSuffix, string newSuffix)
        {
            history.Clear();
            try
            {
                oldSuffix = oldSuffix.Replace(".", "");
                string[] allPaths = Directory.GetFiles(Environment.CurrentDirectory, $"*.{oldSuffix}");

                for (int i = 0; i < allPaths.Length; i++)
                {
                    string fileName = Path.GetFileName(allPaths[i]);
                    string oldFilePath = allPaths[i];
                    fileName = fileName.Replace(oldSuffix, newSuffix);
                    string newFilePath = Path.Combine(directoryPath, fileName);

                    File.Move(oldFilePath, newFilePath);
                    history.Add((oldFilePath, newFilePath));
                }
                Console.WriteLine("Suffix änderung erfolgreich durchgeführt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Umbenennen der Dateien: " + ex.Message);
            }
        }
        public static void ChangePrefix(string directoryPath, string oldPrefix, string newPrefix)
        {
            history.Clear();
            try
            {
                string[] allPaths = Directory.GetFiles(Environment.CurrentDirectory);

                for (int i = 0; i < allPaths.Length; i++)
                {
                    string fileName = Path.GetFileName(allPaths[i]);
                    string oldFilePath = allPaths[i];
                    fileName = fileName.Replace(oldPrefix, newPrefix);
                    string newFilePath = Path.Combine(directoryPath, fileName);

                    File.Move(oldFilePath, newFilePath);
                    history.Add((oldFilePath, newFilePath));
                }
                Console.WriteLine("Prefix änderung erfolgreich durchgeführt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Umbenennen der Dateien: " + ex.Message);
            }
        }
        public static void zeroFunction(string directoryPath, string addOrDelete, int amount)
        {
            history.Clear();
            try
            {
                string[] allPaths = Directory.GetFiles(directoryPath);
                for (int i = 0; i < allPaths.Length; i++)
                {
                    string oldFilePath = allPaths[i];
                    string fileName = Path.GetFileName(allPaths[i]);
                    if (addOrDelete == "add")
                    {
                        fileName = $"0{fileName}";
                    }
                    else
                    {
                        int iterator = 0;
                        for (int j = 0;i < fileName.Length;j++)
                        {
                            if (fileName[j] == '0')
                            {
                                fileName = fileName.Remove(j,1);
                                iterator++;   
                            }
                            if (iterator > 1 && fileName[j] != '0') break;
                            if (iterator > amount) break;
                        }
                    }
                    string newFilePath = Path.Combine(directoryPath, fileName);
                    File.Move(oldFilePath, newFilePath);
                    history.Add((oldFilePath, newFilePath));
                }
                Console.WriteLine("Nullen erfolgreich hinzugefügt/entfernt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Umbenennen der Dateien: " + ex.Message);
            }
        }
        public static void LeadZeros (string directoryPath, int length)
        {
            history.Clear();
            try
            {
                string[] allPaths = Directory.GetFiles(Environment.CurrentDirectory);

                for (int i = 0; i < allPaths.Length; i++)
                {
                    bool numCheck = false;
                    string oldFilePath = allPaths[i];
                    string fileName = Path.GetFileName(allPaths[i]);
                    foreach (char c in fileName)
                    {
                        if (char.IsDigit(c)) 
                        {
                            numCheck= true;
                            break;
                        }
                    }
                    if (numCheck)
                    {
                        string numString = GetNumberFromString(fileName, out int firstNum, out int lastNum);
                        string newNumString = numString.PadLeft(length, '0');
                        fileName = fileName.Remove(firstNum, lastNum - firstNum);
                        fileName = fileName.Insert(firstNum, newNumString);

                        string newFilePath = Path.Combine(directoryPath, fileName);
                        File.Move(oldFilePath, newFilePath);
                        history.Add((oldFilePath, newFilePath));
                    }
                }
                Console.WriteLine("Führende Nullen erfolgreich hinzugefügt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Umbenennen der Dateien: " + ex.Message);
            }
        }
        
        public static void MoveNumbers(string directoryPath, string mode) 
        {
            history.Clear();
            try
            {
                string text = "";
                string[] allPaths = Directory.GetFiles(Environment.CurrentDirectory);

                for (int i = 0; i < allPaths.Length; i++)
                {
                    string oldFilePath = allPaths[i];
                    string fileName = Path.GetFileName(allPaths[i]);
                    fileName = fileName.Replace("-", "");
                    string numString = GetNumberFromString(fileName, out int firstNum, out int lastNum);
                    text = "vorne";

                    fileName = fileName.Remove(firstNum, lastNum - firstNum);
                    if(mode == "first") fileName = fileName.Insert(0, $"{numString}-");

                    if (mode == "last") 
                    {
                        text = "hinten";
                        int position = 0;
                        for (int j = fileName.Length-1; j >= 0; j--)
                        {
                            if (fileName[j] == '.')
                            {
                                position = j;
                                break;
                            }  
                        }
                        fileName = fileName.Insert(position, $"-{numString}");
                    }
                    string newFilePath = Path.Combine(directoryPath, fileName);
                    File.Move(oldFilePath, newFilePath);
                    history.Add((oldFilePath, newFilePath));
                }
                Console.WriteLine($"Der Zahlenblock wurde erfolgreich nach {text} geschoben.");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Fehler beim Umbenennen der Dateien: " + ex.Message);
            }
        }
        public static void Undo()
        {
            try
            {
                foreach (var item in history)
                {
                    File.Move(item.newP, item.oldP);
                }
                history.Clear();
                Console.WriteLine("Dateinamen erfolgreich zurückgesetzt");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Umbenennen der Dateien: " + ex.Message);
            }
        }
        public static string GetNumberFromString(string numberString, out int firstNum, out int lastNum)
        {
            firstNum = 0;
            lastNum = 0;
            bool firstFound = true;

            for (int i = 0; i < numberString.Length; i++)
            {
                if(firstFound && char.IsDigit(numberString[i]))
                {
                    firstNum = i;
                    firstFound = false;
                }
                else if (!firstFound && !char.IsDigit(numberString[i]))
                {
                    lastNum = i;
                    break;
                }
            }
            return numberString.Substring(firstNum, lastNum-firstNum);
        }

        public static void DeletePrefix(string directoryPath, string prefixToDelete)
        {
            history.Clear();
            try
            {
                string[] allPaths = Directory.GetFiles(Environment.CurrentDirectory);

                foreach (string oldFilePath in allPaths)
                {
                    string fileName = Path.GetFileName(oldFilePath);
                    string newFileName = fileName.Remove(0, prefixToDelete.Length);
                    string newFilePath = Path.Combine(directoryPath, newFileName);

                    File.Move(oldFilePath, newFilePath);
                    history.Add((oldFilePath, newFilePath));
                }

                Console.WriteLine("Präfix wurde erfolgreich entfernt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Umbenennen der Dateien:  " + ex.Message);
            }
        }

        public static void DeleteSuffix(string directoryPath, string suffixToDelete)
        {
            history.Clear();
            try
            {
                string[] allPaths = Directory.GetFiles(Environment.CurrentDirectory);

                foreach (string oldFilePath in allPaths)
                {
                    string fileName = Path.GetFileName(oldFilePath);
                    int suffixIndex = fileName.IndexOf(suffixToDelete);
                    string newFileName = fileName.Remove(suffixIndex);
                    string newFilePath = Path.Combine(directoryPath, newFileName);

                    File.Move(oldFilePath, newFilePath);
                    history.Add((oldFilePath, newFilePath));
                }

                Console.WriteLine("Suffix wurde erfolgreich entfernt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Umbenennen der Dateien:  " + ex.Message);
            }
        }

        public static void Count(string directoryPath)
        {
            history.Clear();
            try
            {
                string[] allPaths = Directory.GetFiles(Environment.CurrentDirectory);
                int fileCount = allPaths.Length;


                int numDigits = fileCount >= 10 ? (int)Math.Log10(fileCount) + 1 : 1;

                for (int i = 0; i < fileCount; i++)
                {
                    string fileName = Path.GetFileNameWithoutExtension(allPaths[i]);
                    string extension = Path.GetExtension(allPaths[i]);
                    string oldFilePath = allPaths[i];


                    string prefix = Regex.Replace(fileName, @"\d", string.Empty);

                    int number = i + 1;


                    string paddedNumber = number.ToString().PadLeft(numDigits, '0');
                    string newFileName = prefix + paddedNumber + extension;
                    string newFilePath = Path.Combine(directoryPath, newFileName);

                    File.Move(oldFilePath, newFilePath);
                    history.Add((oldFilePath, newFilePath));
                }
                Console.WriteLine("Numbered prefix change successfully performed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error renaming files: " + ex.Message);
            }
        }
    }
}

