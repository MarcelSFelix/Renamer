using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renamer
{
    public class Methods
    {
        static void RenameImages(string directoryPath, string prefix)
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Umbenennen der Bilder: " + ex.Message);
            }
        }
    }
}

