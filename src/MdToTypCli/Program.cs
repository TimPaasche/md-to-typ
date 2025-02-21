using MdToTyp;
using System.Diagnostics;

namespace MdToTypCli;

using MarkdownClasses;

internal class Program
{
    static void Main(string[] args)
    {

#if DEBUG
        args = new string[]
        {
            //@"C:\Users\DEPAATIM\Desktop\Test\Tex-to-Typst\Test-File.md",
            // @"P:\Masterthesis\Materialien-Liste\Bestellung.md",
            @"D:\Repository\md-to-typ\src\Test-Data\md\test.md",
            "-o",
            "D:\\Repository\\md-to-typ\\src\\Test-Data\\typst",
            "-i"
        };
#endif

        // Check if markdown file is provided
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a path to a markdown file");
            return;
        }

        string input = args[0];

        // Check if markdown file exists
        if (!File.Exists(input))
        {
            Console.WriteLine("markdown file does not exist");
            return;
        }

        // Check if output directory is provided
        if (GetOutputFlag(args, input, out string output)) return;

        // Check if style template is provided
        if (GetStyleFlag(args, out string styleTemplate)) return;

        // Check if title is provided
        if (GetTitleFlag(args, input, out string title)) return;

        // Check if pdf flag is provided
        bool toPdf = GetPdfFlag(args);

        // Check if cache image flag is provided
        bool cacheImage = GetCachImageFlag(args);

        Markdown md = MarkdownExtensions.Deserialize(input);
        if (cacheImage)
        {
            md.CacheImagesAsync(output).Wait();
        }

        string typst = string.IsNullOrEmpty(styleTemplate)
            ? md.ToTypst()
            : md.ToTypst(styleTemplate);

        string file = Path.Combine(output, (title + ".typ"));

        if (File.Exists(file))
        {
            File.Delete(file);
        }

        File.WriteAllText(file, typst);

        if (toPdf == false)
        {
            return;
        }

        ToPdf(typst, file);
    }

    private static bool GetOutputFlag(string[] args, string input, out string output)
    {
        output = null;
        if (args.Any(arg => arg == "-o" || arg == "--output"))
        {
            string flag = args.First(arg => arg == "-o" || arg == "--output");
            int index = Array.IndexOf(args, flag) + 1;
            if (index < args.Length && !args[index].StartsWith('-'))
            {
                output = args[index];
            }
            else
            {
                Console.WriteLine("Please provide a path to an output directory, like -o <OUTPUT> or --output <OUTPUT>");
                return true;
            }
            if (Directory.Exists(output) == false)
            {
                Directory.CreateDirectory(output);
            }
        }
        else
        {
            output = Path.GetDirectoryName(input);
        }

        return false;
    }

    private static bool GetStyleFlag(string[] args, out string styleTemplate)
    {
        styleTemplate = null;
        if (args.Any(arg => arg == "-s" || arg == "--style"))
        {
            string flag = args.First(arg => arg == "-s" || arg == "--style");
            int index = Array.IndexOf(args, flag) + 1;

            if (index < args.Length && !args[index].StartsWith('-') && File.Exists(args[index]))
            {
                styleTemplate = args[index];
            }
            else
            {
                Console.WriteLine("Please provide a path to a style file, like -s <STYLE-TEMPLATE> or --style <STYLE-TEMPLATE>");
                return true;
            }
        }

        return false;
    }

    private static bool GetTitleFlag(string[] args, string input, out string title)
    {
        title = null;
        if (args.Any(arg => arg == "-t" || arg == "--title"))
        {
            string flag = args.First(arg => arg == "-t" || arg == "--title");
            int index = Array.IndexOf(args, flag) + 1;

            if (index < args.Length && !args[index].StartsWith('-'))
            {
                title = args[index];
            }
            else
            {
                Console.WriteLine("Please provide a title, like -t <TITLE> or --title <TITLE>");
                return true;
            }
            if (title.EndsWith(".typ"))
            {
                title = title.Substring(0, title.Length - 4);
            }
        }
        else
        {
            title = Path.GetFileNameWithoutExtension(input);
        }

        return false;
    }

    private static bool GetPdfFlag(string[] args)
    {
        if (args.Any(arg => arg == "-p" || arg == "--pdf"))
        {
            return true;
        }
        return false;
    }

    private static bool GetCachImageFlag(string[] args)
    {
        if (args.Any(arg => arg == "-i" || arg == "--cache-image"))
        {
            return true;
        }
        return false;

    }

    private static void ToPdf(string typst, string file)
    {
        string command = $"typst compile \"{file}\"";

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = $"cmd.exe",           // Use "bash" for Linux/Mac
                Arguments = $"/c {command}",     // /c tells cmd to execute the command and exit
                RedirectStandardOutput = true,   // Redirect the output
                RedirectStandardError = true,    // Redirect the error output
                UseShellExecute = false,         // Required for redirection
                CreateNoWindow = true            // Don't create a terminal window
            }
        };

        // Start the process
        process.Start();

        // Read the output
        string result = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit(); // Wait for the command to complete

        // Output results
        Console.WriteLine(result);

        if (!string.IsNullOrEmpty(error))
        {
            Console.WriteLine(error);
        }

        if (File.Exists(file))
        {
            File.Delete(file);
        }
    }
}
