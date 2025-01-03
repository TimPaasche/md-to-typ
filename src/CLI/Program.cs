namespace CLI;

using MarkdownTypstBridge;

internal class Program
{
    static void Main(string[] args)
    {
        Markdown md = MarkdownExtensions.Deserialize("/Users/timpaasche/Desktop/Repository/md-to-typ/src/Test-Data/md/test.md");
        string consoleOut = md.Serialize();
        Console.WriteLine(consoleOut);
        File.WriteAllText("/Users/timpaasche/Desktop/Repository/md-to-typ/src/Test-Data/md/test-New.md", consoleOut);
    }
}
