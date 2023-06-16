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
        public static void commands()
        {
            string[] commands = { "", "cngpre - Change prefix",
            "cngsuf - Change suffix",
            "delpre - Deletze prefix",
            "delsuf - Delete suffix",
            "commands - Displays a list of all commands",
            "stop - Stops the program"
        };
            Console.WriteLine("List of all commands");
            foreach (string command in commands)
            {
                Console.WriteLine(command);
            }
            Console.WriteLine();
        }
        public static void RenameImages(string directoryPath, string prefix)
        {
            try
            {
                string[] imageFiles = Directory.GetFiles(directoryPath, "*.jpg");
                
                for (int i = 0; i < imageFiles.Length; i++)
                {
                    string name = Path.GetFileName(imageFiles[i]);
                    string oldFilePath = imageFiles[i];
                    string newFileName = prefix + (i + 1) + ".jpg";
                    string newFilePath = Path.Combine(directoryPath, newFileName);

                    File.Move(oldFilePath, newFilePath);
                }
                Console.WriteLine("Prefix änderung erfolgreich durchgeführt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Umbenennen der Bilder: " + ex.Message);
            }
        }
        public static void ChangeSuffix(string directoryPath, string oldSuffix, string newSuffix)
        {
            try
            {
                string pattern = ".txt";
                string[] imageFiles = Directory.GetFiles(directoryPath, $"*{oldSuffix}");

                for (int i = 0; i < imageFiles.Length; i++)
                {
                    string fileName = Path.GetFileName(imageFiles[i]);
                    string oldFilePath = imageFiles[i];
                    fileName = fileName.Replace(oldSuffix, newSuffix);
                    string newFilePath = Path.Combine(directoryPath, fileName);

                    File.Move(oldFilePath, newFilePath);
                }
                Console.WriteLine("Suffix änderung erfolgreich durchgeführt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Umbenennen der Bilder: " + ex.Message);
            }
        }
        public static void ChangePrefix(string directoryPath, string oldPrefix, string newPrefix)
        {
            try
            {
                string[] imageFiles = Directory.GetFiles(directoryPath);

                for (int i = 0; i < imageFiles.Length; i++)
                {
                    string fileName = Path.GetFileName(imageFiles[i]);
                    string oldFilePath = imageFiles[i];
                    fileName = fileName.Replace(oldPrefix, newPrefix);
                    string newFilePath = Path.Combine(directoryPath, fileName);

                    File.Move(oldFilePath, newFilePath);
                }
                Console.WriteLine("Prefix änderung erfolgreich durchgeführt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Umbenennen der Bilder: " + ex.Message);
            }
        }
        public static void zeroFunction(string directoryPath, string addOrDelete)
        {
            try
            {
                string[] imageFiles = Directory.GetFiles(directoryPath);
                Console.WriteLine(imageFiles);
                for (int i = 0; i < imageFiles.Length; i++)
                {
                    string oldFilePath = imageFiles[i];
                    string fileName = Path.GetFileName(imageFiles[i]);
                    if (addOrDelete == "add" | addOrDelete == "Add")
                    {
                        fileName = $"0{fileName}";
                    }
                    else
                    {
                        if (fileName.StartsWith("0"))
                        {
                            fileName = fileName.Substring(1);
                        }
                    }
                    string newFilePath = Path.Combine(directoryPath, fileName);
                    File.Move(oldFilePath, newFilePath);
                }
                Console.WriteLine("Nullen erfolgreich hinzugefügt/entfernt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Umbenennen der Bilder: " + ex.Message);
            }
        }
        public static void leadZeros (string directoryPath, int length)
        {
            try
            {
                string[] imageFiles = Directory.GetFiles(directoryPath);

                for (int i = 0; i < imageFiles.Length; i++)
                {
                    string oldFilePath = imageFiles[i];
                    string fileName = Path.GetFileName(imageFiles[i]);

                    string numString =  getNumberFromString(fileName, out int firstNum, out int lastNum);
                    string newNumString = numString.PadLeft(length, '0');
                    //fileName = fileName.Replace(numString, newNumString);
                    fileName = fileName.Remove(firstNum, lastNum - firstNum);
                    fileName = fileName.Insert(firstNum, newNumString);

                    string newFilePath = Path.Combine(directoryPath, fileName);
                    File.Move(oldFilePath, newFilePath);
                }
                Console.WriteLine("Führende Nullen erfolgreich hinzugefügt/entfernt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Umbenennen der Bilder: " + ex.Message);
            }
        }
        public static string getNumberFromString(string numberString, out int firstNum, out int lastNum)
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
    }
}

