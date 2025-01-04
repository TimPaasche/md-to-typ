using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarkdownClasses.MarkdownObjects;

public class Quote : MarkdownObject
{
    private const string REGEX_PATTERN = @"(>+)(.+)";

    public int NestedDepth { get; private set; }
    public MarkdownObject Content { get; private set; }

    public Quote(string line)
    {
        Match match = Regex.Match(line, REGEX_PATTERN);
        Content = match.Groups[2].Value.Deserialze();
        NestedDepth = match.Groups[1].Length;
    }

    public override string ToString()
    {
        return Content.ToString();
    }

    public override string Serialize()
    {
        return $"{new string('>', NestedDepth)} {Content}{Environment.NewLine}";
    }
}
