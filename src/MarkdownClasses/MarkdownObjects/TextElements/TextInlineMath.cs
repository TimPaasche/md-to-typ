namespace MarkdownClasses.MarkdownObjects.TextElements;

public class TextInlineMath : TextElement
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

    public override string Serialize()
    {
        return $"${Content}$";
    }
}