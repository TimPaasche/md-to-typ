namespace MarkdownClasses.MarkdownObjects.TextElements;

public class TextNormal : MarkdownObject
{
    public string Content { get; private set; }

    public TextNormal(string content)
    {
        this.Content = content.Replace("$", @"\$");
    }

    public override string ToString()
    {
        return Content;
    }

    public override string Serialize()
    {
        return Content;
    }
}
