namespace MarkdownTypstBridge.MarkdownObjects.TextElements;

public class TextInlineCode
{
    public string Content { get; private set; }
    public TextInlineCode(string content)
    {
        this.Content = content;
    }
}
