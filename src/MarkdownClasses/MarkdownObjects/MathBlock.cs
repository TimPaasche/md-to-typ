using System;

namespace MarkdownClasses.MarkdownObjects;

public class MathBlock : MarkdownObject
{
    public string Content { get; private set; }

    public MathBlock(string line)
    {
        Content = line.Trim("\n\r$".ToCharArray());
    }

    public override string ToString()
    {
        return Content;
    }

    public override string Serialize()
    {
        return $"$${Environment.NewLine}{Content}{Environment.NewLine}$${Environment.NewLine}";
    }
}
