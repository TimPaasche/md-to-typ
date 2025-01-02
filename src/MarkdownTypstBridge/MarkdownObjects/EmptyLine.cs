using System;

namespace MarkdownTypstBridge.MarkdownObjects;

public class EmptyLine : MarkdownObject
{
    public EmptyLine() { }

    public override string Serialize()
    {
        return $"{Environment.NewLine}";
    }
}
