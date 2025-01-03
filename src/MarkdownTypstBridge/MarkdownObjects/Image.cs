using System;
using System.Text.RegularExpressions;

namespace MarkdownTypstBridge.MarkdownObjects;

public class Image : MarkdownObject
{
    private const string REGEX_PATTERN = @"!\[(.*?)\]\((.+?)\)";

    public MarkdownObject Alias { get; private set; }
    public string Link { get; private set; }
    public bool Inline { get; private set; }

    public Image(string alias, string hyperRef, bool inline = false)
    {
        this.Alias = alias.Deserialze();
        this.Link = hyperRef;
        this.Inline = inline;
    }

    public Image(string line, bool inline = false)
    {
        Match match = Regex.Match(line, REGEX_PATTERN);

        this.Alias = match.Groups[1].Value.Deserialze();
        this.Link = match.Groups[2].Value;
        this.Inline = inline;
    }

    public override string ToString()
    {
        return Link;
    }

    public override string Serialize()
    {
        string hyperRef = $"![{Alias.Serialize()}]({Link})";
        if (Inline == false)
        {
            hyperRef += Environment.NewLine;
        }
        return hyperRef;
    }
    
    public override string ToTypst()
    {
        string figure = "#figure(" + Environment.NewLine;
        figure += $"  image({Environment.NewLine}";
        figure += $"    alt: {Alias.ToTypst()}{Environment.NewLine},";
        figure += $"    {Link}{Environment.NewLine}";
        figure += $"  ),{Environment.NewLine}";
        figure += $"  caption: [{Alias.ToTypst()}]{Environment.NewLine}";
        figure += $"){Environment.NewLine}";
        return $"#image({Alias.ToTypst()}{Environment.NewLine}  {Link}{Environment.NewLine})";
    }
}
