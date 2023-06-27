using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renamer
{
    public class Methods
    {

        static List<(string oldP, string newP)> history = new List<(string oldP, string newP)>(); 
        public static void commands()
        {
            string[] commands = { "", "cngpre - Change prefix",
            "cngsuf - Change suffix",
            "delpre - Delete prefix",
            "delsuf - Delete suffix",
            "commands - Displays a list of all commands",
            "ledzer - Add leading Zeros",
            "undo - Undo last operation",
            "stop - Stops the program"
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
                string[] allPaths = Directory.GetFiles(directoryPath, $"*.{oldSuffix}");

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
                string[] allPaths = Directory.GetFiles(directoryPath);

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
                //Console.WriteLine(allPaths);
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
                        //if (fileName.StartsWith("0"))
                        //{
                        //    fileName = fileName.Substring(1);
                        //}
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
                string[] allPaths = Directory.GetFiles(directoryPath);

                for (int i = 0; i < allPaths.Length; i++)
                {
                    string oldFilePath = allPaths[i];
                    string fileName = Path.GetFileName(allPaths[i]);
                    

                    string numString =  GetNumberFromString(fileName, out int firstNum, out int lastNum);
                    string newNumString = numString.PadLeft(length, '0');
                    //fileName = fileName.Replace(numString, newNumString);
                    fileName = fileName.Remove(firstNum, lastNum - firstNum);
                    fileName = fileName.Insert(firstNum, newNumString);

                    string newFilePath = Path.Combine(directoryPath, fileName);
                    File.Move(oldFilePath, newFilePath);
                    history.Add((oldFilePath, newFilePath));
                }
                Console.WriteLine("Führende Nullen erfolgreich hinzugefügt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Umbenennen der Dateien: " + ex.Message);
            }
        }
        public static void Enumerate(string directoryPath)
        {
            history.Clear();
        }
        public static void MoveNumbers(string directoryPath, string mode) 
        {
            history.Clear();
            try
            {
                string[] allPaths = Directory.GetFiles(directoryPath);

                for (int i = 0; i < allPaths.Length; i++)
                {
                    string oldFilePath = allPaths[i];
                    string fileName = Path.GetFileName(allPaths[i]);
                    fileName = fileName.Replace("-", "");
                    string numString = GetNumberFromString(fileName, out int firstNum, out int lastNum);
                    //fileName = fileName.Replace(numString, newNumString);
                    string text = "vorne";

                    fileName = fileName.Remove(firstNum, lastNum - firstNum);
                    //fileName = fileName.Trim('-');
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
                    Console.WriteLine($"Der Zahlenblock wurde erfolgreich nach {text} geschoben.");
                    history.Add((oldFilePath, newFilePath));
                }
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

            //string test = getNumberFromString(fileName, out int firstNum, out int lastNum);
        }

        public static void DeletePrefix(string directoryPath, string prefixToDelete)
        {
            try
            {
                string[] allPaths = Directory.GetFiles(directoryPath);

                foreach (string oldFilePath in allPaths)
                {
                    string fileName = Path.GetFileName(oldFilePath);
                    string newFileName = fileName.Remove(0, prefixToDelete.Length);
                    string newFilePath = Path.Combine(directoryPath, newFileName);

                    File.Move(oldFilePath, newFilePath);
                }

                Console.WriteLine("Präfix wurde erfolgreich entfernt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Umbenennen der Dateien:  " + ex.Message);
            }
        }
    }
}

