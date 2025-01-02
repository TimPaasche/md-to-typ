using System;

namespace MarkdownTypstBridge.MarkdownObjects;

public class HorizontalLine : MarkdownObject
{
    public HorizontalLine() { }

    public override string Serialize()
    {
        return $"---{Environment.NewLine}";
    }
}
