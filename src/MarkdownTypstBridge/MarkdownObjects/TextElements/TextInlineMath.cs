namespace MarkdownTypstBridge.MarkdownObjects.TextElements;

public class TextInlineMath
{
    public string Content { get; private set; }
    public TextInlineMath(string content)
    {
        this.Content = content;
    }
}