namespace MarkdownClasses.MarkdownObjects.TextElements;

public class TextBoldItalic : TextElement
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

    public override string Serialize()
    {
        return $"**_{Content.Serialize()}_**";
    }
}
