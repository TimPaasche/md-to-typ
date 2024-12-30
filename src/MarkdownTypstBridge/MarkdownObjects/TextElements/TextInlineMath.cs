namespace MarkdownTypstBridge.MarkdownObjects.TextElements;

public class TextInlineMath : MarkdownObject
{
    public string Content { get; private set; }

    public TextInlineMath(string content)
    {
        this.Content = content;
    }

    public override string ToString()
    {
        return Content;
    }

    public new string Serialize()
    {
        return $"${Content}$";
    }
}