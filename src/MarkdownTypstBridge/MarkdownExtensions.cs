using System;
using System.IO;

namespace MarkdownTypstBridge;

public static class MarkdownExtensions
{
    public static string ToTypst(this Markdown markdown)
    {
        throw new NotImplementedException();
    }

    public static Markdown Deserialize(string path, string title = null)
    {
        if (string.IsNullOrEmpty(title))
        {
            title = Path.GetFileNameWithoutExtension(path);
        }

        string[] lines = File.ReadAllLines(path);
        return new Markdown(title, lines);
    }
}