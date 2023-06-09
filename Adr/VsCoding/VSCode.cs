using System.Diagnostics;

namespace Adr.VsCoding;

public static class VSCode
{
    public static bool IsVSCodeInstalled()
    {
        try
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo("code")
                {
                    Arguments = "--version",
                    UseShellExecute = true,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            process.WaitForExit();
            return true;
        }
        catch (Exception ex)
        {
            Cout.Fail(ex.Message);
        }

        return false;
    }

    public static void OpenFile(string filePath)
    {
        Cout.Success("Opening '{FilePath}' in VS Code", filePath);
        var startInfo = new ProcessStartInfo("code", filePath)
        {
            UseShellExecute = true,
            CreateNoWindow = true
        };

        Process.Start(startInfo);
    }

    public static void OpenFolder(string docsFolder)
    {
        Cout.Info("Opening folder '[yellow]{_adrFolder}[/]' in VS Code", docsFolder);
        var startInfo = new ProcessStartInfo("code", docsFolder)
        {
            UseShellExecute = true,
            CreateNoWindow = true
        };

        Process.Start(startInfo);
    }
}
