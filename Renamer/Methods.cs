using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renamer
{
    public class Methods
    {
        public static void RenameImages(string directoryPath, string prefix)
        {
            try
            {
                string[] imageFiles = Directory.GetFiles(directoryPath, "*.jpg");

                for (int i = 0; i < imageFiles.Length; i++)
                {
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
                string[] imageFiles = Directory.GetFiles(directoryPath, $"*{pattern}");

                for (int i = 0; i < imageFiles.Length; i++)
                {
                    string oldFilePath = imageFiles[i];
                    string newFilePath = imageFiles[i];
                    newFilePath = newFilePath.Replace(oldSuffix, newSuffix);
                    //string newFileName = prefix + (i + 1) + ".jpg";
                    //string newFilePath = Path.Combine(directoryPath, newFileName);

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
                    string oldFilePath = imageFiles[i];
                    string newFilePath = imageFiles[i];
                    newFilePath = newFilePath.Replace(oldPrefix, newPrefix);
                    //string newFileName = prefix + (i + 1) + ".jpg";
                    //string newFilePath = Path.Combine(directoryPath, newFileName);

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

