using System;

namespace MarkdownClasses.MarkdownObjects;

public class EmptyLine : TextElement
{
    public EmptyLine() { }

    public override string Serialize()
    {
        return $"{Environment.NewLine}";
    }
}
