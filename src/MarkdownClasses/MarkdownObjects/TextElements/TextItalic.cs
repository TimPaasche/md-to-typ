namespace MarkdownClasses.MarkdownObjects.TextElements;

public class TextItalic : MarkdownObject
{
    public MarkdownObject Content { get; private set; }

    public TextItalic(string content)
    {
        this.Content = content.Deserialze();
    }

    public override string ToString()
    {
        return Content.ToString();
    }

    public override string Serialize()
    {
        return $"*{Content.Serialize()}*";
    }
    
    public override string ToTypst()
    {
        return $"_{Content.ToTypst()}_";
    }
}
