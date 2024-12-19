using System;

namespace MarkdownTypstBridge;

internal class Markdown
{
    public string Content { get; }

    public Markdown(string content)
    {
        Content = content;
    }
}

internal static class MarkdownExtensions
{
    public static string ToHtml(this Markdown markdown)
    {
        throw new NotImplementedException();
    }

    public static string ToTypst(this Markdown markdown)
    {
        throw new NotImplementedException();
    }
}
