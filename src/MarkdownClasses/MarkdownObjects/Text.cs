using System.Linq;

namespace MarkdownClasses.MarkdownObjects;

public class Text : MarkdownObject
{
    public TextElement[] Content { get; private set; }


    public Text(string line, bool newline)
    {
        Content = line.ToTextElements(newline);
    }

    public override string ToString()
    {
        return string.Concat(Content.Select(obj => { return obj.ToString() + " "; }));
    }

    public override string Serialize()
    {
        return string.Concat(Content.Select(obj => { return obj.Serialize(); }));
    }
}
