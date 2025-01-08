using System.Linq;

namespace MarkdownClasses.MarkdownObjects;

public class Text : MarkdownObject
{
    private const string REGEX_PATTERN = @"\$(.+?)\$|`(.+?)`|\*\*\*(.+?)\*\*\*|___(.+?)___|\*\*_(.+?)_\*\*|__\*(.+?)\*__|\*\*(.+?)\*\*|__(.+?)__|\*(.+?)\*|_(.+?)_|\[(.*?)\]\((.+?)\)|~~(.+?)~~|!\[(.*?)\]\((.+?)\)|:([\d\w_]+?):";
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
