using System;
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
                try
                {
                    // Not avaiable for .NET Core. I will complete here
                    //Image img = Image.FromFile(file);
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
                if (File.Exists(imageDir + fileInfo.Name + ".jpg"))
                    File.Delete(imageDir + fileInfo.Name + ".jpg");
                fileInfo.CopyTo(imageDir + fileInfo.Name + ".jpg");
                Console.WriteLine($"Copied: {i}/{files.Length}");
                i++;
            }
            Console.Beep();
            Console.ReadKey();
        }
    }
}