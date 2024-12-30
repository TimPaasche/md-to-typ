namespace MarkdownTypstBridge.MarkdownObjects.TextElements;

public class TextInlineCode : MarkdownObject
{
    public string Content { get; private set; }

    public TextInlineCode(string content)
    {
        this.Content = content.Trim('`');
    }

    public override string ToString()
    {
        return Content;
    }

    public new string Serialize()
    {
        return $"`{Content}`";
    }
}
