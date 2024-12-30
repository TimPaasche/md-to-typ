using System.Runtime.CompilerServices;

namespace MarkdownTypstBridge.MarkdownObjects.TextElements;

public class TextBold : MarkdownObject
{
    public MarkdownObject Content { get; private set; }

    public TextBold(string content)
    {
        this.Content = content.Deserialze();
    }

    public new string Serialize()
    {
        return $"*{Content.Serialize()}*";
    }

    public override string ToString()
    {
        return Content.ToString();
    }
}
