using HeyRed.Mime;
using Humanizer.Bytes;
using System.IO;

namespace PDFCompress
{
    class Program
    {
        static void Main(string[] args)
        {
            Header();

            try
            {
                var path = string.Empty;

                if (args.Length == 0)
                {
                    while (string.IsNullOrEmpty(path))
                    {
                        Console.Write("Please provide a file path: ");
                        path = Console.ReadLine();
                    }
                }
                else
                {
                    path = args[0];
                }

                if (!File.Exists(path))
                {
                    Console.WriteLine("The given file path is invalid!");
                    Environment.Exit(1);
                }

                var fileName = Path.GetFileName(path);
                Console.WriteLine("Compressing PDF: " + fileName);

                FileInfo fileInformation = new FileInfo(path);
                string mimeType = MimeTypesMap.GetMimeType(path);
                ByteSize fileSize = ByteSize.FromBytes(fileInformation.Length);

                if (fileInformation.Extension == ".pdf" && mimeType == "application/pdf")
                {
                    var comp = new Compressor();
                    Console.WriteLine($"Compressing: {fileName}...");
                    bool isCompressed = comp.Compress(path);

                    if (isCompressed)
                    {
                        Console.WriteLine($"{fileName} is compressed!");
                    }
                    else
                    {
                        Console.WriteLine($"An error occurred while compressing {fileName}");
                        Environment.Exit(1);
                    }
                }
                else
                {
                    Console.WriteLine("Wrong file type!");
                    Environment.Exit(1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Path incomplete or corrupted.");
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }
        }

        static void Header()
        {
            Console.WriteLine("PDF Compressor");
            Console.WriteLine("This utility requires you to have Ghostscript and BASH installed.");
            Console.WriteLine("Build from source: dotnet build");
            Console.WriteLine("Usage: ./CompressionApp [path to PDF]");
        }
    }
}
