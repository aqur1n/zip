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
            foreach (string File in Files)
            {
                System.IO.File.Copy(File, Temp + "/" + Path.GetFileName(File));
            }

            if (File.Exists(Directory.GetCurrentDirectory() + "/zip.zip"))
            {
                File.Delete(Directory.GetCurrentDirectory() + "/zip.zip");            }
            ZipFile.CreateFromDirectory(Temp, Directory.GetCurrentDirectory() + "/zip.zip");
            Directory.Delete(Temp, true);
        }
    }
}