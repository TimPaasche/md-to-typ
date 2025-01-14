namespace MarkdownClasses.MarkdownObjects.TextElements;

public class TextInlineCode : TextElement
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

    public override string Serialize()
    {
        return $"`{Content}`";
    }
}
