namespace CLI;

using MarkdownTypstBridge;
using MarkdownTypstBridge.MarkdownObjects;

internal class Program
{
    static void Main(string[] args)
    {
        Markdown md = MarkdownExtensions.Deserialize("/Users/timpaasche/Desktop/Repository/md-to-typ/src/Test-Data/md/test.md");
    }
}
