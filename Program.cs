using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Compression;
using System.IO;


namespace zip
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string Temp = Path.GetTempPath() + @"/aqur1n-zip";
            string[] Files;
            
            if (args.Length == 1 && Path.GetFileName(args[0]) == "zip.txt")
            {
                Files = File.ReadAllLines(args[0]).ToArray();
            }
            else
            {
                Files = args;
            }
            
            if (Directory.Exists(Temp))
            {
                Directory.Delete(Temp, true);
            }

            Directory.CreateDirectory(Temp);

            string NameZip = "ZepZip";
            foreach (string File in Files)
            {
                if (System.IO.File.Exists(File))
                {
                    System.IO.File.Copy(File, Path.Combine(Temp, Path.GetFileName(File)));

                    NameZip = Path.GetFileName(File);
                } else if (Directory.Exists(File.Split(" => ")[0]))
                {
                    string[] _File = File.Split(" => ");

                    Directory.CreateDirectory(Path.Combine(Temp, _File[1]));
                    DirectoryCopy(_File[0], Path.Combine(Temp, _File[1]));
                }
            }

            if (File.Exists(Directory.GetCurrentDirectory() + "/" + NameZip + ".zip"))
            {
                File.Delete(Directory.GetCurrentDirectory() + "/" + NameZip + ".zip");            
            }
            ZipFile.CreateFromDirectory(Temp, Directory.GetCurrentDirectory() + "/" + NameZip + ".zip");
            Directory.Delete(Temp, true);

            Environment.Exit(0);
        }

        private static void DirectoryCopy(string SourcePath, string DestinationPath)
        {
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));
            }
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);
            }
        }
    }
}