﻿using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace SpotlightImageExtractor
{
    class Program
    { 
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("Your OS is not Windows!");
                Console.ReadKey();
                Process.GetCurrentProcess().Kill();
            }
            string appData = Environment.GetEnvironmentVariable("LocalAppData");
            string spotlightPath = $@"{appData}\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets";
            string[] files = Directory.GetFiles(spotlightPath);
            int i = 1;
            foreach (string file in files)
            {
                string type = "";
                try
                {
                    using(var image = FreeImageAPI.FreeImageBitmap.FromFile(file))
                    {
                        if (image.Height == 1920 & image.Width == 1080)
                            type = "Mobile-";
                        else if (image.Height == 1080 & image.Width == 1920)
                            type = "Desktop-";
                        else
                            throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine($"It's not Image: {i}/{files.Length}");
                    i++;
                    continue;
                }

                string imageDir = Directory.GetCurrentDirectory() + @"\Images\";
                if (!Directory.Exists(imageDir))
                    Directory.CreateDirectory(imageDir);
                FileInfo fileInfo = new FileInfo(file);
                string newPath = $"{imageDir}{type}{fileInfo.Name.Substring(0,5)}.jpg";
                if (File.Exists(newPath))
                    File.Delete(newPath);
                fileInfo.CopyTo(newPath);
                Console.WriteLine($"Copied: {i}/{files.Length}");
                i++;
            }
            Console.Beep();
            Console.ReadKey();
        }
    }
}