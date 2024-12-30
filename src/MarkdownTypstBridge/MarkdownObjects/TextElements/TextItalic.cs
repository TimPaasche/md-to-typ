namespace MarkdownTypstBridge.MarkdownObjects.TextElements;

public class TextItalic : MarkdownObject
{
    public string Content { get; private set; }

    public TextItalic(string content)
    {
        this.Content = content;
    }

    public override string ToString()
    {
        return Content;
    }

    public new string Serialize()
    {
        throw new System.NotImplementedException();
    }
}
