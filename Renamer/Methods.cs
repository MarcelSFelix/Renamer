using System;
using System.Collections.Generic;
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
    }
}

