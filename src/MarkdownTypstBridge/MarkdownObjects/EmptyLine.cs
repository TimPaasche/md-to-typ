using System;

namespace MarkdownTypstBridge.MarkdownObjects;

public class EmptyLine : MarkdownObject
{
    public EmptyLine()
    {
    }

    public new string Serialize()
    {
        return $"{Environment.NewLine}";
    }
}
