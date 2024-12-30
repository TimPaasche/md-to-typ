using System;

namespace MarkdownTypstBridge.MarkdownObjects;

public class HorizontalLine : MarkdownObject
{
    public HorizontalLine() { }

    public new string Serialize()
    {
        return $"---{Environment.NewLine}";
    }
}
