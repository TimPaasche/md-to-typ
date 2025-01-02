using System;
using System.Text.RegularExpressions;

namespace MarkdownTypstBridge.MarkdownObjects;

public class Headline : MarkdownObject
{
    private const string REGEX_PATTERN = @"(#+)\W(.*)|(#+)(.*)";

    public string Content { get; private set; }
    public int Level { get; private set; }

    public Headline(string line) : base()
    {
        var match = Regex.Match(line, REGEX_PATTERN);
        Level = match.Groups[1].Value.Length;
        Content = match.Groups[2].Value;
    }

    public override string Serialize()
    {
        return $"{new string('#', Level)} {Content}{Environment.NewLine}";
    }
}