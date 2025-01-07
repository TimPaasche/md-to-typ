using System.Diagnostics;
using MdToTyp;

namespace CLI;

using MarkdownClasses;

internal class Program
{
    static void Main(string[] args)
    {
        string tex = "\\frac{1}{4} \\sum_{i=1}^4 \\mathbf{P}_i^\\top \\sqrt{v} \\mathbf{\\Sigma}^{-1} \\sqrt{v} \\mathbf{P}_i \\mathbf{j} = \\mathbf{D}^\\top v \\phi";
        var test = MdToTyp.Converter.ConvertTexToTypst(tex);
        Console.WriteLine(test);
        return;

#if DEBUG
        args = new string[]
        {
            @"C:\Users\TLP-PC\Desktop\TEST\summory.md",
            "-s",
            @"C:\Users\TLP-PC\Desktop\TEST\template.typ",
            "-t",
            "Zusammenfasssung-Lukas",
            "--pdf"
        };
        #endif
        
        string input = null;
        string output = null;
        string styleTemplate = null;
        string title = null;
        bool toPdf = false;
        bool addToc = false;
        
        // Check if markdown file is provided
        if(args.Length == 0)
        {
            Console.WriteLine("Please provide a path to a markdown file");
            return;
        }
        
        input = args[0];
        
        // Check if markdown file exists
        if(!File.Exists(input))
        {
            Console.WriteLine("markdown file does not exist");
            return;
        }
        
        // Check if output directory is provided
        if (GetOutputFlag()) return;

        // Check if style template is provided
        if (GetStyleFlag()) return;
        
        // Check if title is provided
        if (GetTitleFlag()) return;
        
        // Check if pdf flag is provided
        toPdf = GetPdfFlag();
        
        Markdown md = MarkdownExtensions.Deserialize(input);
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
        return;
        
        #region Local Functions
        
        bool GetOutputFlag()
        {
            if(args.Any(arg => arg == "-o" || arg == "--output"))
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
                if( Directory.Exists(output) == false)
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

        bool GetStyleFlag()
        {
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

        bool GetTitleFlag()
        {
            if(args.Any(arg => arg == "-t" ||arg == "--title"))
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
                if(title.EndsWith(".typ"))
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

        bool GetPdfFlag()
        {
            if(args.Any(arg => arg == "-p" || arg == "--pdf"))
            {
                return true;
            }
            return false;
        }
        
        #endregion Local Functions
    }
    
    private static void ToPdf(string typst, string file)
    {
        string command = "typst compile " + file;
        
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = $"cmd.exe", // Use "bash" for Linux/Mac
                Arguments = $"/c {command}", // /c tells cmd to execute the command and exit
                RedirectStandardOutput = true, // Redirect the output
                RedirectStandardError = true,  // Redirect the error output
                UseShellExecute = false,      // Required for redirection
                CreateNoWindow = true         // Don't create a terminal window
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
