using System;
using System.Text.RegularExpressions;

namespace MarkdownClasses.MarkdownObjects;

public class Headline : MarkdownObject
{
    private const string REGEX_PATTERN = @"(#+)\W(.*)|(#+)(.*)";

    public MarkdownObject Content { get; private set; }
    public int Level { get; private set; }

    public Headline(string line) : base()
    {
        var match = Regex.Match(line, REGEX_PATTERN);
        Level = match.Groups[1].Value.Length;
        Content = match.Groups[2].Value.Deserialze();
    }

    public override string Serialize()
    {
        return $"{new string('#', Level)} {Content.Serialize()}{Environment.NewLine}";
    }
}