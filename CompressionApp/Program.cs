using HeyRed.Mime;
using Humanizer.Bytes;
using System.IO;
using System.Text;

namespace PDFCompress
{
    class Program
    {
        static void Main(string[] args)
        {
            // see command line arguments in debug properties:
            try
            {
                Console.WriteLine("/////////////////////// PDF Compressor //////////////////////////\n");
                Console.WriteLine("This utility requires you to have Ghostscript installed and added\n" +
                                  "to the system environmental path. Spaces in path may cause errors.\n");

                string outputPath;
                string inputPath;
                if (args.Length == 2)
                {
                    outputPath = args[0];
                    inputPath = args[1];
                }
                else
                {
                    Console.Write("Please give the output path: ");
                    outputPath = Console.ReadLine();
                    Console.Write("Please give the input path: ");
                    inputPath = Console.ReadLine();
                }

                if (!File.Exists(inputPath))
                {
                    Console.WriteLine($"Input path does not exist: {inputPath}");
                    Environment.Exit(1);
                }

                int exitCode = Compressor.Compress(outputPath, inputPath);

                if (exitCode == 0)
                {
                    Console.WriteLine("Compression successful.");
                }
                else
                {
                    Console.WriteLine("Compression failed.");
                }

                Environment.Exit(exitCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Compression failed. Exception message: " + ex.Message);
                Environment.Exit(1);
            }
        }
    }
}
