namespace MarkdownClasses.MarkdownObjects.TextElements;

public class TextBold : TextElement
{
    public MarkdownObject Content { get; private set; }

    public TextBold(string content)
    {
        this.Content = content.Deserialze();
    }

    public override string Serialize()
    {
        return $"**{Content.Serialize()}**";
    }

    public override string ToString()
    {
        return Content.ToString();
    }
}
