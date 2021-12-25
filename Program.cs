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
                string[] _File = File.Split(" => ");

                if (File[0] == '#')
                {
                    continue;
                }
                else if (System.IO.File.Exists(_File[0]))
                {
                    if (_File[1] != null)
                    {
                        if (!Directory.Exists(Path.Combine(Temp, Path.GetDirectoryName(_File[1]))))
                        {
                            Directory.CreateDirectory(Path.Combine(Temp, Path.GetDirectoryName(_File[1])));
                        }
                        if (Path.GetFileName(_File[1]) != null)
                        {
                            System.IO.File.Copy(_File[0], Path.Combine(Path.Combine(Temp, Path.GetDirectoryName(_File[1])), Path.GetFileName(_File[1])), true);
                        }
                        else
                        {
                            System.IO.File.Copy(_File[0], Path.Combine(Path.Combine(Temp, Path.GetDirectoryName(_File[1])), Path.GetFileName(_File[0])), true);
                        }
                    }
                    else
                    {
                        System.IO.File.Copy(_File[0], Path.Combine(Temp, Path.GetFileName(_File[0])), true);
                    }

                    NameZip = Path.GetFileName(_File[0]);
                } 
                else if (Directory.Exists(_File[0]))
                {
                    try
                    {
                        Directory.CreateDirectory(Path.Combine(Temp, _File[1]));
                    }
                    catch { }
                    DirectoryCopy(_File[0], Path.Combine(Temp, _File[1]));
                }
                Console.WriteLine(File);
            }

            if (File.Exists(Directory.GetCurrentDirectory() + "/" + NameZip + ".zip"))
            {
                File.Delete(Directory.GetCurrentDirectory() + "/" + NameZip + ".zip");            
            }
            ZipFile.CreateFromDirectory(Temp, Directory.GetCurrentDirectory() + "/" + NameZip + ".zip");
            Directory.Delete(Temp, true);

            Console.ReadKey();
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