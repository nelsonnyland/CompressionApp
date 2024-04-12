using HeyRed.Mime;
using Humanizer.Bytes;
using System.IO;
using System.Text;

namespace PDFCompress
{
    /// <summary>
    /// This utility requires you to have Ghostscript v10.03.0 installed and added to the system 
    /// environmental path. See command line arguments in debug properties
    /// </summary>
    class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("/////////////////////// PDF Compressor //////////////////////////\n");

                string inputPath = @"E:\nnyland\Desktop\INPUT.pdf";
                string outputPath = @"E:\nnyland\Desktop\OUPUT.pdf";

                if (!File.Exists(inputPath))
                {
                    throw new FileNotFoundException($"Input path does not exist: {inputPath}");
                }

                string args = $"-q -dNOPAUSE -dBATCH -dSAFER -sDEVICE=pdfwrite " +
                    $"-dAutoRotatePages=/All -sPAPERSIZE=letter -dPDFFitPage " +
                    $"-dPDFSETTINGS=/screen -dEmbedAllFonts=true -dSubsetFonts=true " +
                    $"-dColorImageDownsampleType=/Bicubic -dColorImageResolution=144 " +
                    $"-dGrayImageDownsampleType=/Bicubic -dGrayImageResolution=144 " +
                    $"-dMonoImageDownsampleType=/Bicubic -dMonoImageResolution=144 " +
                    $"-sOutputFile=\"{outputPath}\" \"{inputPath}\"";

                // compress
                int exitCode = Compressor.Compress(args);

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
