using System;

namespace MarkdownClasses.MarkdownObjects.TextElements;

public class TextStrikethrough : MarkdownObject
{
    public MarkdownObject Content { get; private set; }

    public TextStrikethrough(string content)
    {
        this.Content = content.Deserialze();
    }

    public override string ToString()
    {
        return Content.ToString();
    }

    public override string Serialize()
    {
        return $"~~{Content.Serialize()}~~";
    }

    public override string ToTypst()
    {
        return $"#strike[{Content.ToTypst()}]";
    }
}
