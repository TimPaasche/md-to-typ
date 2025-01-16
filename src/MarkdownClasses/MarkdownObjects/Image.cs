using System;
using System.Text.RegularExpressions;

namespace MarkdownClasses.MarkdownObjects;

public class Image : TextElement
{
    private const string REGEX_PATTERN = @"^!\[(.*)\]\((.+)\)$";
    
    public MarkdownObject Alias { get; private set; }
    public string ImageRefrence { get; private set; }
    public bool Inline { get; private set; }

    public Image(string line, bool inline = false)
    {
        Match match = Regex.Match(line, REGEX_PATTERN, RegexOptions.Multiline);

        this.Alias = match.Groups[1].Value.Deserialze();
        this.ImageRefrence = match.Groups[2].Value;
        this.Inline = inline;
    }

    public override string ToString()
    {
        return ImageRefrence;
    }

    public override string Serialize()
    {
        string hyperRef = $"![{Alias.Serialize()}]({ImageRefrence})";
        if (Inline == false)
        {
            hyperRef += Environment.NewLine;
        }
        return hyperRef;
    }
}
