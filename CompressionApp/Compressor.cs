using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace PDFCompress
{
    class Compressor
    {
        public static int Compress(string outputPath, string inputPath)
        {
            int exitCode = 1;

            string gsArgs2 = $"-q -dNOPAUSE -dBATCH -dSAFER -sDEVICE=pdfwrite " +
                $"-dAutoRotatePages=/All -sPAPERSIZE=letter -dPDFFitPage " +
                $"-dPDFSETTINGS=/screen -dEmbedAllFonts=true -dSubsetFonts=true " +
                $"-dColorImageDownsampleType=/Bicubic -dColorImageResolution=144 " +
                $"-dGrayImageDownsampleType=/Bicubic -dGrayImageResolution=144 " +
                $"-dMonoImageDownsampleType=/Bicubic -dMonoImageResolution=144 " +
                $"-sOutputFile={outputPath} {inputPath}";

            try
            {
                var proc = new Process();
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.FileName = @"C:\Program Files\gs\gs10.03.0\bin\gswin64c.exe";
                proc.StartInfo.Arguments = gsArgs2;

                proc.Start();
                
                string errorMessage = proc.StandardError.ReadToEnd();
                string outputMessage = proc.StandardOutput.ReadToEnd();
                
                proc.WaitForExit();

                if (proc.ExitCode == 1)
                {
                    Console.WriteLine("Error Message:");
                    Console.WriteLine(errorMessage);
                }
                if (!string.IsNullOrEmpty(outputMessage))
                {
                    Console.WriteLine("Output Message:");
                    Console.WriteLine(outputMessage);
                }
                
                exitCode = proc.ExitCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception running shell command: " + ex.Message);
            }

            return exitCode;
        }

        //public string Compress(string fileBytes, string fileName)
        //{
        //    var fileDate = DateTime.Now;
        //    // JS compression
        //    var debugPort = 9222;
        //    var flags = V8ScriptEngineFlags.EnableDebugging | 
        //        V8ScriptEngineFlags.AwaitDebuggerAndPauseOnStart |
        //        V8ScriptEngineFlags.EnableRemoteDebugging;
        //    var v8Engine = new V8ScriptEngine(flags, debugPort);
        //    v8Engine.DocumentSettings.SearchPath = @"..\..\..\js\";
        //    v8Engine.DocumentSettings.AccessFlags = DocumentAccessFlags.EnableFileLoading;
        //    //var builder = new StringBuilder();
        //    //string[] jsPaths =
        //    //{
        //    //    @"..\..\..\js\PDFManager.js",
        //    //    @"..\..\..\js\blob-stream-0.1.3.js",
        //    //    @"..\..\..\js\FileSaver.min-2.0.4.js",
        //    //    @"..\..\..\js\FileSaver.min.js.map",
        //    //    @"..\..\..\js\pdf.min-2.5.207.js",
        //    //    @"..\..\..\js\pdf.worker.min-2.5.207.js",
        //    //    @"..\..\..\js\pdfkit-standalone-0.10.0.js",
        //    //    @"..\..\..\js\sortable.min.1.10.2.js"
        //    //};
        //    //foreach (var path in jsPaths)
        //    //{
        //    //    builder.Append(File.ReadAllText(path));
        //    //}

        //    try
        //    {
        //        var jsFile = File.ReadAllText(@"..\..\..\js\PDFManager.js");
        //        //v8Engine.Execute(builder.ToString());
        //        v8Engine.Execute(jsFile);

        //        fileBytes = v8Engine.Script.compress(fileBytes, fileName, fileDate);
        //    }
        //    catch (ScriptEngineException ex)
        //    {
        //        Console.WriteLine("ScriptEngineException: " + ex.Message);
        //    }

        //    return fileBytes;
        //}
    }
}

/**
string gsArgs1 = $"-q -dNOPAUSE -dBATCH -dSAFER -dOverprint=simulate -sDEVICE=pdfwrite " +
                $"-dPDFSETTINGS=/ebook -dEmbedAllFonts=true -dSubsetFonts=true -dAutoRotatePages=/None " +
                $"-dColorImageDownsampleType=/Bicubic -dColorImageResolution=150 " +
                $"-dGrayImageDownsampleType=/Bicubic -dGrayImageResolution=150 " +
                $"-dMonoImageDownsampleType=/Bicubic -dMonoImageResolution=150 " +
                $"-sOutputFile={outputPath} {inputPath}";

string gsArgs3 = $"-sDEVICE=pdfwrite -dCompatibilityLevel=1.4 -dPDFSETTINGS=/default " +
                $"-dNOPAUSE -dQUIET -dBATCH -dDetectDuplicateImages -dCompressFonts=true -r150 " +
                $"-sOutputFile={outputPath} {inputPath}";
 */