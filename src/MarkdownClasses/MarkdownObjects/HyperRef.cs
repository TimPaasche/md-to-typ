using System;
using System.Text.RegularExpressions;

namespace MarkdownClasses.MarkdownObjects;

public class HyperRef : TextElement
{
    private const string REGEX_PATTERN = @"\[(.*?)\]\((.+?)\)";

    public MarkdownObject Alias { get; private set; }
    public string Url { get; private set; }
    public bool Inline { get; private set; }

    public HyperRef(string alias, string hyperRef, bool inline = false)
    {
        this.Alias = alias.Deserialze();
        this.Url = hyperRef;
        this.Inline = inline;
    }

    public HyperRef(string line, bool inline = false)
    {
        Match match = Regex.Match(line, REGEX_PATTERN);

        this.Alias = match.Groups[1].Value.Deserialze();
        this.Url = match.Groups[2].Value;
        this.Inline = inline;
    }

    public override string ToString()
    {
        return Url;
    }

    public override string Serialize()
    {
        string hyperRef = $"[{Alias.Serialize()}]({Url})";
        if (Inline == false)
        {
            hyperRef += Environment.NewLine;
        }
        return hyperRef;
    }
}
