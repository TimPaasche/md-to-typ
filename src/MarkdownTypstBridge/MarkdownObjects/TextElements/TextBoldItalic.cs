using System;
using System.Linq;

namespace MarkdownTypstBridge.MarkdownObjects.TextElements;

public class TextBoldItalic : MarkdownObject
{
    public MarkdownObject Content { get; private set; }

    public TextBoldItalic(string content)
    {
        this.Content = content.Deserialze();
    }

    public override string ToString()
    {
        return Content.ToString();
    }

    public new string Serialize()
    {
        return $"**_{Content.Serialize()}_**";
    }
}
