namespace MarkdownTypstBridge.MarkdownObjects.TextElements;

public class TextItalic
{
    public string Content { get; private set; }
    public TextItalic(string content)
    {
        this.Content = content;
    }
}
