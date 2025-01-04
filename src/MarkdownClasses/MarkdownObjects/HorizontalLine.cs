using System;

namespace MarkdownClasses.MarkdownObjects;

public class HorizontalLine : MarkdownObject
{
    public HorizontalLine() { }

    public override string Serialize()
    {
        return $"---{Environment.NewLine}";
    }
    
    public override string ToTypst()
    {
        return $"#line(length: 100%){Environment.NewLine}";
    }
}
