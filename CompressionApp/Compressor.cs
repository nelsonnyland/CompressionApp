using System;
using System.Diagnostics;

namespace PDFCompress
{
    class Compressor
    {   
        public bool Compress(string path)
        {
            var oldName = Path.GetFileNameWithoutExtension(path);
            var newName = oldName + "_compressed";

            path = path.Replace(":", "");
            path = path.Replace("\\", "/");
            var drive = path.Substring(0, 1).ToLower();
            path = "/mnt/" + drive + path.Substring(1);
            //var directory = path.Substring(0, path.IndexOf(oldName));

            var input = path;
            var output = input.Replace(oldName, newName);

            var command = $"gs -sDEVICE=pdfwrite -dCompatibilityLevel=1.4 -dPDFSETTINGS=/printer -dNOPAUSE -dQUIET -dBATCH -sOutputFile='{ output }' '{ input }'";

            var proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.FileName = "bash";
            proc.StartInfo.Arguments = $"-c \"{command}\"";

            try
            {
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception running shell command: " + ex.Message);
            }

            return !Convert.ToBoolean(proc.ExitCode);
        }

    }
}