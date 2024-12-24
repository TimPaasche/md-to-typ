namespace MarkdownTypstBridge.MarkdownObjects.TextElements;

public class TextNormal
{
    public string Content { get; private set; }

    public TextNormal(string content)
    {
        this.Content = content;
    }
}
